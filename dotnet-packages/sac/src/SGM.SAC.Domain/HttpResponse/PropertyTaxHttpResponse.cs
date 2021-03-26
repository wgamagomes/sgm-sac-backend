using System;

namespace SGM.SAC.Domain.HttpResponse
{
    public class PropertyTaxHttpResponse
    {
        public int Id { get; set; }
        public string PropertyRegistration { get; set; }
        public bool IsRuralTax { get; set; }
        public bool Succeeded { get; set; }
        public string Content { get; set; }
    }
}
