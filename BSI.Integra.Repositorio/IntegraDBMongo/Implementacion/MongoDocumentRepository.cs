using BSI.Integra.Persistencia.IntegraDBMongo.Context;
using BSI.Integra.Repositorio.IntegraDBMongo.Interface;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BSI.Integra.Repositorio.IntegraDBMongo.Implementacion
{
    public class MongoDocumentRepository : IMongoDocumentRepository
    {
        private readonly MongoDBContext _context;

        public MongoDocumentRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<BsonDocument>> GetAllAsync(string collectionName)
        {
            var collection = _context.GetCollection<BsonDocument>(collectionName);
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<BsonDocument>> FindAsync(string collectionName, string filterJson)
        {
            var collection = _context.GetCollection<BsonDocument>(collectionName);
            var filter = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(filterJson);
            return await collection.Find(filter).ToListAsync();
        }

        public async Task<BsonDocument> GetByIdAsync(string collectionName, string id)
        {
            var collection = _context.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<BsonDocument>> ExecuteQueryAsync(string collectionName, BsonDocument filter)
        {
            var collection = _context.GetCollection<BsonDocument>(collectionName);
            return await collection.Find(filter).ToListAsync();
        }
        public async Task<List<BsonDocument>> FindWithAggregateAsync(string collectionName, BsonDocument[] pipeline)
        {
            var collection = _context.GetCollection<BsonDocument>(collectionName);
            return await collection.Aggregate<BsonDocument>(pipeline).ToListAsync();
        }
    }
}