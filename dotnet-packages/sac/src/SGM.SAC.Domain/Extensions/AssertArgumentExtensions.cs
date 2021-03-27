namespace SGM.SAC.Domain.Extensions
{
    public static class AssertArgumentExtensions
    {
        public static bool AssertArgumentIsEmpty(this string value) => (string.IsNullOrWhiteSpace(value));

        public static bool AssertArgumentIsNull(this object @object) => @object == null;
    }
}
