using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/Cities")]
public class CitiesController(IAppRepository appRepository, IMapper mapper) : Controller
{
    [HttpGet]
    [Route("GetCities")]
    public ActionResult GetCities()
    {
        var cities = appRepository.GetCities();
        var citiesReturn = mapper.Map<List<CityForListDto>>(cities);
        return Ok(cities);
    }

    [HttpPost]
    [Route("add")]
    public ActionResult Add ([FromBody]City city)
    {
        appRepository.Add(city);
        appRepository.SaveAll();
        return Ok(city);
    }

    [HttpGet]
    public ActionResult GetCitiesById(int id)
    {
        var cities = appRepository.GetCityById(id);
        var citytoReturn = mapper.Map<CityForDetailDto>(cities);
        return Ok(citytoReturn);
    }

    [HttpGet]
    [Route("Photos")]
    public ActionResult GetPhotosById(int id)
    {
        var photos = appRepository.GetPhotosbyCity(id);
        return Ok(photos);
    }

}