using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudSubCategoriaService
    {
        #region Metodos Base
        SolicitudSubCategoria Add(SolicitudSubCategoria entidad);
        SolicitudSubCategoria Update(SolicitudSubCategoria entidad);
        bool Delete(int id, string usuario);
        List<SolicitudSubCategoria> Add(List<SolicitudSubCategoria> listadoEntidad);
        List<SolicitudSubCategoria> Update(List<SolicitudSubCategoria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        SolicitudSubCategoria ObtenerPorId(int id);
        IEnumerable<ComboSubCategoriaDTO> ObtenerCombo();
    }
}
