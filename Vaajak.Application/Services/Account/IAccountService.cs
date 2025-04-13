using Vaajak.Application.Dto.Account;
using Vaajak.Application.Dto.Primitives;
using X.PagedList;

namespace Vaajak.Application.Services.Account
{
    public interface IAccountService
    {
        Task<IPagedList<AccountDto>> GetAllUsersAsync(PaginationRequestDTO paginationRequestDTO);
        Task<SignupDto> SignupAsync(SignupDto signupDto);
        Task<SigninDto> SigninAsync(SigninDto signinDto);
    }
}