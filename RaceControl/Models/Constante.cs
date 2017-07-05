using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace RaceControl.Models
{
    public class Constante
    {
     
        const string quote = "\"";

        /// <summary>
        /// Permite crear un mensaje de alerta para el usuario
        /// </summary>
        /// <param name="msj"></param>
        /// <returns></returns>
        public static string alertDanger(string msj)
        {
            string alert = "<div class=" + quote + "alert alert-danger" + quote + "><a class=" + quote +
                "close" + quote + " data-dismiss=" + quote + "alert" + quote + ">×</a><span> " + msj + " </span></div>";

            return alert;
        }

        /// <summary>
        /// Permite crear un mensaje de informacion
        /// para el usuario
        /// </summary>
        /// <param name="msj"></param>
        /// <returns></returns>
        public static string alertInfo(string msj)
        {
            string alert = "<div class=" + quote + "alert alert-info" + quote + "><a class=" + quote +
                "close" + quote + " data-dismiss=" + quote + "alert" + quote + ">×</a><span> " + msj + " </span></div>";

            return alert;
        }

        /// <summary>
        /// Permite crear un mensaje de informacion
        /// para el usuario
        /// </summary>
        /// <param name="msj"></param>
        /// <returns></returns>
        public static string alertWarning(string msj)
        {
            string alert = "<div class=" + quote + "alert alert-warning" + quote + "><a class=" + quote +
                "close" + quote + " data-dismiss=" + quote + "alert" + quote + ">×</a><span> " + msj + " </span></div>";

            return alert;
        }

        public static bool IsNumeric(string s)
        {
            int output;
            return int.TryParse(s, out output);
        }

        public class defaultTabRevision
        {
            public static int TAB_OBSERVACION = 1;
            public static int TAB_PRECINTO = 2;
        }



        public class RolesRaceControl
        {
            public static int ADMINISTRADOR = 1;
            public static int CLIENTE = 2;
            public static int OPERADOR = 3;

            public static string STR_ADMINISTRADOR = "Administrador";
            public static string STR_CLIENTE = "Cliente";
            public static string STR_OPERADOR = "Operador";

        }

      



        }
}