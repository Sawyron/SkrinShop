using SkrinShop.Console.Core;
using SkrinShop.Persistence.Users;

namespace SkrinShop.Console.Users;

internal class UserMapper : IMapper<UserDto, User>
{
    public User Map(UserDto input) => new()
    {
        Email = input.Email,
        Name = input.FullName
    };
}
