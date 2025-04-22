using WebApiDemo.Model;

namespace WebApiDemo.Repositories;

public interface IPeopleRepository
{
    IEnumerable<Person> GetAll();

    Person? GetById(string id);

    void Insert(Person person);

    void Update(Person person);

    void Delete(string id);
}
