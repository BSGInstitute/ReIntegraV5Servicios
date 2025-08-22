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
    public interface ISolicitudSubCategoriaRepository : IGenericRepository<TSolicitudSubCategorium>
    {
        #region Metodos Base
        TSolicitudSubCategorium Add(SolicitudSubCategoria entidad);
        TSolicitudSubCategorium Update(SolicitudSubCategoria entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSolicitudSubCategorium> Add(IEnumerable<SolicitudSubCategoria> listadoEntidad);
        IEnumerable<TSolicitudSubCategorium> Update(IEnumerable<SolicitudSubCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        SolicitudSubCategoria ObtenerPorId(int id);
        bool InsertarProblema (SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO);
        bool ActualizarProblema(SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO);
        bool EliminarProblema(SolicitudProblemaEntradaDTO solicitudCategoriaEntradaDTO);
        IEnumerable<ComboSubCategoriaDTO> ObtenerCombo();
    }
}
