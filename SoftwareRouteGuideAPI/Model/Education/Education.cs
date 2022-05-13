using System;

namespace SoftwareRouteGuideAPI.Model.Education
{
    public class Education
    {
        public int educationID { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string Photo1 { get; set; }
        public string Paragraph1 { get; set; }
        public string Paragraph2 { get; set; }
        public string Paragraph3 { get; set; }
        public string Subtitle1 { get; set; }
        public string Paragraph4 { get; set; }
        public string Paragraph5 { get; set; }
        public string Photo2 { get; set; }
        public string Paragraph6 { get; set; }
        public string Paragraph7 { get; set; }
        public string Paragraph8 { get; set; }
        public string Subtitle2 { get; set; }
        public string Paragraph9 { get; set; }
        public string Paragraph10 { get; set; }
        public int userID { get; set; }
        public string status { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}