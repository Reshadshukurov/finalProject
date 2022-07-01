using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class LatestWorkImage : BaseEntity
    {
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
