using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IGmailClienteService
    {
        #region Metodos Base
        GmailCliente Add(GmailCliente entidad);
        GmailCliente Update(GmailCliente entidad);
        bool Delete(int id, string usuario);

        List<GmailCliente> Add(List<GmailCliente> listadoEntidad);
        List<GmailCliente> Update(List<GmailCliente> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GmailClienteDTO> ObtenerGmailCliente();
        IEnumerable<GmailClienteComboDTO> ObtenerCombo();
        CorreoClienteCredencialDTO ObtenerClienteCredencial(int idAsesor);
        CorreoBodyDTO ObtenerCorreoBody(int IdAsesor, int IdCorreo, string Folder);
    }
}
