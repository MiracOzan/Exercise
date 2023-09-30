namespace WebApi.Models;

public class City
{
    public City()
    {
        Photos = new List<Photo>();
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string COUNTRYCODE { get; set; }
    public string DISTRICT { get; set; }
    public string POPULATION { get; set; }
    public int UserId { get; set; }

    public List<Photo> Photos { get; set; }
}