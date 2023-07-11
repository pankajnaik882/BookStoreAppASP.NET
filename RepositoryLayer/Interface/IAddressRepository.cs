using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepository
    {
        public AddressModel AddAddress(AddressModel address, int userID, string Type);

        public IEnumerable<AddressModel> GetAddress(int userId);

        public AddressModel UpdateAddress(int userID, AddressModel address);
    }
}
