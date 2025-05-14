﻿namespace NestedCrud.API.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public int EmployeeId { get; set; }
}