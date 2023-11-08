﻿using ParkingLotApi.Dtos;

namespace ParkingLotApi.Services
{
    public class ParkingLotService
    {
        public async Task<ParkingLotDto> CreateAsync(ParkingLotDto parkingLotDto)
        {
            if (parkingLotDto.Capacity < 10)
            {
                throw new ArgumentException();
            }

            return null;
        }
    }
}