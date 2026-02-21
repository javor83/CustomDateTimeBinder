using System.ComponentModel.DataAnnotations;

namespace demo_proj.Models
{
    public class CanSmokeAttribute:ValidationAttribute
    {
        private string mAgeField = "";
        //************************************************************************
        public CanSmokeAttribute(string age_f)
        {
            this.mAgeField = age_f;
        }

        //************************************************************************
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var compvalue = validationContext.ObjectType.GetProperty(this.mAgeField).GetValue(validationContext.ObjectInstance);



            if (value != null)
            {
                int age = Convert.ToInt32(value);
                bool allow_smoke = Convert.ToBoolean(compvalue);
                if (allow_smoke)
                {
                    if (age >= 18)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("DO NOT SMOKE " + age.ToString());
                    }
                }
                else
                {
                    return ValidationResult.Success;
                }

                
            }
            else
                return new ValidationResult("Missing years");

           
        }
        //************************************************************************
    }
}
