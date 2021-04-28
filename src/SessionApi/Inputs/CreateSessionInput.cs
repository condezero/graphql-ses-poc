using SessionApi.Models;
using System.Collections.Generic;

namespace SessionApi.Inputs
{
    public record CreateSessionInput(ICollection<SessionState> SessionStates);
}
