using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPgeneralProyectoAplicacionAnexoService
    {
        PgeneralProyectoAplicacionAnexoDTO Insertar(PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO, string usuario);
        PgeneralProyectoAplicacionAnexoDTO Actualizar(PgeneralProyectoAplicacionAnexoDTO pgeneralProyectoAplicacionAnexoDTO, string usuario);
        IEnumerable<PgeneralProyectoAplicacionAnexoDTO> ObtenerListaPgeneralProyectoAplicacionAnexo(int id);
        bool Eliminar(int id, string usuario);
        string GuardarArchivo(IFormFile file);
    }
}
