using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadMasivaService
    {
        #region Metodos Base

        #endregion
        Task<string> SubirArchivoAsync(IFormFile archivo);
        public string InsertarArchivo(string nombreArchivo, string usuario);
        public List<InformacionBaseOportunidadMasiva> ProcesarInformacionOportunidades(List<InformacionBaseOportunidadMasiva> datos, string usuario);
        public object ProcesarOportunidadMasiva(IFormFile file, string usuario);
        public bool CrearOportunidadCrearAlumnoVentas(RegistroOportunidadAlumnoDTO formulario);
        public bool ActualizarAlumnoCrearOportunidadVentas(RegistroOportunidadAlumnoDTO formulario);
        public void InsertarHistorialOportunidad(int idOportunidad, string usuario);
        public List<OportunidadMasivaDTO> ObtenerOportunidadesMasivas();


        }
}
