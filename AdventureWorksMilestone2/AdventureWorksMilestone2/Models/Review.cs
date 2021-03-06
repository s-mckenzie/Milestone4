//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdventureWorksMilestone2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Review
    {
        public int id { get; set; }
        public int ProductID { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage ="* Required")]
        public decimal Rating { get; set; }
        [Required(ErrorMessage ="* Required")]
        [Display(Name ="Review")]
        public string Review1 { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
