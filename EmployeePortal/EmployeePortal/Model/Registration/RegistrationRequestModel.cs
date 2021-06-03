namespace EmployeePortal.Model.Registration
{
    public class RegistrationRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AdminKey { get; set; }
    }
}