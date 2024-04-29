namespace BackendProcessor.Data.Dto;

public class PatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string BloodType { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public DateTime DateOfCreation { get; set; }

    public PatientDto(int id, string firstName, string lastName, string userName , string email, string password, DateOnly dateOfBirth, string gender, string bloodType , string contactNumber, string address, DateTime dateOfCreation)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Password = password;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        BloodType = bloodType;
        ContactNumber = contactNumber;
        Address = address;
        DateOfCreation = dateOfCreation;
    }
}