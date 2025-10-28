using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.LinkedIn;
using Google.Api.Ads.AdWords.v201809;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.LinkedIn
{
    public class LinkedInApiRepository : ILinkedInApiRepository
    {
        private IDapperRepository _dapperRepository;
        public LinkedInApiRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }



        public void InsertarObjetoSerializadoForm(FormLinkedinDTO entidad)
        {
            try
            {
                var query = "mkt.SP_LinkedInForm_Insertar";
                var parametros = new
                {
                    Id = entidad.Id,
                    Form_name = entidad.Form_name,
                    Form_description = entidad.Form_description,
                    Form_headline = entidad.Form_headline,
                    Question = entidad.Question,
                    PaddingStart = 0
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoForm() {ex.Message}", ex);
            }
        }

        public List<IntDTO> BusquedaDeGruposCampaign()
        {
            try
            {
                List<IntDTO> respuesta = new List<IntDTO>();
                var query = @"
                    SELECT
	                   Id as Valor
                    FROM mkt.T_LinkedInGroupCampaign
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<IntDTO>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BusquedaDeGruposCampaign() {ex.Message}", ex);
            }
        }
        public List<IntDTO> BusquedaDeForms()
        {
            try
            {
                List<IntDTO> respuesta = new List<IntDTO>();
                var query = @"
                    SELECT
	                   Id as Valor
                    FROM mkt.T_LinkedInForm
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<IntDTO>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BusquedaDeForms() {ex.Message}", ex);
            }
        }
        public IntDTO? ObtenerPorIdForm(int? idLinkedInForm)
        {
            try
            {
                IntDTO rpta = new IntDTO();
                var query = @"
                    SELECT
	                    Id as Valor
                    FROM mkt.T_LinkedInForm
                    WHERE Id = @idLinkedInForm";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idLinkedInForm });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }
        }


        public StringDTO? ObtenerPorIdLead(string? idLinkedInLead)
        {
            try
            {
                StringDTO rpta = new StringDTO();
                var query = @"
                    SELECT
	                    GuidLinkedInLead as Valor
                    FROM mkt.T_LinkedInLead
                    WHERE Estado = 1 AND GuidLinkedInLead = @idLinkedInLead";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idLinkedInLead });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdLead()", ex);
            }
        }


        public void InsertarObjetoSerializadoGrupoCampaign(GroupCampaignLinkedInDTO entidad)
        {
            try
            {
                var query = "mkt.SP_LinkedInGroupCampaign_Insertar";
                var parametros = new
                {
                    idLinkedInGroupCampaign = entidad.Id,
                    Nombre = entidad.name,
                    ServingStatuses = entidad.servingStatuses,
                    EstadoGroupCampaign = entidad.status,
                    UltimaModificacion = entidad.lastModified
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoGrupoCampaign() {ex.Message}", ex);
            }
        }
        public void InsertarObjetoSerializadoCampaign(CampaignLinkedInDTO entidad)
        {
            try
            {
                var query = "mkt.SP_LinkedInCampaign_Insertar";
                var parametros = new
                {
                    IdLinkedInCampaign = entidad.Id,
                    IdLinkedInGroupCampaign = entidad.IdLinkedInGroupCampaign,
                    Nombre = entidad.Nombre,
                    EstadoGroupCampaign = entidad.Status,
                    ObjectiveType = entidad.ObjectiveType,
                    PacingStrategy = entidad.PacingStrategy,
                    TypeCampaign = entidad.TypeCampaign,
                    UltimaModificacion = entidad.UltimaModificacion
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoCampaign() {ex.Message}", ex);
            }
        }


        public void InsertarObjetoSerializadoQuestionLead(QuestionLeadDTO entidad)
        {
            try
            {
                var query = "mkt.SP_QuestionLead_Insertar";
                var parametros = new
                {
                    GuidLinkedInLead = entidad.IdLinkedInLead,
                    IdLinkedInForm = entidad.IdLinkedInForm,
                    NroQuestionForm = entidad.IdQuestionForm,
                    Respuesta = entidad.Answer,

                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoQuestionLead() {ex.Message}", ex);
            }
        }

        public void InsertarObjetoSerializadoQuestionForm(QuestionFormDTO entidad)
        {
            try
            {
                var query = "mkt.SP_QuestionForm_Insertar";
                var parametros = new
                {
                    IdLinkedInForm = entidad.IdLinkedInForm,
                    NroQuestionForm = entidad.IdQuestionForm,
                    Question = entidad.Question,
                    MultiSelect = entidad.IdMultiSelect,
                    Respuesta = entidad.Respuesta,

                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoQuestionForm() {ex.Message}", ex);
            }
        }

        public void InsertarObjetoSerializadoLeadLinkedIn(LinkedInLeadApiDTO entidad)
        {
            try
            {
                var query = "mkt.SP_LinkedInLead_Insertar";
                var parametros = new
                {
                    GuidLinkedInLead = entidad.IdLinkedInLead,
                    IdLinkedInForm = entidad.IdLinkedInForm,
                    IdLinkedInCampaign = entidad.IdLinkedInCampaign,
                    LeadType = entidad.LeadType,
                    TestLead = entidad.TestLead,
                    FechaLead = entidad.FechaLead,
                    Question = entidad.Question

                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoLeadLinkedIn() {ex.Message}", ex);
            }
        }







        public IntDTO? ObtenerPorIdGrupoCampaign(long? idLinkedInGroupCampaign)
        {
            try
            {
                IntDTO rpta = new IntDTO();
                var query = @"
                    SELECT
	                    Id as Valor
                    FROM mkt.T_LinkedInGroupCampaign
                    WHERE Estado = 1 AND Id = @idLinkedInGroupCampaign";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idLinkedInGroupCampaign });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdGrupoCampaign()", ex);
            }
        }


        public IntDTO? ObtenerPorIdCampaign(long? idLinkedInCampaign)
        {
            try
            {
                IntDTO rpta = new IntDTO();
                var query = @"
                    SELECT
	                    Id as Valor
                    FROM mkt.T_LinkedInCampaign
                    WHERE Estado = 1 AND Id = @idLinkedInCampaign";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idLinkedInCampaign });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdCampaign()", ex);
            }
        }


        public StringDTO ObtenerToken()
        {
            try
            {
                StringDTO respuesta = new StringDTO();
                var query = @"
                    SELECT
	                    Token As Valor
                    FROM mkt.T_LinkedInToken
                    WHERE Estado = 1 ";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<StringDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IntDTO? ObtenerLeadFormStart(int? idLinkedInForm)
        {
            try
            {
                IntDTO rpta = new IntDTO();
                var query = @"
                    SELECT
	                    PaddingStart as Valor
                    FROM mkt.T_LinkedInForm
                    WHERE Estado = 1 AND Id = @idLinkedInForm";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idLinkedInForm });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerLeadForm()", ex);
            }
        }


        public void ActualizarStartLeadsForm(FormStart entidad)
        {
            try
            {
                var query = "mkt.SP_LinkedInForm_Actualizar";
                var parametros = new
                {
                    IdLinkedInForm = entidad.IdLinkedInForm,
                    value = entidad.value
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarStartLeadsForm() {ex.Message}", ex);
            }
        }
        public List<StringDTO> BuscarLeadsSinOportunindades()
        {
            try
            {
                List<StringDTO> respuesta = new List<StringDTO>();
                var query = @"
                    SELECT
	                   GuidLinkedInLead as Valor
                    FROM mkt.T_LinkedInLead
                    WHERE Estado = 1 AND  OportunidadRegistrada = 0 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<StringDTO>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BuscarLeadsSinOportunindades() {ex.Message}", ex);
            }
        }

        public List<InformacionBaseOportunidad> BuscarDatoLead()
        {
            try
            {
                List<InformacionBaseOportunidad> respuesta = new List<InformacionBaseOportunidad>();
                var query = @"
                    SELECT
	                    GuidLinkedInLead,Nombres,Apellidos,Correo,Celular,Pais,Cargo,AreaFormacion,AreaTrabajo,Industria,CentroCosto,Origen,Asesor,TipoDato,FaseOportunidad
                    FROM mkt.V_LinkedInApiLeadSinOportunidad";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<InformacionBaseOportunidad>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BuscarDatoLead() {ex.Message}", ex);
            }
        }
        public List<InformacionBaseOportunidad> BuscarDatoLeadAprobados()
        {
            try
            {
                List<InformacionBaseOportunidad> respuesta = new List<InformacionBaseOportunidad>();
                var query = @"
                    SELECT
	                   GuidLinkedInLead,Nombres,Apellidos,Correo,Celular,Pais,Cargo,AreaFormacion,AreaTrabajo,Industria,CentroCosto,Origen,Asesor,TipoDato,FaseOportunidad,FechaRegistroCampania
                    FROM mkt.V_LinkedinApiLeadSinOportunidadAprobado";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<InformacionBaseOportunidad>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BuscarDatoLeadAprobados() {ex.Message}", ex);
            }
        }
        public List<InformacionBaseOportunidad> BuscarDatoLeadARevisar()
        {
            try
            {
                List<InformacionBaseOportunidad> respuesta = new List<InformacionBaseOportunidad>();
                var query = @"
                    SELECT
	                    GuidLinkedInLead,Nombres,Apellidos,Correo,Celular,Pais,Cargo,AreaFormacion,AreaTrabajo,Industria,CentroCosto,Origen,Asesor,TipoDato,FaseOportunidad,FechaRegistroCampania
                    FROM mkt.V_LinkedinApiLeadSinOportunidadRevisarMkt";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<InformacionBaseOportunidad>>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en BuscarDatoLeadARevisar() {ex.Message}", ex);
            }
        }
        public void ActualizarOportunidadLead(StringDTO Id)
        {
            try
            {
                var query = "mkt.SP_LinkedInLead_ActualizaOportunidad";
                var parametros = new
                {
                    GuidLinkedInLead = Id.Valor,
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);

            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarOportunidadLead() {ex.Message}", ex);
            }
        }
        public void VerificarOportunidadEnLeads(VerificarOportunidadLead dto)
        {
            try
            {
                var query = "mkt.SP_LinkedInLead_ActualizarOportunidad";
                var parametros = new
                {
                    GuidLinkedInLead = dto.GuidLinkedInLead,
                    IdAlumno = dto.IdAlumno,
                    IdOportunidad = dto.IdOportunidad
                };
                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertaOportunidadesparaRevisar()
        {
            try
            {
                var query = "mkt.SP_LinkedInLeadOportunidadRevisar_Insertar";

                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (resultado != null)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertaOportunidadesparaRevisar() {ex.Message}", ex);
            }
        }
        public void ValidarOportunindadesCreadas(ValidacionOportunidadCreadaLeadDTO entidad)
        {
            try
            {

                var query = "mkt.SP_RegistroLeadSubido_ValidarOportunidad";
                var parametros = new
                {
                    Fecha = entidad.Fecha,
                    Registros = entidad.Registros
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);

            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ValidarOportunindadesCreadas() {ex.Message}", ex);
            }
        }
        public void RegistrarOportunidadesNoCreadads(DateTime fecha)
        {
            try
            {
                var query = "mkt.SP_LinkedInLeadOportunidadPerdida_Insertar";
                var parametros = new
                {
                    Fecha = fecha
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en RegistrarOportunidadesNoCreadads() {ex.Message}", ex);
            }
        }


        public BoolDTO ObtenerEstadoEnvio(int Id)
        {
            try
            {
                BoolDTO respuesta = new BoolDTO();
                var query = @"
                    SELECT
	                    EstadoDeEnvio As Valor
                    FROM mkt.T_LinkedInLeadControlEnvio
                    WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<BoolDTO>(resultado);
                    return respuesta;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void ActualizarEstadoEnviado(int Id)
        {
            try
            {
                var query = "mkt.SP_LinkedInLeadControlEnvio_ActualizarEstadoEnvio";
                var parametros = new
                {
                    Id = Id
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarEstadoEnviado() {ex.Message}", ex);
            }
        }

        public void ActualizarEstadoEnviadoError(int Id)
        {
            try
            {
                var query = "mkt.SP_LinkedInLeadControlEnvio_ActualizarEstadoEnvioError";
                var parametros = new
                {
                    Id = Id
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarEstadoEnviado() {ex.Message}", ex);
            }
        }




        public IEnumerable<ReporteLeadsDTO> ObtenerReporteLeads()
        {
            try
            {
                List<ReporteLeadsDTO> rpta = new List<ReporteLeadsDTO>();
                var query = @"
                    SELECT
	                    GuidLinkedInLead,
                        Nombres,
                        Apellidos,
                        Correo,
                        Celular,
                        Pais,
                        Cargo,
                        AreaFormacion,
                        AreaTrabajo,
                        Industria, 
                        GrupoCampaña,
                        CentroCosto,
                        Origen,
                        FechaLead,
                        OportunidadRegistrada
                    FROM [mkt].[V_LinkedInApiReporte] 
                    ORDER BY FechaLead DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteLeadsDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ReporteLeadsDTO> ObtenerReporteLeadsByFecha(FiltroLandingPagePortaLinkedInDTO filtro)
        {
            try
            {
                List<ReporteLeadsDTO> rpta = new List<ReporteLeadsDTO>();

                var query = "mkt.SP_LinkedInLead_BuscarReportePorFecha";
                var parametros = new
                {
                    FechaInicio = filtro.FechaInicial,
                    FechaFin = filtro.FechaFinal,
                    IdTipoFecha = filtro.IdTipoFecha
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteLeadsDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerReporteLeadsByFecha() {ex.Message}", ex);
            }

        }





        public IEnumerable<ReporteLeadsPendientesDTO> ObtenerReportePendientes()
        {
            try
            {
                List<ReporteLeadsPendientesDTO> rpta = new List<ReporteLeadsPendientesDTO>();
                var query = @"
                    SELECT
	                    GuidLinkedInLead,
                        Nombres,
                        Apellidos,
                        Correo,
                        Celular,
                        Pais,
                        Cargo,
                        CargoOriginal,
                        AreaFormacion,
                        AreaFormacionOriginal,
                        AreaTrabajo,
                        AreaTrabajoOriginal,
                        Industria, 
                        IndustriaOriginal,
                        GrupoCampania,
                        CentroCosto,
                        Origen,
                        FechaLead,
                        FechaIntegra,
                        OportunidadRegistrada,
                        CuentaAsociada,
                        UrlPerfilLinkedIn
                    FROM mkt.V_LinkedinReportePendiente
                    ORDER BY FechaLead DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteLeadsPendientesDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool CrearFormularioRegularizado(LinkedInActualizarDTO dto, string usuario)
        {
            try
            {
                var query = "mkt.SP_LinkedInFormularioRegularizado_Insertar";
                var parametros = new
                {
                    GuidLinkedInLead = dto.GuidLinkedInLead,
                    Cargo=dto.Cargo,
                    AreaTrabajo = dto.AreaTrabajo,
                    AreaFormacion = dto.AreaFormacion,
                    Industria = dto.Industria,
                    UrlPerfil = dto.UrlPerfil,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en CrearFormularioRegularizado() {ex.Message}", ex);
            }

        }


        public IntDTO? ObtenerIdPorGuidLinkedInLead(string GuidLinkedInLead)
        {
            try
            {
                IntDTO rpta = new IntDTO();
                var query = @"
                    SELECT
	                    Id as Valor
                    FROM mkt.T_LinkedInFormularioRegularizado
                    WHERE Estado = 1 AND GuidLinkedInLead = @GuidLinkedInLead";
                var resultado = _dapperRepository.FirstOrDefault(query, new { GuidLinkedInLead });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdGrupoCampaign()", ex);
            }
        }


        public bool ActualizarFormularioRegularizado(LinkedInActualizarDTO dto, string usuario)
        {
            try
            {
                var query = "mkt.SP_LinkedInFormularioRegularizado_Actualizar";
                var parametros = new
                {
                    GuidLinkedInLead = dto.GuidLinkedInLead,
                    Cargo = dto.Cargo,
                    AreaTrabajo = dto.AreaTrabajo,
                    AreaFormacion = dto.AreaFormacion,
                    Industria = dto.Industria,
                    UrlPerfil = dto.UrlPerfil,
                    Usuario = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                        return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarFormularioRegularizado() {ex.Message}", ex);
            }

        }


        public bool ActualizarPaisQuestionLeadForm(LinkedInActualizarDTO dto, string usuario)
        {
            try
            {
                var query = "mkt.SP_QuestionLeadForm_ActualizarPais";
                var parametros = new
                {
                    GuidLinkedInLead = dto.GuidLinkedInLead,
                    Pais = dto.Pais,
                    UsuarioModificacion = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarFormularioRegularizado() {ex.Message}", ex);
            }

        }



        public List<InformacionBaseOportunidad> ObtenerReportePendientesRevisados()
        {
            try
            {
                List<InformacionBaseOportunidad> rpta = new List<InformacionBaseOportunidad>();
                var query = @"
                    SELECT
	                    GuidLinkedInLead,Nombres,Apellidos,Correo,Celular,Pais,Cargo,AreaFormacion,AreaTrabajo,Industria,CentroCosto,Origen,Asesor,TipoDato,FaseOportunidad,FechaRegistroCampania
                    FROM mkt.V_LinkedinReportePendiente
                    ORDER BY FechaRegistroCampania asc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<InformacionBaseOportunidad>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public BoolDTO ValidarCreacionOportunidadLinkedinEstado()
        {
            try
            {
                BoolDTO rpta = new BoolDTO { Valor = false };

      
                var query = @"
                    SELECT EstadoDeEnvio AS Valor 
                    FROM mkt.T_LinkedInLeadControlEnvio 
                    WHERE Id IN (5, 7)";

                var resultados = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultados) && !resultados.Contains("[]"))
                {
                    var lista = JsonConvert.DeserializeObject<List<BoolDTO>>(resultados);

                    rpta.Valor = lista.Any(x => x.Valor.Value);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public BoolDTO ValidarObtencionLeadLinkedinEstado()
        {
            try
            {
                BoolDTO rpta = new BoolDTO();
                var query = @"
                    SELECT
	                    Estado as Valor
                    FROM mkt.V_ControlEnvioLinkedin";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<BoolDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public QuestionLeadFormDTO? ObtenerQuestionLeadForm(string GuidLinkedInLead)
        {
            try
            {
                QuestionLeadFormDTO rpta = new QuestionLeadFormDTO();
                var query = @"
                   SELECT Id,
                       GuidLinkedInLead,
                       Nombre,
                       Pais,
                       Cargo,
                       AreaFormacion,
                       AreaTrabajo,
                       Industria
                    FROM mkt.T_QuestionLeadForm 
                    WHERE Estado = 1 AND GuidLinkedInLead = @GuidLinkedInLead";
                var resultado = _dapperRepository.FirstOrDefault(query, new { GuidLinkedInLead });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<QuestionLeadFormDTO>(resultado)!;

                    return rpta;
                }
                return rpta; ;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdGrupoCampaign()", ex);
            }
        }


    }
}
