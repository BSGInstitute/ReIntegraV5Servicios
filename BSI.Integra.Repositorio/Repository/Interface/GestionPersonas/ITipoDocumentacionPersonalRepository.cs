using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoDocumentacionPersonalRepository : IGenericRepository<TTipoDocumentacionPersonal>
    {
        #region Metodos Base
        TTipoDocumentacionPersonal Add(TipoDocumentacionPersonal entidad);
        TTipoDocumentacionPersonal Update(TipoDocumentacionPersonal entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTipoDocumentacionPersonal> Add(IEnumerable<TipoDocumentacionPersonal> listadoEntidad);
        IEnumerable<TTipoDocumentacionPersonal> Update(IEnumerable<TipoDocumentacionPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TipoDocumentacionPersonal? ObtenerPorId(int id);

        List<FiltroCombosDTO> ObtenerIdYNombreParaCombo();
        
    }
}
