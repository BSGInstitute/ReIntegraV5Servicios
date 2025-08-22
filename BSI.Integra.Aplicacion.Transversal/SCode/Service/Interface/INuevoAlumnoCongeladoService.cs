using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface INuevoAlumnoCongeladoService
    {
        #region Metodos Base
        NuevoAlumnoCongelado Add(NuevoAlumnoCongeladoDTO data);
        NuevoAlumnoCongelado Update(NuevoAlumnoCongeladoDTO data);
        bool Delete(int id, string usuario);

        List<NuevoAlumnoCongelado> Add(List<NuevoAlumnoCongelado> listadoEntidad);
        List<NuevoAlumnoCongelado> Update(List<NuevoAlumnoCongelado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<NuevoAlumnoCongeladoDTO> ObtenerListaNuevoAlumnoCongelado();
        IEnumerable<NuevoAlumnoCongeladoExcelDTO> MostrarDatosExcel(IFormFile ArchivoExcel);
        bool InsertarExcelAlumnoCongelado(FiltroNuevoAlumnoCongeladoExcelDTO json);


    }
}
