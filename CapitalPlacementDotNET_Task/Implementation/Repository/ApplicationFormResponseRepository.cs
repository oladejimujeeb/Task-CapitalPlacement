using CapitalPlacementDotNET_Task.AppContext;
using CapitalPlacementDotNET_Task.Entities;
using CapitalPlacementDotNET_Task.Interface.IRepository;

namespace CapitalPlacementDotNET_Task.Implementation.Repository
{
    public class ApplicationFormResponseRepository : BaseRepository<ApplicationFormResponse>, IApplicationFormResponseRepository
    {
        public ApplicationFormResponseRepository(ApplicationContext context)
        {
            Context = context;
        }
    }
}
