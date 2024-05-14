using CapitalPlacementDotNET_Task.Helper;

namespace CapitalPlacementDotNET_Task.Model
{
    public class ApplicationFormModel
    {
        public List<PersonalInformationModel> PersonalInformation { get; set; } = new List<PersonalInformationModel>();
        public List<CustomQuestionViewModel> CustomQuestions { get; set; } = new List<CustomQuestionViewModel>();
    }
}
