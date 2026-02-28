using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Users;

public sealed class AdminUpdateUser
{
    public sealed record Command(
        UserId UserId,
        string Name,
        string? AccessCode,
        bool IsAdmin) : ICommand<Result>;

    public sealed class CommandHandler : ICommandHandler<Command, Result>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public CommandHandler(TrackrDbContext dbContext, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> HandleAsync(Command command, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .Where(u => u.Id == command.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (user is null)
            {
                return new NotFoundError($"User with ID {command.UserId} not found.");
            }

            user.Name = command.Name;
            user.IsAdmin = command.IsAdmin;

            var existing = await _dbContext.Users
                .Where(u => u.Name == user.Name && u.Id != user.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (existing is not null)
            {
                return new ValidationError(nameof(command.Name), "User with the same name already exists");
            }

            if (!string.IsNullOrWhiteSpace(command.AccessCode))
            {
                user.AccessCode = _passwordHasher.HashPassword(user, command.AccessCode);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
