using Entity.DTO.Operational;
using Entity.DTO.Security;

namespace Entity.DTO
{
    public class UserPersonRoleDto
    {
        public UserDto User { get; set; }
        public PersonDto Person { get; set; }
       

        public int RoleId { get; set; } // ID del rol a asignar
    }
}
