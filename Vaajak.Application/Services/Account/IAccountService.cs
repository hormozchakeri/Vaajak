using Vaajak.Application.Dto.Account;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Domain.Entities;
using X.PagedList;

namespace Vaajak.Application.Services.Account
{
    public interface IAccountService
    {
        Task<IPagedList<AccountDto>> GetAllUsersAsync(PaginationRequestDTO paginationRequestDTO);
        Task<SignupDto> SignupAsync(SignupDto signupDto);
        Task<SigninResponseDto> SigninAsync(SigninDto signinDto);
        Task<SigninResponseDto> RefreshTokenAsync(string refreshToken);
        Task<ProfileDto> GetProfileAsync(string userId);
    }
}