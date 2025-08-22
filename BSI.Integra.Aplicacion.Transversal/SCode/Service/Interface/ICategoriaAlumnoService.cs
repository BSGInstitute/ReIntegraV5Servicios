using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICategoriaAlumnoService
    {
        List<CategoriaAlumnoDTO> ObtenerCategoriaAlumno();
        List<FechaPagoDTO> ObtenerFechaPago(int matriculaCabecera);
    }
}
