﻿using AutoMapper;
using Entity.DTO.Operational;
using Entity.Model.Operational;
using Repository.Operational.Interface;
using Service.Operational.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Operational.Implements
{
    public class CollectorFarmService : ICollectorFarmService
    {
        private readonly ICollectorFarmRepository _collectorFarmRepository;
        private readonly IMapper _mapper;

        public CollectorFarmService(ICollectorFarmRepository collectorFarmRepository, IMapper mapper)
        {
            _collectorFarmRepository = collectorFarmRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CollectorFarmDto>> GetAll()
        {
            var collectorFarms = await _collectorFarmRepository.GetAll();
            return _mapper.Map<IEnumerable<CollectorFarmDto>>(collectorFarms);
        }

        public async Task<CollectorFarmDto> GetById(int id)
        {
            var collectorFarm = await _collectorFarmRepository.GetById(id);
            return _mapper.Map<CollectorFarmDto>(collectorFarm);
        }

        public async Task Add(CollectorFarmDto collectorFarmDto)
        {
            var collectorFarm = _mapper.Map<CollectorFarm>(collectorFarmDto);
            await _collectorFarmRepository.Add(collectorFarm);
        }

        public async Task Update(CollectorFarmDto collectorFarmDto)
        {
            var collectorFarm = _mapper.Map<CollectorFarm>(collectorFarmDto);
            await _collectorFarmRepository.Update(collectorFarm);
        }

        public async Task Delete(int id)
        {
            await _collectorFarmRepository.Delete(id);
        }
    }
}
