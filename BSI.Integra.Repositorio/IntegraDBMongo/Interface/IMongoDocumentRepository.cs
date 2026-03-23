using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.IntegraDBMongo.Interface
{
    public interface IMongoDocumentRepository
    { 
        Task<List<BsonDocument>> GetAllAsync(string collectionName); 
        Task<List<BsonDocument>> FindAsync(string collectionName, string filterJson); 
        Task<BsonDocument> GetByIdAsync(string collectionName, string id); 
        Task<List<BsonDocument>> ExecuteQueryAsync(string collectionName, BsonDocument filter);
        Task<List<BsonDocument>> FindWithAggregateAsync(string collectionName, BsonDocument[] pipeline);
    }
}
