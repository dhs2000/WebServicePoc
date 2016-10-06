using System;

using FluentValidation;

namespace ApplicationServices.Projects
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> IsGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(IsValidGuid);
        }

        private static bool IsValidGuid(string value)
        {
            try
            {
                var guid = new Guid(value);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}