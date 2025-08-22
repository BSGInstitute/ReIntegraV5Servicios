using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEstadoProgramaEspecificoRepository : IGenericRepository<TEstadoPespecifico>
    {
        #region Metodos Base
        TEstadoPespecifico Add(EstadoPespecifico entidad);
        TEstadoPespecifico Update(EstadoPespecifico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoPespecifico> Add(IEnumerable<EstadoPespecifico> listadoEntidad);
        IEnumerable<TEstadoPespecifico> Update(IEnumerable<EstadoPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public IEnumerable<FiltroDTO> ObtenerEstadoPespecificoParaCombo();
        public IEnumerable<ComboDTO> ObtenerComboEstado();
        
        }
}
