using System.ComponentModel.DataAnnotations;

namespace TrackMyMedia.Shared.Models
{
    public class ComicModel : MediaItemModel
    {
        [Required]
        public int IssueNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Publisher { get; set; }

        public string Description { get; set; }

        public decimal? AudienceRating { get; set; }

        public decimal? CriticRating { get; set; }

        public int? AverageLength { get; set; }
    }
}
