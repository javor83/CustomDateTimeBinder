using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace demo_proj.Models
{
    public class DateAgeTimeModelBinder : IModelBinder
    {
        private readonly string _dateFormat;
        public DateAgeTimeModelBinder(string dateFormat)
        {
            this._dateFormat = dateFormat;
        }

        Task IModelBinder.BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueResult != ValueProviderResult.None && (string.IsNullOrEmpty(valueResult.FirstValue) == false))
            {
                string dateValue = valueResult.FirstValue;

                if (DateTime.TryParseExact(
                    dateValue,
                    this._dateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime date_result))
                {
                    bindingContext.Result = ModelBindingResult.Success(date_result);
                }
                else
                    if (DateTime.TryParse(dateValue, new CultureInfo("bg-bg"), DateTimeStyles.None, out date_result))
                    {
                        bindingContext.Result = ModelBindingResult.Success(date_result);
                    }
                    else
                    {
                        bindingContext.ModelState.AddModelError
                            (
                                bindingContext.ModelName,
                                $"Expected format {this._dateFormat}"
                            );
                    }
            }
            return Task.CompletedTask;
        }
    }
}
