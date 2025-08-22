using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IRecuperacionSesionService
    {
        List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera);
        RecuperacionSesion Insert(RecuperacionSesion entidad);
        RecuperacionSesion Update(RecuperacionSesion entidad);
        RecuperacionSesion Delete(int id, string usuario);
    }
}
