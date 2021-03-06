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
    using Foolproof;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Reviews = new HashSet<Review>();
            this.SalesOrderDetails = new HashSet<SalesOrderDetail>();
        }

        [Required]
        public int ProductID { get; set; }
        [Required]
        [MaxLength(25)]
        [Remote("CheckName", "BikeManager")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Product Number")]
        [MaxLength(25)]
        [Remote("CheckNumber", "BikeManager")]
        public string ProductNumber { get; set; }
        [MaxLength(15)]
        public string Color { get; set; }
        [Required]
        [Display(Name = "Standard Cost")]
        [Range(typeof(decimal), "0.00", "10000.00")]
        public decimal StandardCost { get; set; }
        [Required]
        [Display(Name = "List Price")]
        [Range(typeof(decimal), "0.00", "10000.00")]
        [GreaterThan("StandardCost")]
        public decimal ListPrice { get; set; }
        [MaxLength(5)]
        public string Size { get; set; }
        [Range(typeof(decimal), "0.00", "10000.00")]
        public Nullable<decimal> Weight { get; set; }
        [Required]
        [Display(Name = "Product Category")]
        public Nullable<int> ProductCategoryID { get; set; }
        [Required]
        [Display(Name = "Product Model")]
        [Range(0, 1000)]
        public Nullable<int> ProductModelID { get; set; }
        [Required]
        [Display(Name = "Sell Start Date")]
        public System.DateTime SellStartDate { get; set; }
        [Display(Name = "Sell End Date")]
        public Nullable<System.DateTime> SellEndDate { get; set; }
        [Display(Name = "Discontinued Date")]
        public Nullable<System.DateTime> DiscontinuedDate { get; set; }
        [ScaffoldColumn(false)]
        public byte[] ThumbNailPhoto { get; set; }
        public string ThumbnailPhotoFileName { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductModel ProductModel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; }
    }
}
