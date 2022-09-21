//using NZWalks.API.Models.Domain;

using System.ComponentModel.DataAnnotations;

namespace NZWalks.Web.Models.DTO
{
    public class WalkDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation Properties
        //public Region Region { get; set; }
        //public WalkDifficulty WalkDifficulty { get; set; }
    }
}
