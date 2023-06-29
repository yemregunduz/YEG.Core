using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace YEG.Core.CrossCuttingConcerns.Exceptions
{
    public class AuthorizationProblemDetails : ProblemDetails
    {
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
