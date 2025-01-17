﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ParkingLotApi.Models;

namespace ParkingLotApi.Repositories
{
    public class ParkingLotsRepository : IParkingLotsRepository
    {
        private readonly IMongoCollection<ParkingLot> parkingLotCollection;

        public ParkingLotsRepository(IOptions<ParkingLotDatabaseSettings> parkingLotDatabaseSettings)
        {
            var mongoClient = new MongoClient(parkingLotDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(parkingLotDatabaseSettings.Value.DatabaseName);
            parkingLotCollection = mongoDatabase.GetCollection<ParkingLot>(parkingLotDatabaseSettings.Value.CollectionName);
        }

        public async Task<ParkingLot> CreateParkingLot(ParkingLot parkingLot)
        {
            await parkingLotCollection.InsertOneAsync(parkingLot);
            return await parkingLotCollection.Find(a => a.Name == parkingLot.Name).FirstAsync();
        }

        public async Task<ParkingLot> GetParkingLotById(string id)
        {
            return await parkingLotCollection.Find(parkingLot => parkingLot.Id == id).FirstOrDefaultAsync();
        }

        public async Task DeleteParkingLotById(string id)
        {
            await parkingLotCollection.DeleteOneAsync(parkingLot => parkingLot.Id == id);
        }

        public async Task<List<ParkingLot>> GetParkingLotByPageInfo(int pageIndex, int pageSize)
        {
            return await parkingLotCollection.Find(parkingLot => true)
                .Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<List<ParkingLot>> GetAllParkingLot()
        {
            return await parkingLotCollection.Find(parkingLot => true).ToListAsync();
        }

        public async Task<ParkingLot> UpdateParkingLotInfo(string id, ParkingLot parkingLot)
        {
            await parkingLotCollection.ReplaceOneAsync(parkingLot => parkingLot.Id == id, parkingLot);
            return parkingLot;
        }
    }
}
