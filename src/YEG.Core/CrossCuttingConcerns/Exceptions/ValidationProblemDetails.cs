using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace YEG.Core.CrossCuttingConcerns.Exceptions
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public object Errors { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);

    }
}
