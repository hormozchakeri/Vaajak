using Microsoft.AspNetCore.Identity;
using Vaajak.Application.Dto.Account;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Domain.Common.Auth;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Account;
using X.PagedList;
using X.PagedList.Extensions;

namespace Vaajak.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IJwtTokenGenerator  _jwtTokenGenerator;

        public AccountService(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _accountRepository = accountRepository;
            _jwtTokenGenerator  = jwtTokenGenerator;
        }

        public async Task<IPagedList<AccountDto>> GetAllUsersAsync(PaginationRequestDTO paginationRequestDTO)
        {
            try
            {
                var accounts = await _accountRepository.GetAllAsync();
                var accountDto = accounts.Select(account => new AccountDto
                {
                    Id = account.Id,
                    Username = account.UserName,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                });

                var paginatedAccounts = accountDto.ToPagedList(paginationRequestDTO.PageNumber, paginationRequestDTO.PageSize);
                return paginatedAccounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        public async Task<SigninResponseDto> SigninAsync(SigninDto signinDto)
        {
            var user = await _accountRepository.SigninAsync(signinDto.Username, signinDto.Password);
            if (user == null)
                throw new Exception("کاربری با این مشخصات یافت نشد");

            var accessToken  = await _jwtTokenGenerator.GenerateJwtTokenAsync(user);
            var refreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(user);

            return new SigninResponseDto
            {
                Token        = accessToken,
                RefreshToken = refreshToken,
                Username     = user.UserName ?? string.Empty,
                Email        = user.Email    ?? string.Empty,
            };
        }

        public async Task<SigninResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var userId = _jwtTokenGenerator.ValidateRefreshToken(refreshToken)
                ?? throw new Exception("Invalid or expired refresh token.");

            var user = await _accountRepository.FindByIdAsync(userId)
                ?? throw new Exception("User not found.");

            var newAccessToken  = await _jwtTokenGenerator.GenerateJwtTokenAsync(user);
            var newRefreshToken = await _jwtTokenGenerator.GenerateRefreshTokenAsync(user);

            return new SigninResponseDto
            {
                Token        = newAccessToken,
                RefreshToken = newRefreshToken,
                Username     = user.UserName ?? string.Empty,
                Email        = user.Email    ?? string.Empty,
            };
        }

        public async Task<SignupDto> SignupAsync(SignupDto signupDto)
        {
            try
            {
                var account = new User
                {
                    UserName = signupDto.UserName,
                    Email = signupDto.Email,
                    FirstName = signupDto.FirstName,
                    LastName = signupDto.LastName,
                    // Do not set PasswordHash directly
                };

                var createdUser = await _accountRepository.SignupAsync(account, signupDto.Password);

                return new SignupDto
                {
                    UserName = createdUser.UserName,
                    Email = createdUser.Email,
                    FirstName = createdUser.FirstName,
                    LastName = createdUser.LastName,
                    // Exclude password from return for security
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProfileDto> GetProfileAsync(string userId)
        {
            var user = await _accountRepository.FindByIdAsync(userId);

            return user == null
                ? throw new Exception("کاربر یافت نشد")
                : new ProfileDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

    }
}
