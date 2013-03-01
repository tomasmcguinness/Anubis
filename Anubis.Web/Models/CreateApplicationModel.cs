using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Anubis.Web.Models
{
    public class CreateApplicationModel
    {
        [Display(Name="New Application Name")]
        [Required]
        public string ApplicationName { get; set; }
    }
}