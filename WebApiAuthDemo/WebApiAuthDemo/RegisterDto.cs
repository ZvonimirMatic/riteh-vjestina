﻿namespace WebApiAuthDemo;

public class RegisterDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public DateTime? Birthday { get; set; }
}
