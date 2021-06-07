using HelloShortly.KMM.RestApi.Dtos.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloShortly.KMM.RestApi.Services
{
    public interface IKeyRangeProviderService
    {
        KeyRangeResponseDto GetUniqueKeyRange();
    }
}
