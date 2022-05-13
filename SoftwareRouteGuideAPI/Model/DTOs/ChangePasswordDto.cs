namespace SoftwareRouteGuideAPI.Model.DTOs
{
    public class ChangePasswordDto
    {
        public int userID { get; set; }
        public string currentPassword { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
}