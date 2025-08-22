using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IAreaCcRepository : IGenericRepository<TAreaCc>
    {
        #region Metodos Base
        TAreaCc Add(AreaCC entidad);
        TAreaCc Update(AreaCC entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAreaCc> Add(IEnumerable<AreaCC> listadoEntidad);
        IEnumerable<TAreaCc> Update(IEnumerable<AreaCC> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        AreaCC ObtenerPorId(int id);
        IEnumerable<AreaCC> Obtener();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        Task<IEnumerable<AreaCCDTO>> ObtenerAreaCCAsync();
    }
}
