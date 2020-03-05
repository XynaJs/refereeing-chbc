﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RefereeingCHBC.Models
{
    public class UserRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}