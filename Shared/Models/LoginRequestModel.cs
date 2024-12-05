using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class LoginRequestModel
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }
}