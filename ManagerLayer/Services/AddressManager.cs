using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class AddressManager : IAddressManager
    {
        private IAddressRepository addressRepository;

        public AddressManager(IAddressRepository addressRepository) 
        {
            this.addressRepository = addressRepository;
        }

        public AddressModel AddAddress(AddressModel address, int userID, string Type)
        {
            return  this.addressRepository.AddAddress(address, userID, Type);
        }

        public IEnumerable<AddressModel> GetAddress(int userId)
        {
            return this.addressRepository.GetAddress(userId);
        }

        public AddressModel UpdateAddress(int userID, AddressModel address)
        {
            return this.addressRepository.UpdateAddress(userID, address);
        }
    }
}
