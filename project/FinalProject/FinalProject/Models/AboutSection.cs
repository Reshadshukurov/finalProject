using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class AboutSection : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Text { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
