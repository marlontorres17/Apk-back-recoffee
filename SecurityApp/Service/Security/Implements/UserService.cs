using AutoMapper;
using Entity.DTO.Operational;
using Entity.DTO.Security;
using Entity.Model.Security;
using Repository.Operational.Interface;
using Repository.Security.Interface;
using Service.Security.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Security.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
     
        private readonly IPersonRepository _personRepository; // Añadir repositorio de personas
        private readonly IUserRoleRepository _userRoleRepository; // Repositorio para UserRole
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPersonRepository personRepository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            
            _personRepository = personRepository; // Inicializar repositorio de personas
            _userRoleRepository = userRoleRepository; // Inicializar repositorio de UserRole
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepository.GetById(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task Add(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.Add(user);
        }

        public async Task Update(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.Update(user);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task AddUserAndPerson(UserDto userDto, PersonDto personDto, int roleId)
        {
            // Primero, crear la Persona
            var person = _mapper.Map<Person>(personDto);
            await _personRepository.Add(person);

            // Obtener el ID de la Persona recién creada
            userDto.PersonId = person.Id; // Asegúrate de que UserDto tenga una propiedad PersonId

            // Luego, crear el Usuario
            var user = _mapper.Map<User>(userDto);
            await _userRepository.Add(user);

            // Asignar el rol al usuario
            var userRole = new UserRole
            {
                UserId = user.Id, // ID del usuario creado
                RoleId = roleId // ID del rol a asignar
            };
            await _userRoleRepository.Add(userRole);
        }
    }
}
