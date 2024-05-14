using CapitalPlacementDotNET_Task.Entities;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementDotNET_Task.Model
{
    public class SunmitApplicationFromModel
    {
        [Required(ErrorMessage ="Application FormId Cannot be Null")]
        public Guid ApplicationFormId { get; set; }
        public List<QuestionRespondsModel>QuestionResponds { get; set; } = new List<QuestionRespondsModel>();
    }
    public class QuestionRespondsModel
    {
        public Guid CustomQuestionId { get; set; }
        public Guid PersonalInformationId { get; set; }
        public string QuestionResponse { get; set; }= string.Empty;
    }
}
