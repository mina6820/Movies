
namespace TestingMVC.Repo
{
    public interface IRepository<T> where T : class
    {
        /// <summary>//////////////////
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll();
        public T GetById(string id);
        public void Insert(T obj);
        public void Update(T obj);
        public void Delete(string id);
        public void Save();
    }
}