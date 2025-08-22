using BSI.Integra.Aplicacion.DTO;
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
    public interface IAsignacionAutomaticaErrorRepository : IGenericRepository<TAsignacionAutomaticaError>
    {
        #region Metodos Base
        TAsignacionAutomaticaError Add(AsignacionAutomaticaError entidad);
        TAsignacionAutomaticaError Update(AsignacionAutomaticaError entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsignacionAutomaticaError> Add(IEnumerable<AsignacionAutomaticaError> listadoEntidad);
        IEnumerable<TAsignacionAutomaticaError> Update(IEnumerable<AsignacionAutomaticaError> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<AsignacionAutomaticaErrorDTO> ObtenerError(int idAsignacionAutomatica);
        public List<ValorIntDTO> ObtenerErrorAsignacionAutomatica(int Id);


    }
}
