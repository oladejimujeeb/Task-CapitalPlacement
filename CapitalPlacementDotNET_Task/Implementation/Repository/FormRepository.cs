using CapitalPlacementDotNET_Task.AppContext;
using CapitalPlacementDotNET_Task.Entities;
using CapitalPlacementDotNET_Task.Interface.IRepository;

namespace CapitalPlacementDotNET_Task.Implementation.Repository
{
    public class FormRepository:BaseRepository<ApplicationForm>, IFormRepository
    {
        public FormRepository(ApplicationContext context)
        {
            Context = context;
        }
    }
}
