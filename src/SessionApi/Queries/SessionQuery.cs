using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MongoDB.Driver;
using SessionApi.Models;
using System;

namespace SessionApi.Queries
{
    public class SessionQuery
    {
        [UseMongoDbPaging]
        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IExecutable<Session> GetSessions([Service] IMongoCollection<Session> collection) => collection.AsExecutable();
 

        [UseFirstOrDefault]
        public IExecutable<Session> GetSessionById(
            [Service] IMongoCollection<Session> collection,
            [ID] Guid id) => collection.Find(x => x.Id == id).AsExecutable();

    }
}
