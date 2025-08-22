using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRecuperacionSesionRepository : IGenericRepository<TRecuperacionSesion>
    {
        List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera);
        RecuperacionSesion ObtenerPorId(int Id);
        TRecuperacionSesion Update(RecuperacionSesion entidad);
        TRecuperacionSesion Add(RecuperacionSesion entidad);
        bool Delete(int id, string usuario);
        bool Exist(int id);
    }
}
