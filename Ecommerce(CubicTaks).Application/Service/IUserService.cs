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
    public interface IUserService
    {
        Task<ResultView<UserDto>> RegisterUserAsync(CreateUserDto dto);
        Task<ResultView<string>> LoginAsync(LoginModel model);

    }
}
