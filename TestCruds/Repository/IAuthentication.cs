using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCruds.Models;

namespace TestCruds.Repository.Interface
{
    public interface IAuthentication
    {
        Login Authenticate(Login Model);
        int AddAdmin(Login login);
        List<AspNetUsers> GetAdmins();
        AspNetUsers GetAdminByID(string name);

        int UpdateAdmins(AspNetUsers EditAdm);

    }
}
