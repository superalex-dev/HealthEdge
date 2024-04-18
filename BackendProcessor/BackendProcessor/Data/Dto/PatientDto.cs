namespace BackendProcessor.Data.Dto;

public class PatientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string ContactNumber { get; set; }
    public string Address { get; set; }
    public int UserId { get; set; }

    public PatientDto(int id, string firstName, string lastName, string email, DateOnly dateOfBirth, string gender, string contactNumber, string address, int userId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        ContactNumber = contactNumber;
        Address = address;
        UserId = userId;
    }
}