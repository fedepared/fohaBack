using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Foha.Dtos;
using Foha.Models;
using Foha.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Foha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthRepository<Usuario> _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ITokenRepository _token;
        private readonly IDataRepository<Usuario> _context;
        

        public AuthController(IAuthRepository<Usuario> repo, IConfiguration config, IMapper mapper, ITokenRepository token)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
            _token=token;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            registerDto.nombreUs = registerDto.nombreUs.ToLower();
            
            if (await _repo.UserExists(registerDto.nombreUs))
            {
                return BadRequest("El usuario ya existe");
            }
                
            var userToCreate = _mapper.Map<Usuario>(registerDto);
            var createdUser = await _repo.Register(userToCreate, registerDto.pass,registerDto.isOp,registerDto.idSector);
            return StatusCode(201, new { nombreUs = createdUser.NombreUs, pass = createdUser.Pass, isOp=createdUser.IsOp,idSector=createdUser.IdSector});
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            
            var userFromRepo = await _repo.Login(loginDto.NombreUs.ToLower(), loginDto.Pass,loginDto.RefreshToken);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.IdUser.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.NombreUs)
            };

            

            var jwtToken = _token.GenerateAccessToken(claims);
            var refreshToken = await _token.GenerateRefreshToken();

            userFromRepo.RefreshToken = refreshToken;
            await PutUser(userFromRepo);
            
            
            
            // var key = new SymmetricSecurityKey(Encoding.UTF8
            //     .GetBytes(_config.GetSection("AppSettings:Token").Value));
            // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            // var tokenDescriptor = new SecurityTokenDescriptor
            // {
            //     Subject = new ClaimsIdentity(claims),
            //     Expires = DateTime.Now.AddDays(1),
            //     SigningCredentials = creds
            // };

            // var tokenHandler = new JwtSecurityTokenHandler();

            // var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return Ok(new {
                isOp=userFromRepo.IsOp,
                sector=userFromRepo.IdSector,
                token = jwtToken.Result,
                //refreshToken = refreshToken
            }); 

            // return Ok(new {token = tokenHandler.WriteToken(token), user = userFromRepo.NombreUs});
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // if (id != editUsuarioDto.IdCliente)
            // {
            //     return BadRequest();
            // }

            var preUsuario = _mapper.Map<Usuario>(usuario);
            _repo.Update(preUsuario);
            await _repo.SaveAsync(preUsuario);

            return NoContent();
        }
    }
}