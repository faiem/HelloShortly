using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.ShortAliasesHelper
{
    public class Base62Generator : IBase62Generator
    {
        private readonly string keyElements = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private readonly long Base = 62;

        public long Decode(string value)
        {
            long i = 0;

            foreach (var c in value)
            {
                i = (i * Base) + keyElements.IndexOf(c);
            }

            return i;
        }

        public string Encode(long value)
        {
            if (value == 0) return keyElements[0].ToString();

            var s = string.Empty;

            while (value > 0)
            {
                int x = (int)(value % Base);
                s += keyElements[x];
                value /= Base;
            }
            return string.Join(string.Empty, s.Reverse());
        }
    }
}
