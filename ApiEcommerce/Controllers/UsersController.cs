using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]

        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return Ok(usersDto);
        }


    }
}
