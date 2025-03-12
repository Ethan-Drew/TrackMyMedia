using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class AnimeModel : MediaItemModel
    {
        [Required]
        public int EpisodeCount { get; set; }

        [Required]
        public int SeasonCount { get; set; }

        [Required]
        [StringLength(255)]
        public string Studio { get; set; }

        public string Description { get; set; }

        public decimal? AudienceRating { get; set; }

        public decimal? CriticRating { get; set; }

        public int? AverageLength { get; set; }

    }
}
