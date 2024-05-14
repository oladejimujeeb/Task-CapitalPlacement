using CapitalPlacementDotNET_Task.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementDotNET_Task.AppContext
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
            
        }
        public DbSet<ApplicationForm> ApplicationForms { get; set; }
        public DbSet<PersonalInformation> PersonalInformations { get; set; }
        public DbSet<CustomQuestion> CustomQuestions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<ApplicationFormResponse> ApplicationFormsResponse { get; set; }
    }
}
