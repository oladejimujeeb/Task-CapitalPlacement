namespace CapitalPlacementDotNET_Task.Helper
{
    public static class FormHelper
    {
        public const string LastName = nameof(LastName);
        public const string FirstName  = nameof(FirstName);
        public const string Email = nameof(Email);
        public const string PhoneNumber = nameof(PhoneNumber);
        public const string Nationality = nameof(Nationality);
        public const string CurrentAddress ="Current Residence";
        public const string IDNumber = "ID Number";
        public const string Gender = nameof(Gender);
        public const string DateOfBirth = "Date Of Birth";
        
        public static List<string> Fields = new List<string>() {LastName, FirstName, Email};
    }

    
}
