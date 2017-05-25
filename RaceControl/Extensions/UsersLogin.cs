using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace RaceControl.Extensions
{
    public class UsersLogin
    {

        public static string NombreUsuario()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserName();

        }
    }
}