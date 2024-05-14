using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Entities
{
    public class ApplicationForm:BaseEntity
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public List<CustomQuestion> CustomQuestions { get; set; } = new List<CustomQuestion>();
        public List<PersonalInformation> PersonalInformation { get; set; } = new List<PersonalInformation> { };
    }
}
