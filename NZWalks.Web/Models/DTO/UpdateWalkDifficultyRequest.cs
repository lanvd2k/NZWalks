using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Web.Models.DTO
{
    public class UpdateWalkDifficultyRequest
    {
        [Required]
        [ValidateNever]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
