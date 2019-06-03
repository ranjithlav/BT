using BT.Contacts.Common;
using BT.Contacts.Domain;
using EntityModel = BT.Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BT.Contacts.Infrastructure.Implementation.Context
{
    public class ContactsDbContext : BaseDbContext
    {
        public ContactsDbContext(IOptions<DB> dbOptions): base(dbOptions)
        {
            dbOptions.CheckNull();
        }

        public virtual DbSet<EntityModel.Contact> Contacts { get; set; }
        public virtual DbSet<EntityModel.Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntityModel.Contact>()
                .HasMany(a => a.Addresses)
                .WithOne(c => c.Contact);
        }
    }
}
