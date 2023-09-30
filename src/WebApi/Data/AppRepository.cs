using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class AppRepository : IAppRepository
{
    private DataContext _context;

    public AppRepository(DataContext context)
    {
        _context = context;
    }

    public void Add<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public void Update<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public bool SaveAll()
    {
        return _context.SaveChanges() > 0;
    }

    public List<City> GetCities()
    {
        var cities = _context.City.Include(c => c.Photos).ToList();
        return cities;
    }

    public List<Photo> GetPhotosbyCity(int id)
    {
        var Photos = _context.Photo.Where(p => p.CityId == id).ToList();
        return Photos;
    }

    public City GetCityById(int cityId)
    {
        var cities = _context.City.FirstOrDefault(c => c.Id == cityId);
        return cities;
    }

    public Photo GetPhoto(int id)
    {
        var photos = _context.Photo.FirstOrDefault(photo => photo.Id == id);
        return photos;
    }
}