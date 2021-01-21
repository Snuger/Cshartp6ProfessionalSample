namespace BuilderDesigin
{
    public class DbContextOptionBuilder
    {
        private DbContextOptions _options;

        public DbContextOptionBuilder(string connectionStringOrName)
        {
            _options = new DbContextOptions(connectionStringOrName);
        }

        public DbContextOptionBuilder(DbContextOptions options)
        {
            _options = options;
        }

        public virtual DbContextOptions Options => _options;


        public virtual DbContextOptionBuilder UserMemoryCache() {
            return this;
        }

    }
}