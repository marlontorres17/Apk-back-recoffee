using Entity.Model.Operational;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Operational.Interface
{
    public interface ICollectorFarmRepository
    {
        Task<IEnumerable<CollectorFarm>> GetAll();
        Task<CollectorFarm> GetById(int id);
        
        Task Update(CollectorFarm collectorFarm);
        Task Delete(int id);

        CollectorFarm GetByPersonAndFarm(int personId, int farmId);
        Task Add(CollectorFarm collectorFarm);
    }
}
