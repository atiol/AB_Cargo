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

    public partial class SENDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SENDER()
        {
            this.CARGOes = new HashSet<CARGO>();
            this.RECEIVERs = new HashSet<RECEIVER>();
        }
    
        public decimal TC { get; set; }
        public string FIRST_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string LAST_NAME { get; set; }
        public string PHONE { get; set; }
        public string CITY { get; set; }
        [Key]
        public decimal S_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CARGO> CARGOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECEIVER> RECEIVERs { get; set; }
    }
}