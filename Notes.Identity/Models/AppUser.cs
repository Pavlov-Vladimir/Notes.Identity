﻿namespace Notes.Identity.Models;

public class AppUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
}