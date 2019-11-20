using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.MessageBrokers;

namespace Identity.Service.Contexts
{
    internal class CorrelationContext : ICorrelationContextAccessor
    {
        object ICorrelationContextAccessor.CorrelationContext { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
