using MediatR;
using SGM.SAC.Domain.Dto;

namespace SGM.SAC.Domain.QuerySide.Queries
{
    public class PropertyTaxQuery: IRequest<PropertyTaxResult>
    {
        public string PropertyRegistration { get; private set; }
        public bool IsRuralTax { get; private set; }

        public static PropertyTaxQuery Create(string propertyRegistration, bool isRuralTax) => new PropertyTaxQuery { PropertyRegistration = propertyRegistration, IsRuralTax = isRuralTax };
    }
}
