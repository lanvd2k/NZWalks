using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NZWalks.Web.Models.DTO
{
    public class AddWalkRequestVM
    {
        public AddWalkRequestVM()
        {
            Walk = new AddWalkRequest();
        }
        public AddWalkRequest Walk { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RegionList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WalkDifficultyList { get; set; }
    }
}
