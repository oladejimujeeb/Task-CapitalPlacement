using CapitalPlacementDotNET_Task.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementDotNET_Task.Model
{
    public class ApplicationFormResquestModel
    {
        [Required(ErrorMessage = "Program Title is Required")]
        public string ProgramTitle { get; set; }
        [Required (ErrorMessage ="Program Description is Required")]
        public string ProgramDescription { get; set; }
        public bool HidePhoneNumber { get; set; }
        public bool HideNationality { get; set; }
        public bool HideCurrentAddress { get; set; }
        public bool HideIDNumber { get; set; }
        public bool HideGender { get; set; }
        public bool HideDateOfBirth { get; set; }
        public List<CustomQuestionModel> CustomQuestions { get; set; } = new List<CustomQuestionModel>();
    }
}
