using BT.Contacts.Common;
using BT.Contacts.Domain;
using BT.Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BT.Contacts.Infrastructure.Implementation.Context
{
    public class AddressDbContext : BaseDbContext
    {
        public AddressDbContext(IOptions<DB> dbOptions) : base(dbOptions)
        {
            dbOptions.CheckNull();
        }

        public virtual DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().ToTable(BT_Tables.Addresses);
        }
    }
}
