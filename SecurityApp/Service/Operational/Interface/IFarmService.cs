﻿using Entity.DTO.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Operational.Interface
{
    public interface IFarmService
    {
        Task<IEnumerable<FarmDto>> GetAll();
        Task<FarmDto> GetById(int id);
        Task Add(FarmDto farmDto);
        Task Update(FarmDto farmDto);
        Task Delete(int id);

        
    }
}
