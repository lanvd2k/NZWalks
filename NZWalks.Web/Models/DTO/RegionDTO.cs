using System.ComponentModel.DataAnnotations;

namespace NZWalks.Web.Models.DTO
{
    public class RegionDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
        public string ImageUrl { get; set; }
    }
}
