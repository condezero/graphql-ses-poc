using HotChocolate;
using MongoDB.Driver;
using SessionApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SessionApi.Resolvers
{
    public class SessionNodeResolver
    {
        public Task<Session> ResolveAsync([Service] IMongoCollection<Session> collection, Guid id) =>
               collection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
