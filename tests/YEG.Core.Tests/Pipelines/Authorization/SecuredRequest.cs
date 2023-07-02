﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YEG.Core.Pipelines.Authorization;

namespace YEG.Core.Tests.Pipelines.Authorization
{
    public class SecuredRequest : IRequest<string>, ISecuredRequest
    {
        public string[] Roles { get; } = new string[] {"Role2","Role3" };
    }
}