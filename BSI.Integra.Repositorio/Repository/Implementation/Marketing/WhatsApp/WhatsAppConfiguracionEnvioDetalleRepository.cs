using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WhatsAppConfiguracionEnvioDetalleRepository
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_WhatsAppConfiguracionEnvioDetalle
    /// </summary>
    public class WhatsAppConfiguracionEnvioDetalleRepository : GenericRepository<TWhatsAppConfiguracionEnvioDetalle>, IWhatsAppConfiguracionEnvioDetalleRepository
    {
        private Mapper _mapper;

        public WhatsAppConfiguracionEnvioDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionEnvioDetalle, WhatsAppConfiguracionEnvioDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TWhatsAppConfiguracionEnvioDetalle MapeoEntidad(WhatsAppConfiguracionEnvioDetalleDTO entidad)
        {
            try
            {
     
                TWhatsAppConfiguracionEnvioDetalle modelo = _mapper.Map<TWhatsAppConfiguracionEnvioDetalle>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionEnvioDetalle Add(WhatsAppConfiguracionEnvioDetalleDTO entidad)
        {
            try
            {
                var WhatsAppConfiguracionEnvioDetalle = MapeoEntidad(entidad);
                base.Insert(WhatsAppConfiguracionEnvioDetalle);
                return WhatsAppConfiguracionEnvioDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TWhatsAppConfiguracionEnvioDetalle Update(WhatsAppConfiguracionEnvioDetalleDTO entidad)
        {
            try
            {
                var WhatsAppConfiguracionEnvioDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                WhatsAppConfiguracionEnvioDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(WhatsAppConfiguracionEnvioDetalle);
                return WhatsAppConfiguracionEnvioDetalle;
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


        public IEnumerable<TWhatsAppConfiguracionEnvioDetalle> Add(IEnumerable<WhatsAppConfiguracionEnvioDetalleDTO> listadoEntidad)
        {
            try
            {
                List<TWhatsAppConfiguracionEnvioDetalle> listado = new List<TWhatsAppConfiguracionEnvioDetalle>();
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

        public IEnumerable<TWhatsAppConfiguracionEnvioDetalle> Update(IEnumerable<WhatsAppConfiguracionEnvioDetalleDTO> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TWhatsAppConfiguracionEnvioDetalle> listado = new List<TWhatsAppConfiguracionEnvioDetalle>();
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


        public MensajesWhatsAppRespondidosDTO ObtenerMensajesWhatsAppRespondidos(FiltroMensajesWhatsAppRespondidosDTO filtro)
        {
            try
            {
                string nombreCentroCosto = string.Empty;
                string programa = string.Empty;
                string nombreAlumno = string.Empty;
                string celular = string.Empty;
                string asesor = string.Empty;
                string ultimoMensaje = string.Empty;
                DateTime fecha = DateTime.Now;
                bool filtroFecha = false;
                bool seleccionada = false;
                bool noSeleccionada = false;

                if (filtro.ListaEstadoCreacionOportunidad.Count > 0)
                {
                    foreach (var item in filtro.ListaEstadoCreacionOportunidad)
                    {
                        if (item.Valor == 2) seleccionada = true;
                        else if (item.Valor == 1) noSeleccionada = true;
                    }
                }

                if (filtro.FiltroKendo != null)
                {
                    foreach (var item in filtro.FiltroKendo.Filters)
                    {
                        switch (item.Field)
                        {
                            case "NombreCentroCosto":
                                nombreCentroCosto = item.Value;
                                break;
                            case "NombrePrograma":
                                programa = item.Value;
                                break;
                            case "NombreAlumno":
                                nombreAlumno = item.Value;
                                break;
                            case "Celular":
                                celular = item.Value;
                                break;
                            case "NombrePersonal":
                                asesor = item.Value;
                                break;
                            case "UltimoMensajeRecibido":
                                ultimoMensaje = item.Value;
                                break;
                            case "FechaUltimoMensaje":
                                filtroFecha = true;
                                fecha = Convert.ToDateTime(item.Value);
                                break;
                        }
                    }
                }

                var correosGmail = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerMensajesWhatsAppRespondidos",
                    new
                    {
                        Skip = filtro.Skip,
                        Take = filtro.Take,
                        NombreCentroCosto = nombreCentroCosto,
                        Programa = programa,
                        NombreAlumno = nombreAlumno,
                        Celular = celular,
                        Asesor = asesor,
                        Fecha = fecha,
                        UltimoMensajeRecibido = ultimoMensaje,
                        FiltroFecha = filtroFecha,
                        Seleccionada = seleccionada,
                        NoSeleccionada = noSeleccionada,
                        BandejaRespondidos = filtro.BandejaRespondidos
                    });

                MensajesWhatsAppRespondidosDTO mensajesWhatsAppRespondidosDTO = new MensajesWhatsAppRespondidosDTO();
                if (!string.IsNullOrEmpty(correosGmail) && correosGmail != "[]")
                {
                    mensajesWhatsAppRespondidosDTO.ListaMensajesWhatsAppRespondidos = JsonConvert.DeserializeObject<List<ResumenMensajesWhatsAppRespondidosDTO>>(correosGmail);

                    var cantidad = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ObtenerMensajesWhatsAppRespondidosCantidad",
                    new
                    {
                        Skip = filtro.Skip,
                        Take = filtro.Take,
                        NombreCentroCosto = nombreCentroCosto,
                        Programa = programa,
                        NombreAlumno = nombreAlumno,
                        Celular = celular,
                        Asesor = asesor,
                        Fecha = fecha,
                        UltimoMensajeRecibido = ultimoMensaje,
                        FiltroFecha = filtroFecha,
                        Seleccionada = seleccionada,
                        NoSeleccionada = noSeleccionada,
                        BandejaRespondidos = filtro.BandejaRespondidos
                    });
                    var diccionario = JsonConvert.DeserializeObject<Dictionary<string, int>>(cantidad);

                    mensajesWhatsAppRespondidosDTO.Total = diccionario.Select(x => x.Value).FirstOrDefault();

                    List<PalabrasOfensivasDTO> ListaPalabrasOfensivas = new List<PalabrasOfensivasDTO>();
                    string query = string.Empty;
                    query = "SELECT PalabraFiltrada FROM mkt.T_DiccionarioPalabraOfensiva";
                    var respuesta = _dapperRepository.QueryDapper(query, null);
                    ListaPalabrasOfensivas = JsonConvert.DeserializeObject<List<PalabrasOfensivasDTO>>(respuesta);

                    List<ResumenMensajesWhatsAppRespondidosDTO> ListaMensajesWhatsAppRespondidosActualizada = new List<ResumenMensajesWhatsAppRespondidosDTO>();
                    string palabraOfensivaComparar = "";
                    string armarPalabraSensurada = "";

                    foreach (ResumenMensajesWhatsAppRespondidosDTO alumnoDatos in mensajesWhatsAppRespondidosDTO.ListaMensajesWhatsAppRespondidos)
                    {
                        string UltimoMensaje = alumnoDatos.UltimoMensajeRecibido;
                        if (UltimoMensaje != null)
                        {
                            UltimoMensaje = alumnoDatos.UltimoMensajeRecibido?.ToLower();
                            foreach (var palabraOfensiva in ListaPalabrasOfensivas)
                            {
                                palabraOfensivaComparar = palabraOfensiva.PalabraFiltrada?.ToString();
                                palabraOfensivaComparar = palabraOfensiva.PalabraFiltrada?.ToLower();
                                if (UltimoMensaje.Contains(palabraOfensivaComparar))
                                {
                                    armarPalabraSensurada = palabraOfensivaComparar.Substring(0, 1);
                                    int switCaracter = 0;
                                    for (int i = 0; i < palabraOfensivaComparar.Length - 1; i++)
                                    {
                                        if (switCaracter < palabraOfensivaComparar.Length - 1)
                                        {
                                            switCaracter++;
                                            armarPalabraSensurada = armarPalabraSensurada + "#";
                                        }
                                        if (switCaracter < palabraOfensivaComparar.Length - 1)
                                        {
                                            switCaracter++;
                                            armarPalabraSensurada = armarPalabraSensurada + "&";
                                        }
                                        if (switCaracter < palabraOfensivaComparar.Length - 1)
                                        {
                                            switCaracter++;
                                            armarPalabraSensurada = armarPalabraSensurada + "%";
                                        }
                                        if (switCaracter < palabraOfensivaComparar.Length - 1)
                                        {
                                            switCaracter++;
                                            armarPalabraSensurada = armarPalabraSensurada + "$";
                                        }
                                        if (switCaracter < palabraOfensivaComparar.Length - 1)
                                        {
                                            switCaracter++;
                                            armarPalabraSensurada = armarPalabraSensurada + "%";
                                        }
                                    }
                                    UltimoMensaje = UltimoMensaje.Replace(palabraOfensivaComparar, armarPalabraSensurada);
                                }
                            }
                            alumnoDatos.UltimoMensajeRecibido = UltimoMensaje;
                        }
                        else
                        {
                            alumnoDatos.UltimoMensajeRecibido = UltimoMensaje;
                        }
                        ListaMensajesWhatsAppRespondidosActualizada.Add(alumnoDatos);
                    }
                    mensajesWhatsAppRespondidosDTO.ListaMensajesWhatsAppRespondidos.Clear();
                    mensajesWhatsAppRespondidosDTO.ListaMensajesWhatsAppRespondidos.AddRange(ListaMensajesWhatsAppRespondidosActualizada);
                    return mensajesWhatsAppRespondidosDTO;
                }
                mensajesWhatsAppRespondidosDTO.Total = 0;

                return mensajesWhatsAppRespondidosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta en mkt.T_WhatsAppConfiguracionEnvioDetalle
        /// </summary>
        /// <param name="filtro">Objeto de tipo WhatsAppConfiguracionEnvioDetalleBO</param>
        /// <returns>Id de la transaccion</returns>
        public int InsertarWhatsAppConfiguracionEnvioDetalle(WhatsAppConfiguracionEnvioDetalle filtro)
        {
            var resultado = new ValorIntDTO();

            string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionEnvioDetalle]";

            var query = _dapperRepository.QuerySPFirstOrDefault(spQuery, new
            {
                filtro.IdWhatsAppConfiguracionLogEjecucion,
                filtro.EnviadoCorrectamente,
                filtro.MensajeError,
                filtro.IdConjuntoListaResultado,
                filtro.ConjuntoListaNroEjecucion,
                filtro.Mensaje,
                filtro.WhatsAppId,
                filtro.UsuarioCreacion,
                filtro.UsuarioModificacion
            });

            if (!string.IsNullOrEmpty(query))
            {
                resultado = JsonConvert.DeserializeObject<ValorIntDTO>(query);
            }

            return (int)resultado.Valor;
        }

        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtro)
        {
            try
            {
                List<ObtenerReporteMensajesWhatsAppPorTipoDTO> items = new List<ObtenerReporteMensajesWhatsAppPorTipoDTO>();

                var query = _dapperRepository.QuerySPDapper("mkt.SP_ReporteMensajesWhatsAppPlantillas", new
                {
                    fechaInicio = filtro.FechaInicio,
                    fechaFin = filtro.FechaFin

                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ObtenerReporteMensajesWhatsAppPorTipoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivos(ReporteMensajesWhatsAppFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapperRepository.QuerySPDapper("mkt.SP_GenerarReporteWhatsAppMasivo", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin

                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosPorArea(ReporteMensajesWhatsAppPorAreaFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapperRepository.QuerySPDapper("mkt.SP_GenerarReporteWhatsAppMasivoPorArea", new
                {
                    FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0),
                    FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59),
                    Area = filtro.IdArea
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);

                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtro)
        {
            try
            {
                List<ReporteWhatsAppEnvioMasivoDTO> items = new List<ReporteWhatsAppEnvioMasivoDTO>();

                var query = _dapperRepository.QuerySPDapper("mkt.SP_ObtenerWhatsAppMasivoConjuntoLista", new
                {
                    FechaInicio = filtro.FechaInicio,
                    FechaFin = filtro.FechaFin,
                    IdPersonal = filtro.IdPersonal,
                    IdPais = filtro.IdPais
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<ReporteWhatsAppEnvioMasivoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }

    }
}
