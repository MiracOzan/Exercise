﻿namespace WebApi.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public List<City> Citys { get; set; } = new();
}
    