using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class MusicModel : MediaItemModel
    {
        [Required]
        [StringLength(255)]
        public string Artist { get; set; }

        [Required]
        [StringLength(255)]
        public string Album { get; set; }

        [Required]
        [StringLength(255)]
        public string Genre { get; set; }

        public string Description { get; set; }

        public decimal? AudienceRating { get; set; }

        public decimal? CriticRating { get; set; }

        public int? AverageLength { get; set; }
    }
}
