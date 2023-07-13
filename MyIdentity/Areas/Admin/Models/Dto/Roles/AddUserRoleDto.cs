using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyIdentity.Areas.Admin.Models.Dto.Roles;

    public class AddUserRoleDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }

