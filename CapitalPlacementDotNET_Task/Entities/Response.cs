using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Entities
{
    public class Response:BaseEntity
    {
        public string QuestionResponse { get; set; }=string.Empty;
        public Guid CustomQuestionId { get; set; }
        public Guid PersonalInformationId { get; set; }
        public Guid ApplicationFormResponseId { get; set; }
    }
}
