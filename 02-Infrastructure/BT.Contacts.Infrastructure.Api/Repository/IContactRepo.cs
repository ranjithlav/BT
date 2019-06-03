using EntityModel = BT.Contacts.Domain.Entities;
using System.Collections.Generic;

namespace BT.Contacts.Infrastructure.Api.Repository
{
    public interface IContactRepo
    {
        EntityModel.Contact Add(EntityModel.Contact contact);

        IEnumerable<EntityModel.Contact> GetAll();

        IEnumerable<EntityModel.Contact> GetAll(string zipcode);

        EntityModel.Contact Get(int contactId);

        bool Delete(int contactId);
    }
}
