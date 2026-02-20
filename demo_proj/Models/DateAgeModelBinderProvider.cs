using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace demo_proj.Models
{
    

   

    public class DateAgeModelBinderProvider : IModelBinderProvider
    {
        private readonly string _dateFormat;
        //**************************************************************************
        public DateAgeModelBinderProvider(string dateFormat)
        {
            this._dateFormat = dateFormat;
        }
        //**************************************************************************
        IModelBinder? IModelBinderProvider.GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(context));
            }
            
            var x = context.Metadata.ModelType;//типът е Post object типа-
            //изпращаме Person, следователно context.Metadata.ModelType = Person type

            Console.WriteLine(x.Name);
            if (x == typeof(DateTime) || x == typeof(DateTime?))
            {
                return new DateAgeTimeModelBinder(this._dateFormat);
            }
            return null;
        }
        //**************************************************************************
    }
}
