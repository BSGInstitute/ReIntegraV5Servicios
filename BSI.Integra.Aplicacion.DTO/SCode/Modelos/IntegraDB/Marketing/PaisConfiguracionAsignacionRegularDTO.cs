using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PaisConfiguracionAsignacionRegularDTO
    {
        public int Id { get; set; }
        public int IdAsignacionRegular { get; set; }
        public int IdPGeneral { get; set; }
        public bool ActivarAsignacionPaisConfiguracion { get; set; }
        public bool DatoCalidadPeru { get; set; }
        public  int DistribucionPeru { get; set; }
        public bool DatoCalidadColombia { get; set; }
        public int DistribucionColombia { get; set; }
        public bool DatoCalidadBolivia { get; set; }
        public  int DistribucionBolivia { get; set; }
        public bool DatoCalidadMexico { get; set; }
        public int DistribucionMexico { get; set; }
        public bool DatoCalidadChile { get; set; }
        public int DistribucionChile { get; set; }
        public bool  DatoCalidadInternacional { get; set; }
        public int  DistribucionInternacional { get; set; }
       

    }
}
