using Microsoft.AspNetCore.Http;
using CodingTask.Domain.SeedWork;

namespace CodingTask.API.SeedWork
{
    public class BusinessRuleValidationExceptionProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
    {
        public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
        {
            this.Title = "Business rule validation error";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = exception.Details;
            this.Type = "https://somedomain/business-rule-validation-error";
        }
    }
}