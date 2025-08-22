using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGmailCorreoRepository : IGenericRepository<TGmailCorreo>
    {
        #region Metodos Base
        TGmailCorreo Add(GmailCorreo entidad);
        TGmailCorreo AddSync(GmailCorreo entidad);
        TGmailCorreo Update(GmailCorreo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGmailCorreo> Add(IEnumerable<GmailCorreo> listadoEntidad);
        IEnumerable<TGmailCorreo> Update(IEnumerable<GmailCorreo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<GmailCorreoDTO> ObtenerGmailCorreo();
        IEnumerable<GmailCorreoComboDTO> ObtenerCombo();
        IEnumerable<CorreoEnviadoPorPersonalDTO> ObtenerCorreosEnviadosPorFiltroBandeja(FiltroBandejaCorreoParaRepositorioDTO filtroBandejaCorreo);

        string SubirArchivo(byte[] archivo, string tipo, string nombreArchivo);
        Task<string> SubirArchivoAsync(byte[] archivo, string tipo, string nombreArchivo);
        GmailCorreo ObtenerCorreoPorId(int idCorreo);
        List<CorreoAlumnoVentasDTO> ObtenerCorreosAlumnosSoloVentas(string emailAlumno);
        List<StringDTO> listaDestinatariosExitosoFacturama();
        List<StringDTO> listaDestinatariosErroneoFacturama();
    }
}