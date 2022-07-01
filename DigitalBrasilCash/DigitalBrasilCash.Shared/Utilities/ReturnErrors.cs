using System.Linq;
using FluentValidation.Results;
using System.Collections.Generic;

namespace DigitalBrasilCash.Shared.Utilities
{
    public static class ReturnErrors
    {
        public static object CreateObjetError(IList<ValidationFailure> errors)
        {
            return errors.Select(x => new { x.PropertyName, x.ErrorMessage });
        }
    }
}
