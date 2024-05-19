namespace BackendProcessor.Data.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfCreation { get; set; }
    public string ContactNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }

    public UserDto(int id, string firstName, string lastName, string userName, string email, DateTime dateOfCreation, string contactNumber, DateOnly dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        DateOfCreation = dateOfCreation;
        ContactNumber = contactNumber;
        DateOfBirth = dateOfBirth;
    }
}