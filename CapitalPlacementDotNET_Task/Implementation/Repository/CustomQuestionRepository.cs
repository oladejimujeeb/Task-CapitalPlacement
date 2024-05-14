using CapitalPlacementDotNET_Task.AppContext;
using CapitalPlacementDotNET_Task.Entities;
using CapitalPlacementDotNET_Task.Interface.IRepository;

namespace CapitalPlacementDotNET_Task.Implementation.Repository
{
    public class CustomQuestionRepository : BaseRepository<CustomQuestion>, ICustomQuestionRepository
    {
        public CustomQuestionRepository(ApplicationContext context)
        {
            Context = context;
        }
    }
}
