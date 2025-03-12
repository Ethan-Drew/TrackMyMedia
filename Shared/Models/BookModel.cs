using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class BookModel : MediaItemModel
    {
        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(255)]
        public string Genre { get; set; }

        [StringLength(13)]
        public string ISBN { get; set; }

        public string Description { get; set; }

        public decimal? AudienceRating { get; set; }

        public decimal? CriticRating { get; set; }

        public int? AverageLength { get; set; } // Average reading time in minutes (e.g., total pages * average reading speed)
    }
}
