using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Blog : BaseEntity
    {
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string NewsImage { get; set; }
        public string PostTitle { get; set; }
        public string PostUser { get; set; }
        public DateTime PostDate { get; set; }
        public string PostText { get; set; }
        public string PostImage { get; set; }



        [NotMapped]
        public IFormFile NewsImg { get; set; }
        [NotMapped]
        public IFormFile PostImg { get; set; }
    }
}
