using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_ZubakovSemon.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}

