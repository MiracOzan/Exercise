using WebApi.Models;

namespace WebApi.Dto;

public class CityForDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Photo> Photos { get; set; }
}
