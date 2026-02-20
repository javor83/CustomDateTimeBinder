using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace demo_proj.Models
{
    

   

    public class DateAgeModelBinderProvider : IModelBinderProvider
    {
        private readonly string _dateFormat;
        public DateAgeModelBinderProvider(string dateFormat)
        {
            this._dateFormat = dateFormat;
        }

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(context));
            }
            //BinderType == null - защо ?
            if (context.Metadata.BinderType == typeof(DateTime) || context.Metadata.BinderType == typeof(DateTime?))
            {
                return new DateAgeTimeModelBinder(this._dateFormat);
            }
            return null;
        }
    }
}
