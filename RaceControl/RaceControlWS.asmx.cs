using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace RaceControl
{
    /// <summary>
    /// Descripción breve de RaceControlWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class RaceControlWS : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }


        [WebMethod]
        public string loginWS(String email, String password)
        {
            
           

            //WsResp wsResp = null;
            //try
            //{
            //    context = new DB_A0C5ED_racecontrolEntities();

            //    var consulta = (from tabla in context.Usuario
            //                    where tabla.email == email
            //                    select tabla).FirstOrDefault();  //BD.usuario.Where(u => u.email == email).FirstOrDefault();

            //    if (consulta != null)
            //    {
            //        if (password == consulta.password) wsResp = new WsResp(true, "Autentican exitosa");
            //        else wsResp = new WsResp(false, "Contraseña incorrecta");
            //    }
            //    else wsResp = new WsResp(false, "El mail no  se encuentra registrado");


            //}
            //catch (Exception ex)
            //{
            //    wsResp = new WsResp(false, "[Error] " + ex.Message);
            //}
            //return JsonConvert.SerializeObject(wsResp);
            return "";
        }

    }
}
