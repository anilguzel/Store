using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAccess.Models.IdentityModels
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
