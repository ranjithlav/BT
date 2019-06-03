using BT.Contacts.Common;
using BT.Contacts.Domain;
using BT.Contacts.Domain.Entities;
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

        public virtual DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable(BT_Tables.Contacts);
        }
    }
}
