using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaceControl.Models
{
    /// <summary>
    /// Clase que me sirve para mantener la carrera , categoria y piloto 
    /// seleccionado por el usuario al momento de realizar la tecnica
    /// </summary>
    public class TecnicaTmp
    {
        /// <summary>
        /// Carrera seleccionada
        /// </summary>
        public Carrera carrera { get; set; }
        /// <summary>
        /// Categoria seleccionada
        /// </summary>
        public Categoria categoria { get; set; }
        /// <summary>
        /// piloto seleccionado
        /// </summary>
        public Piloto piloto { get; set; }

        public Tecnica tecnica { get; set; }


        //Contructor
        public TecnicaTmp()
        {
            this.carrera = null;
            this.categoria = null;
            this.piloto = null;
            this.tecnica = null;
        }


    }
}