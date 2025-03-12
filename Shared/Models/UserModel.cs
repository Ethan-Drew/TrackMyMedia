using System;
using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class UserModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress] 
        [StringLength(255)]
        public required string Email { get; set; }

        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public required string PasswordHash { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public required bool IsActive { get; set; }

        [Required]
        [StringLength(50)]
        public required string Role { get; set; }
    }
}
