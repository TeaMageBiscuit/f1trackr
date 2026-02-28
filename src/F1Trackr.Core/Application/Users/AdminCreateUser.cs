using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Users;

public sealed class AdminCreateUser
{
    public sealed record Command(string Name, string AccessCode, bool IsAdmin) : ICommand<Result<UserId>>;

    public sealed class CommandHandler : ICommandHandler<Command, Result<UserId>>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CommandHandler(TrackrDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserId>> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = new UserId(Guid.CreateVersion7()),
                Name = command.Name,
                AccessCode = string.Empty,
                IsAdmin = command.IsAdmin,
            };

            var existing = await _dbContext.Users
                .Where(u => u.Name == user.Name)
                .SingleOrDefaultAsync(cancellationToken);

            if (existing is not null)
            {
                return new ValidationError(nameof(command.Name), "User with the same name already exists");
            }

            user.AccessCode = _passwordHasher.HashPassword(user, command.AccessCode);

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
