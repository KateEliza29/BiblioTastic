namespace BiblioTastic.Interfaces 
{
    public interface ICRUD<T>
    {
        public bool Create(T item);
        public List<T> Read();
        public List<T> Select(int id);
        public bool Update(T item);
        public bool Delete(int id);
    }
}