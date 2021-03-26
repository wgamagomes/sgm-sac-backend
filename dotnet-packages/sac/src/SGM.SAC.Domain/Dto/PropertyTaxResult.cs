using SGM.SAC.Domain.HttpResponse;

namespace SGM.SAC.Domain.Dto
{
    public class PropertyTaxResult
    {
        public PropertyTaxDto PropertyTax { get; private set; }

        public static PropertyTaxResult Create(PropertyTaxHttpResponse httpResponse) => new PropertyTaxResult
        {
            PropertyTax = new PropertyTaxDto
            {
                PropertyRegistration = httpResponse.PropertyRegistration,
                Content = httpResponse.Content,
                Id = httpResponse.Id,
                Succeeded = httpResponse.Succeeded,
                IsRuralTax = httpResponse.IsRuralTax,
            }
        };
    }
}
