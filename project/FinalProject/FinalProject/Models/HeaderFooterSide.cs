using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class HeaderFooterSide : BaseEntity
    {
        public string HeaderLogo { get; set; }
        public string FooterLogo { get; set; }
        public string CopyRightText { get; set; }

        [NotMapped]
        public IFormFile HeaderLogoImg { get; set; }
        [NotMapped]
        public IFormFile FooterLogoImg { get; set; }
    }
}
