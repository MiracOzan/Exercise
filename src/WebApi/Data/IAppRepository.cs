using WebApi.Models;

namespace WebApi.Data;

public interface IAppRepository
{
    void Add<T> (T entity) where T: class;
    void Update<T> (T entity);
    void Delete<T> (T entity) where T : class;
    bool SaveAll();
    List<City> GetCities();
    List<Photo> GetPhotosbyCity(int id);
    City GetCityById(int cityId);
    Photo GetPhoto(int id);
}    

