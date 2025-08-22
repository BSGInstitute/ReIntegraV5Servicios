using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IMaterialPespecificoDetalleService
    {
        bool SubirMaterialArchivo(SubirMaterialPEspecificoDetalleDTO dto, string usuario);
        MaterialPEspecificoDetalleFurDTO ObtenerDetalleFur(int idMaterialPEspecificoDetalle);


    }
}
