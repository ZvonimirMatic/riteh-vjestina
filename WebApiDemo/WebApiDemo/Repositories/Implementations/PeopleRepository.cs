using System;
using WebApiDemo.Model;

namespace WebApiDemo.Repositories.Implementations;

public class PeopleRepository : IPeopleRepository
{
    private readonly List<Person> _people = new List<Person>();

    public IEnumerable<Person> GetAll()
    {
        return _people;
    }

    public Person? GetById(string id)
    {
        return _people.FirstOrDefault(x => x.Id == id);
    }

    public void Insert(Person person)
    {
        _people.Add(person);
    }

    public void Update(Person person)
    {
        var existingPerson = GetById(person.Id);
        if (existingPerson is null)
        {
            return;
        }

        existingPerson.FirstName = person.FirstName;
        existingPerson.LastName = person.LastName;
        existingPerson.Birthday = person.Birthday;
    }

    public void Delete(string id)
    {
        var existingPerson = GetById(id);
        if (existingPerson is null)
        {
            return;
        }

        _people.Remove(existingPerson);
    }
}
