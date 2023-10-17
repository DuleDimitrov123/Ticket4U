﻿namespace Users.Infrastructure.Identity.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException(string existingUser, string property)
        : base($"User with {property} : {existingUser} already exists.")
    {

    }
}
