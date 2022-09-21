﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.Web.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
