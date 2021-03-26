using SGM.SAC.Domain.HttpResponse;

namespace SGM.SAC.Domain.Dto
{
    public class PropertyTaxResult
    {
        public PropertyTaxDto PropertyTax { get; private set; }

        public static PropertyTaxResult Create(PropertyTaxHttpResponse propertyTaxHttpResponse) => new PropertyTaxResult
        {
            PropertyTax = new PropertyTaxDto
            {
                PropertyRegistration = propertyTaxHttpResponse.PropertyRegistration,
                Content = propertyTaxHttpResponse.Content
            }
        };
    }
}
