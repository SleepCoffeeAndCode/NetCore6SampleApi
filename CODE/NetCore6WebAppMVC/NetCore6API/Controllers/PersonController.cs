using BusinessLayer.DTO;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using NetCore6API.Models;

namespace NetCore6API.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _service;

    public PersonController(IPersonService service)
    {
        _service = service;
    }

    [HttpPost("CreateTable")]
    public IActionResult CreateTable()
    {
        try
        {
            _service.CreatePersonTable();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("AddPerson")]
    public IActionResult AddPerson([FromBody] PersonModel person)
    {
        try
        {
            PersonDTO? _person = new PersonDTO()
            {
                FIRST_NAME = person.FIRST_NAME,
                LAST_NAME = person.LAST_NAME,
                EMAIL = person.EMAIL,
                GENDER = person.GENDER,
                CREATED_DATE = person.CREATED_DATE.HasValue ? person.CREATED_DATE : DateTime.Now,
            };
            _service.AddPerson(_person);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetAllPersons")]
    public IActionResult GetAllPersons()
    {

        try
        {
            List<PersonModel>? listToReturn = new List<PersonModel>();
            IEnumerable<PersonDTO>? list = _service.GetAllPersons();
            foreach (var item in list)
            {
                PersonModel person = new PersonModel()
                {
                    FIRST_NAME = item.FIRST_NAME,
                    LAST_NAME = item.LAST_NAME,
                    EMAIL = item.EMAIL,
                    GENDER = item.GENDER,
                    CREATED_DATE = item.CREATED_DATE,
                    ID = item.ID
                };
                listToReturn.Add(person);
            }
            return Ok(listToReturn);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("GetPersonById")]
    public IActionResult GetPersonById(int id)
    {

        try
        {
            
            PersonDTO? personDtoList = _service.GetPersonById(id).FirstOrDefault();
            PersonModel personModel = new PersonModel();
            if(personDtoList != null)
            {
                personModel.GENDER = personDtoList.GENDER;
                personModel.FIRST_NAME= personDtoList.FIRST_NAME;
                personModel.LAST_NAME= personDtoList.LAST_NAME;
                personModel.EMAIL = personDtoList.EMAIL;
                personModel.ID = personDtoList.ID;
                personModel.CREATED_DATE= personDtoList.CREATED_DATE;
            }
            return Ok(personModel);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("UpdatePerson")]
    public IActionResult UpdatePerson([FromBody] PersonModel person)
    {

        try
        {
            PersonDTO personDTO = new PersonDTO()
            {
                ID = person.ID,
                CREATED_DATE = person.CREATED_DATE,
                FIRST_NAME = person.FIRST_NAME,
                LAST_NAME = person.LAST_NAME,
                EMAIL = person.EMAIL,
                GENDER = person.GENDER,
            };
            _service.UpdatePerson(personDTO);
            return Ok();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
