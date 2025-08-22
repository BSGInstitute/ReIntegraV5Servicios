using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Repositorio.Repository.Interface;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class WhatsAppConfiguracionEnvioRepository: GenericRepository<TWhatsAppConfiguracionEnvio>, IWhatsAppConfiguracionEnvioRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionEnvioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TWhatsAppConfiguracionEnvio MapeoEntidad(WhatsAppConfiguracionEnvioDTO entidad)
        {
            try
            {
                TWhatsAppConfiguracionEnvio modelo = _mapper.Map<TWhatsAppConfiguracionEnvio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Metodos Base


        public TWhatsAppConfiguracionEnvio Add(WhatsAppConfiguracionEnvioDTO entidad)
        {
            try
            {
                var AlumnoCuponRegistro = MapeoEntidad(entidad);
                base.Insert(AlumnoCuponRegistro);
                return AlumnoCuponRegistro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionEnvio Update(WhatsAppConfiguracionEnvioDTO entidad)
        {
            try
            {
                var AlumnoCuponRegistro = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AlumnoCuponRegistro.RowVersion = entidadExistente.RowVersion;

                base.Update(AlumnoCuponRegistro);
                return AlumnoCuponRegistro;
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

        public IEnumerable<TWhatsAppConfiguracionEnvio> Add(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppConfiguracionEnvio> listado = new List<TWhatsAppConfiguracionEnvio>();
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

        public IEnumerable<TWhatsAppConfiguracionEnvio> Update(IEnumerable<WhatsAppConfiguracionEnvioDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppConfiguracionEnvio> listado = new List<TWhatsAppConfiguracionEnvio>();
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

        public TWhatsAppConfiguracionEnvio FirstById(int id)
        {
            try
            {
                return base.FirstById(id);
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// Autor: Fischer Valdez - Gian Miranda
        /// Fecha: 05/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Las configuracionActivas por IdConjuntoLista
        /// </summary>
        /// <param name="idConjuntoLista">Id del ConjuntoLista a ejecutar (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos(ConjuntoListaDetalleWhatsAppDTO)</returns>
        public List<ConjuntoListaDetalleWhatsAppDTO> ObtenerConfiguracionPorIdConjuntoLista(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleWhatsAppDTO> configuracion = new List<ConjuntoListaDetalleWhatsAppDTO>();
                string consultaConfiguracion = @"SELECT Id, IdConjuntoListaDetalle, Nombre, Descripcion, IdPlantilla, IdPersonal, IdConjuntoLista
                                                FROM mkt.V_WhatsAppConfiguracionLista
                                                WHERE IdConjuntoLista = @IdConjuntoLista and EstadoConjuntoListaDetalle = 1 AND Activo = 1";
                var resultadoConfiguracion = _dapperRepository.QueryDapper(consultaConfiguracion, new { idConjuntoLista });

                if (resultadoConfiguracion != "[]" && resultadoConfiguracion != "null")
                {
                    configuracion = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleWhatsAppDTO>>(resultadoConfiguracion);

                    return configuracion;
                }
                return configuracion;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina Registros Procesados sin enviar por conjuntoLista
        /// </summary>
        /// <returns></returns>
        public int EliminarEnviosProcesados(int idConjuntoLista)
        {
            try
            {
                ValorIntDTO respuesta = new ValorIntDTO();
                string _query = "mkt.SP_WhatsAppMensajePublicidad_Eliminar";
                string query = _dapperRepository.QuerySPFirstOrDefault(_query, new { IdConjuntoLista = idConjuntoLista });
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(query);
                    return Convert.ToInt32(respuesta.Valor);
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina SeguimientoPreProcesoListaWhatsAppBO mediante un SP para llamarlo desde replica
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        public void EliminarWhatsAppConfiguracionMailingGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_EliminarWhatsAppConfiguracionMailingGeneral]";
                var query = _dapperRepository.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserta SeguimientoPreProcesoListaWhatsAppBO mediante un SP para llamarlo desde replica
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de tipo WhatsAppConfiguracionEnvioBO</returns>
        public WhatsAppConfiguracionEnvioDTO InsertarWhatsAppConfiguracionGeneralMailing(int idCampaniaGeneralDetalle)
        {
            try
            {
                WhatsAppConfiguracionEnvioDTO objResultado = new WhatsAppConfiguracionEnvioDTO();

                string spQuery = "mkt.SP_InsertarWhatsAppConfiguracionGeneralMailing";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle
                });
                if (!string.IsNullOrEmpty(query))
                {
                    objResultado = JsonConvert.DeserializeObject<WhatsAppConfiguracionEnvioDTO>(query);
                }

                return objResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="usuarioResponsable">Usuario responsable del cambio del flag</param>
        /// <param name="estadoHabilitado">Flag para determinar si está habilitado o deshabilitado la recuperacion del modulo</param>
        /// <returns>Boolean</returns>
        public bool ActualizarEstadoWhatsAppRecuperacion(string tipo, string usuarioResponsable, bool estadoHabilitado,int IdModuloSistemaWhatsAppMailing)
        {
            try
            {
                string spQuery = "mkt.SP_ActualizarEstadoWhatsAppRecuperacion";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    IdModuloSistema = IdModuloSistemaWhatsAppMailing,
                    Tipo = tipo,
                    UsuarioResponsable = usuarioResponsable,
                    EstadoHabilitado = estadoHabilitado
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="servidor">Servidor detectado</param>
        /// <returns>Boolean</returns>
        public bool InsertarRegistroCaidaServidor(string servidor)
        {
            try
            {
                string spQuery = "conf.SP_InsertarRegistroCaidaServidor";
                var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
                {
                    Servidor = servidor
                });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ConjuntoListaDetalleWhatsAppDTO> ConsultaWhatsAppYConfiguracionEnvio(int IdConjuntoLista)
        {
            List<ConjuntoListaDetalleWhatsAppDTO> retornar = new List<ConjuntoListaDetalleWhatsAppDTO>();
            string query = "SELECT " +
                "vlist.Id, " +
                "vlist.IdConjuntoListaDetalle, " +
                "vlist.Nombre, " +
                "vlist.Descripcion, " +
                "vlist.IdPlantilla, " +
                "vlist.IdPersonal, " +
                "vlist.IdConjuntoLista, " +
                "eprog.IdPgeneral, " +
                "eprog.IdTipoEnvioPrograma " +
                "FROM mkt.V_WhatsAppConfiguracionLista as vlist " +
                "INNER JOIN mkt.T_WhatsAppConfiguracionEnvioPorPrograma as eprog " +
                "ON vlist.Id=eprog.IdWhatsAppConfiguracionEnvio " +
                "WHERE vlist.IdConjuntoLista = @IdConjuntoLista " +
                "AND vlist.EstadoConjuntoListaDetalle = 1 " +
                "AND vlist.Activo = 1 " +
                "AND eprog.IdTipoEnvioPrograma<3";
            var respuesta = _dapperRepository.QuerySPDapper(query, new
            {
                IdConjuntoLista
            });
            if (respuesta != "[]" && respuesta != "null")
            {
                var configuracion = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionListaYWhatsAppConfiguracionEnvioPorPrograma>>(respuesta);
                retornar = configuracion.GroupBy(x => new
                {
                    x.Id,
                    x.IdConjuntoListaDetalle,
                    x.Nombre,
                    x.Descripcion,
                    x.IdPlantilla,
                    x.IdPersonal,
                    x.IdConjuntoLista,
                    x.IdPgeneral,
                    x.IdTipoEnvioPrograma
                }).Select(g=>new ConjuntoListaDetalleWhatsAppDTO
                {
                   Descripcion=g.Key.Descripcion.ToString(),
                    Id = g.Key.Id,
                    IdConjuntoListaDetalle =  g.Key.IdConjuntoListaDetalle,
                    Nombre = g.Key.Nombre,
                    IdPlantilla = g.Key.IdPlantilla,
                    IdPersonal = g.Key.IdPersonal,
                    IdConjuntoLista= g.Key.IdConjuntoLista,
                    ProgramaPrincipal= configuracion.Where(j=> j.Id==g.Key.Id && j.IdTipoEnvioPrograma==1).Select(f=> new WhatsAppConfiguracionEnvioPorProgramaDTO
                    {
                        IdPgeneral=f.IdPgeneral,
                        IdTipoEnvioPrograma=0,
                        IdWhatsAppConfiguracionEnvio=0
                    }).ToList(),
                    ProgramaSecundario= configuracion.Where(j => j.Id == g.Key.Id && j.IdTipoEnvioPrograma == 2).Select(f => new WhatsAppConfiguracionEnvioPorProgramaDTO
                    {
                        IdPgeneral = f.IdPgeneral,
                        IdTipoEnvioPrograma = 0,
                        IdWhatsAppConfiguracionEnvio = 0
                    }).ToList(),
                }).ToList();
            }
            return retornar;
        }
    }
}