using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/Cities")]
public class CitiesController : Controller
{
    private readonly IAppRepository _appRepository;
    private readonly IMapper _mapper;

    public CitiesController(IAppRepository appRepository, IMapper mapper)
    {
        _appRepository = appRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("GetCities")]
    public ActionResult GetCities()
    {
        var cities = _appRepository.GetCities();
        var citiesReturn = _mapper.Map<List<CityForListDto>>(cities);
        return Ok(cities);
    }

    [HttpPost]
    [Route("add")]
    public ActionResult Add ([FromBody]City city)
    {
        _appRepository.Add(city);
        _appRepository.SaveAll();
        return Ok(city);
    }

    [HttpGet]
    public ActionResult GetCitiesById(int id)
    {
        var cities = _appRepository.GetCityById(id);
        var citytoReturn = _mapper.Map<CityForDetailDto>(cities);
        return Ok(citytoReturn);
    }

    [HttpGet]
    [Route("Photos")]
    public ActionResult GetPhotosById(int id)
    {
        var photos = _appRepository.GetPhotosbyCity(id);
        return Ok(photos);
    }

}