using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.VewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public Banner banner { get; set; }
        public AboutSection aboutSection { get; set; }
        public StartSection startSection { get; set; }
        public BigScreen bigScreen { get; set; }
        public LatestWork latestWork { get; set; }
        public List<Service> services { get; set; }
        public List<LatestWorkImage> latestWorkImages { get; set; }
        public Pricing pricing { get; set; }
        public List<Blog> blogs { get; set; }


    }
}
