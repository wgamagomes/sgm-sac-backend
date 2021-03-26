using MediatR;
using Polly;
using Polly.Retry;
using SGM.SAC.Domain.Dto;
using SGM.SAC.Domain.HttpResponse;
using SGM.SAC.Domain.QuerySide.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.SAC.Domain.QuerySide.QueryHandlers
{
    public class PropertyTaxQueryHandler : IRequestHandler<PropertyTaxQuery, PropertyTaxResult>
    {  
        private readonly AsyncRetryPolicy _retryPolicy;

        public PropertyTaxQueryHandler()
        {  
            _retryPolicy =  Policy.Handle<ArgumentOutOfRangeException>()
            .WaitAndRetryAsync(3,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<PropertyTaxResult> Handle(PropertyTaxQuery request, CancellationToken cancellationToken)
        {
            return await _retryPolicy.ExecuteAsync(async () =>
            {
                return await Task.Run(async () =>
                {
                    var result = new PropertyTaxHttpResponse();

                    return PropertyTaxResult.Create(result);
                });
            });
        }
    }
}
