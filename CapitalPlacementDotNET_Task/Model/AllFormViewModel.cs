namespace CapitalPlacementDotNET_Task.Model
{
    public class AllFormViewModel
    {
        public Guid FormId { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
    }
    public class FormViewModel
    {
        public Guid FormId { get; set; }
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public string FormContent { get; set; } 
    }
    public class ApplicationResponseModel
    {
        public string FormTitle { get; set; }
        public string FormDescription { get; set; }
        public IDictionary<string, string> FormContent { get; set; } = new Dictionary<string, string>();
    }
}
