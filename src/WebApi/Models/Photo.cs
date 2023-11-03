﻿namespace WebApi.Models;

public class Photo(City cities)
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public DateTime DateAdded { get; set; }
    public string Description { get; set; }
    public bool IsMain { get; set; }
    public string Url { get; set; }
    public string PublicId { get; set; }

    public City Cities { get; set; } = cities;
}    

