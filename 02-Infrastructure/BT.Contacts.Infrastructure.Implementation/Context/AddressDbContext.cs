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

        public virtual DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>()
                .HasOne(c => c.Contact)
                .WithMany(a => a.Addresses)

                .HasForeignKey(fk => fk.ContactId);
        }
    }
}
