using WebApiDemo.Dtos;
using WebApiDemo.Model;
using WebApiDemo.Repositories;

namespace WebApiDemo.Services.Implementations;

public class PeopleService : IPeopleService
{
    private readonly IPeopleRepository _peopleRepository;

    public PeopleService(IPeopleRepository peopleRepository)
    {
        _peopleRepository = peopleRepository;
    }

    public IEnumerable<PersonDto> GetPeople()
    {
        var people = _peopleRepository.GetAll();

        return people
            .Select(x => PersonDto.FromModel(x))
            .ToList();
    }

    public PersonDto? GetPersonById(string id)
    {
        var person = _peopleRepository.GetById(id);

        return person is null ? null : PersonDto.FromModel(person);
    }

    public PersonDto CreatePerson(CreatePersonDto createPersonDto)
    {
        var person = new Person
        {
            FirstName = createPersonDto.FirstName,
            LastName = createPersonDto.LastName,
            Birthday = createPersonDto.Birthday,
        };

        _peopleRepository.Insert(person);

        return PersonDto.FromModel(person);
    }

    public PersonDto? UpdatePerson(string id, UpdatePersonDto updatePersonDto)
    {
        var existingPerson = _peopleRepository.GetById(id);
        if (existingPerson is null)
        {
            return null;
        }

        existingPerson.FirstName = updatePersonDto.FirstName;
        existingPerson.LastName = updatePersonDto.LastName;
        existingPerson.Birthday = updatePersonDto.Birthday;

        _peopleRepository.Update(existingPerson);

        return PersonDto.FromModel(existingPerson);
    }

    public void DeletePerson(string id)
    {
        _peopleRepository.Delete(id);
    }
}
