using SGM.SAC.Domain.Entities;

namespace SGM.GEP.Infra.Data.Mongo.Log
{
    public class Log: Entity
    {
        public string StackTrace { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string Level { get; set; }
        public string Message { get; internal set; }
    }
}
