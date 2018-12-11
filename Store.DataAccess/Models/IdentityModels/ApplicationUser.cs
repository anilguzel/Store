using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAccess.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public string AvatarPath { get; set; }
        public string ActivationCode { get; set; }
        public string PreRole { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
