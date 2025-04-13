using Microsoft.AspNetCore.Identity;
using Vaajak.Application.Dto.Account;
using Vaajak.Application.Dto.Primitives;
using Vaajak.Domain.Entities;
using Vaajak.Domain.Repositories.Account;
using X.PagedList;
using X.PagedList.Extensions;

namespace Vaajak.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IPagedList<AccountDto>> GetAllUsersAsync(PaginationRequestDTO paginationRequestDTO)
        {
            try
            {
                var accounts = await _accountRepository.GetAllUsersAsync();
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

        public Task<SigninDto> SigninAsync(SigninDto signinDto)
        {
            throw new NotImplementedException();
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
    }
}
