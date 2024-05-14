using CapitalPlacementDotNET_Task.Enums;
using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Entities
{
    public class CustomQuestion:BaseEntity
    {
        public string Question { get; set; }
        public QuestionType QuestionType { get; set; }
        public Guid ApplicationFormId { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
    }
}
