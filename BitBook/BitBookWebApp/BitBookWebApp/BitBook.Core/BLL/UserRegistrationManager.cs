using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookWebApp.BitBook.Core.DAL;
using BitBookWebApp.Models;

namespace BitBookWebApp.BitBook.Core.BLL
{
    public class UserRegistrationManager
    {
        UserRegistrationGateway aUserRegistrationGateway=new UserRegistrationGateway();

        public bool SaveUserRegistraion(User aUser)
        {
            return aUserRegistrationGateway.SaveUserRegistraion(aUser);
        }

        public bool IsEmailAleadyExist(string email)
        {
            return aUserRegistrationGateway.IsEmailAleadyExist(email);
        }
    }
}