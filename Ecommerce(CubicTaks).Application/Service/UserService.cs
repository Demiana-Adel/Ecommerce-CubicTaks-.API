using AutoMapper;
using Ecommerce_CubicTaks_.Application.Contract;
using Ecommerce_CubicTaks_.Application.Service.Jwt;
using Ecommerce_CubicTaks_.Dto.User;
using Ecommerce_CubicTaks_.Dto.ViewResult;
using Ecommerce_CubicTaks_.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_CubicTaks_.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public UserService(IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<ResultView<string>> LoginAsync(LoginModel model)
        {
            var result = new ResultView<string>();

            var user = await _userRepository.GetByUsernameAsync(model.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                result.IsSuccess = false;
                result.Message = "Invalid username or password.";
                return result;
            }

            var token = _jwtService.GenerateToken(user.Username, user.Role);

            result.IsSuccess = true;
            result.Message = "Login successful.";
            result.Entity = token;
            return result;
        }
        public async Task<ResultView<UserDto>> RegisterUserAsync(CreateUserDto dto)
        {
            var existing = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
            {
                return new ResultView<UserDto>
                {
                    IsSuccess = false,
                    Message = "Username already exists."
                };
            }

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = string.IsNullOrWhiteSpace(dto.Role) ? "user" : dto.Role
            };

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();

            return new ResultView<UserDto>
            {
                IsSuccess = true,
                Message = "User registered successfully.",
                Entity = _mapper.Map<UserDto>(user)
            };
        }

    }

}
