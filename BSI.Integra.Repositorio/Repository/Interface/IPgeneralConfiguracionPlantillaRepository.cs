using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPGeneralConfiguracionPlantillaRepository : IGenericRepository<TPgeneralConfiguracionPlantilla>
    {
        #region Metodos Base
        TPgeneralConfiguracionPlantilla Add(PgeneralConfiguracionPlantilla entidad);
        TPgeneralConfiguracionPlantilla Update(PgeneralConfiguracionPlantilla entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralConfiguracionPlantilla> Add(IEnumerable<PgeneralConfiguracionPlantilla> listadoEntidad);
        IEnumerable<TPgeneralConfiguracionPlantilla> Update(IEnumerable<PgeneralConfiguracionPlantilla> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        DatosGenerarCertificadoDTO ObtenerDatosParaConstanciasPorMatricula(int idMatriculaCabecera);
        DatosAlumnosOperacionesDTO ObtenerDatoAlumno(int IdMatriculaCabecera);
        List<PgeneralConfiguracionPlantillaDTO> ObtenerPGeneralConfiguracionPlantillaPorIdPGeneral(int idPGeneral);
        IEnumerable<PgeneralConfiguracionPlantilla> ObtenerPorIdPGeneralYIdPlantillaBase(int idPGeneral, int idPlantillaBase);
        PgeneralConfiguracionPlantilla ObtenerPorId(int id);
    }
}
