﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WinStudio.Track.Web.WebApi
{
    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {

        private string _callbackQueryParamter;

        public JsonpMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(DefaultMediaType);
            SupportedMediaTypes.Add(new MediaTypeWithQualityHeaderValue("text/javascript"));

            MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
        }

        public string CallbackQueryParameter
        {
            get { return _callbackQueryParamter ?? "callback"; }
            set { _callbackQueryParamter = value; }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            string callback;
            if (IsJsonpRequest(out callback))
            {
                return Task.Factory.StartNew(() =>
                {
                    var writer = new StreamWriter(writeStream);
                    writer.Write(callback + "(");
                    writer.Flush();
                    base.WriteToStreamAsync(type, value, writeStream, content, transportContext).Wait();
                    writer.Write(")");
                    writer.Flush();
                });
            }
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
        }

        private bool IsJsonpRequest(out string callback)
        {
            callback = null;
            switch (HttpContext.Current.Request.HttpMethod)
            {
                case "POST":
                    callback = HttpContext.Current.Request.Form[CallbackQueryParameter];
                    break;
                default:
                    callback = HttpContext.Current.Request.QueryString[CallbackQueryParameter];
                    break;
            }
            return !string.IsNullOrEmpty(callback);
        }
    }
}