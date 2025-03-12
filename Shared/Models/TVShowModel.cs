using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class TVShowModel : MediaItemModel
    {
        [Required]
        public int Seasons { get; set; }

        [Required]
        public int Episodes { get; set; }

        [Required]
        [StringLength(255)]
        public string Genre { get; set; }

        public string Description { get; set; }

        public decimal? AudienceRating { get; set; }

        public decimal? CriticRating { get; set; }

        public int? AverageLength { get; set; }
    }
}
