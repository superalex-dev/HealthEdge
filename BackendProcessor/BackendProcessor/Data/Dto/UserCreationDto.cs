﻿namespace BackendProcessor.Data.Dto;

public class UserCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public UserCreationDto(string firstName, string lastName, string userName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Password = password;
    }
}