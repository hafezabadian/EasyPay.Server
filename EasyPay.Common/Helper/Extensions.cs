using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPay.Common.Helper
{
    public static class Extensions
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
         //---------| با قرار دادن دیس قبل از ورودی تابع --------\\
        //---میتوان این تابع را به صورت زنجیره ای استفاده کرد |---\\
        public static int ToAge(this DateTime dateOfBirth)
        {
            var age = DateTime.Today.Date.Year - dateOfBirth.Year;
            if (age + dateOfBirth.Year > DateTime.Today.Date.Year)
            { age--; }
            return age;
        }//-------------------------------------------------------//
    }
}
