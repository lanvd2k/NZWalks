using System.ComponentModel.DataAnnotations;

namespace NZWalks.Web.Models.DTO
{
    public class UpdateWalkRequest
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Length { get; set; }

        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
