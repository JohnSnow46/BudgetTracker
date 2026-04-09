using BudgetTracker.Application.Dto.Budget;
using BudgetTracker.Application.Dto.User;
using BudgetTracker.Application.Exceptions;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain;
using BudgetTracker.Domain.Interfaces;

namespace BudgetTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<UserResponseDto> CreateUserAsync(CreateUserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                Login = userDto.Login,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password, workFactor: 12),
            };

            var budget = new Budget
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
            };

            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.Budgets.CreateAsync(budget);
            await _unitOfWork.CompleteAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = userDto.Name,
                Login = userDto.Login,
                BudgetResponse = new BudgetResponseDto
                {
                    Id = budget.Id,
                    Balance = budget.Balance,
                }
            };
        }

        public async Task DeleteUserAsync(Guid Id)
        {
            if(Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(Id));
            }

            var user = await _unitOfWork.Users.GetByIdAsync(Id);
            if (user == null)
            {
                throw new UserNotFoundException(Id);
            }

            await _unitOfWork.Users.DeleteAsync(Id);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Login = u.Login,
            });
        }

        public async Task<UserResponseDto> GetUserByIdAsync(Guid Id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(Id);

            if(user == null)
            {
                throw new UserNotFoundException(Id);
            }

            var budget = (await _unitOfWork.Budgets.FindAsync(b => b.UserId == Id)).FirstOrDefault();

            if(budget == null)
            {
                throw new InvalidOperationException(nameof(budget));
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                BudgetResponse = new BudgetResponseDto
                {
                    Id = budget.Id,
                    Balance = budget.Balance,
                }
            };
        }

        public async Task<UserResponseDto> UpdateUserAsync(Guid Id, UpdateUserDto userDto)
        {
            if(Id == Guid.Empty)
            {
                throw new ArgumentException(nameof(Id));
            }
            if(userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var user = await _unitOfWork.Users.GetByIdAsync(Id);

            if(user == null)
            {
                throw new UserNotFoundException(Id);
            }

            user.Login = userDto.Login;
            user.Name = userDto.Name;

            await _unitOfWork.CompleteAsync();

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
            };
        }
    }
}
