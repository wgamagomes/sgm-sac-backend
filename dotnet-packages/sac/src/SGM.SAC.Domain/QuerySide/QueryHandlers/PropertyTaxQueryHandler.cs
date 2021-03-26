using MediatR;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using SGM.SAC.Domain.Dto;
using SGM.SAC.Domain.HttpResponse;
using SGM.SAC.Domain.QuerySide.Queries;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.SAC.Domain.QuerySide.QueryHandlers
{
    public class PropertyTaxQueryHandler : IRequestHandler<PropertyTaxQuery, PropertyTaxResult>
    {
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public PropertyTaxQueryHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = httpClient.BaseAddress.ToString();
        }

        public async Task<PropertyTaxResult> Handle(PropertyTaxQuery request, CancellationToken cancellationToken)
        {
            var responseString = string.Empty;

            if (!request.IsRuralTax)
                responseString = await _httpClient.GetStringAsync($"{_remoteServiceBaseUrl}/iptu/{request.PropertyRegistration}");
            else
                responseString = await _httpClient.GetStringAsync($"{_remoteServiceBaseUrl}/itr/{request.PropertyRegistration}");

            var result = JsonConvert.DeserializeObject<PropertyTaxHttpResponse>(responseString);
            result.PropertyRegistration = request.PropertyRegistration;

            return PropertyTaxResult.Create(result);

        }
    }
}
