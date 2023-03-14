using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FastBot.States
{
    internal class BaseRepository<T> : IRepository<T>
    {
        protected string DBNAME = $@"Filename=.\{Assembly.GetEntryAssembly().GetName().Name}.db; Connection=shared";
        private readonly string tableName;

        protected BaseRepository(string tableName)
        {
            this.tableName = tableName;
        }

        public BaseRepository() : this("UserStates")
        {

        }

        public void Add(T entity)
        {
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                col.Insert(entity);
            }
        }

        public void Update(T entity)
        {
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                col.Update(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            IEnumerable<T> results;
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return results = col.FindAll().ToList();
            }
        }

        public T Get(long id)
        {
            using (var db = new LiteDatabase(DBNAME))
            {
                var col = db.GetCollection<T>(tableName);
                return col.FindById(id);
            }
        }
    }
}