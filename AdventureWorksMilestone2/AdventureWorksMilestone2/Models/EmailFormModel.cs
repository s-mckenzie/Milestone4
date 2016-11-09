using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorksMilestone2.Models
{
    public class EmailFormModel
    {
        [Required, Display(Name = "First Name")]
        public string FromFirstName { get; set; }

        [Required, Display(Name = "Last Name")]
        public string FromLastName { get; set; }

        [Required, Display(Name = "Email")]
        [EmailAddress]
        //[RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "The Email Address is not valid.")]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }

    }
}