using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Service : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
