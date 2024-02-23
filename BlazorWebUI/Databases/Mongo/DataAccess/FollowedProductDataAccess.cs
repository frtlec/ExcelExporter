using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using BlazorWebUI.Components.Pages;
using BlazorWebUI.Databases.Mongo.DataModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace BlazorWebUI.Databases.Mongo.DataAccess
{
    public class FollowedProductDataAccess
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<FollowedProduct> _followedProducts;
        public FollowedProductDataAccess(IMongoDBSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            _database = _client.GetDatabase(settings.DatabaseName);
            _followedProducts = _database.GetCollection<FollowedProduct>(nameof(FollowedProduct));
        }
        public async Task<List<FollowedProduct>> GetAll()
        {
            var getall = await _followedProducts.Find(_ => true).ToListAsync();
            return getall;
        }
        public async Task Remove(string id)
        {
            await _followedProducts.DeleteOneAsync(m => m.Id == id);
        }
        public async Task Update(string id,decimal trendyolPrice,int trendyolStock,string trendyolLink)
        {

            var filter = Builders<FollowedProduct>.Filter.Eq(f=>f.Id, id);
            var update = Builders<FollowedProduct>.Update.Combine(
                    Builders<FollowedProduct>.Update.Set(p=>p.TrendyolLink, trendyolLink),
                    Builders<FollowedProduct>.Update.Set(p=>p.TrendyolPrice, trendyolPrice),
                    Builders<FollowedProduct>.Update.Set(p=>p.TrendyolStock, trendyolStock)
                );;
            await _followedProducts.UpdateManyAsync(filter, update);
        }
        public async Task RemoveAndCreateMany(List<FollowedProduct> list)
        {


            await _followedProducts.DeleteManyAsync(x=> list.Any(f => f.ProductId == x.ProductId) == false);
            var currentDatas = _followedProducts.Find(x => list.Any(f=>f.ProductId==x.ProductId)).ToList();
            var insertItems = list.Except(currentDatas).ToList();
            insertItems.ForEach(f=>f.CreatedDate = DateTime.Now);

            if (insertItems.Count>0)
            {
               await _followedProducts.InsertManyAsync(insertItems);
            }
            
       
        }
        public async Task Create(FollowedProduct model)
        {
            var exists = await (await _followedProducts.FindAsync(f => f.ProductId == model.ProductId)).AnyAsync();
            if (exists == false)
            {
                model.CreatedDate = DateTime.Now;
                await _followedProducts.InsertOneAsync(model);
            }

        }
        public async Task BulkCreateAsync(List<FollowedProduct> list)
        {
            var founds = await _followedProducts.Find(_ => list.Any(x => x.ProductId == _.ProductId && x.SourceSite == _.SourceSite)).ToListAsync();

            foreach (var item in founds)
            {
                FollowedProduct followedProduct = list.First(f => f.ProductId == item.ProductId && f.SourceSite == item.SourceSite);

                list.Remove(followedProduct);
            }

            await _followedProducts.InsertManyAsync(list);
        }
    }
}
