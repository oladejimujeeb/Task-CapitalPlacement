using CapitalPlacementDotNET_Task.Model;

namespace CapitalPlacementDotNET_Task.Entities
{
    public class PersonalInformation:BaseEntity
    {
        public string Field { get; set; }
        public string FieldType { get; set; } = "Text";
        public Guid ApplicationFormId { get; set; }
        public ApplicationForm ApplicationForm { get; set; }

    }
}
