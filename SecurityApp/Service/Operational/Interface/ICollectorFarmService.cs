using Entity.DTO.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Operational.Interface
{
    public interface ICollectorFarmService
    {
        Task<IEnumerable<CollectorFarmDto>> GetAll();
        Task<CollectorFarmDto> GetById(int id);
        Task Add(CollectorFarmDto collectorFarmDto);
        Task Update(CollectorFarmDto collectorFarmDto);
        Task Delete(int id);

    }
}
