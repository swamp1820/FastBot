using FastBot.States;

namespace FastBot.Example.Repo;

public class CustomRepository : IRepository<User>
{
    Dictionary<long, User> inMemoryTable = new Dictionary<long, User>();
    public void Add(User entity)
    {
        inMemoryTable.TryAdd(entity.Id, entity);
    }

    public User Get(long id)
    {
        inMemoryTable.TryGetValue(id, out User u);
        return u == null ? new User(id): u;
    }

    public IEnumerable<User> GetAll()
    {
        return inMemoryTable.Select(kv => kv.Value).ToList<User>();
    }

    public void Update(User entity)
    {
        inMemoryTable[entity.Id] = entity;
    }
}