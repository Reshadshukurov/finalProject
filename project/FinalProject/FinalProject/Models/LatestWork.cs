using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class LatestWork : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
 
    }
}
