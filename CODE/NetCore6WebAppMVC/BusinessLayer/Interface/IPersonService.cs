using BusinessLayer.DTO;

namespace BusinessLayer.Interface;

public interface IPersonService
{
    void CreatePersonTable();
    IEnumerable<PersonDTO> GetAllPersons();
    IEnumerable<PersonDTO> GetPersonById(int id);
    IEnumerable<PersonDTO> GetPersonByFirstName(string fname);
    int AddPerson(PersonDTO person);
    void UpdatePerson(PersonDTO person);
}
