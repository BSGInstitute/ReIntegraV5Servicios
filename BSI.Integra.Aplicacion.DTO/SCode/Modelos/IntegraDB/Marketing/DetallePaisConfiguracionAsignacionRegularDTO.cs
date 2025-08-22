using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DetallePaisConfiguracionAsignacionRegularDTO
    {
        public int Id { get; set; }
        public int  IdPaisConfiguracionAsignacionRegular { get; set; }
        public bool DatoCalidad { get; set; }
        public bool DatoCalidadWhatsapp { get; set; }
        public bool DatoCalidadMailing { get; set; }
        public int Distribucion { get; set; }
        public int IdPais { get; set; }
        
    }
    
}
