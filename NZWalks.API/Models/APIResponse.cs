using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NZWalks.API.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        [Required]
        public object Result { get; set; }
    }
}
