//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AB_CargoServices.Models
{   
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LOCATION
    {
        [Key]
        [Display(Name = "City Code")]
        public decimal CITY_ID { get; set; }
        [Display(Name = "City Name")]
        public string CITY_NAME { get; set; }
    }
}
