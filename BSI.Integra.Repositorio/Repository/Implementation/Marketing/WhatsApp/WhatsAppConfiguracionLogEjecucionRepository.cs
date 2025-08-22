using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppConfiguracionLogEjecucionRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_WhatsAppConfiguracionLogEjecucion
    /// </summary>
    public class WhatsAppConfiguracionLogEjecucionRepository : GenericRepository<TWhatsAppConfiguracionLogEjecucion>, IWhatsAppConfiguracionLogEjecucionRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionLogEjecucionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionLogEjecucion, WhatsAppConfiguracionLogEjecucion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TAgendaTab, AgendaTab>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TAgendaTab MapeoEntidad(AgendaTab entidad)
        {
            try
            {
                TAgendaTab modelo = _mapper.Map<TAgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private AgendaTab MapeoEntidadReverse(TAgendaTab entidad)
        {
            try
            {
                AgendaTab modelo = _mapper.Map<AgendaTab>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Metodos Base

        public IEnumerable<WhatsAppConfiguracionLogEjecucion> GetBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionLogEjecucion> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionLogEjecucion> listadoBO = new List<WhatsAppConfiguracionLogEjecucion>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionLogEjecucion objetoBO = _mapper.Map<WhatsAppConfiguracionLogEjecucion>(itemEntidad);
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionLogEjecucion FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionLogEjecucion entidad = base.FirstById(id);
                WhatsAppConfiguracionLogEjecucion objetoBO = new WhatsAppConfiguracionLogEjecucion();
                _mapper.Map<WhatsAppConfiguracionLogEjecucion>(entidad);

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionLogEjecucion FirstBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionLogEjecucion entidad = base.FirstBy(filter);
                WhatsAppConfiguracionLogEjecucion objetoBO = _mapper.Map<WhatsAppConfiguracionLogEjecucion>(entidad);
                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionLogEjecucion entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AsignacionId(TWhatsAppConfiguracionLogEjecucion entidad, WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TWhatsAppConfiguracionLogEjecucion MapeoEntidad(WhatsAppConfiguracionLogEjecucion objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionLogEjecucion entidad = new TWhatsAppConfiguracionLogEjecucion();
                entidad = _mapper.Map<TWhatsAppConfiguracionLogEjecucion>(objetoBO);

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Descripcion: Valida si el numero ya ha sido enviado para no duplicar el envio caso exita no procede caso contrario si procede para el envio en whatsapp 
        /// </summary>
        /// <param name="Celular">Numero de celular del alumno</param>
        /// <returns>Retorna verdadero si existe el numero ya enviado caso contrario falso</returns>
        public int VerificadEnvioDuplicado(string Celular)
        {
            try
            {
                //ValorStringDTO rpta = new ValorStringDTO();
                string Query = "Select Celular From mkt.V_VerificarEnvioFechaActual Where Celular=@Celular";
                string QueryRespuesta = _dapperRepository.FirstOrDefault(Query, new { Celular });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    return 1;
                }
                return 2;
            }
            catch
            {
                return 2;
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucion</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsappConfiguracionLogEjecucion(WhatsAppConfiguracionLogEjecucion filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionLogEjecucion]";

            var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.FechaInicio,
                filtro.FechaFin,
                filtro.IdWhatsAppConfiguracionEnvio,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return (int)resultado.Valor;
        }

        /// <summary>
        /// Actualiza en mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucion</param>
        /// <returns>Id de la transaccion</returns>
        public bool ActualizarWhatsappConfiguracionLogEjecucionFechaFin(WhatsAppConfiguracionLogEjecucion filtro)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarWhatsAppConfiguracionLogEjecucionFechaFin]";

                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    filtro.Id,
                    filtro.FechaFin
                });

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Autor: Edson Mayta Escobedo
        /// Descripcion: si actualizamos el estado del log a cero podremos detener el log que esta duplicandose innecesariamente
        /// </summary>
        /// <param name="IdWhatsAppConfiguracionLogEjecucion">Id del log que va verificar</param>
        /// <returns>Retorna el estado del log</returns>
        public int ObtenerLogActivo(int IdWhasAppConfiguracionLogEjecucion)
        {
            try
            {
                WhatsAppConfiguracionLogEjecucionDTO WhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionDTO();
                var Query = "SELECT id, estado FROM mkt.T_WhatsAppConfiguracionLogEjecucion WHERE id= @IdWhasAppConfiguracionLogEjecucion AND Estado=0";
                var QueryRespuesta = _dapperRepository.FirstOrDefault(Query, new { IdWhasAppConfiguracionLogEjecucion });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    WhatsAppConfiguracionLogEjecucion = JsonConvert.DeserializeObject<WhatsAppConfiguracionLogEjecucionDTO>(QueryRespuesta);
                    return 0;
                }
                return 1;
            }
            catch
            {
                return 1;
            }
        }


        /// <summary>
        /// Autor: Edson Mayta Escobedo
        /// Descripcion: Trae el log activo asociado al whatsappConfiguracionEnvio
        /// </summary>
        /// <param name="idwhatsappConfiguracionEnvio">Id del whatsappConfiguracionEnvio que va verificar</param>
        /// <returns>Retorna 1 cuando existe un log activo</returns>
        public int obtenerOtrosLogActivos(int idwhatsappConfiguracionEnvio)
        {
            try
            {
                WhatsAppConfiguracionLogEjecucionDTO WhatsAppConfiguracionLogEjecucion = new WhatsAppConfiguracionLogEjecucionDTO();
                var Query = "SELECT id, estado FROM mkt.T_WhatsAppConfiguracionLogEjecucion WHERE IdWhatsAppConfiguracionEnvio =@idwhatsappConfiguracionEnvio AND Estado=1";
                var QueryRespuesta = _dapperRepository.FirstOrDefault(Query, new { idwhatsappConfiguracionEnvio });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    WhatsAppConfiguracionLogEjecucion = JsonConvert.DeserializeObject<WhatsAppConfiguracionLogEjecucionDTO>(QueryRespuesta);
                    return WhatsAppConfiguracionLogEjecucion.Id;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Autor: 
        /// Descripcion: Trae el log activo asociado al whatsappConfiguracionEnvio
        /// </summary>
        /// <param name="idwhatsappConfiguracionEnvio">Id del whatsappConfiguracionEnvio que va verificar</param>
        /// <returns>Retorna 1 cuando existe un log activo</returns>
        public List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> ObtenerLogActivoCampaniaGeneralDetalleResponsableWhatsApp(int IdCampaniaGeneralDetalleResponsableWhatsApp)
        {
            List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO> WhatsAppConfiguracionLogEjecucion = new List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO>();
            try
            {
                var Query = "SELECT Id, IdCampaniaGeneralDetalleResponsableWhatsApp,FechaEnvio,HoraEnvio,Estado FROM mkt.T_CampaniaGeneralDetalleResponsableAlumnoLogWhatsApp WHERE IdCampaniaGeneralDetalleResponsableWhatsApp = @IdCampaniaGeneralDetalleResponsableWhatsApp AND Estado = 1";
                var QueryRespuesta = _dapperRepository.QueryDapper(Query, new { IdCampaniaGeneralDetalleResponsableWhatsApp });
                if (!string.IsNullOrEmpty(QueryRespuesta) & !QueryRespuesta.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    WhatsAppConfiguracionLogEjecucion = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleResponsableAlumnoLogWhatsAppDTO>>(QueryRespuesta);
                    return WhatsAppConfiguracionLogEjecucion;
                }
                return WhatsAppConfiguracionLogEjecucion;
            }
            catch
            {
                return WhatsAppConfiguracionLogEjecucion;
            }
        }

        /// <summary>
        /// Actualiza en mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucionBO</param>
        /// <returns>Id de la transaccion</returns>
        public bool EliminarLog(int Id, string Usuario)
        {
            try
            {
                string spQuery = "[mkt].[SP_EliminarLog]";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = Id,
                    Usuario = Usuario
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Inserta un log
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionLogEjecucionBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarLogWhatsApp(int IdCampaniaGeneralDetalleResponsableWhatsApp, string HoraEnvio, string FechaInicioEnvioWhatsapp, string Usuario)
        {
            try
            {

                string spQuery = "[mkt].[SP_InsertarLogWhatsApp]";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalleResponsableWhatsApp,
                    HoraEnvio,
                    FechaInicioEnvioWhatsapp,
                    Usuario
                });
                if (!string.IsNullOrEmpty(query) & !query.Contains("null"))
                {
                    //rpta = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    IdLogInsertadoDTO IdCampaniaGeneralDetalleResponsableLogWhatsApp = JsonConvert.DeserializeObject<IdLogInsertadoDTO>(query);
                    return IdCampaniaGeneralDetalleResponsableLogWhatsApp.Valor;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

    }
}
