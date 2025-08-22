using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AdwordsRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 14/02/2022
    /// <summary>
    /// Gestión general de T_Adwords
    /// </summary>
    public class AdwordsRepository : IAdwordsRepository
    {
        private IDapperRepository _dapperRepository;
        public AdwordsRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }


        public CampaniaAdwordsDTO ObtenerDatosCampaniaAdwords(string idcampania)
        {
            try
            {
                CampaniaAdwordsDTO rpta = new CampaniaAdwordsDTO();
                var query = @"
                    SELECT
	                    CampaniaGoogleId,
	                    NombreCampania,
	                    NombreFormulario,
                        ClaveFormulario,
	                    IdCentroCosto,
	                    EsRemarketing
                    FROM mkt.T_CampaniaGoogleAds
                    WHERE Estado = 1 and campaniaGoogleId = @campaniaGoogleId";
                var resultado = _dapperRepository.FirstOrDefault(query, new { campaniaGoogleId = idcampania });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<CampaniaAdwordsDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CampaniaAdwordsTodoDTO> ObtenerTodoCampaniaAdwords()
        {
            try
            {
                List<CampaniaAdwordsTodoDTO> rpta = new List<CampaniaAdwordsTodoDTO>();
                var query = @"
                    SELECT
                        Id,
	                    CampaniaGoogleId,
	                    NombreCampania,
	                    NombreFormulario,
                        ClaveFormulario,
	                    IdCentroCosto,
	                    EsRemarketing
                    FROM mkt.T_CampaniaGoogleAds
                    WHERE Estado = 1 order by id desc";
                var resultado = _dapperRepository.QueryDapper(query,null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CampaniaAdwordsTodoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public CampaniaAdwordsTodoDTO ObtenerCampaniaAdwordsPorId(int idcampania)
        {
            try
            {
                CampaniaAdwordsTodoDTO rpta = new CampaniaAdwordsTodoDTO();
                var query = @"
                    SELECT
                        Id,
	                    CampaniaGoogleId,
	                    NombreCampania,
	                    NombreFormulario,
                        ClaveFormulario,
	                    IdCentroCosto,
	                    EsRemarketing
                    FROM mkt.T_CampaniaGoogleAds
                    WHERE Estado = 1 and id = @idcampania";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idcampania = idcampania });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<CampaniaAdwordsTodoDTO>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarCampaniaAdwords(CampaniaAdwordsDTO datos)
        {
            try
            {
                var query = @"EXEC mkt.SP_InsertaCampaniaAdwords
                      @CampaniaGoogleId,
                      @NombreCampania,
                      @NombreFormulario,
                      @ClaveFormulario,
                      @IdCentroCosto,
                      @EsRemarketing,
                      @Estado,
                      @Usuario";

                var parametros = new
                {
                    CampaniaGoogleId = datos.CampaniaGoogleId,
                    NombreCampania = datos.NombreCampania,
                    NombreFormulario = datos.NombreFormulario,
                    ClaveFormulario = datos.ClaveFormulario,
                    IdCentroCosto = datos.IdCentroCosto,
                    EsRemarketing = datos.EsRemarketing,
                    Estado = datos.Estado,
                    Usuario = datos.Usuario
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool ActualizarCampaniaAdwords(ActualzarCampaniaAdwordsDTO datos)
        {
            try
            {
                var query = @"EXEC mkt.SP_ActualizarCampaniaGoogleAds
                      @Id,
                      @CampaniaGoogleId,
                      @NombreCampania,
                      @NombreFormulario,
                      @ClaveFormulario,
                      @IdCentroCosto,
                      @EsRemarketing,
                      @Usuario";

                var parametros = new
                {
                    Id = datos.Id,
                    CampaniaGoogleId = datos.CampaniaGoogleId,
                    NombreCampania = datos.NombreCampania,
                    NombreFormulario = datos.NombreFormulario,
                    ClaveFormulario = datos.ClaveFormulario,
                    IdCentroCosto = datos.IdCentroCosto,
                    EsRemarketing = datos.EsRemarketing,
                    Usuario = datos.Usuario
                };

                var resultado = _dapperRepository.QueryDapper(query, parametros);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCampaniaAdwords(int id)
        {
            try
            {
                var query = @"Update [mkt].[T_CampaniaGoogleAds] set Estado = 0 where id = @id";

                var resultado = _dapperRepository.QueryDapper(query, new { id = id });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
