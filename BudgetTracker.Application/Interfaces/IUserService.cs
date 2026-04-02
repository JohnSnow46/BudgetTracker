using BudgetTracker.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateUserAsync(CreateUserDto user);
        Task DeleteUserAsync(Guid Id);
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto> UpdateUserAsync(Guid Id, UpdateUserDto user);
        Task<UserResponseDto> GetUserByIdAsync(Guid Id);
    }
}
