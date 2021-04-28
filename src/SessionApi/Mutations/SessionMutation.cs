using HotChocolate;
using MongoDB.Driver;
using SessionApi.Inputs;
using SessionApi.Models;
using SessionApi.Payloads;
using System.Linq;
using System.Threading.Tasks;

namespace SessionApi.Mutations
{
    public class SessionMutation
    {
        public async Task<CreateSessionPayload> CreateSessionAsync(
            [Service] IMongoCollection<Session> collection,
            CreateSessionInput input)
        {
            var session = new Session()
            {
                State = input.SessionStates
            };

            await collection.InsertOneAsync(session);

            return new CreateSessionPayload(session);
        }
        public async Task<UpsertSessionStatePayload> UpdateSessionStateAsync(
            [Service] IMongoCollection<Session> collection, UpsertSessionStateInput input)
        {

            Session sesToUpdate = await collection.Find(p => p.Id == input.Id).FirstOrDefaultAsync();
            if (sesToUpdate.State.Any(p => p.Key == input.State.Key))
            {
                foreach (var state in sesToUpdate.State.Where(p => p.Key == input.State.Key))
                {
                    state.Value = input.State.Value;
                }
            }
            else
            {
                sesToUpdate.State.Add(input.State);
            }
            await collection.ReplaceOneAsync(p => p.Id == input.Id, sesToUpdate);
            return new UpsertSessionStatePayload(sesToUpdate);
        }
    }
}
