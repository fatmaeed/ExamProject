using ExamProject.Application.Utils;
using Microsoft.AspNetCore.Mvc;

public static class FailureIActionResult {

    public static IActionResult FailureHandler(Failure failure) {
        switch (failure) {
            case NotFoundFailure _:

                return new NotFoundObjectResult(
                    new ProblemDetails {
                        Title = failure.Message,
                        Status = 404,
                        Detail = failure.Details
                    }
                    );

            case BadRequestFailure _:
                return new BadRequestResult();

            case UnauthorizedFailure _:
                return new UnauthorizedResult();

            case ForbiddenFailure _:
                return new ForbidResult();

            case ServerFailure _:
                return new StatusCodeResult(500);

            default:
                return new BadRequestResult();
        }
    }
}