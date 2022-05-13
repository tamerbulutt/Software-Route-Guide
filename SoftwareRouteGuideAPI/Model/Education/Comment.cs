using System;

namespace SoftwareRouteGuideAPI.Model.Education
{
    public class Comment
    {
        public int commentID { get; set; }
        public string content { get; set; }
        public string fullname { get; set; }
        public DateTime date { get; set; }
        public int educationID { get; set; }
    }
}