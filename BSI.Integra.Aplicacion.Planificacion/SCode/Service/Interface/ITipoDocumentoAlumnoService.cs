using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ITipoDocumentoAlumnoService
    {
        IEnumerable<TipoDocumentoAlumnoDTO> Obtener();
        IEnumerable<PlantilaCertificadoConstanciaDTO> ObtenerPlantillaCertificadoConstancia();
        TipoDocumentoAlumnoDetalleDTO ObtenerIdsDetalleTipoDocumento(int idTipoDocumentoAlumno);
        TipoDocumentoAlumnoCombosDTO ObtenerCombosTipoDocumento();
        TipoDocumentoAlumnoListaDetalleConfiguracionDTO ObtenerDetalleConfiguracionCerficicado(int idTipoDocumentoAlumno);
        TipoDocumentoAlumnoDTO InsertarTipoDocumentoAlumno(TipoDocumentoAlumnoEntidadDTO entidad, string usuario);
        TipoDocumentoAlumnoDTO ActualizarTipoDocumentoAlumno(TipoDocumentoAlumnoEntidadDTO dto, string usuario);
        bool EliminarTipoDocumentoAlumno(int id, string usuario);
    }
}
