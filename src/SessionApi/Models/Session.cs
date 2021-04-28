using System;
using System.Collections.Generic;
using HotChocolate.Types.Relay;
using SessionApi.Resolvers;

namespace SessionApi.Models
{
    [Node(IdField = nameof(Id),
           NodeResolverType = typeof(SessionNodeResolver),
           NodeResolver = nameof(SessionNodeResolver.ResolveAsync))]
    public class Session
    {
        public Guid Id { get; init; }
        public ICollection<SessionState> State { get; init; }

    }
}
