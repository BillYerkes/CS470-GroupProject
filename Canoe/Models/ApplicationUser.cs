using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Canoe.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
    }

    public class EditUser
    {

        public EditUser()
        {
            Roles = new List<string>();
        }

        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int AccessFailedCount { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }

        public string LockoutEnd { get; set; }
        public List<string> Roles { get; set; }

    }

    public class UserRoles
    {
        public string RoleId { get; set; }
        public string UserID { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }

    }


}
