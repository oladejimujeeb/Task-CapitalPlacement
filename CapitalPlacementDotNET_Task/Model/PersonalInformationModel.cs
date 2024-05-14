namespace CapitalPlacementDotNET_Task.Model
{
    public class PersonalInformationModel
    {
        public Guid PersonalInformationId { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; } = "Text";
        
    }
}
