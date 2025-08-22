using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    /// Repositorio: AreaFormacionRepository
    /// Autor: Rodrigo Montesinos Paredes.
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneral
    /// </summary>
    public class CampaniaGeneralRepositorio : GenericRepository<TCampaniaGeneral>, ICampaniaGeneralRepositorio
    {
        private Mapper _mapper;
        public CampaniaGeneralRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaGeneral, CampaniaGeneralDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCampaniaGeneral MapeoEntidad(CampaniaGeneralDTO entidad)
        {
            try
            {
                TCampaniaGeneral modelo = _mapper.Map<TCampaniaGeneral>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<CampaniaGeneralDTO> MapeoEntidadLista(IEnumerable<TCampaniaGeneral> entidad)
        {
            try
            {
                IEnumerable<CampaniaGeneralDTO> modelo = _mapper.Map<IEnumerable<CampaniaGeneralDTO>>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCampaniaGeneral Add(CampaniaGeneralDTO entidad)
        {
            try
            {
                var AreaFormacion = MapeoEntidad(entidad);
                base.Insert(AreaFormacion);
                return AreaFormacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCampaniaGeneral Update(CampaniaGeneralDTO entidad)
        {
            try
            {
                var AreaFormacion = MapeoEntidad(entidad);
                AreaFormacion.Estado = true;
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaFormacion.RowVersion = entidadExistente.RowVersion;
                base.Update(AreaFormacion);
                return AreaFormacion;
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


        public IEnumerable<TCampaniaGeneral> Add(IEnumerable<CampaniaGeneralDTO> listadoEntidad)
        {
            try
            {
                List<TCampaniaGeneral> listado = new List<TCampaniaGeneral>();
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

        public IEnumerable<TCampaniaGeneral> Update(IEnumerable<CampaniaGeneralDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCampaniaGeneral> listado = new List<TCampaniaGeneral>();
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

        public CampaniaGeneralDTO Obtener(int idCampaniaGeneral)
        {
            try
            {
                TCampaniaGeneral result = base.FirstBy(x => x.Id == idCampaniaGeneral);
                CampaniaGeneralDTO res = _mapper.Map<CampaniaGeneralDTO>(result);
                return res;
            }catch(Exception e)
            {
                throw e;
            }
        }

        /// Autor: Rodrigo Montesinos
        /// Fecha: 28/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase CampaniaGeneral</returns>
        public List<CampaniaGeneralDTO> ObtenerListaCampaniaGeneral()
        {
            try
            {
                IEnumerable<TCampaniaGeneral> result = base.GetBy(x => x.Estado == true);
                var resp = MapeoEntidadLista(result);
                return resp.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 28/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase CampaniaGeneral</returns>
        public List<CampaniaGeneralDTO> ObtenerTodosPorIdCampaniaGeneral(int id)
        {
            try
            {
                IEnumerable<TCampaniaGeneral> result = base.GetBy(x => x.Id == id);
                var resp = MapeoEntidadLista(result);
                return resp.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase PrioridadesCampaniaGeneralDetalleDTO</returns>
        public List<PrioridadesCampaniaGeneralDetalleDTO> ObtenerListaCampaniaGeneralDetalleConProgramas(int idCampaniaGeneral)
        {
            try
            {
                string query = @"
                  SELECT Id, 
                       Nombre,
                       Prioridad, 
                       Asunto,
                       IdPersonal, 
                       IdCentroCosto,
                       IdConjuntoAnuncio,
                       CantidadContactosMailing,
                       CantidadContactosWhatsapp, 
                       EnEjecucion,
                       NoIncluyeWhatsaap,
                       RowVersion, 
                       IdCampaniaGeneralDetallePrograma, 
                       IdPGeneral, 
                       NombreProgramaGeneral, 
                       IdArea, 
                       IdSubArea, 
                       CantidadSubidosMailChimp,
                        IdCampaniaGeneralDetalleResponsable,
                        IdResponsable,
                        Dia1,
                        Dia2,
                        Dia3,
                        Dia4,
                        Dia5,
                        Total
                FROM mkt.V_ObtenerCampaniaGeneralDetalle
                WHERE EstadoCampaniaGeneralDetalle = 1
                      AND IdCampaniaGeneral = @idCampaniaGeneral
                ORDER BY Id;
                ";
                var respuestaQuery = _dapperRepository.QueryDapper(query, new { idCampaniaGeneral });
                IEnumerable<CampaniaGeneralDetalleConProgramasDTO> listaCampaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleConProgramasDTO>>(respuestaQuery);

                var listaPrioridades = (from p in listaCampaniaGeneralDetalle
                                        group p by new
                                        {
                                            p.Id,
                                            p.Nombre,
                                            p.Prioridad,
                                            p.Asunto,
                                            p.IdPersonal,
                                            p.IdCentroCosto,
                                            p.IdConjuntoAnuncio,
                                            p.CantidadContactosMailing,
                                            p.CantidadContactosWhatsapp,
                                            p.CantidadSubidosMailChimp,
                                            p.EnEjecucion,
                                            p.NoIncluyeWhatsaap,

                                        } into g
                                        select new PrioridadesCampaniaGeneralDetalleDTO
                                        {
                                            Id = g.Key.Id,
                                            Nombre = g.Key.Nombre,
                                            Prioridad = g.Key.Prioridad,
                                            Asunto = g.Key.Asunto,
                                            IdPersonal = g.Key.IdPersonal,
                                            IdCentroCosto = g.Key.IdCentroCosto,
                                            IdConjuntoAnuncio = g.Key.IdConjuntoAnuncio,
                                            CantidadContactosMailing = g.Key.CantidadContactosMailing,
                                            CantidadContactosWhatsapp = g.Key.CantidadContactosWhatsapp,
                                            EnEjecucion = g.Key.EnEjecucion,
                                            NoIncluyeWhatsaap = g.Key.NoIncluyeWhatsaap,
                                            CantidadSubidosMailChimp = g.Key.CantidadSubidosMailChimp,
                                            ProgramasFiltro = g.Select(o => new CampaniaGeneralDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaGeneralDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                NombreProgramaGeneral = o.NombreProgramaGeneral
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                            Areas = g.Where(x => x.IdArea != null).Select(o => o.IdArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            SubAreas = g.Where(x => x.IdSubArea != null).Select(o => o.IdSubArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            Responsables = g.Select(o => new CampaniaGeneralDetalleResponsableDTO
                                            {
                                                Id = o.IdCampaniaGeneralDetalleResponsable,
                                                IdResponsable = o.IdResponsable,
                                                Dia1 = o.Dia1,
                                                Dia2 = o.Dia2,
                                                Dia3 = o.Dia3,
                                                Dia4 = o.Dia4,
                                                Dia5 = o.Dia5,
                                                Total = o.Total
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                        }).ToList();

                return listaPrioridades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleEstadoEnEjecucionDTO</returns>
        public List<CampaniaGeneralDetalleEstadoEnEjecucionDTO> ObtenerEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneral)
        {
            try
            {
                var resultado = new List<CampaniaGeneralDetalleEstadoEnEjecucionDTO>();

                string query = @"SELECT IdCampaniaGeneral,
                                        IdCampaniaGeneralDetalle,
                                        EnEjecucion
                                FROM [mkt].[V_ObtenerEstadoEjecucionCampaniaGeneralDetalle]
                                WHERE IdCampaniaGeneral = @idCampaniaGeneral";

                var respuestaDB = _dapperRepository.QueryDapper(query, new { idCampaniaGeneral });

                if (!string.IsNullOrEmpty(respuestaDB) && !respuestaDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleEstadoEnEjecucionDTO>>(respuestaDB);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalleResponsables con sus respectivos dias, filtrado por el IdCampaniaMailingDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase ResponsableDTO</returns>
        public List<ResponsablesDTO> ObtenerListaCampaniaGeneralDetalleResponsables(int idCampaniaGeneralDetalle)
        {
            try
            {
                var Responsables = new List<ResponsablesDTO>();
                var query = @"
                            SELECT Id, 
                                    IdPersonal,
                                    Dia1,
                                    Dia2,
                                    Dia3,
                                    Dia4,
                                    Dia5,
                                    Total
                            FROM mkt.T_CampaniaGeneralDetalleResponsable
                            WHERE Estado = 1
                            AND IdCampaniaGeneralDetalle=@idCampaniaGeneralDetalle;";

                var respuestaDB = _dapperRepository.QueryDapper(query, new { idCampaniaGeneralDetalle });
                Responsables = JsonConvert.DeserializeObject<List<ResponsablesDTO>>(respuestaDB);

                return Responsables;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public TCampaniaGeneral Add(CampaniaGeneral entidad)
        {
            throw new NotImplementedException();
        }
        public bool CrearUrlFormularioPrioridad(int idCampaniaGeneralDetalle, string usuarioResponsable)
        {
            try
            {
                string spReporte = "[mkt].[SP_GenerarUrlFormularioPublicidadMailing]";
                string resultadoReporte = _dapperRepository.QuerySPDapper(spReporte, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, UsuarioResponsable = usuarioResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerUrlFormularioPrioridad(int idCampaniaGeneralDetalle)
        {
            try
            {
                StringDTO resultadoUrl = null;

                string spReporte = "[mkt].[SP_ObtenerUrlFormularioPublicidadMailing]";
                string resultadoReporte = _dapperRepository.QuerySPFirstOrDefault(spReporte, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    resultadoUrl = JsonConvert.DeserializeObject<StringDTO>(resultadoReporte);
                }

                return resultadoUrl != null ? resultadoUrl.Valor : string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene Las Campanias generales a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadCampaniaGeneralParaEjecutarDTO</returns>
        public List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar()
        {
            try
            {
                #region Captura del tiempo
                var horaActual = DateTime.Now;

                string fechaEnvio = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = string.Empty;

                minutoActual = horaActual.Minute.ToString().Length == 1 ? minutoActual = "0" + horaActual.Minute : minutoActual = horaActual.Minute.ToString();
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

                var listaCampaniaGeneralWhatsapp = new List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>();
                string query = "com.SP_CampaniaGeneral_ParaEjecutarWhatsapp";
                var resultadoListaWhatsApp = _dapperRepository.QuerySPDapper(query, new { fechaEnvio, horaEnvio });
                if (!string.IsNullOrEmpty(resultadoListaWhatsApp) && !resultadoListaWhatsApp.Contains("[]"))
                    listaCampaniaGeneralWhatsapp = JsonConvert.DeserializeObject<List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>>(resultadoListaWhatsApp);

                return listaCampaniaGeneralWhatsapp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public TCampaniaGeneral FirstBy(Expression<Func<TCampaniaGeneral, bool>> filter)
        {
            try
            {
               return base.FirstBy(filter);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
