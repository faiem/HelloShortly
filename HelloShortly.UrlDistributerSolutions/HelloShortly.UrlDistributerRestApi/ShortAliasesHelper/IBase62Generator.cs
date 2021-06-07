using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.UrlDistributerRestApi.ShortAliasesHelper
{
    public interface IBase62Generator
    {
        string Encode(long value);
        long Decode(string value);
    }
}
