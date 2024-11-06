namespace Ultimate_Front.DAL
{
    public class DbSet<TEntity> where TEntity : class
    {
        private readonly List<TEntity> _dataStore;

        public DbSet(IEnumerable<TEntity> initialData)
        {
            _dataStore = new List<TEntity>(initialData);
        }

        public async Task<TEntity> FindAsync(object key)
        {
            return await Task.Run(() =>
            {
                return _dataStore.FirstOrDefault(e => e.GetType().GetProperty("Id").GetValue(e).Equals(key));
            });
        }
    }

}
