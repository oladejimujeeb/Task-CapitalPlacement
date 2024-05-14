using CapitalPlacementDotNET_Task.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementDotNET_Task.Model
{
    public class CustomQuestionModel
    {
        [Required(ErrorMessage = "Question is Required")]
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        //public Guid ApplicationFormId { get; set; }

    }
    public class CustomQuestionViewModel
    {
        [Required(ErrorMessage = "Question is Required")]
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public Guid QuestionId { get; set; }
    }
}
