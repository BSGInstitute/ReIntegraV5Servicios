using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    public class ReporteActividadesRealizadasTresCxService : IReporteActividadesRealizadasTresCxService
    {

        private IUnitOfWork _unitOfWork;
        //private Mapper _mapper;
        public ReporteActividadesRealizadasTresCxService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO> ReporteActividadesRealizadasAlterno(ReporteActividadesRealizadasFiltrosDTO? filtro)
        {
            try
            {
                var esActual = false;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);

                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(filtro!.IdAsesor);

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
                var data = _unitOfWork.ReporteTresCxRepository.ObtenerReporteActividadesRealizadasTresCx(filtro, fechaInicio, fechaFin, esActual);

                var result = data.Where(s => s.IdActividad != 0).GroupBy(p => new
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
                    p.EstadoSeguimientoWhatsApp,
                    p.OtroMedio,
                }).Select(g => new CompuestoActividadesRealizadasAlternoTresCxDTO
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
                    EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                    OtroMedio = g.Key.OtroMedio,
                    LlamadasIntegra = g.Select(o => new InformacionLlamadaAlternoDTO
                    {
                        Id = o.IdLlamadaWebphone,
                        DuracionTimbrado = o.DuracionTimbradoIntegra,
                        DuracionContesto = o.DuracionContestoIntegra,
                        FechaInicioLlamada = o.FechaInicioLlamadaIntegra,
                        FechaFinLlamada = o.FechaFinLlamadaIntegra,
                        EstadoLlamada = o.EstadoLlamadaCentral,
                        SubEstadoLlamada = o.SubEstadoLlamadaCentral,
                        NombreGrabacion = o.NombreGrabacionIntegra,
                        Webphone = o.WebphoneIntegra,
                        TelefonoDestinoReal = "",
                        TelefonoDestino = "",
                        AnexoCentral = "",
                        OrigenLlamada = "Integra"
                    }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                    LlamadasTresCx = g.Select(o => new InformacionLlamadaAlternoDTO
                    {
                        Id = o.IdTresCx,
                        DuracionTimbrado = o.DuracionTimbradoTresCx,
                        DuracionContesto = o.DuracionContestoTresCx,
                        FechaInicioLlamada = o.FechaInicioLlamadaTresCX,
                        FechaFinLlamada = o.FechaFinLlamadaTresCX,
                        EstadoLlamada = o.EstadoLlamadaTresCX,
                        SubEstadoLlamada = o.SubEstadoLlamadaTresCX,
                        NombreGrabacion = o.NombreGrabacionTresCx,
                        Webphone = o.WebphoneTresCx,
                        TelefonoDestinoReal = o.TelefonoDestinoReal,
                        TelefonoDestino = o.TelefonoDestino,
                        AnexoCentral = o.AnexoCentral,
                        OrigenLlamada = "3cx"
                    }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                    LlamadasRingover = g.Select(o => new InformacionLlamadaAlternoDTO
                    {
                        Id = o.IdRingover,
                        DuracionTimbrado = o.DuracionTimbradoRingover,
                        DuracionContesto = o.DuracionContestoRingover,
                        FechaInicioLlamada = o.FechaInicioLlamadaRingover,
                        FechaFinLlamada = o.FechaFinLlamadaRingover,
                        EstadoLlamada = o.EstadoLlamadaRingover,
                        SubEstadoLlamada = o.SubEstadoLlamadaRingover,
                        NombreGrabacion = o.NombreGrabacionRingover,
                        Webphone = o.WebphoneRingover,
                        TelefonoDestinoReal = o.TelefonoDestinoReal,
                        TelefonoDestino = o.TelefonoDestino,
                        AnexoCentral = o.AnexoCentral,
                        OrigenLlamada = "Ringover"
                    }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()
                }).ToList();

                var result2 = data.Where(s => s.IdActividad == 0).Select(g => new CompuestoActividadesRealizadasAlternoTresCxDTO
                {
                    IdActividad = g.IdActividad,
                    NombreCentroCosto = g.NombreCentroCosto,
                    NombreCompletoContacto = g.NombreCompletoContacto,
                    CodigoFaseFinal = g.CodigoFaseFinal,
                    NombreTipoDato = g.NombreTipoDato,
                    NombreOrigen = g.NombreOrigen,
                    FechaProgramada = g.FechaProgramada,
                    FechaReal = g.FechaReal,
                    NombreActividadCabecera = g.TelefonoDestino,
                    NombreOcurrencia = g.NombreOcurrencia,
                    ComentarioActividad = g.ComentarioActividad,
                    NombreCompletoAsesor = g.NombreCompletoAsesor,
                    IdAlumno = g.IdAlumno,
                    IdOportunidad = g.IdOportunidad,
                    ProbabilidadActual = g.ProbabilidadActual,
                    CodigoFaseOrigen = g.CodigoFaseOrigen,
                    IdFaseOportunidadInicial = g.IdFaseOportunidadInicial,
                    FechaModificacion = g.FechaModificacion,
                    NombreCategoriaOrigen = g.NombreCategoriaOrigen,
                    EstadoOcurrencia = g.EstadoOcurrencia,
                    NombreGrupo = g.NombreGrupo,
                    EstadoSeguimientoWhatsApp = g.EstadoSeguimientoWhatsApp,
                    OtroMedio = g.OtroMedio,
                    LlamadasTresCx = new List<InformacionLlamadaAlternoDTO>(){
                        new InformacionLlamadaAlternoDTO(){
                            Id = g.IdTresCx,
                            DuracionTimbrado = g.DuracionTimbradoTresCx,
                            DuracionContesto = g.DuracionContestoTresCx,
                            FechaInicioLlamada = g.FechaInicioLlamadaTresCX,
                            FechaFinLlamada = g.FechaFinLlamadaTresCX,
                            EstadoLlamada = g.EstadoLlamadaTresCX,
                            SubEstadoLlamada = g.SubEstadoLlamadaTresCX,
                            NombreGrabacion = g.NombreGrabacionTresCx,
                            Webphone = g.WebphoneTresCx,
                            TelefonoDestinoReal = g.TelefonoDestinoReal,
                            TelefonoDestino = g.TelefonoDestino,
                            AnexoCentral = g.AnexoCentral,
                            OrigenLlamada = "3cx"
                        }
                    },
                    LlamadasRingover = new List<InformacionLlamadaAlternoDTO>(){
                        new InformacionLlamadaAlternoDTO(){
                            Id = g.IdRingover,
                            DuracionTimbrado = g.DuracionTimbradoRingover,
                            DuracionContesto = g.DuracionContestoRingover,
                            FechaInicioLlamada = g.FechaInicioLlamadaRingover,
                            FechaFinLlamada = g.FechaFinLlamadaRingover,
                            EstadoLlamada = g.EstadoLlamadaRingover,
                            SubEstadoLlamada = g.SubEstadoLlamadaRingover,
                            NombreGrabacion = g.NombreGrabacionRingover,
                            Webphone = g.WebphoneRingover,
                            TelefonoDestinoReal = g.TelefonoDestinoReal,
                            TelefonoDestino = g.TelefonoDestino,
                            AnexoCentral = g.AnexoCentral,
                            OrigenLlamada = "Ringover"
                        }
                    }
                }).ToList();

                List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO> final = new List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO>();
                List<CompuestoActividadesRealizadasAlternoTresCxDTO> resultado = new List<CompuestoActividadesRealizadasAlternoTresCxDTO>();

                resultado = result.OrderBy(x => x.FechaReal).ToList();

                for (int i = 0; i < resultado.Count(); i++)
                {
                    for (int j = i + 1; j < resultado.Count(); j++)
                    {
                        if (resultado[i].IdActividad == resultado[j].IdActividad)
                        {
                            resultado[j].IdActividad = -1;
                            resultado[i].CodigoFaseFinal = resultado[j].CodigoFaseFinal;
                        }
                    }
                }
                resultado.AddRange(result2);
                var resultado2 = resultado.OrderBy(x => x.FechaReal).ToList();
                List<CompuestoActividadesRealizadasAlternoTresCxDTO> resultadoAux = new();
                List<CompuestoActividadesRealizadasAlternoTresCxDTO> resultadoAuxTemp = new();
                foreach (var item in resultado2)
                {
                    if (item.IdActividad != -1)
                    {
                        resultadoAuxTemp.Add(item);
                    }
                }
                for (int i = 0; i < resultadoAuxTemp.Count(); i++)
                {
                    if (i == 0)
                    {
                        resultadoAux.Add(resultadoAuxTemp[i]);
                    }
                    else
                    {
                        if (resultadoAuxTemp[i].IdActividad == 0 && resultadoAux.LastOrDefault()!.IdActividad == 0)
                        {
                            var numeroTemp = resultadoAuxTemp[i].LlamadasTresCx.FirstOrDefault()!;
                            var numeroTempIndex = resultadoAux.LastOrDefault()!.LlamadasTresCx.FirstOrDefault()!;
                            if (numeroTempIndex.TelefonoDestinoReal.Trim() == numeroTemp.TelefonoDestinoReal.Trim()
                                || numeroTemp.TelefonoDestinoReal.Trim().Contains(numeroTempIndex.TelefonoDestino.Trim())
                                || numeroTempIndex.TelefonoDestinoReal.Trim().Contains(numeroTemp.TelefonoDestino.Trim())
                            )
                            {
                                resultadoAux.LastOrDefault()!.LlamadasTresCx.AddRange(resultadoAuxTemp[i].LlamadasTresCx);
                                //resultadoAux.LastOrDefault()!.LlamadasCentral = resultadoAux.LastOrDefault()!.LlamadasCentral.OrderBy(o => o.FechaInicioLlamada).ToList();
                            }
                            else
                            {
                                resultadoAux.Add(resultadoAuxTemp[i]);
                            }
                        }
                        else
                        {
                            resultadoAux.Add(resultadoAuxTemp[i]);
                        }
                    }
                }

                //resultadoAux = resultadoAux.OrderBy(o => o.FechaReal).ToList();

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
                    var llamadas = new List<InformacionLlamadaAlternoDTO>();
                    if (item.LlamadasIntegra != null && item.LlamadasIntegra.Count() > 0)
                    {
                        llamadas.AddRange(item.LlamadasIntegra.Where(x => x.Id != null).ToList());
                    }
                    if (item.LlamadasTresCx != null && item.LlamadasTresCx.Count() > 0)
                    {
                        llamadas.AddRange(item.LlamadasTresCx.Where(x => x.Id != null).ToList());
                    }
                    if (item.LlamadasRingover != null && item.LlamadasRingover.Count() > 0)
                    {
                        llamadas.AddRange(item.LlamadasRingover.Where(x => x.Id != null).ToList());
                    }
                    item.LlamadasIntegraTresCx = llamadas.OrderBy(x => x.FechaInicioLlamada).ToList();
                }
                foreach (var item in resultadoAux)
                {
                    ProcesadoDataActividadesRealizadasAlternoTresCxDTO itemDetalle = new()
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
                        EstadoSeguimientoWhatsApp = item.EstadoSeguimientoWhatsApp,
                        OtroMedio = item.OtroMedio,
                    };

                    if (item.LlamadasIntegraTresCx.Count() > 0)
                    {
                        var ordenLlamadas = item.LlamadasIntegraTresCx.OrderBy(x => x.FechaInicioLlamada).ToList();

                        var primeraLlamda = ordenLlamadas.FirstOrDefault()!;
                        var ultimaLlamada = ordenLlamadas.LastOrDefault()!;
                        var primeraFecha = primeraLlamda.FechaInicioLlamada!.Value;

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
                        }

                        totalTimbrado += (item.LlamadasIntegraTresCx.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                        totalContesto += (item.LlamadasIntegraTresCx.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                        if (minutos >= 0)
                            totalPerdido += minutos;

                        //item.LlamadasIntegraTresCx = item.LlamadasIntegraTresCx.OrderBy(x => x.FechaInicioLlamada).ToList();


                        var primeraFechaFin = DateTime.Now;
                        if (diferenciaHoraria != null)
                        {
                            primeraFechaFin = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
                        }

                        int contadorLlamadas = 0;
                        double minutosLlamada = 0;

                        foreach (var llamada in item.LlamadasIntegraTresCx)
                        {
                            if (llamada.NombreGrabacion != null)
                            {
                                itemDetalle.NombreGrabacion.Add(new NombreGrabacionAlternoDTO()
                                {
                                    Webphone = llamada.Webphone,
                                    Tipo = "",
                                    NombreGrabacion = llamada.NombreGrabacion,
                                    OrigenLlamada = llamada.OrigenLlamada
                                });
                            }
                            llamada.MinutosPerdidos = null;
                            if (contadorLlamadas > 0)
                            {
                                var min = ((llamada.FechaInicioLlamada.Value - primeraFechaFin).TotalMinutes);//segundos.
                                if (min < 0) min = 0;
                                var minTexto = min.ToString("0.0");
                                minutosLlamada = Convert.ToDouble(minTexto);
                                llamada.MinutosPerdidos = Convert.ToDouble(minTexto);
                            }
                            primeraFechaFin = llamada.FechaFinLlamada.Value;
                            minutosTotalLlamada += minutosLlamada;
                            contadorLlamadas++;
                        }
                        itemDetalle.FechaLlamada = item.LlamadasIntegraTresCx.Select(o => new FechaLlamadaDTO
                        {
                            MinutosPerdidos = o.MinutosPerdidos,
                            Inicio = o.FechaInicioLlamada.Value.ToString("HH:mm"),
                            Termino = o.FechaFinLlamada.Value.ToString("HH:mm"),
                            OrigenLlamada = o.OrigenLlamada
                        }).ToList();
                    }
                    else
                    {
                        var min = ((itemDetalle.FechaReal - fechaTemp).TotalSeconds).ToString("0.0");
                        minutos = Convert.ToDouble(min);
                        if (minutos >= 0)
                            totalPerdido += minutos;

                        flag = true;
                        fechaTemp = itemDetalle.FechaReal;
                    }

                    itemDetalle.MinutosPerdidosOcurrencia = 0;
                    if (item.LlamadasIntegraTresCx != null && item.LlamadasIntegraTresCx.Count() > 0)
                    {
                        var ultimaLlamada = item.LlamadasIntegraTresCx.LastOrDefault();
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

                    if (fechaTempActividad >= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 12, 30, 00) && fechaTempActividad <= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 16, 00, 00))
                    {
                        mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                        itemDetalle.MayorTiempo = mayorPerdido;
                    }
                    fechaTempActividad = itemDetalle.FechaReal;
                    //LLamada Central
                    itemDetalle.Tiempos = item.LlamadasIntegraTresCx.Select(o => new TiempoTresCXDTO
                    {
                        Webphone = o.Webphone ?? "",
                        TT = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + "m",
                        TC = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + "m",
                        OrigenLlamada = o.OrigenLlamada
                    }).ToList();
                    itemDetalle.Estados = item.LlamadasIntegraTresCx.Select(o => new EstadoTresCXDTO
                    {
                        Tipo = o.EstadoLlamada,
                        SubTipo = o.SubEstadoLlamada,
                        OrigenLlamada = o.OrigenLlamada
                    }).ToList();
                    itemDetalle.ExisteLlamadaExitosa = itemDetalle.Estados.Any(x => x.Tipo == "Llamada Exitosa" || (x.Tipo == "Respondido" && x.SubTipo == "Respondido"));
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
        ///Autor: Flavio R.M.F.
        ///Fecha: 27/05/2024
        /// <summary>
        /// Obtiene las actividades realizadas por un asesor en un determinado dia
        /// </summary>
        /// <param name="filtro"> Filtros de búsqueda reportes</param>
        /// <returns> Lista de ProcesadoDataActividadesRealizadasAlternoTresCxDTO </returns>
        public List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO> ReporteActividadesRealizadas(ReporteActividadesRealizadasFiltrosDTO? filtro)
        {
            try
            {
                var esActual = false;
                var fechaActualTemp = DateTime.Now;
                var fechaActual = new DateTime(fechaActualTemp.Year, fechaActualTemp.Month, fechaActualTemp.Day, 0, 0, 0);

                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(filtro!.IdAsesor);

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
                //var data = _unitOfWork.ReporteTresCxRepository.ObtenerReporteActividadesRealizadasTresCx(filtro, fechaInicio, fechaFin, esActual);
                var data = _unitOfWork.ReporteTresCxRepository.ObtenerReporteActividadesRealizadas(filtro, fechaInicio, fechaFin, esActual);

                var result = data.Where(s => s.IdActividad != 0).GroupBy(p => new
                {
                    p.IdActividad,
                    p.IdOportunidadSeguimiento,
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
                    p.EstadoSeguimientoWhatsApp,
                    p.EsOtroMedio,
                    p.DiferenciaFechaActualFechaRealmin
                }).Select(g => new CompuestoActividadRealizadaDTO
                {
                    IdActividad = g.Key.IdActividad,
                    IdOportunidadSeguimiento = g.Key.IdOportunidadSeguimiento,
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
                    EstadoSeguimientoWhatsApp = g.Key.EstadoSeguimientoWhatsApp,
                    OtroMedio = g.Key.EsOtroMedio,
                    DiferenciaFechaActualFechaRealmin = g.Key.DiferenciaFechaActualFechaRealmin==null?0 : g.Key.DiferenciaFechaActualFechaRealmin.Value,
                    Llamadas = g.Select(o => new InformacionLlamadaAlternoDTO
                    {
                        Id = o.IdRegistroLlamada,
                        DuracionTimbrado = o.DuracionTimbrado,
                        DuracionContesto = o.DuracionContesto,
                        FechaInicioLlamada = o.FechaInicioLlamada,
                        FechaFinLlamada = o.FechaFinLlamada,
                        EstadoLlamada = o.EstadoLlamada,
                        SubEstadoLlamada = o.SubEstadoLlamada,
                        NombreGrabacion = o.UrlGrabacion,
                        Webphone = o.WebphoneGrabacion,
                        TelefonoDestinoReal = o.TelefonoDestinoReal,
                        TelefonoDestino = o.TelefonoDestino,
                        AnexoCentral = o.AnexoCentral,
                        OrigenLlamada = o.OrigenLlamada
                    }).OrderBy(o => o.FechaInicioLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                }).ToList();

                var result2 = data.Where(s => s.IdActividad == 0).Select(g => new CompuestoActividadRealizadaDTO
                {
                    IdActividad = g.IdActividad,
                    IdOportunidadSeguimiento = g.IdOportunidadSeguimiento,
                    NombreCentroCosto = g.NombreCentroCosto,
                    NombreCompletoContacto = g.NombreCompletoContacto,
                    CodigoFaseFinal = g.CodigoFaseFinal,
                    NombreTipoDato = g.NombreTipoDato,
                    NombreOrigen = g.NombreOrigen,
                    FechaProgramada = g.FechaProgramada,
                    FechaReal = g.FechaReal,
                    NombreActividadCabecera = g.TelefonoDestinoReal ?? "",
                    NombreOcurrencia = g.NombreOcurrencia,
                    ComentarioActividad = g.ComentarioActividad,
                    NombreCompletoAsesor = g.NombreCompletoAsesor,
                    IdAlumno = g.IdAlumno,
                    IdOportunidad = g.IdOportunidad,
                    ProbabilidadActual = g.ProbabilidadActual,
                    CodigoFaseOrigen = g.CodigoFaseOrigen,
                    IdFaseOportunidadInicial = g.IdFaseOportunidadInicial,
                    FechaModificacion = g.FechaModificacion,
                    NombreCategoriaOrigen = g.NombreCategoriaOrigen,
                    EstadoOcurrencia = g.EstadoOcurrencia,
                    NombreGrupo = g.NombreGrupo,
                    EstadoSeguimientoWhatsApp = g.EstadoSeguimientoWhatsApp,
                    OtroMedio = g.EsOtroMedio,
                    DiferenciaFechaActualFechaRealmin = g.DiferenciaFechaActualFechaRealmin==null?0:g.DiferenciaFechaActualFechaRealmin.Value,
                    Llamadas = new List<InformacionLlamadaAlternoDTO>(){
                        new InformacionLlamadaAlternoDTO(){
                            Id = g.IdRegistroLlamada,
                            DuracionTimbrado = g.DuracionTimbrado,
                            DuracionContesto = g.DuracionContesto,
                            FechaInicioLlamada = g.FechaInicioLlamada,
                            FechaFinLlamada = g.FechaFinLlamada,
                            EstadoLlamada = g.EstadoLlamada ?? "",
                            SubEstadoLlamada = g.SubEstadoLlamada ?? "",
                            NombreGrabacion = g.UrlGrabacion,
                            Webphone = g.WebphoneGrabacion ?? "",
                            TelefonoDestinoReal = g.TelefonoDestinoReal ?? "",
                            TelefonoDestino = g.TelefonoDestino ?? "",
                            AnexoCentral = g.AnexoCentral ?? "",
                            OrigenLlamada = g.OrigenLlamada ?? ""
                        }
                    },
                }).ToList();

                List<ProcesadoDataActividadesRealizadasAlternoTresCxDTO> final = new();
                List<CompuestoActividadRealizadaDTO> resultado = new();

                resultado = result.OrderBy(x => x.FechaReal).ToList();

                for (int i = 0; i < resultado.Count(); i++)
                {
                    for (int j = i + 1; j < resultado.Count(); j++)
                    {
                        if (resultado[i].IdActividad == resultado[j].IdActividad)
                        {
                            resultado[j].IdActividad = -1;
                            resultado[i].CodigoFaseFinal = resultado[j].CodigoFaseFinal;
                        }
                    }
                }
                resultado.AddRange(result2);
                var resultado2 = resultado.OrderBy(x => x.FechaReal).ToList();
                List<CompuestoActividadRealizadaDTO> resultadoAuxTemp = new();
                foreach (var item in resultado2)
                {
                    if (item.IdActividad != -1)
                        resultadoAuxTemp.Add(item);
                }
                List<CompuestoActividadRealizadaDTO> resultadoAux = new();
                for (int i = 0; i < resultadoAuxTemp.Count(); i++)
                {
                    try
                    {
                        if (i == 0)
                            resultadoAux.Add(resultadoAuxTemp[i]);
                        else
                        {
                            if (resultadoAuxTemp[i].IdActividad == 0 && resultadoAux.LastOrDefault()!.IdActividad == 0)
                            {
                                var numeroTemp = resultadoAuxTemp[i].Llamadas.FirstOrDefault()!;
                                var numeroTempIndex = resultadoAux.LastOrDefault()!.Llamadas.FirstOrDefault()!;
                                if (numeroTempIndex.TelefonoDestinoReal.Trim() == numeroTemp.TelefonoDestinoReal.Trim()
                                    || numeroTemp.TelefonoDestinoReal.Trim().Contains(numeroTempIndex.TelefonoDestino.Trim())
                                    || numeroTempIndex.TelefonoDestinoReal.Trim().Contains(numeroTemp.TelefonoDestino.Trim())
                                )
                                {
                                    resultadoAux.LastOrDefault()!.Llamadas.AddRange(resultadoAuxTemp[i].Llamadas);
                                }
                                else
                                {
                                    resultadoAux.Add(resultadoAuxTemp[i]);
                                }
                            }
                            else
                            {
                                resultadoAux.Add(resultadoAuxTemp[i]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new BadRequestException($"Error en la interaccion: {i}");
                    }
                }

                //resultadoAux = resultadoAux.OrderBy(o => o.FechaReal).ToList();

                //Variables Temporales ------------
                var flag = false;
                var contadorActividad = 0;
                double minutos = 0;
                double totalContesto = 0;
                double totalTimbrado = 0;
                double totalPerdido = 0;
                double mayorPerdido = 0;
                double minutosTotalLlamada = 0;
                DateTime fechaUltimaLlamadaTemp = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 00, 00, 00);
                DateTime fechaRealTemp = new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 00, 00, 00);

                int diaFecha = filtro.Fecha.Day;

                //Fin Variables Temporales --------

                foreach (var item in resultadoAux)
                {
                    try
                    {
                        ProcesadoDataActividadesRealizadasAlternoTresCxDTO itemDetalle = new()
                        {
                            IdActividad = item.IdActividad,
                            IdOportunidadSeguimiento = item.IdOportunidadSeguimiento,
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
                            EstadoSeguimientoWhatsApp = item.EstadoSeguimientoWhatsApp,
                            OtroMedio = item.OtroMedio,
                            DiferenciaFechaActualFechaRealmin = item.DiferenciaFechaActualFechaRealmin
                        };
                        if (item.Llamadas == null)
                        {
                            item.Llamadas = new();
                        }
                        else
                        {
                            itemDetalle.TieneLlamadaHumano = item.Llamadas.Where(w => w.SubEstadoLlamada == "Humano").Count() > 0 ? true : false;
                        }

                        itemDetalle.MinutosPerdidosOcurrencia = 0;
                        if (item.Llamadas.Count() > 0)
                        {
                            item.Llamadas = item.Llamadas.Where(x => x.Id != null).OrderBy(x => x.FechaInicioLlamada).ToList();

                            var primeraLlamda = item.Llamadas.FirstOrDefault()!;
                            var ultimaLlamada = item.Llamadas.LastOrDefault()!;
                            var primeraFecha = primeraLlamda.FechaInicioLlamada!.Value;

                            if (contadorActividad > 0 && flag)
                            {
                                if (diaFecha == fechaUltimaLlamadaTemp.Day)
                                {
                                    var min = ((primeraFecha - fechaUltimaLlamadaTemp).TotalSeconds).ToString("0.0");
                                    minutos = Convert.ToDouble(min);
                                }
                                else
                                    minutos = 0;
                            }
                            flag = true;
                            fechaUltimaLlamadaTemp = ultimaLlamada.FechaFinLlamada!.Value;

                            totalTimbrado += (item.Llamadas.Select(s => Convert.ToDouble(s.DuracionTimbrado))).Sum();
                            totalContesto += (item.Llamadas.Select(s => Convert.ToDouble(s.DuracionContesto))).Sum();
                            if (minutos >= 0)
                                totalPerdido += minutos;

                            var primeraFechaFin = DateTime.Now;
                            if (diferenciaHoraria != null)
                                primeraFechaFin = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);

                            int contadorLlamadas = 0;
                            double minutosLlamada = 0;

                            foreach (var llamada in item.Llamadas)
                            {
                                if (llamada.NombreGrabacion != null)
                                {
                                    itemDetalle.NombreGrabacion.Add(new NombreGrabacionAlternoDTO()
                                    {
                                        Webphone = llamada.Webphone,
                                        Tipo = "",
                                        NombreGrabacion = llamada.NombreGrabacion,
                                        OrigenLlamada = llamada.OrigenLlamada
                                    });
                                }
                                llamada.MinutosPerdidos = null;
                                if (contadorLlamadas > 0)
                                {
                                    var min = ((llamada.FechaInicioLlamada!.Value - primeraFechaFin).TotalMinutes);
                                    if (min < 0) min = 0;
                                    var minTexto = min.ToString("0.0");
                                    minutosLlamada = Convert.ToDouble(minTexto);
                                    llamada.MinutosPerdidos = Convert.ToDouble(minTexto);
                                }
                                primeraFechaFin = llamada.FechaFinLlamada!.Value;
                                minutosTotalLlamada += minutosLlamada;
                                contadorLlamadas++;
                            }
                            itemDetalle.FechaLlamada = item.Llamadas.Select(o => new FechaLlamadaDTO
                            {
                                MinutosPerdidos = o.MinutosPerdidos,
                                Inicio = o.FechaInicioLlamada!.Value.ToString("HH:mm"),
                                Termino = o.FechaFinLlamada!.Value.ToString("HH:mm"),
                                OrigenLlamada = o.OrigenLlamada
                            }).ToList();

                            var minutosOcurrencia = ((itemDetalle.FechaReal - ultimaLlamada.FechaFinLlamada!.Value).TotalMinutes);
                            if (minutosOcurrencia < 0) minutosOcurrencia = 0;
                            itemDetalle.MinutosPerdidosOcurrencia = Convert.ToDouble(minutosOcurrencia.ToString("0.0"));
                        }
                        else
                        {
                            var min = ((itemDetalle.FechaReal - fechaUltimaLlamadaTemp).TotalSeconds).ToString("0.0");
                            minutos = Convert.ToDouble(min);
                            if (minutos >= 0)
                                totalPerdido += minutos;

                            flag = true;
                            fechaUltimaLlamadaTemp = itemDetalle.FechaReal;
                        }

                        itemDetalle.MinutosTotalIntervaleLlamadas = minutosTotalLlamada;
                        itemDetalle.MinutosIntervalo = minutos;
                        itemDetalle.MinutosTotalContesto = totalContesto;
                        itemDetalle.MinutosTotalTimbrado = totalTimbrado;
                        itemDetalle.MinutosTotalPerdido = totalPerdido;

                        if (fechaRealTemp >= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 12, 30, 00)
                            && fechaRealTemp <= new DateTime(filtro.Fecha.Year, filtro.Fecha.Month, filtro.Fecha.Day, 16, 00, 00))
                        {
                            mayorPerdido = mayorPerdido > minutos ? mayorPerdido : minutos;
                            itemDetalle.MayorTiempo = mayorPerdido;
                        }
                        fechaRealTemp = itemDetalle.FechaReal;

                        itemDetalle.Tiempos = item.Llamadas.Select(o => new TiempoTresCXDTO
                        {
                            Webphone = o.Webphone ?? "",
                            TT = ((double)(o.DuracionTimbrado.GetValueOrDefault()) / 60).ToString("0.0") + "m",
                            TC = ((double)(o.DuracionContesto.GetValueOrDefault()) / 60).ToString("0.0") + "m",
                            OrigenLlamada = o.OrigenLlamada
                        }).ToList();

                        itemDetalle.Estados = item.Llamadas.Select(o => new EstadoTresCXDTO
                        {
                            Tipo = o.EstadoLlamada,
                            SubTipo = o.SubEstadoLlamada,
                            OrigenLlamada = o.OrigenLlamada
                        }).ToList();

                        itemDetalle.ExisteLlamadaExitosa = itemDetalle.Estados.Any(x => x.Tipo == "Llamada Exitosa" || (x.Tipo == "Respondido" && x.SubTipo == "Respondido"));
                        itemDetalle.TotalEjecutadas = 0;
                        itemDetalle.TotalNoEjecutadas = 0;
                        itemDetalle.TotalAsignacionManual = 0;

                        final.Add(itemDetalle);
                        contadorActividad++;
                    }
                    catch (Exception ex)
                    {
                        throw new BadRequestException($"Error en la interaccion: {contadorActividad + 1}");
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

        public List<ProcesadoDataChatAsistenteVirtualDTO> ReporteChatAsistenteVirtual(ReporteChatAsistenteVirtualFiltrosDTO? filtro)
        {
            try
            {

                var filtroOrdenado = new ReporteChatAsistenteVirtualFiltroOrdenadoDTO();

                if (filtro.Asesores.Count() > 0)
                {
                    filtroOrdenado.Asesores = string.Join(",", filtro.Asesores);
                }

                filtroOrdenado.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                filtroOrdenado.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);
                

                var data = _unitOfWork.ReporteTresCxRepository.ReporteChatAsistenteVirtual(filtroOrdenado);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

}
