using System.Security.Claims;
using F1Trackr.Core.Domain.Management;
using F1Trackr.Core.Infrastructure.EntityFramework;
using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace F1Trackr.Core.Application.Users;

public sealed class AdminDeleteUser
{
    public sealed record Command(UserId UserId) : ICommand<Result>;

    public sealed class CommandHandler : ICommandHandler<Command, Result>
    {
        private readonly TrackrDbContext _dbContext;
        private readonly ClaimsPrincipal _currentUser;

        public CommandHandler(TrackrDbContext dbContext, ClaimsPrincipal currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
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

            if (!_currentUser.IsAdmin)
            {
                return new AccessDeniedError("Only admin users can delete other users.");
            }

            if (_currentUser.UserId != command.UserId)
            {
                return new ValidationError("You cannot delete your own account.");
            }

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
