using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceControl.Models
{
    public class Constante
    {

        const string quote = "\"";

        public static string alertDanger(string msj)
        {
            string alert = "<div class=" + quote + "alert alert-danger" + quote + "><a class=" + quote +
                "close" + quote + " data-dismiss=" + quote + "alert" + quote + ">×</a><span> " + msj + " </span></div>";

            return alert;
        }
    }
}