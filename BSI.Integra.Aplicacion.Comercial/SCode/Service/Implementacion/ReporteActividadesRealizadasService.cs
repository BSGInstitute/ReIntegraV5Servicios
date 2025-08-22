using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: ReporteActividadesRealizadasService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 14/09/2023
    /// <summary>
    /// Gestión general de la tabla T_ReporteActividadesRealizadas
    /// </summary>
    public class ReporteActividadesRealizadasService : IReporteActividadesRealizadasService
    {
        private IUnitOfWork _unitOfWork;
        //private Mapper _mapper;
        public ReporteActividadesRealizadasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //var config = new MapperConfiguration(
            //    cfg =>
            //    {
            //        cfg.CreateMap<TReporteActividadesRealizadas, ReporteActividadesRealizadas>(MemberList.None).ReverseMap();
            //    }
            //);
            //_mapper = new Mapper(config);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 30/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Actividades Realizadas
        /// </summary>
        /// <param name="filtro"> filtros de búsqueda </param>
        /// <returns> Información de Actividades Realizadas: List<ProcesadoDataActividadesRealizadasDTO> <returns>
        public List<ProcesadoDataActividadesRealizadasAlternoDTO> ReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO filtro)
        {
            try
            {
                var esActual = false;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);


                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(filtro.IdAsesor);

                DateTime fechaInicio = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 0, 0, 0);
                if (DateTime.Compare(fechaInicio, fechaActual) == 0)
                {
                    esActual = true;
                }
                DateTime fechaFin = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 23, 59, 59);
                if (filtro.EstadoFiltroHora)
                {
                    fechaInicio = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, filtro.HoraInicio, filtro.MinutosInicio, 0);
                    fechaFin = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, filtro.HoraFin, filtro.MinutosFin, 0);

                }
                var data = new List<ReporteRealizadaDataDTO>();

                if (esActual)
                    data = _unitOfWork.ReportesRepository.ObtenerReporteActividadesRealizadas(filtro, fechaInicio, fechaFin);
                else
                    data = _unitOfWork.ReportesRepository.ObtenerReporteActividadesRealizadasCongelado(filtro, fechaInicio, fechaFin);

                List<CompuestoActividadesRealizadasAlternoDTO> resultado = new List<CompuestoActividadesRealizadasAlternoDTO>();
                List<CompuestoActividadesRealizadasAlternoDTO> resultadoAux = new List<CompuestoActividadesRealizadasAlternoDTO>();
                var result = (from p in data
                              group p by new
                              {
                                  p.IdActividad,
                                  p.NombreCentroCosto,
                                  p.NombreCompletoContacto,
                                  p.CodigoFaseFinal,
                                  p.NombreTipoDato,
                                  p.NombreOrigen,
                                  p.FechaProgramada,
                                  p.FechaReal,
                                  p.NombreActividadCabecera,
                                  p.NombreOcurrencia,
                                  p.ComentarioActividad,
                                  p.NombreCompletoAsesor,
                                  p.IdAlumno,
                                  p.IdOportunidad,
                                  p.ProbabilidadActual,
                                  p.CodigoFaseOrigen,
                                  p.IdFaseOportunidadInicial,
                                  p.FechaModificacion,
                                  p.NombreCategoriaOrigen,
                                  p.EstadoOcurrencia,
                                  p.NombreGrupo,
                              } into g
                              select new CompuestoActividadesRealizadasAlternoDTO
                              {
                                  IdActividad = g.Key.IdActividad,
                                  NombreCentroCosto = g.Key.NombreCentroCosto,
                                  NombreCompletoContacto = g.Key.NombreCompletoContacto,
                                  CodigoFaseFinal = g.Key.CodigoFaseFinal,
                                  NombreTipoDato = g.Key.NombreTipoDato,
                                  NombreOrigen = g.Key.NombreOrigen,
                                  FechaProgramada = g.Key.FechaProgramada,
                                  FechaReal = g.Key.FechaReal,
                                  NombreActividadCabecera = g.Key.NombreActividadCabecera,
                                  NombreOcurrencia = g.Key.NombreOcurrencia,
                                  ComentarioActividad = g.Key.ComentarioActividad,
                                  NombreCompletoAsesor = g.Key.NombreCompletoAsesor,
                                  IdAlumno = g.Key.IdAlumno,
                                  IdOportunidad = g.Key.IdOportunidad,
                                  ProbabilidadActual = g.Key.ProbabilidadActual,
                                  CodigoFaseOrigen = g.Key.CodigoFaseOrigen,
                                  IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                                  FechaModificacion = g.Key.FechaModificacion,
                                  NombreCategoriaOrigen = g.Key.NombreCategoriaOrigen,
                                  EstadoOcurrencia = g.Key.EstadoOcurrencia,
                                  NombreGrupo = g.Key.NombreGrupo,

                                  LlamadasIntegra = g.Select(o => new InformacionLlamadaAlternoDTO
                                  {
                                      Id = o.IdLlamadaWebphone,
                                      DuracionTimbrado = o.DuracionTimbrado,
                                      DuracionContesto = o.DuracionContesto,
                                      FechaInicioLlamada = o.FechaInicioLlamada,
                                      FechaFinLlamada = o.FechaFinLlamada,
                                      EstadoLlamada = null,
                                      SubEstadoLlamada = null,
                                      NombreGrabacion = o.NombreGrabacionIntegra,
                                      Webphone = o.Webphone
                                  }).OrderByDescending(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                  LlamadasCentral = g.Select(o => new InformacionLlamadaAlternoDTO
                                  {
                                      Id = o.IdTresCX,
                                      DuracionTimbrado = o.DuracionTimbradoTresCx,
                                      DuracionContesto = o.DuracionContestoTresCx,
                                      FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                                      FechaFinLlamada = o.FechaFinLlamadaTresCX,
                                      EstadoLlamada = o.EstadoLlamadaTresCX,
                                      SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                                      NombreGrabacion = o.NombreGrabacionTresCX,

                                  }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                              }).OrderBy(x => x.FechaReal);

                List<ProcesadoDataActividadesRealizadasAlternoDTO> final = new List<ProcesadoDataActividadesRealizadasAlternoDTO>();

                resultado = result.ToList();
                for (int i = 0; i < resultado.Count(); i++)
                {
                    for (int j = i + 1; j < resultado.Count(); j++)
                    {
                        if (resultado[i].IdActividad == resultado[j].IdActividad)
                        {
                            resultado[j].IdActividad = 0;
                            resultado[i].CodigoFaseFinal = resultado[j].CodigoFaseFinal;
                        }
                    }
                }

                foreach (var item in resultado)
                {
                    if (item.IdActividad != 0)
                    {
                        resultadoAux.Add(item);
                    }
                }

                //Variables Temporales ------------
                var flag = false;
                var count = 0;
                double minutos = 0;
                double totalContesto = 0;
                double totalTimbrado = 0;
                double totalPerdido = 0;
                double mayorPerdido = 0;
                double minutosTotalLlamada = 0;
                DateTime fechaTemp = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 00, 00, 00);
                DateTime fechaTempActividad = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 00, 00, 00);

                int diaFecha = filtro.Fecha.Day;
                //Fin Variables Temporales --------

                foreach (var item in resultadoAux)
                {
                    ProcesadoDataActividadesRealizadasAlternoDTO itemDetalle = new()
                    {
                        IdActividad = item.IdActividad,
                        NombreCentroCosto = item.NombreCentroCosto,
                        NombreCompletoContacto = item.NombreCompletoContacto,
                        CodigoFaseFinal = item.CodigoFaseFinal,
                        NombreTipoDato = item.NombreTipoDato,
                        NombreOrigen = item.NombreOrigen,
                        FechaProgramada = item.FechaProgramada,
                        FechaReal = item.FechaReal,
                        NombreActividadCabecera = item.NombreActividadCabecera,
                        NombreOcurrencia = item.NombreOcurrencia,
                        ComentarioActividad = item.ComentarioActividad,
                        NombreCompletoAsesor = item.NombreCompletoAsesor,
                        IdAlumno = item.IdAlumno,
                        IdOportunidad = item.IdOportunidad,
                        ProbabilidadActual = item.ProbabilidadActual,
                        CodigoFaseOrigen = item.CodigoFaseOrigen,
                        NombreCategoriaOrigen = item.NombreCategoriaOrigen,
                        EstadoOcurrencia = item.EstadoOcurrencia,
                        NombreGrupo = item.NombreGrupo,
                    };
                    if (item.LlamadasIntegra != null && item.LlamadasIntegra.Count() > 0)
                    {
                        var ordenLlamadas = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();
                        //var fechaUltima = ordenLlamadas.Select(s => s.FechaInicioLlamada).FirstOrDefault();
                        var primeraLlamda = ordenLlamadas.FirstOrDefault()!;
                        var ultimaLlamada = ordenLlamadas.LastOrDefault()!;
                        var primeraFecha = primeraLlamda.FechaInicioLlamada!.Value;

                        //primeraFecha = fechaUltima.AddSeconds(contesto + timbrado);

                        if (count > 0 && flag)
                        {
                            if (diaFecha == fechaTemp.Day)
                            {
                                var min = ((primeraFecha - fechaTemp).TotalSeconds).ToString("0.0");
                                minutos = Convert.ToDouble(min);
                            }
                            else
                                minutos = 0;
                        }
                        if (ultimaLlamada != null)
                        {
                            flag = true;
                            fechaTemp = ultimaLlamada.FechaFinLlamada!.Value;
                            //double contesto = Convert.ToDouble(primeraLlamda.DuracionContesto);
                            //double timbrado = Convert.ToDouble(primeraLlamda.DuracionTimbrado);
                            //fechaTemp = fechaTemp.AddSeconds(contesto + timbrado);
                        }
                        totalTimbrado += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                        totalContesto += (item.LlamadasIntegra.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                        if (minutos >= 0)
                            totalPerdido += minutos;

                        item.LlamadasIntegra = item.LlamadasIntegra.OrderBy(x => x.FechaInicioLlamada).ToList();

                        foreach (var llamada in item.LlamadasCentral)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                itemDetalle.NombreGrabacionIntegra.Add(new NombreGrabacionAlternoDTO() { Tipo = "reproducirLlamada3CX", NombreGrabacion = llamada.NombreGrabacion });
                            }
                        }
                        var primeraFechaFin = DateTime.Now;
                        if (diferenciaHoraria != null)
                        {
                            primeraFechaFin = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                        }
                        int contadorLlamadas = 0;
                        double minutosLlamada = 0;


                        foreach (var llamada in item.LlamadasIntegra)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                if (llamada.Webphone == "Silcom")
                                {
                                    itemDetalle.NombreGrabacionIntegra.Add(new NombreGrabacionAlternoDTO() { Webphone = llamada.Webphone, Tipo = "reproducirLlamadaNuevoWebPhone", NombreGrabacion = llamada.NombreGrabacion });
                                }
                                else if (llamada.Webphone == "Silcom Migrado")
                                {
                                    itemDetalle.NombreGrabacionIntegra.Add(new NombreGrabacionAlternoDTO() { Webphone = llamada.Webphone, Tipo = "reproducirLlamadaNuevoWebPhoneMigrado", NombreGrabacion = llamada.NombreGrabacion });
                                }
                                else
                                {
                                    itemDetalle.NombreGrabacionIntegra.Add(new NombreGrabacionAlternoDTO() { Tipo = "", NombreGrabacion = "" });
                                }
                            }
                            llamada.MinutosPerdidos = null;
                            if (contadorLlamadas > 0)
                            {
                                var min = ((llamada.FechaInicioLlamada.Value - primeraFechaFin).TotalMinutes);//segundos.
                                if (min < 0) min = 0;
                                var minTexto = min.ToString("0.0");
                                minutosLlamada = Convert.ToDouble(minTexto);
                                //string estilo = ClasificarColorLlamadaRealizadasActividadesMinutos(minutosLlamada);
                                llamada.MinutosPerdidos = Convert.ToDouble(minTexto);
                            }
                            primeraFechaFin = llamada.FechaFinLlamada.Value;
                            minutosTotalLlamada += minutosLlamada;
                            contadorLlamadas++;
                        }
                        itemDetalle.TiemposDuracionLlamadas = item.LlamadasIntegra.Select(o => new TiempoTresCXDTO
                        {
                            TT = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + " m",
                            TC = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + " m"
                        }).ToList();
                        itemDetalle.FechaLlamada = item.LlamadasIntegra.Select(o => new FechaLlamadaDTO
                        {
                            MinutosPerdidos = o.MinutosPerdidos,
                            Inicio = o.FechaInicioLlamada.Value.ToString("HH:mm"),
                            Termino = o.FechaFinLlamada.Value.ToString("HH:mm"),
                        }).ToList();
                    }
                    else
                    {
                        //flag = true;
                        //fechaTemp = itemDetalle.FechaReal;


                        var min = ((itemDetalle.FechaReal - fechaTemp).TotalSeconds).ToString("0.0");
                        minutos = Convert.ToDouble(min);
                        if (minutos >= 0)
                            totalPerdido += minutos;

                        flag = true;
                        fechaTemp = itemDetalle.FechaReal;
                    }

                    itemDetalle.MinutosPerdidosOcurrencia = 0;

                    if (item.LlamadasIntegra != null && item.LlamadasIntegra.Count() > 0)
                    {
                        var ultimaLlamada = item.LlamadasIntegra.LastOrDefault();
                        var min = ((itemDetalle.FechaReal - ultimaLlamada.FechaFinLlamada.Value).TotalMinutes);//segundos.
                        if (min < 0) min = 0;
                        var minTexto = min.ToString("0.0");
                        itemDetalle.MinutosPerdidosOcurrencia = Convert.ToDouble(minTexto);
                    }

                    itemDetalle.MinutosTotalIntervaleLlamadas = minutosTotalLlamada;
                    itemDetalle.MinutosIntervalo = minutos;
                    itemDetalle.MinutosTotalContesto = totalContesto;
                    itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                    itemDetalle.MinutosTotalPerdido = totalPerdido;
                    count++;
                    //var fechaTempReal = itemDetalle.FechaReal;
                    if (fechaTempActividad >= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 12, 30, 00) && fechaTempActividad <= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 16, 00, 00))
                    {
                        mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                        itemDetalle.MayorTiempo = mayorPerdido;
                    }
                    fechaTempActividad = itemDetalle.FechaReal;

                    itemDetalle.TiemposTresCX = item.LlamadasCentral.Select(o => new TiempoTresCXDTO
                    {
                        TT = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + "m",
                        TC = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + "m"
                    }).ToList();
                    itemDetalle.EstadosTresCX = item.LlamadasCentral.Select(o => new EstadoTresCXDTO
                    {
                        Tipo = o.EstadoLlamada,
                        SubTipo = o.SubEstadoLlamada,
                    }).ToList();
                    itemDetalle.ExisteLlamadaExitosa = itemDetalle.EstadosTresCX.Any(x => x.Tipo == "Llamada Exitosa");
                    //itemDetalle.ExisteLlamadaExitosa = itemDetalle.EstadosTresCX.Contains("Llamada Exitosa");
                    itemDetalle.TotalEjecutadas = 0;
                    itemDetalle.TotalNoEjecutadas = 0;
                    itemDetalle.TotalAsignacionManual = 0;


                    final.Add(itemDetalle);
                }

                if (filtro.EstadoLlamada != null)
                {
                    if (filtro.EstadoLlamada == 1)
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == true).ToList();
                    }
                    else
                    {
                        final = final.Where(x => x.ExisteLlamadaExitosa == false).ToList();
                    }
                }
                var dataFinal = final.OrderByDescending(s => s.FechaReal).ToList();
                return dataFinal;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 06/10/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene información para combos de interfaz
        /// </summary>
        /// <returns> Combo para modulo actividades realizadas <returns>
        public async Task<FiltroReporteActividadRealizadaAlternoDTO> ObtenerCombo(int idPersonal)
        {
            try
            {
                FiltroReporteActividadRealizadaAlternoDTO filtro = new FiltroReporteActividadRealizadaAlternoDTO();
                var taskAsistente = _unitOfWork.PersonalRepository.ObtenerPersonalAsignadoVentasAsync(idPersonal);
                var taskEstadoOcurrencia = _unitOfWork.EstadoOcurrenciaRepository.ObtenerComboAsync();
                var taskFaseOportunidad = _unitOfWork.FaseOportunidadRepository.ObtenerComboAsync();
                var taskTipoDato = _unitOfWork.TipoDatoRepository.ObtenerComboAsync();
                var taskProbabilidad = _unitOfWork.ProbabilidadRegistroPwRepository.ObtenerComboAsync();
                var taskCategoriaOrigen = _unitOfWork.TipoCategoriaOrigenRepository.ObtenerComboAsync();
                filtro.EstadoOcurrencia = await taskEstadoOcurrencia;
                filtro.FaseOportunidad = await taskFaseOportunidad;
                filtro.TipoDato = await taskTipoDato;
                filtro.Probabilidad = await taskProbabilidad;
                filtro.CategoriaOrigen = await taskCategoriaOrigen;
                List<PersonalAsignadoDTO> asistentes = await taskAsistente;
                filtro.Asesores = asistentes;
                return filtro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
