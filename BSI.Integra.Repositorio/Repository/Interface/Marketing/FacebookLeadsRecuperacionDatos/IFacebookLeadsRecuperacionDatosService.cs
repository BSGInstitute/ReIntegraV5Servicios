using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.FacebookLeadsRecuperacionDatos;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.FacebookLeadsRecuperacionDatos
{
    public interface IFacebookLeadsRecuperacionDatosService
    {
        Task<FacebookLeadsRecuperacionDatosResponseDTO> ObtenerPorIdAsync(string idLead);
    }
}
