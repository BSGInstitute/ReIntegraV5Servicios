using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoProgramaEspecificoService
    {
        #region Metodos Base
        EstadoPespecifico Add(EstadoPespecifico entidad);
        EstadoPespecifico Update(EstadoPespecifico entidad);
        bool Delete(int id, string usuario);

        List<EstadoPespecifico> Add(List<EstadoPespecifico> listadoEntidad);
        List<EstadoPespecifico> Update(List<EstadoPespecifico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<FiltroDTO> ObtenerEstadoPespecificoParaCombo();
        public IEnumerable<ComboDTO> ObtenerComboEstado();
    }
}
