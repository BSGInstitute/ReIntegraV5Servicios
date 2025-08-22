using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IPasarelaPagoPwService
    {
        #region Metodos Base
        PasarelaPagoPw Add(PasarelaPagoPwEnvioDTO entidadDTO, string Usuario);
        PasarelaPagoPw Update(PasarelaPagoPwEnvioDTO entidadDTO, string Usuario);
        bool Delete(int id, string usuario);

        List<PasarelaPagoPw> Add(List<PasarelaPagoPw> listadoEntidad);
        List<PasarelaPagoPw> Update(List<PasarelaPagoPw> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<RegistroPasarelaPagoPWDTO> ObtenerPasarelaPagoPw();
        IEnumerable<PasarelaPagoPwComboDTO> ObtenerCombo();
        IEnumerable<PasarelaPagoPwAgendaDTO> ObtenerPasarelaPagoPorIdAlumno(int idAlumno);
        bool RegistroMedioPagoMatriculaCronograma(RegistroMedioPagoMatriculaCronogramaDTO modelo);
        int BuscarIdMatriculaCabeceraPorCodigoMatricula(string codigoMatricula);
    }
}
