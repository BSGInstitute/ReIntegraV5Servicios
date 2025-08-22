using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IOportunidadIsVerificadaService
    {
        #region Metodos Base
        OportunidadIsVerificada Add(OportunidadIsVerificada entidad);
        OportunidadIsVerificada Update(OportunidadIsVerificada entidad);
        bool Delete(int id, string usuario);

        List<OportunidadIsVerificada> Add(List<OportunidadIsVerificada> listadoEntidad);
        List<OportunidadIsVerificada> Update(List<OportunidadIsVerificada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        OportunidadIsVerificada ObtenerPorIdOportunidadOIdMatriculaCabecera(int idOportunidad, int idOportunidadMatriculaCabecera);
        public List<OportunidadesVerificadasDTO> ObtenerOportunidadesVerificadas();
        public object ObtenerOportunidadesISM();
        public object ObtenerCombosVerificacionOportunidadISM();
        OportunidadIsVerificadaDTO InsertarOportunidadVerificada(OportunidadCodigoMatriculaDTO dto);
    }
}
