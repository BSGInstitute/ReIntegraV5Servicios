using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILlamadaWebphoneAsteriskRepository
    {
        #region Metodos Base
        TLlamadaWebphoneAsterisk Add(LlamadaWebphoneAsterisk entidad);
        TLlamadaWebphoneAsterisk Update(LlamadaWebphoneAsterisk entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TLlamadaWebphoneAsterisk> Add(IEnumerable<LlamadaWebphoneAsterisk> listadoEntidad);
        IEnumerable<TLlamadaWebphoneAsterisk> Update(IEnumerable<LlamadaWebphoneAsterisk> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ValorIntDTO ModificarLlamadaWebphone(int idLlamada, string url, string nombreUsuario, int duracionContesto, int nroBytes);
        IntDTO ObtenerCdrIdRegularizacion();
        List<LlamadaWebphoneAsterisk> ObtenerRegistrosCdrId();
    }
}
