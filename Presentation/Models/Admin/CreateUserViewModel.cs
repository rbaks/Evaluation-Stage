using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.Admin
{
    public class CreateUserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Remote(controller: "Auth", action: "IsEmailTaken")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
