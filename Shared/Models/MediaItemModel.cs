using System;
using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class MediaItemModel
    {
        [Key]
        public int MediaId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public decimal? MyRating { get; set; }

        public int? TimeSpent { get; set; }

        [Required]
        [StringLength(50)]
        public string MediaType { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
