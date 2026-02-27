using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace demo_proj.Models
{
    public class StringListModelBinder : IModelBinder
    {
        Task IModelBinder.BindModelAsync(ModelBindingContext bindingContext)
        {

            var value_provider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value_provider != ValueProviderResult.None)
            {
                string? value = value_provider.FirstValue;
                if (value != null)
                {

                    string[] comma_list = PointCards.CommaList(value);
                    if (comma_list != null)
                    {
                        if (Array.IndexOf<string>(comma_list,"A") > -1)
                        {
                            
                            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "A team !");
                           
                        }   
                        else
                        {
                            bindingContext.Result = ModelBindingResult.Success(comma_list);
                        }
                        
                    }
                    else
                    {
                       
                        bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid comma-separated list. 1");
                    }

                }
              
            }

           
            return Task.CompletedTask;
        }
    }

    public class StringListModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(string[]))
            {
                return new StringListModelBinder();
            }
            return null;
        }
    }
}
