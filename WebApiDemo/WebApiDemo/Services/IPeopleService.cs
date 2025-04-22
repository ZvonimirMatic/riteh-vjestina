using WebApiDemo.Dtos;

namespace WebApiDemo.Services;

public interface IPeopleService
{
    IEnumerable<PersonDto> GetPeople();

    PersonDto? GetPersonById(string id);

    PersonDto CreatePerson(CreatePersonDto createPersonDto);

    PersonDto? UpdatePerson(string id, UpdatePersonDto updatePersonDto);

    void DeletePerson(string id);
}
