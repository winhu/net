using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace System.Web.Http
{
    public static class HashHelper
    {
        public static string ToMD5Hash(this byte[] bytes)
        {
            StringBuilder hash = new StringBuilder();
            MD5 md5 = MD5.Create();

            md5.ComputeHash(bytes)
                  .ToList()
                  .ForEach(b => hash.AppendFormat("{0:x2}", b));

            return hash.ToString();
        }

        public static string ToMD5Hash(this string inputString)
        {
            return Encoding.UTF8.GetBytes(inputString).ToMD5Hash();
        }
    }
    public class Nonce
    {
        private static ConcurrentDictionary<string, Tuple<int, DateTime>>
        nonces = new ConcurrentDictionary<string, Tuple<int, DateTime>>();

        public static string Generate()
        {
            byte[] bytes = new byte[16];

            using (var rngProvider = new RNGCryptoServiceProvider())
            {
                rngProvider.GetBytes(bytes);
            }

            string nonce = bytes.ToMD5Hash();

            nonces.TryAdd(nonce, new Tuple<int, DateTime>(0, DateTime.Now.AddMinutes(10)));

            return nonce;
        }
        public static bool IsValid(string nonce, string nonceCount)
        {
            Tuple<int, DateTime> cachedNonce = null;
            nonces.TryGetValue(nonce, out cachedNonce);

            if (cachedNonce != null) // nonce is found
            {
                // nonce count is greater than the one in record
                if (Int32.Parse(nonceCount) > cachedNonce.Item1)
                {
                    // nonce has not expired yet
                    if (cachedNonce.Item2 > DateTime.Now)
                    {
                        // update the dictionary to reflect the nonce count just received in this request
                        nonces[nonce] = new Tuple<int, DateTime>(Int32.Parse(nonceCount), cachedNonce.Item2);

                        // Every thing looks ok - server nonce is fresh and nonce count seems to be 
                        // incremented. Does not look like replay.
                        return true;
                    }
                }
            }

            return false;
        }
    }
    public class AuthenticationHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = request.Headers;
                if (headers.Authorization != null)
                {
                    Header header = new Header(request.Headers.Authorization.Parameter,
                                                                                                                      request.Method.Method);

                    if (Nonce.IsValid(header.Nonce, header.NounceCounter))
                    {
                        // Just assuming password is same as username for the purpose of illustration
                        string password = header.UserName;

                        string ha1 = String.Format("{0}:{1}:{2}", header.UserName, header.Realm,
                                                                                                                             password).ToMD5Hash();

                        string ha2 = String.Format("{0}:{1}", header.Method, header.Uri).ToMD5Hash();

                        string computedResponse = String
                                      .Format("{0}:{1}:{2}:{3}:{4}:{5}",
                                            ha1, header.Nonce, header.NounceCounter,
                                                                                         header.Cnonce, "auth", ha2).ToMD5Hash();

                        if (String.CompareOrdinal(header.Response, computedResponse) == 0)
                        {
                            // digest computed matches the value sent by client in the response field.
                            // Looks like an authentic client! Create a principal.
                            var claims = new List<Claim>
                            {
                                            new Claim(ClaimTypes.Name, header.UserName),
                                            new Claim(ClaimTypes.AuthenticationMethod, AuthenticationMethods.Password)
                            };

                            var principal = new ClaimsPrincipal(new[] { new ClaimsIdentity(claims, "Digest") });

                            Thread.CurrentPrincipal = principal;

                            if (HttpContext.Current != null)
                                HttpContext.Current.User = principal;
                        }
                    }
                }

                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest",
                                                                                     Header.UnauthorizedResponseHeader.ToString()));
                }

                return response;
            }
            catch (Exception)
            {
                var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Digest",
                                                                                       Header.UnauthorizedResponseHeader.ToString()));

                return response;
            }
        }
    }


}
