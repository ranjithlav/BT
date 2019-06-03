using BT.Contacts.Common;
using BT.Contacts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BT.Contacts.Infrastructure.Implementation.Context
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly IOptions<DB> _dbOptions;
        private readonly string _connectionString;

        public BaseDbContext(IOptions<DB> dbOptions)
        {
            dbOptions.CheckNull();

            _dbOptions = dbOptions;

            _connectionString = _dbOptions.Value.BTConnectionString;

            _connectionString.CheckNullOrEmpty(nameof(_connectionString));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
    }
}
