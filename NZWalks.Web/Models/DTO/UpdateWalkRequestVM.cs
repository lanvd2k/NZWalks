using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NZWalks.Web.Models.DTO
{
    public class UpdateWalkRequestVM
    {
        public UpdateWalkRequestVM()
        {
            Walk = new UpdateWalkRequest();
        }
        public UpdateWalkRequest Walk { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RegionList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WalkDifficultyList { get; set; }
    }
}
