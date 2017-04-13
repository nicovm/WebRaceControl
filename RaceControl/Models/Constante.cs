using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}