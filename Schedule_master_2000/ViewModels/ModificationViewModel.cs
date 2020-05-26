﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule_master_2000.ViewModels
{
    public class ModificationViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid E-mail format.")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Compare("PasswordAgain", ErrorMessage = "Passwords do not match.")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        public string PasswordAgain { get; set; }
    }
}
