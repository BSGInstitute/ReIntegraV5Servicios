using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConfiguracionBeneficioProgramaGeneralRepository : IGenericRepository<TConfiguracionBeneficioProgramaGeneral>
    {
        #region Metodos Base
        TConfiguracionBeneficioProgramaGeneral Add(ConfiguracionBeneficioProgramaGeneral entidad);
        TConfiguracionBeneficioProgramaGeneral Update(ConfiguracionBeneficioProgramaGeneral entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConfiguracionBeneficioProgramaGeneral> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneral> listadoEntidad);
        IEnumerable<TConfiguracionBeneficioProgramaGeneral> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ConfiguracionBeneficioProgramaGeneralAlternoDTO> ObtenerPorIdPgeneralIdBeneficio(int idPGeneral, int idBeneficio);
        IEnumerable<ConfiguracionBeneficioProgramaGeneralComboDTO> ObtenerCombo();
        List<PgeneralConfiguracionBeneficioDTO> ObtenerPGeneralConfiguracionBeneficios(int idPgeneral);
        Task<List<BeneficiosConfiguradosProgramaGeneralDTO>> ObtenerBeneficiosConfiguradosProgramaGeneralAsync(int idPgeneral, int idCodigoPais);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2Internacional(int idPGeneral);
        Task<List<BeneficioDTO>> ObtenerBeneficiosPGeneralTipo1V2InternacionalAsync(int idPGeneral);
        List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2Internacionaljson(int idPGeneral);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1V2(int idPGeneral, int codigoPais);
        Task<List<BeneficioDTO>> ObtenerBeneficiosPGeneralTipo1V2Async(int idPGeneral, int codigoPais);
        List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1V2json(int idPGeneral, int codigoPais);
        List<BeneficioDTO> ObtenerBeneficiosPGeneralTipo1(int idPGeneral, int codigoPais);
        Task<List<BeneficioDTO>> ObtenerBeneficiosPGeneralTipo1Async(int idPGeneral, int codigoPais);
        List<BeneficioDTOjson> ObtenerBeneficiosPGeneralTipo1json(int idPGeneral, int codigoPais);
        BeneficioDTO ObtenerBeneficiosPGeneralTipo2(int idPGeneral);
        Task<BeneficioDTO> ObtenerBeneficiosPGeneralTipo2Async(int idPGeneral);
        BeneficioDTOjson ObtenerBeneficiosPGeneralTipo2json(int idPGeneral);
        BeneficioDetalleRequisitoDTO ObtenerBeneficioDetalleRequisitoPorPGeneralYBeneficio(int idBeneficio, int idPGeneral);
        ConfiguracionBeneficioProgramaGeneral ObtenerPorId(int id);
        IEnumerable<DocumentoPwVersionesPGeneralDTO> ObtenerIntroduccionBeneficio(int idPGeneral);

    }
}