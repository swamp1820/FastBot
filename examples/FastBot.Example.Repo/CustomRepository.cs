using FastBot.States;

namespace FastBot.Example.Repo;

internal class CustomRepository : IRepository<User>
{
    readonly Dictionary<long, User> inMemoryTable = new();
    public void Add(User entity)
    {
        inMemoryTable.TryAdd(entity.Id, entity);
    }

    public User Get(long id)
    {
        inMemoryTable.TryGetValue(id, out User u);
        return u == null ? new User(id) : u;
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