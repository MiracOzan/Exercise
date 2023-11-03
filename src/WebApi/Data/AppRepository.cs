using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class AppRepository(DataContext context) : IAppRepository
{
    public void Add<T>(T entity) where T : class
    {
        context.Add(entity);
    }

    public void Update<T>(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete<T>(T entity) where T : class
    {
        context.Remove(entity);
    }

    public bool SaveAll()
    {
        return context.SaveChanges() > 0;
    }

    public List<City> GetCities()
    {
        var cities = context.City.Include(c => c.Photos).ToList();
        return cities;
    }

    public List<Photo> GetPhotosbyCity(int id)
    {
        var Photos = context.Photo.Where(p => p.CityId == id).ToList();
        return Photos;
    }

    public City GetCityById(int cityId)
    {
        var cities = context.City.FirstOrDefault(c => c.Id == cityId);
        return cities;
    }

    public Photo GetPhoto(int id)
    {
        var photos = context.Photo.FirstOrDefault(photo => photo.Id == id);
        return photos;
    }
}