using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.GeneracionDeDataParaConfiguracionPreEnvio;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionDeEnvioParaWhatsAppRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDeEnvioParaWhatsApp
    /// </summary>
    public class ConfiguracionDeEnvioParaWhatsAppRepository : GenericRepository<TConfiguracionDeEnvioParaWhatsApp>, IConfiguracionDeEnvioParaWhatsAppRepository
    {
        private Mapper _mapper;

        public ConfiguracionDeEnvioParaWhatsAppRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionDeEnvioParaWhatsApp, ConfiguracionDeEnvioParaWhatsApp>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfiguracionDeEnvioParaWhatsApp, ConfiguracionDeEnvioParaWhatsAppDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionDeEnvioParaWhatsApp MapeoEntidad(ConfiguracionDeEnvioParaWhatsApp entidad)
        {
            try
            {
                TConfiguracionDeEnvioParaWhatsApp modelo = _mapper.Map<TConfiguracionDeEnvioParaWhatsApp>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDeEnvioParaWhatsApp Add(ConfiguracionDeEnvioParaWhatsApp entidad)
        {
            try
            {
                var ConfiguracionDeEnvioParaWhatsApp = MapeoEntidad(entidad);
                base.Insert(ConfiguracionDeEnvioParaWhatsApp);
                return ConfiguracionDeEnvioParaWhatsApp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionDeEnvioParaWhatsApp Update(ConfiguracionDeEnvioParaWhatsApp entidad)
        {
            try
            {
                var ConfiguracionDeEnvioParaWhatsApp = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionDeEnvioParaWhatsApp.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionDeEnvioParaWhatsApp);
                return ConfiguracionDeEnvioParaWhatsApp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TConfiguracionDeEnvioParaWhatsApp> Add(IEnumerable<ConfiguracionDeEnvioParaWhatsApp> listadoEntidad)
        {
            try
            {
                List<TConfiguracionDeEnvioParaWhatsApp> listado = new List<TConfiguracionDeEnvioParaWhatsApp>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TConfiguracionDeEnvioParaWhatsApp> Update(IEnumerable<ConfiguracionDeEnvioParaWhatsApp> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionDeEnvioParaWhatsApp> listado = new List<TConfiguracionDeEnvioParaWhatsApp>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla ObtenerPrioridadPAraEnvioDeWpp(int id)
        {
            try
            {
                ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla retornar = new ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla();
                string sql = @"SELECT
                                IdCampaniaGeneral, 
                                IdPlantilla,
								NombrePlantilla,
                                Id, 
                                FechaDeEnvio, 
                                FechaFinDeEnvio, 
                                TiempoEntreEnvios, 
                                IdCampaniaGeneralDetalle, 
                                Nombre,                                 
                                UsuarioCreacion, 
                                UsuarioModificacion,
                                HoraDeEnvio, 
                                CampaniaGeneralNombre
                            FROM WHP.V_ObtencionDeDataParaConfiguracionDeEnvio 
                            where Id =@id";

                var queryResultado = _dapperRepository.FirstOrDefault(sql, new { id });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    retornar = JsonConvert.DeserializeObject<ConfiguracionDeEnvioParaWhatsAppMasCampaniaGeneralYPlantilla>(queryResultado);
                }
                return retornar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ObtenerPrioridadesDeFiltroWppDTO> ObtenerPrioridadesDeFiltroWpp(int idCampaniaGeneral, int IdCampaniaGeneralDetalle)
        {
            try
            {
                List<ObtenerPrioridadesDeFiltroWppDTO> resultado = new List<ObtenerPrioridadesDeFiltroWppDTO>();
                string sql = "select * from whp.V_ObtencionDeDataParaLasPrioridadesDeEnvioWhatsApp as vista where vista.IdCampaniaGeneral = @idCampaniaGeneral AND vista.Id = @IdCampaniaGeneralDetalle";
                var queryResultado = _dapperRepository.QueryDapper(sql, new { idCampaniaGeneral,IdCampaniaGeneralDetalle });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    var res  = JsonConvert.DeserializeObject<List<ObtenerPrioridadesDeFiltroWppSQL>>(queryResultado);
                    resultado = res.GroupBy(x => new
                    {
                        x.Id,
                        x.IdCampaniaGeneral,
                        x.Prioridad,
                        x.Nombre
                    }).Select(g => new ObtenerPrioridadesDeFiltroWppDTO
                    {
                        Id=g.Key.Id,
                        Prioridad=g.Key.Prioridad,
                        IdCampaniaGeneral=g.Key.IdCampaniaGeneral,
                        Nombre=g.Key.Nombre,
                        Configuracion=res.Where(x=>x.Id==g.Key.Id).Select(x=> new TConfiguracionDeEnvioParaWhatsAppHelper
                        {
                            FechaDeEnvio=x.FechaDeEnvio,
                            FechaFinDeEnvio=x.FechaFinDeEnvio,
                            HoraDeEnvio=x.HoraDeEnvio,
                            IdPlantilla=x.IdPlantilla,
                            IdConfiguracionDeEnvioParaWhatsApp=x.IdConfiguracionDeEnvioParaWhatsApp,
                            TiempoEntreEnvios=x.TiempoEntreEnvios,
                        }).FirstOrDefault(),
                        Encargados= res.Where(x => x.Id == g.Key.Id).Select(x => new TPersonalEncargadoDeEnvioDeConsultaHelper
                        {
                            Dia1=x.Dia1,
                            Dia2=x.Dia2,    
                            Dia3=x.Dia3,
                            Dia4=x.Dia4,
                            Dia5=x.Dia5,
                            FechaDia1=x.FechaDia1,
                            FechaDia2=x.FechaDia2,
                            FechaDia3=x.FechaDia3,
                            FechaDia4=x.FechaDia4,
                            FechaDia5=x.FechaDia5,
                            IdCampaniaGeneralDetalle=x.IdCampaniaGeneralDetalle,
                            IdPersonal=x.IdPersonal,
                            IdPersonalEncargadoDeEnvioDeConsulta=x.IdPersonalEncargadoDeEnvioDeConsulta
                        }).ToList(),
                    }).ToList();
                    return resultado;
                }
                return resultado;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio> ObtenerDataParaGenerarWhatsAppConfiguracionPreenvio(int idcampaniaGeneral)
        {
            try
            {
                List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio> retornar = new List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio>();
                string sql = @"SELECT 
                            IdPlantilla, 
			                IdConjuntoListaDetalle, 
			                Clave, 
			                Valor, 
                            IdCampaniaGeneralDetalle, 
			                IdCampaniaGeneral
                        FROM WHP.V_ObtenerDatosParaConfiguracionPlantillaWpp
                        WHERE IdCampaniaGeneral=@idcampaniaGeneral";
                var respuesta = _dapperRepository.QueryDapper(sql, new { idcampaniaGeneral });
                if (respuesta != "[]" && respuesta != "null")
                {
                    retornar = JsonConvert.DeserializeObject<List<ObtenerGeneracionDeDataParaConfiguracionPreEnvio>>(respuesta);
                }
                return retornar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
