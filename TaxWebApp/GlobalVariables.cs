using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace TaxWebApp
{
    public static class GlobalVariables
    {
        public static HttpClient TaxCalculatorClient = new HttpClient();
        
        static GlobalVariables()
        {
            TaxCalculatorClient.BaseAddress = new Uri("https://localhost:44364/api/");
            TaxCalculatorClient.DefaultRequestHeaders.Clear();
            TaxCalculatorClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}