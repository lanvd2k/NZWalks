using System.ComponentModel.DataAnnotations;

namespace NZWalks.Web.Models.DTO
{
    public class UpdateRegionRequest
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Area { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Long { get; set; }
        [Required]
        public long Population { get; set; }
    }
}
