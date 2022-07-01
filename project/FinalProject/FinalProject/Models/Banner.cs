using System;

namespace FinalProject.Models
{
    public class Banner : BaseEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string ButtonText { get; set; }
        public string ButtonLink { get; set; }
    }
}
