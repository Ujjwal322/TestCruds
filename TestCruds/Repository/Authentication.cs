using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestCruds.Models;
using TestCruds.Repository.Interface;

namespace TestCruds.Repository
{
    public class Authentication : IAuthentication
    {
        private readonly AppSetting _appSettings;
        public Authentication(IOptions<AppSetting> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public int AddAdmin(Login login)
        {
            int returnVal = 0;
            List<Login> asps = new List<Login>();
            try
            {
                using (var db = new TestDetailContext())
                {
                    // aspNet.PasswordHash = BCrypt.Net.BCrypt.HashPassword(aspNet.PasswordHash);
                    Login users;
                    foreach (var adm in db.AspNetUsers.ToList())
                    {
                        users = new Login();
                        users.UserName = adm.UserName;
                        asps.Add(users);
                    }
                    AspNetUsers AdminEntity = new AspNetUsers();
                    AdminEntity.UserName = login.UserName;
                    AdminEntity.Email = login.Email;
                    AdminEntity.PasswordHash = login.PasswordHash;
                    AdminEntity.FullName = login.FullName;

                    bool AdminNameexist = asps.Any(x => x.UserName.ToLower() == AdminEntity.UserName.ToLower());

                    if (AdminNameexist == true)
                    {
                        returnVal = -1;
                    }

                    if (AdminNameexist == false)
                    {
                        db.AspNetUsers.Add(AdminEntity);
                        returnVal = db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return returnVal;
        }

        public AspNetUsers GetAdminByID(string name)
        {
            throw new NotImplementedException();
        }

        public List<AspNetUsers> GetAdmins()
        {
            throw new NotImplementedException();
        }

        public int UpdateAdmins(AspNetUsers EditAdm)
        {
            throw new NotImplementedException();
        }

        public Login Authenticate(Login Model)
        {
            List<Login> users = new List<Login>();

            using (var db = new TestDetailContext())
            {
                Login lg;
                foreach (var Adm in db.AspNetUsers.ToList())
                {
                    lg = new Login();
                    lg.UserName = Adm.UserName;
                    lg.FullName = Adm.FullName;
                    lg.Email = Adm.Email;
                    lg.PasswordHash = Adm.PasswordHash;
                    users.Add(lg);
                }
            }
            bool IsvalidPassword = false;
            var user = users.SingleOrDefault(x => x.UserName == Model.UserName && x.PasswordHash == Model.PasswordHash);
            if (user != null)
            {
                // IsvalidPassword = BCrypt.Net.BCrypt.Verify(Model.Password, user.Password);
            }

            if (user == null )
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        new Claim(ClaimTypes.Role, "Admin"),
                    //new Claim(ClaimTypes.Version, "V3.1")
                }),
                Expires = DateTime.UtcNow.AddMinutes(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.PasswordHash = null;

            return user;
        }

        //Login IAuthentication.Authentication(Login Model)
        //{
        //    throw new NotImplementedException();
        //}

        //Public Login Authenticate(Login Model)
        //{
        //    List<Login> users = new List<Login>();

        //    using (var db = new TestDetailContext())
        //    {
        //        Login lg;
        //        foreach (var Adm in db.AspNetUsers.ToList())
        //        {
        //            lg = new Login();
        //            lg.UserName = Adm.UserName;
        //            lg.FullName = Adm.FullName;
        //            lg.Email = Adm.Email;             
        //            lg.PasswordHash = Adm.PasswordHash;
        //            users.Add(lg);
        //        }
        //    }

        //    bool IsvalidPassword = false;
        //    var user = users.SingleOrDefault(x => x.UserName == Model.UserName);
        //    if (user != null)
        //    {
        //       // IsvalidPassword = BCrypt.Net.BCrypt.Verify(Model.Password, user.Password);
        //    }

        //    if (user == null || IsvalidPassword == false)
        //    {
        //        return null;
        //    }

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Key);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName.ToString()),
        //            new Claim(ClaimTypes.Role, "Admin"),
        //            //new Claim(ClaimTypes.Version, "V3.1")
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(300),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    user.Token = tokenHandler.WriteToken(token);

        //    user.PasswordHash = null;

        //    return user;
        //}
    }
}
