using F1Trackr.Core.Results;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;

namespace F1Trackr.Server;

public static class ResultExtensions
{
    extension(Result result)
    {
        public Results<NoContent, NotFound, ForbidHttpResult, BadRequest<IDictionary<string, string[]>>> ToApiResult()
        {
            return result.ToApiResult(TypedResults.NoContent);
        }

        public Results<TSuccessResult, NotFound, ForbidHttpResult, BadRequest<IDictionary<string, string[]>>> ToApiResult<TSuccessResult>(
            Func<TSuccessResult> onSuccess)
            where TSuccessResult : IResult
        {
            if (result.IsSuccess)
            {
                return onSuccess();
            }

            if (result.HasError<AccessDeniedError>())
            {
                return TypedResults.Forbid();
            }

            if (result.HasError<NotFoundError>())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.BadRequest(result.ToErrorDictionary());
        }
    }

    extension<TValue>(Result<TValue> result)
    {
        public Results<TSuccessResult, NotFound, ForbidHttpResult, BadRequest<IDictionary<string, string[]>>> ToApiResult<TSuccessResult>(
            Func<TValue, TSuccessResult> onSuccess)
            where TSuccessResult : IResult
        {
            if (result.IsSuccess)
            {
                return onSuccess(result.Value);
            }

            if (result.HasError<AccessDeniedError>())
            {
                return TypedResults.Forbid();
            }

            if (result.HasError<NotFoundError>())
            {
                return TypedResults.NotFound();
            }

            return TypedResults.BadRequest(result.ToErrorDictionary());
        }
    }

    extension(IResultBase result)
    {
        private IDictionary<string, string[]> ToErrorDictionary()
        {
            return result.Errors.ToDictionary<IError, string, string[]>(
                error => error is ValidationError validationError ? validationError.PropertyName : string.Empty,
                error => [error.Message]);
        }
    }
}
