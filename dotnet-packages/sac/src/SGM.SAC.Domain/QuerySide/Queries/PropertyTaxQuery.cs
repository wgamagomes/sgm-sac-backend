using MediatR;
using SGM.SAC.Domain.Dto;

namespace SGM.SAC.Domain.QuerySide.Queries
{
    public class PropertyTaxQuery: IRequest<PropertyTaxResult>
    {
        public string PropertyRegistration { get; private set; }

        public static PropertyTaxQuery Create(string propertyRegistration) => new PropertyTaxQuery { PropertyRegistration = propertyRegistration };
    }
}
