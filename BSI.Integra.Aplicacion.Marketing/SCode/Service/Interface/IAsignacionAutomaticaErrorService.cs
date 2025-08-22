using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAsignacionAutomaticaErrorService
    {
        #region Metodos Base
        AsignacionAutomaticaError Add(AsignacionAutomaticaError entidad);
        AsignacionAutomaticaError Update(AsignacionAutomaticaError entidad);
        bool Delete(int id, string usuario);

        List<AsignacionAutomaticaError> Add(List<AsignacionAutomaticaError> listadoEntidad);
        List<AsignacionAutomaticaError> Update(List<AsignacionAutomaticaError> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<AsignacionAutomaticaErrorDTO> ObtenerError(int idAsignacionAutomatica);
        List<ValorIntDTO> ObtenerErrorAsignacionAutomatica(int Id);

    }
}
