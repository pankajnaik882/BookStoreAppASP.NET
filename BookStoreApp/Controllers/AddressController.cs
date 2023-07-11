using CommonLayer;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private IAddressManager addressManager;

        public AddressController(IAddressManager addressManager) 
        {
            this.addressManager = addressManager;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost]
        [Route("AddAddress")]

        public IActionResult AddAddress(AddressModel addressModel , String AddressType)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);

                AddressModel addressmodelData = this.addressManager.AddAddress(addressModel, UserID, AddressType);

                if (addressmodelData != null)
                {
                    return this.Ok(new {success = true , message = "Address Added Successfully" , result = addressmodelData});
                }
                    return this.BadRequest(new {success = true , message = "Process Failed"});
            }
            catch (Exception)
            {
                return this.NotFound(new {success = false});

            }
        }

        [Authorize(Roles =Role.User)]
        [HttpGet]
        [Route("GetAllAddress")]

        public IActionResult GetAllAddresses()
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                List<AddressModel> addresslistData = (List<AddressModel>)this.addressManager.GetAddress(UserID);

                if (addresslistData != null)
                {
                    return this.Ok(new {success = true , message = "AddressList Fetched Successfully" , result = addresslistData});
                }

                return this.BadRequest(new {success = true , message = "Process Failed "});
            }
            catch (Exception ex)
            {
                return this.NotFound(new {success = false , message = ex.Message});
            }
        }

        [Authorize(Roles =Role.User)]
        [HttpPatch]
        [Route("UpdateAddress")]

        public IActionResult UpdateAddress(AddressModel addressModel)
        {
            try
            {
                int UserID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                AddressModel addressModelData = this.addressManager.UpdateAddress(UserID, addressModel);

                if (addressModelData != null)
                {
                    return this.Ok(new { success = true, message = "Addresses Fatched Successfully", result = addressModelData });
                }
                return this.BadRequest(new { success = true, message = "Process Failed " });

            }
            catch
            {
                return this.NotFound(new {success = false });
            }
        }
    }
}
