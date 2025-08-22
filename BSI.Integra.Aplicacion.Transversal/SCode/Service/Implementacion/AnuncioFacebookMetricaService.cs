using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    /// 
    public class AnuncioFacebookMetricaService : IAnuncioFacebookMetricaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AnuncioFacebookMetricaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAnuncioFacebookMetrica, AnuncioFacebookMetrica>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCampaniaFacebook, CampaniaFacebook>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConjuntoAnuncioFacebook, ConjuntoAnuncioFacebook>(MemberList.None).ReverseMap();
                cfg.CreateMap<AnuncioFacebook, TAnuncioFacebook>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public AnuncioFacebookMetrica Add(AnuncioFacebookMetrica entidad)
        {
            try
            {
                var modelo = _unitOfWork.AnuncioFacebookMetricaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AnuncioFacebookMetrica>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AnuncioFacebookMetrica Update(AnuncioFacebookMetrica entidad)
        {
            try
            {
                var modelo = _unitOfWork.AnuncioFacebookMetricaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AnuncioFacebookMetrica>(modelo);
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
                _unitOfWork.AnuncioFacebookMetricaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnuncioFacebookMetrica> Add(List<AnuncioFacebookMetrica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AnuncioFacebookMetricaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AnuncioFacebookMetrica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnuncioFacebookMetrica> Update(List<AnuncioFacebookMetrica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AnuncioFacebookMetricaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AnuncioFacebookMetrica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.AnuncioFacebookMetricaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene y estrucutra el reporte de anuncio de Cacebook
        /// </summary>
        /// <param name="idAreaCapacitacion">Id del area de capacitacion (PK de la tabla mkt.T_AreaCapacitacion)</param>
        /// <returns>Lsita de objetos de clase ReporteAnuncioFacebookMetricaDiasDTO</returns>
        public List<ReporteAnuncioFacebookMetricaDiasDTO> EstructurarReporteAnuncioFacebook(int? idAreaCapacitacion)
        {
            try
            {
                var _repAnuncioFacebookMetrica = _unitOfWork.AnuncioFacebookMetricaRepository;
                List<ReporteAnuncioFacebookMetricaDTO> listaReporteAnuncioFacebookMetrica = _repAnuncioFacebookMetrica.ObtenerReporteAnuncioFacebookMetrica(idAreaCapacitacion);

                DateTime fechaActual = DateTime.Now.Date;
                DateTime fechaUnDia = DateTime.Now.AddDays(-1).Date;
                DateTime fechaTresDias = DateTime.Now.AddDays(-3).Date;
                DateTime fechaSieteDias = DateTime.Now.AddDays(-7).Date;

                List<int> listaIdFacebookAnuncio = listaReporteAnuncioFacebookMetrica.Select(s => s.IdFacebookAnuncio).Distinct().ToList();

                List<ReporteAnuncioFacebookMetricaDiasDTO> listaUnicaCrudaDias = new List<ReporteAnuncioFacebookMetricaDiasDTO>();

                // Actual
                var listaActual = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta == fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.FacebookIdConjuntoAnuncio,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio,

                    }).ToList();

                // Un dia
                var listaUnDia = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaUnDia && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.IdGrupoFiltroProgramaCritico,
                        g.FacebookIdConjuntoAnuncio,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio,



                    }).ToList();

                // Tres dias
                var listaTresDias = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaTresDias && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.FacebookIdConjuntoAnuncio,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio,

                    }).ToList();

                // Siete dias
                var listaSieteDias = listaReporteAnuncioFacebookMetrica
                    .Where(w => w.FechaConsulta >= fechaSieteDias && w.FechaConsulta < fechaActual)
                    .GroupBy(g => new
                    {
                        g.IdCampaniaFacebook,
                        g.FacebookIdConjuntoAnuncio,
                        g.IdGrupoFiltroProgramaCritico,
                        g.NombreGrupoFiltroProgramaCritico,
                        g.FacebookNombreCampania,
                        g.IdFacebookConjuntoAnuncio,
                        g.FacebookNombreConjuntoAnuncio,
                        g.PresupuestoDiarioConjuntoAnuncio,
                        g.IdFacebookAnuncio,
                        g.FacebookIdAnuncio,
                        g.FacebookNombreAnuncio,


                    }).ToList();

                foreach (int idAnuncio in listaIdFacebookAnuncio)
                {
                    ReporteAnuncioFacebookMetricaDiasDTO conjuntoAnuncio = listaReporteAnuncioFacebookMetrica
                        .Where(w => w.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new ReporteAnuncioFacebookMetricaDiasDTO
                        {
                            IdCampaniaFacebook = s.IdCampaniaFacebook,
                            FacebookIdConjuntoAnuncio = s.FacebookIdConjuntoAnuncio,
                            IdGrupoFiltroProgramaCritico = s.IdGrupoFiltroProgramaCritico,
                            NombreGrupoFiltroProgramaCritico = s.NombreGrupoFiltroProgramaCritico,
                            FacebookNombreCampania = s.FacebookNombreCampania,
                            IdFacebookConjuntoAnuncio = s.IdFacebookConjuntoAnuncio,
                            FacebookNombreConjuntoAnuncio = s.FacebookNombreConjuntoAnuncio,
                            PresupuestoDiarioConjuntoAnuncio = s.PresupuestoDiarioConjuntoAnuncio,
                            IdFacebookAnuncio = s.IdFacebookAnuncio,
                            FacebookIdAnuncio = s.FacebookIdAnuncio,
                            FacebookNombreAnuncio = s.FacebookNombreAnuncio,


                        }).First();

                    List<MetricaIndividualDTO> metricaTemporal = new List<MetricaIndividualDTO>();

                    int suma = 0;
                    var resultado = new Decimal(suma);

                    metricaTemporal = listaActual
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = agrupar(s).Sum(op => op.Gasto),
                            Impresiones = agrupar(s).Sum(op => op.Impresiones),
                            CostoPorMil = agrupar(s).Sum(op => op.Impresiones) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / (agrupar(s).Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = agrupar(s).Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            //Registros = s.Sum(op => op.Registros),
                            Registros = agrupar(s).Sum(op => op.Registros),
                            //Registros = s.GroupBy(op => op.IdFacebookAnuncio).Select(op => op.FirstOrDefault()).Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = agrupar(s).Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / agrupar(s).Sum(op => op.RegistrosMuyAlta),
                            RangoA = s.Where(x => x.ConfiguracionABC == "A").Sum(op => op.RegistrosMuyAlta),
                            RangoB = s.Where(x => x.ConfiguracionABC == "B").Sum(op => op.RegistrosMuyAlta),
                            RangoC = s.Where(x => x.ConfiguracionABC == "C").Sum(op => op.RegistrosMuyAlta),




                            cantidad = s.Count(),
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.Actual = metricaTemporal.First();
                    metricaTemporal.Clear();

                    metricaTemporal = listaUnDia
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = agrupar(s).Sum(op => op.Gasto),
                            Impresiones = agrupar(s).Sum(op => op.Impresiones),
                            CostoPorMil = agrupar(s).Sum(op => op.Impresiones) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / (agrupar(s).Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = agrupar(s).Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = agrupar(s).Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = agrupar(s).Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / agrupar(s).Sum(op => op.RegistrosMuyAlta),
                            //ConfiguracionABC = s.FirstOrDefault().ConfiguracionABC,
                            RangoA = s.Where(x => x.ConfiguracionABC == "A").Sum(op => op.RegistrosMuyAlta),
                            RangoB = s.Where(x => x.ConfiguracionABC == "B").Sum(op => op.RegistrosMuyAlta),
                            RangoC = s.Where(x => x.ConfiguracionABC == "C").Sum(op => op.RegistrosMuyAlta),



                            cantidad = s.Count(),

                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.UnDia = metricaTemporal.First();
                    metricaTemporal.Clear();

                    metricaTemporal = listaTresDias
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = agrupar(s).Sum(op => op.Gasto),
                            Impresiones = agrupar(s).Sum(op => op.Impresiones),
                            CostoPorMil = agrupar(s).Sum(op => op.Impresiones) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / (agrupar(s).Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = agrupar(s).Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = agrupar(s).Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = agrupar(s).Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / agrupar(s).Sum(op => op.RegistrosMuyAlta),
                            RangoA = s.Where(x => x.ConfiguracionABC == "A").Sum(op => op.RegistrosMuyAlta),
                            RangoB = s.Where(x => x.ConfiguracionABC == "B").Sum(op => op.RegistrosMuyAlta),
                            RangoC = s.Where(x => x.ConfiguracionABC == "C").Sum(op => op.RegistrosMuyAlta),


                            cantidad = s.Count(),
                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.TresDias = metricaTemporal.First();
                    metricaTemporal.Clear();
                      
                    metricaTemporal = listaSieteDias
                        .Where(w => w.Key.IdFacebookAnuncio == idAnuncio)
                        .Select(s => new MetricaIndividualDTO
                        {
                            Gasto = agrupar(s).Sum(op => op.Gasto),
                            Impresiones = agrupar(s).Sum(op => op.Impresiones),
                            CostoPorMil = agrupar(s).Sum(op => op.Impresiones) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / (agrupar(s).Sum(op => op.Impresiones) * 1.0 / 1000),
                            CantidadClics = agrupar(s).Sum(op => op.CantidadClics),
                            ImpresionesPorClic = s.Sum(op => op.CantidadClics) == 0 ? 0 : s.Sum(op => op.Impresiones * 1.0) / s.Sum(op => op.CantidadClics * 1.0),
                            Registros = agrupar(s).Sum(op => op.Registros),
                            ClicPorRegistro = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.Registros * 1.0),
                            RegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta),
                            PorcentajeRegistrosMuyAlta = s.Sum(op => op.Registros) == 0 ? 0 : s.Sum(op => op.RegistrosMuyAlta * 1.0) / s.Sum(op => op.Registros * 1.0),
                            ClicsRegistrosMuyAlta = s.Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : s.Sum(op => op.CantidadClics * 1.0) / s.Sum(op => op.RegistrosMuyAlta * 1.0),
                            GastoPorRegistrosMuyAlta = agrupar(s).Sum(op => op.RegistrosMuyAlta) == 0 ? 0 : agrupar(s).Sum(op => op.Gasto) / agrupar(s).Sum(op => op.RegistrosMuyAlta),

                            RangoA = s.Where(x => x.ConfiguracionABC == "A").Sum(op => op.RegistrosMuyAlta),
                            RangoB = s.Where(x => x.ConfiguracionABC == "B").Sum(op => op.RegistrosMuyAlta),
                            RangoC = s.Where(x => x.ConfiguracionABC == "C").Sum(op => op.RegistrosMuyAlta),


                            cantidad = s.Count(),

                        }).ToList();

                    if (!metricaTemporal.Any()) metricaTemporal.Add(new MetricaIndividualDTO());
                    conjuntoAnuncio.SieteDias = metricaTemporal.First();
                    metricaTemporal.Clear();

                    listaUnicaCrudaDias.Add(conjuntoAnuncio);
                }

                return listaUnicaCrudaDias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<ReporteAnuncioFacebookMetricaDTO> agrupar(IEnumerable<ReporteAnuncioFacebookMetricaDTO> s)
        {
            var ss = s.GroupBy(op => op.IdFacebookMetrica).Select(op => op.FirstOrDefault());
            return ss;
        }
        //private List<ReporteAnuncioFacebookMetricaDTO> agrupar(IEnumerable<ReporteAnuncioFacebookMetricaDTO> s)
        //{
        //    var ss = s.GroupBy(op => new { op.Gasto, op.Impresiones, op.Registros, op.RegistrosMuyAlta }).Select(x => s.Where(a => a.Gasto == x.Key.Gasto && a.Impresiones == x.Key.Impresiones && a.Registros == x.Key.Registros).FirstOrDefault()).ToList();
        //    return ss;
        //}


        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0 <summary>
        /// Autor: Margiory Ramirez .// <summary>
        /// Actualiza las metricas de Facebook por intervalo de fecha
        /// </summary>
        /// <param ></param>
        /// <returns>Obtiene informacion de Registros</returns>
        /// </summary>
        /// <returns></returns>
        public object ObtenerInformacionBasica()
        {

            try
            {
                var _repAnuncioFacebookMetrica = _unitOfWork.AnuncioFacebookMetricaRepository;
                var resultado = new
                {
                    UltimaModificacion = _repAnuncioFacebookMetrica.ObtenerUltimaModificacion()
                };

                return (JsonConvert.SerializeObject(resultado));
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }

        public object ObtenerCombosAnuncioFacebookMetrica()
        {

            try
            {
                var _repAnuncioFacebookMetrica = _unitOfWork.AnuncioFacebookMetricaRepository;
                var resultado = new
                {
                    ListaArea = _repAnuncioFacebookMetrica.ObtenerComboAreaAnuncioFacebookMetrica()
                };

                return resultado;
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }


        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza las metricas de Facebook por intervalo de fecha
        /// </summary>
        /// <param name="FechaFiltroDescarga">Objeto de clase AnuncioFacebookMetricaFechaDTO</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con el mensaje de error</returns>

        public object ActualizarMetricaFacebookAnuncioPorIntervaloFecha(AnuncioFacebookMetricaFechaDTO FechaFiltroDescarga)
        {

            try
            {
                //IntegraAspNetUsersRepositorio _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
                var _repIntegraAspNetUsers = _unitOfWork.IntegraAspNetUserRepository;
                if (!_repIntegraAspNetUsers.ExistePorNombreUsuario(FechaFiltroDescarga.Usuario))
                {
                    return ("El usuario no existe");
                }

                string usuarioResponsable = _repIntegraAspNetUsers.ObtenerEmailPorNombreUsuario(FechaFiltroDescarga.Usuario);

                List<string> correosCopia = new List<string>
                {
                    "mramirez@bsginstitute.com"
                };

                var facebook = new FacebookService(_unitOfWork);
                DateTime fechaInicio = DateTime.Now;
                string cadenaFechaInicio = FechaFiltroDescarga.FechaInicio.ToString("yyyy-MM-dd");
                string cadenaFechaFinal = FechaFiltroDescarga.FechaFin.ToString("yyyy-MM-dd");

                List<AnuncioFacebookMetricaDTO> resultado = facebook.DescargarMetricaFacebookAnuncio(cadenaFechaInicio, cadenaFechaFinal);
                if (resultado.Count() > 0)
                {
                    bool resultadoActualizacion = ActualizarMetricaIntegra(resultado, FechaFiltroDescarga.Usuario);
                }

                DateTime fechaFin = DateTime.Now;

                TMK_MailService mailservicePersonalizado = new TMK_MailService();
                TMKMailDataDTO contenidoMailing = new TMKMailDataDTO
                {
                    Sender = "mramirez@bsginstitute.com",
                    Recipient = usuarioResponsable,
                    Subject = $"Actualización de datos de la fecha {cadenaFechaInicio}",
                    Message = EstructurarMensajeAnuncioFacebook(cadenaFechaInicio, fechaInicio, fechaFin),
                    Cc = string.Empty,
                    Bcc = string.Join(",", correosCopia),
                    AttachedFiles = null
                };

                mailservicePersonalizado.SetData(contenidoMailing);
                mailservicePersonalizado.SendMessageTask();

                return (true);
            }
            catch (Exception ex)
            {
                return (ex);
            }
        }


        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool ActualizarMetricaIntegra(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario)
        {
            try
            {
                var _repAnuncioFacebook = _unitOfWork.AnuncioFacebookRepository;
                var _repAnuncioFacebookMetrica = _unitOfWork.AnuncioFacebookMetricaRepository;
                // FacebookBO facebook = new FacebookBO();

                List<AnuncioFacebookMetrica> listaAInsertar = new List<AnuncioFacebookMetrica>();
                bool resultadoEliminado = _repAnuncioFacebookMetrica.EliminarDatosPorFecha(DateTime.Parse(datosActualizar[0].date_start), usuario);

                bool resultadoRegularizacion = RegularizarJerarquiaFacebook(datosActualizar, usuario);

                List<TAnuncioFacebook> anuncioFacebookAInsertar = _repAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.ad_id).Contains(x.FacebookIdAnuncio)).ToList();

                foreach (var anuncioAInsertar in datosActualizar)
                {
                    try
                    {
                        if (anuncioAInsertar != null)
                            listaAInsertar.Add(MapeoAnuncioFacebookMetricaDTOBO(anuncioAInsertar, anuncioFacebookAInsertar.Where(w => w.FacebookIdAnuncio == anuncioAInsertar.ad_id).First().Id, usuario));
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                var DatosMapeadosAdto = _mapper.Map<List<TAnuncioFacebookMetrica>>(listaAInsertar);
                bool resultadoInsercion = _repAnuncioFacebookMetrica.Insert(DatosMapeadosAdto);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool RegularizarJerarquiaFacebook(List<AnuncioFacebookMetricaDTO> datosActualizar, string usuario)
        {
            try
            {
                //FacebookBO facebook = new FacebookBO();
                var _repCampaniaFacebook = _unitOfWork.CampaniaFacebookRepository;
                var _repConjuntoAnuncioFacebook = _unitOfWork.ConjuntoAnuncioFacebookRepository;
                var _repAnuncioFacebook = _unitOfWork.AnuncioFacebookRepository;
                var facebook = new FacebookService(_unitOfWork);

                List<string> campaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => ss.FacebookIdCampania).ToList();
                List<FacebookIntegraIdDTO> conjuntoAnuncioFacebookIntegra = _repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.IdAnuncioFacebook, NombreFacebook = ss.Name, IdCampaniaFacebook = ss.IdCampaniaFacebook }).ToList();
                List<string> anuncioFacebookIntegra = _repAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.ad_id).Contains(x.FacebookIdAnuncio)).Select(ss => ss.FacebookIdAnuncio).ToList();

                List<string> facebookCampaniaFaltante = datosActualizar.Where(w => !campaniaFacebookIntegra.Contains(w.campaign_id)).Select(ss => ss.campaign_id).Distinct().ToList();
                List<string> facebookConjuntoAnuncioFaltante = datosActualizar.Where(w => !conjuntoAnuncioFacebookIntegra.Select(s => s.IdFacebook).Contains(w.adset_id)).Select(ss => ss.adset_id).Distinct().ToList();
                List<string> facebookAnuncioFaltante = datosActualizar.Where(w => !anuncioFacebookIntegra.Contains(w.ad_id)).Select(ss => ss.ad_id).Distinct().ToList();

                if (facebookCampaniaFaltante.Any())
                {

                    List<CampaniaFacebookDTO> resultadoCampaniaFacebook = facebook.DescargarCampaniayPadre(facebookCampaniaFaltante).ToList();

                    // Verificacion de conjunto anuncio Facebook
                    List<CampaniaFacebook> listaFacebookCampaniaRegularizado = new List<CampaniaFacebook>();

                    foreach (var campaniaFacebook in resultadoCampaniaFacebook)
                    {
                        listaFacebookCampaniaRegularizado.Add(MapeoCampaniaFacebookDTOBO(campaniaFacebook));
                    }

                    bool resultadoInsercion = _repCampaniaFacebook.Insert(_mapper.Map<List<TCampaniaFacebook>>(listaFacebookCampaniaRegularizado));

                    _unitOfWork.Commit();
                }

                if (facebookConjuntoAnuncioFaltante.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdCampaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.FacebookIdCampania, NombreFacebook = ss.FacebookNombreCampania }).Distinct().ToList();
                    List<ConjuntoAnuncioFacebookJerarquiaDTO> resultadoConjuntoAnuncioFacebook = facebook.DescargarConjuntoAnuncioyPadre(facebookConjuntoAnuncioFaltante).ToList();

                    // Verificacion de conjunto anuncio Facebook
                    List<ConjuntoAnuncioFacebook> listaFacebookConjuntoAnuncioRegularizado = new List<ConjuntoAnuncioFacebook>();

                    foreach (var conjuntoAnuncioFacebook in resultadoConjuntoAnuncioFacebook)
                    {
                        var dataf = listaIdCampaniaFacebookIntegra.FirstOrDefault(w => w.IdFacebook == conjuntoAnuncioFacebook.campaign_id);
                        if (dataf != null)
                        {
                            var idIntegra = dataf.IdIntegra;
                            var NombreFaecbook = dataf.NombreFacebook;
                            listaFacebookConjuntoAnuncioRegularizado.Add(
                                MapeoConjuntoAnuncioFacebookDTOBO(conjuntoAnuncioFacebook, idIntegra
                                , NombreFaecbook));
                        }
                    }

                    bool resultadoInsercion = _repConjuntoAnuncioFacebook.Insert(_mapper.Map<List<TConjuntoAnuncioFacebook>>(listaFacebookConjuntoAnuncioRegularizado));
                    _unitOfWork.Commit();
                }

                // Regularizar informacion ConjuntoAnuncioFacebook
                List<ConjuntoAnuncioFacebook> listaConjuntoAnuncioFacebookIntegraRegularizar = _mapper.Map<List<ConjuntoAnuncioFacebook>>(_repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Distinct().ToList());

                if (listaConjuntoAnuncioFacebookIntegraRegularizar.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdCampaniaFacebookIntegra = _repCampaniaFacebook.GetBy(x => datosActualizar.Select(s => s.campaign_id).Contains(x.FacebookIdCampania)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.FacebookIdCampania, NombreFacebook = ss.FacebookNombreCampania }).Distinct().ToList();
                    List<ConjuntoAnuncioFacebookJerarquiaDTO> resultadoConjuntoAnuncioFacebookRegularizar = facebook.DescargarConjuntoAnuncioyPadre(listaConjuntoAnuncioFacebookIntegraRegularizar.Select(s => s.IdAnuncioFacebook).ToList()).ToList();

                    foreach (var conjuntoAnuncioFacebookIntegraRegularizar in listaConjuntoAnuncioFacebookIntegraRegularizar)
                    {
                        try
                        {
                            ConjuntoAnuncioFacebookJerarquiaDTO elementoResultadoJerarquia = resultadoConjuntoAnuncioFacebookRegularizar.Where(w => w.id == conjuntoAnuncioFacebookIntegraRegularizar.IdAnuncioFacebook).First();

                            conjuntoAnuncioFacebookIntegraRegularizar.Name = elementoResultadoJerarquia.name;
                            conjuntoAnuncioFacebookIntegraRegularizar.NombreCampania = listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == elementoResultadoJerarquia.campaign_id).First().NombreFacebook;
                            conjuntoAnuncioFacebookIntegraRegularizar.OptimizationGoal = elementoResultadoJerarquia.optimization_goal;
                            conjuntoAnuncioFacebookIntegraRegularizar.CampaignId = elementoResultadoJerarquia.campaign_id;
                            conjuntoAnuncioFacebookIntegraRegularizar.BillinEevent = elementoResultadoJerarquia.billing_event;
                            conjuntoAnuncioFacebookIntegraRegularizar.IdCampaniaFacebook = listaIdCampaniaFacebookIntegra.Where(w => w.IdFacebook == elementoResultadoJerarquia.campaign_id).First().IdIntegra;
                            conjuntoAnuncioFacebookIntegraRegularizar.Status = elementoResultadoJerarquia.status;
                            conjuntoAnuncioFacebookIntegraRegularizar.EffectiveStatus = elementoResultadoJerarquia.effective_status;
                            conjuntoAnuncioFacebookIntegraRegularizar.ConfiguredStatus = elementoResultadoJerarquia.configured_status;
                            conjuntoAnuncioFacebookIntegraRegularizar.UsuarioModificacion = usuario;
                            conjuntoAnuncioFacebookIntegraRegularizar.FechaModificacion = DateTime.Now;

                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.daily_budget)) conjuntoAnuncioFacebookIntegraRegularizar.DailyBudget = int.Parse(elementoResultadoJerarquia.daily_budget);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.start_time)) conjuntoAnuncioFacebookIntegraRegularizar.StartTime = DateTime.Parse(elementoResultadoJerarquia.start_time);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.budget_remaining)) conjuntoAnuncioFacebookIntegraRegularizar.BudgetRemaining = double.Parse(elementoResultadoJerarquia.budget_remaining);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.updated_time)) conjuntoAnuncioFacebookIntegraRegularizar.UpdatedTime = DateTime.Parse(elementoResultadoJerarquia.updated_time);
                            if (!string.IsNullOrEmpty(elementoResultadoJerarquia.created_time)) conjuntoAnuncioFacebookIntegraRegularizar.CreatedTime = DateTime.Parse(elementoResultadoJerarquia.created_time);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    var data = _mapper.Map<IEnumerable<TConjuntoAnuncioFacebook>>(listaConjuntoAnuncioFacebookIntegraRegularizar);

                    var resultadoActualizacion = _repConjuntoAnuncioFacebook.Update(data);
                    _unitOfWork.Commit();
                }

                if (facebookAnuncioFaltante.Any())
                {
                    List<FacebookIntegraIdDTO> listaIdConjuntoAnuncioFacebookIntegra = _repConjuntoAnuncioFacebook.GetBy(x => datosActualizar.Select(s => s.adset_id).Contains(x.IdAnuncioFacebook)).Select(ss => new FacebookIntegraIdDTO { IdIntegra = ss.Id, IdFacebook = ss.IdAnuncioFacebook, NombreFacebook = ss.Name }).ToList();
                    List<AnuncioFacebookDTO> resultadoAnuncioFacebook = facebook.DescargarAnuncioyPadre(facebookAnuncioFaltante).ToList();

                    // Verificacion de anuncio Facebook
                    List<AnuncioFacebook> listaFacebookAnuncioRegularizado = new List<AnuncioFacebook>();

                    foreach (var anuncioFacebook in resultadoAnuncioFacebook)
                    {
                        var datafiltrada = listaIdConjuntoAnuncioFacebookIntegra.Where(w => w.IdFacebook == anuncioFacebook.adset_id).First();
                        if (datafiltrada != null)
                            listaFacebookAnuncioRegularizado.Add(MapeoAnuncioFacebookDTOBO(anuncioFacebook, datafiltrada.IdIntegra));
                    }

                    bool resultadoInsercion = _repAnuncioFacebook.Insert(_mapper.Map<List<TAnuncioFacebook>>(listaFacebookAnuncioRegularizado));
                    _unitOfWork.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Mapeo de DTO a BO para campania Facebook
        /// </summary>
        /// <param name="campaniaFacebook">Objeto de clase CampaniaFacebookDTO</param>
        /// <returns>Objeto de clase CampaniaFacebookBO</returns>
        public CampaniaFacebook MapeoCampaniaFacebookDTOBO(CampaniaFacebookDTO campaniaFacebook)
        {
            try
            {
                CampaniaFacebook campaniaFacebookBO = new CampaniaFacebook
                {
                    FacebookIdCampania = campaniaFacebook.id,
                    FacebookNombreCampania = campaniaFacebook.name,
                    FacebookIdCuenta = campaniaFacebook.account_id,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return campaniaFacebookBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Mapeo de DTO a BO para conjunto anuncio Facebook
        /// </summary>
        /// <param name="conjuntoAnuncioFacebook">Objeto de clase ConjuntoAnuncioFacebookJerarquiaDTO</param>
        /// <param name="idCampaniaFacebook">Id de la campania Facebook (PK de la tabla mkt.T_CampaniaFacebook)</param>
        /// <param name="nombreCampaniaFacebook">Nombre de la campania Facebook</param>
        /// <returns>Objeto de clase CampaniaFacebookBO</returns>
        public ConjuntoAnuncioFacebook MapeoConjuntoAnuncioFacebookDTOBO(ConjuntoAnuncioFacebookJerarquiaDTO conjuntoAnuncioFacebook, int? idCampaniaFacebook, string? nombreCampaniaFacebook)
        {
            try
            {
                ConjuntoAnuncioFacebook conjuntoAnuncio = new ConjuntoAnuncioFacebook
                {
                    IdAnuncioFacebook = conjuntoAnuncioFacebook.id,
                    Name = conjuntoAnuncioFacebook.name,
                    NombreCampania = nombreCampaniaFacebook,
                    OptimizationGoal = conjuntoAnuncioFacebook.optimization_goal,
                    CampaignId = conjuntoAnuncioFacebook.campaign_id,
                    BillinEevent = conjuntoAnuncioFacebook.billing_event,
                    IdCampaniaFacebook = idCampaniaFacebook,
                    Status = conjuntoAnuncioFacebook.status,
                    EffectiveStatus = conjuntoAnuncioFacebook.effective_status,
                    ConfiguredStatus = conjuntoAnuncioFacebook.configured_status,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.daily_budget)) conjuntoAnuncio.DailyBudget = int.Parse(conjuntoAnuncioFacebook.daily_budget);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.start_time)) conjuntoAnuncio.StartTime = DateTime.Parse(conjuntoAnuncioFacebook.start_time);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.budget_remaining)) conjuntoAnuncio.BudgetRemaining = double.Parse(conjuntoAnuncioFacebook.budget_remaining);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.updated_time)) conjuntoAnuncio.UpdatedTime = DateTime.Parse(conjuntoAnuncioFacebook.updated_time);
                if (!string.IsNullOrEmpty(conjuntoAnuncioFacebook.created_time)) conjuntoAnuncio.CreatedTime = DateTime.Parse(conjuntoAnuncioFacebook.created_time);

                return conjuntoAnuncio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Mapeo de DTO a BO para anuncio Facebook metrica
        /// </summary>
        /// <param name="anuncioFacebook">Objeto de clase AnuncioFacebookDTO</param>
        /// <param name="idConjuntoAnuncioFacebook">Id del conjunto anuncio de Facebook (PK de la tabla mkt.T_ConjuntoAnuncioFacebook)</param>
        /// <returns>Objeto de clase AnuncioFacebookMetricaBO</returns>
        public AnuncioFacebook MapeoAnuncioFacebookDTOBO(AnuncioFacebookDTO anuncioFacebook, int idConjuntoAnuncioFacebook)
        {
            try
            {
                AnuncioFacebook anuncioFacebookBO = new AnuncioFacebook
                {
                    FacebookIdAnuncio = anuncioFacebook.id,
                    FacebookNombreAnuncio = anuncioFacebook.name,
                    FacebookIdConjuntoAnuncio = anuncioFacebook.adset_id,
                    IdConjuntoAnuncioFacebook = idConjuntoAnuncioFacebook,
                    Estado = true,
                    UsuarioCreacion = "Regularizacion metrica",
                    UsuarioModificacion = "Regularizacion metrica",
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return anuncioFacebookBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Mapeo de DTO a BO para anuncio Facebook metrica
        /// </summary>
        /// <param name="anuncioFacebookMetrica">Objeto de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="idAnuncioFacebook">Id del anuncio de Facebook (PK de la tabla mkt.T_AnuncioFacebook)</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>Objeto de clase AnuncioFacebookMetricaBO</returns>
        public AnuncioFacebookMetrica MapeoAnuncioFacebookMetricaDTOBO(AnuncioFacebookMetricaDTO anuncioFacebookMetrica, int idAnuncioFacebook, string usuario)
        {
            try
            {
                CultureInfo formatoDecimal = new System.Globalization.CultureInfo("en-US");


                //var _registro = ValorEstatico.Instance;

                AnuncioFacebookMetrica anuncioFacebookMetricaBO = new AnuncioFacebookMetrica()
                {
                    IdAnuncioFacebook = idAnuncioFacebook,
                    Gasto = decimal.Parse(anuncioFacebookMetrica.spend, formatoDecimal),
                    IdMoneda = ValorEstatico.IdMonedaDolares == 0 ? 19 : ValorEstatico.IdMonedaDolares,
                    Impresiones = int.Parse(anuncioFacebookMetrica.impressions ),
                    CantidadClicsUnicos = int.Parse(anuncioFacebookMetrica.unique_clicks),
                    CantidadClics = int.Parse(anuncioFacebookMetrica.clicks),
                    CantidadClicsEnlace = int.Parse(anuncioFacebookMetrica.inline_link_clicks ?? "0"),
                    Alcance = int.Parse(anuncioFacebookMetrica.reach),
                    FechaConsulta = DateTime.Parse(anuncioFacebookMetrica.date_start ),
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return anuncioFacebookMetricaBO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez .
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza la metrica en Integra, segun la DB de Facebook
        /// </summary>
        /// <param name="datosActualizar">Lista de objetos de clase AnuncioFacebookMetricaDTO</param>
        /// <param name="usuario">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public string EstructurarMensajeAnuncioFacebook(string cadenaFechaInicio, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente la actualización de los datos de Facebook:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>FECHA ACTUALIZADA:</h3>
                        <h3>{cadenaFechaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{fechaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{fechaFin}</h3>
                    </td>
                </tr>
            </table>";

                return texto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
