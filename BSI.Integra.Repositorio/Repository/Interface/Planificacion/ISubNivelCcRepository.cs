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
    public interface ISubNivelCcRepository : IGenericRepository<TSubNivelCc>
    {
        #region Metodos Base
        TSubNivelCc Add(SubNivelCc entidad);
        TSubNivelCc Update(SubNivelCc entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSubNivelCc> Add(IEnumerable<SubNivelCc> listadoEntidad);
        IEnumerable<TSubNivelCc> Update(IEnumerable<SubNivelCc> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<SubNivelCcListaDTO> ObtenerPorFiltro(int skip, int take, string subNivel);
        public SubNivelCc ObtenerPorId(int id);
        Task<IEnumerable<SubNivelCcDTO>> ObtenerSubNivelCCAsync();
        Task<IEnumerable<ComboDTO>> ObtenerSubNivelCCPorAreaCCAsync(int idAreaCC);

    }
}
