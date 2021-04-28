using HotChocolate.Types.Relay;
using SessionApi.Models;
using System;

namespace SessionApi.Inputs
{
    public record UpsertSessionStateInput([ID] Guid Id, SessionState State);
}
