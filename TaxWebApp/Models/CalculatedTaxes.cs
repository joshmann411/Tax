using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxWebApp.Models
{
    public class CalculatedTaxes
    {
        public int Id { get; set; }

        public Nullable<int> TaxCalculated { get; set; }
        public string DateStmp { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string PoastalCode { get; set; }
        public Nullable<int> AnnualIncome { get; set; }
    }
}