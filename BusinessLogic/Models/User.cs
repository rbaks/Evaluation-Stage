using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Models
{
    public class User : IdentityUser
    {
        public string PhotoPath { get; set; }
    }
}
