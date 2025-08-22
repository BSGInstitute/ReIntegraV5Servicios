using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Operacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Operacion
{
    public interface ICentroCostoCertificadoRepository : IGenericRepository<TCentroCostoCertificado>
    {
        #region Metodos Base
        TCentroCostoCertificado Add(CentroCostoCertificado entidad);
        TCentroCostoCertificado Update(CentroCostoCertificado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCentroCostoCertificado> Add(IEnumerable<CentroCostoCertificado> listadoEntidad);
        IEnumerable<TCentroCostoCertificado> Update(IEnumerable<CentroCostoCertificado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        CentroCostoCertificado ObtenerPorId(int id);
        CentroCostoCertificado ObtenerPorCentroCosto(int idCentroCosto);
        

        #endregion

    }
}
