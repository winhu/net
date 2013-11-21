using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace WinStudio.Security
{
    public class Md5Provider : AbstractCryptProvider, ICryptProvider
    {
        public Md5Provider(Encoding encoding)
            : base(encoding)
        {
            _provider = MD5.Create();
        }

    } 
}
