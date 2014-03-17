using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WinStudio.Security
{
    public class Sha1Provider : BaseCryptProvider
    {
        public Sha1Provider(Encoding encoding)
            : base(encoding)
        {
            _provider = SHA1.Create();
        }
    }
}
