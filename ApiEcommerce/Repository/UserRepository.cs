using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiEcommerce.Models;
using ApiEcommerce.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ApiEcommerce.Repository;

public class UserRepository : IUserRepository
{

    public readonly ApplicationDbContext _db;
    private string? secretKey;

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public User? GetUser(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public ICollection<User> GetUsers()
    {
        return _db.Users.OrderBy(u => u.Username).ToList();
    }

    public bool IsUniqueUser(string username)
    {
        return !_db.Users.Any(u => u.Username.ToLower().Trim() == username.ToLower().Trim());
    }

    public async Task<UserLoginResponseDto> Login(UserLoginDto userLoginDto)
    {
        if (string.IsNullOrEmpty(userLoginDto.Username))
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "El Username es requerido"
            };
        }

        var user = await _db.ApplicationUsers.FirstOrDefaultAsync<ApplicationUser>(u => u.UserName != null && u.UserName.ToLower().Trim() == userLoginDto.Username.ToLower().Trim());
        if (user == null)
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "Username no encontrado"
            };
        }
        if (userLoginDto.Password == null)
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "Password requerido"
            };
        }
        bool isValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
        //if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.Password))
        if (!isValid)
        {
            return new UserLoginResponseDto()
            {
                Token = "",
                User = null,
                Message = "Credenciales son incorrectas"
            };
        }

        //Generar JWT
        var handlerToken = new JwtSecurityTokenHandler();
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("SecretKey no esta configurada");
        }
        var roles = await _userManager.GetRolesAsync(user);
        var key = System.Text.Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim("username", user.UserName ?? string.Empty),
                new Claim(ClaimTypes.Role, roles.FirstOrDefault() ?? string.Empty),

            }
            ),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = handlerToken.CreateToken(tokenDescriptor);
        return new UserLoginResponseDto()
        {
            Token = handlerToken.WriteToken(token),
            /* User = new UserRegisterDto()
            {
                Username = user.Username,
                Name = user.Name,
                Role = user.Role,
                Password = user.Password ?? ""
            },
             */
            User = _mapper.Map<UserDataDto>(user),
            Message = "Usuario logueado correctamente."

        };

    }

    /* public async Task<User> Register(CreateUserDto createUserDto)
    {
        var encriptedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
        var user = new User()
        {
            Username = createUserDto.Username ?? "No Username",
            Name = createUserDto.Name,
            Role = createUserDto.Role,
            Password = encriptedPassword
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    } */

    public async Task<UserDataDto> Register(CreateUserDto createUserDto)
    {
        if (string.IsNullOrEmpty(createUserDto.Username))
        {
            throw new ArgumentNullException("El username es requerido");
        }

        if (createUserDto.Password == null)
        {
            throw new ArgumentNullException("La password es requerida");
        }

        var user = new ApplicationUser()
        {
            UserName = createUserDto.Username,
            Email = createUserDto.Username,
            NormalizedEmail = createUserDto.Username.ToUpper(),
            Name = createUserDto.Name
        };
        var result = await _userManager.CreateAsync(user, createUserDto.Password);
        if (result.Succeeded)
        {
            var userRole = createUserDto.Role ?? "User";
            var roleExists = await _roleManager.RoleExistsAsync(userRole);
            if (!roleExists)
            {
                var identityRole = new IdentityRole(userRole);
                await _roleManager.CreateAsync(identityRole);
            }
            await _userManager.AddToRoleAsync(user, userRole);
            var createdUser = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == createUserDto.Username);
            return _mapper.Map<UserDataDto>(createdUser);
        }
        throw new ApplicationException("No se pudo crear el registro");
    } 
    

}
