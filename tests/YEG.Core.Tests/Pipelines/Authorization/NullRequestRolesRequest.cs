using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.Core.Pipelines.Authorization;

namespace YEG.Core.Tests.Pipelines.Authorization
{
    public class NullRequestRolesRequest : IRequest<string>, ISecuredRequest
    {
        public string[] Roles { get; } = null;
    }
}
