using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGmailClienteRepository : IGenericRepository<TGmailCliente>
    {
        #region Metodos Base
        TGmailCliente Add(GmailCliente entidad);
        TGmailCliente Update(GmailCliente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGmailCliente> Add(IEnumerable<GmailCliente> listadoEntidad);
        IEnumerable<TGmailCliente> Update(IEnumerable<GmailCliente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<GmailClienteDTO> ObtenerGmailCliente();
        IEnumerable<GmailClienteComboDTO> ObtenerCombo();
        CorreoClienteCredencialDTO? ObtenerClienteCredencial(int idAsesor);
        public GmailCliente ObtenerPorIdAsesor(int idAsesor);

    }
}