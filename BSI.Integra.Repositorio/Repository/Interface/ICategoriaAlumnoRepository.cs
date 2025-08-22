using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICategoriaAlumnoRepository : IGenericRepository<TCategoriaAlumno>
    {
        List<CategoriaAlumnoDTO> ObtenerCategoriaAlumno();
        List<FechaPagoDTO> ObtenerFechaPago(int matriculaCabecera);
    }
}
