using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace demo_proj.Models
{


    public class PointCards
    {
        public int X { get; set; }
        public int Y { get; set; }

        public string Caption()
        {
            return $"X{this.X}Y{this.Y}";
        }
        //********************************************************************************
        public static PointCards? FromString(string point_value)
        {
            PointCards? result = null;

            if (string.IsNullOrEmpty(point_value) == false)
            {
                int pos_y = point_value.IndexOf('Y');
                string y_value = point_value.Substring(pos_y + 1);
                string x_value = point_value.Substring(1, pos_y - 1);
                result = new PointCards()
                {
                    X = int.Parse(x_value),
                    Y = int.Parse(y_value)

                };

            }
            return result;

        }
        //********************************************************************************
        public static string[] CommaList(string? comma_list)
        {
            string[] result = new string[] { };


            if (string.IsNullOrEmpty(comma_list) == false)
            {


                result = comma_list.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => s.Trim())
                                        .ToArray();
            }
            return result;
        }
        //********************************************************************************
    }


}
