using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Model.DTOs
{
    public class EducationSuggestDto
    {
        public int porposalID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public string SubContent { get; set; }
        public int userID { get; set; }
        public string status { get; set; }
                     
    }
}
