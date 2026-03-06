using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoSesionEstadoRepository : IGenericRepository<TPEspecificoSesionEstado>
    {
        #region Metodos Base
        TPEspecificoSesionEstado Add(PEspecificoSesionEstado entidad);
        TPEspecificoSesionEstado Update(PEspecificoSesionEstado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPEspecificoSesionEstado> Add(IEnumerable<PEspecificoSesionEstado> listadoEntidad);
        IEnumerable<TPEspecificoSesionEstado> Update(IEnumerable<PEspecificoSesionEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecificoSesionEstadoDTO> Obtener();
        PEspecificoSesionEstado? ObtenerPorId(int id);
    }
}
