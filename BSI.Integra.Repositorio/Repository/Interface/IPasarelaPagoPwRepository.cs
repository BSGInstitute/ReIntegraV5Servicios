using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPasarelaPagoPwRepository : IGenericRepository<TPasarelaPagoPw>
    {
        #region Metodos Base
        TPasarelaPagoPw Add(PasarelaPagoPw entidad);
        TPasarelaPagoPw Update(PasarelaPagoPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPasarelaPagoPw> Add(IEnumerable<PasarelaPagoPw> listadoEntidad);
        IEnumerable<TPasarelaPagoPw> Update(IEnumerable<PasarelaPagoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RegistroPasarelaPagoPWDTO> ObtenerPasarelaPagoPw();
        IEnumerable<PasarelaPagoPwComboDTO> ObtenerCombo();
        IEnumerable<PasarelaPagoPwAgendaDTO> ObtenerPasarelaPagoPorIdAlumno(int idAlumno);
        int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula);
        int BuscarIdMatriculaCabeceraPrueba(string codigoMatricula);
        PasarelaPagoPw ObtenerPorId(int id);

    }
}