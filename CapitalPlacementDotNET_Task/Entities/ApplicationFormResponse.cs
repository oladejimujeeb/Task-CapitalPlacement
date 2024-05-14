using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Entities
{
    public class ApplicationFormResponse:BaseEntity
    {
        public Guid ApplicationFormId { get; set; }
        public ApplicationForm ApplicationForm { get; set; }
        public List<Response> QuestionResponse { get; set; } = new List<Response>();
        public string SubmissionID {  get; set; }
    }
}
