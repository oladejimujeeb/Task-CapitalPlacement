using CapitalPlacementDotNET_Task.Enums;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementDotNET_Task.Model
{
    public class UpdateApplicationFormModel
    {
        [Required]
        public Guid ApplicationId { get; set; }
        public string? FormTitle { get; set; }
        public string? FormDescription { get; set; }
        public List<UpCustomQuestionModel> CustomQuestions { get; set; } = new List<UpCustomQuestionModel>();
    }

    public class UpCustomQuestionModel
    {
        public Guid QuestionId { get; set; }
        public string? Question { get; set; }
        public QuestionType? QuestionType { get; set; }
    }
}
