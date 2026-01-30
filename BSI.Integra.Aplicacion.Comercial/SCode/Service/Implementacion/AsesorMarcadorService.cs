using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.Win32;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    public class AsesorMarcadorService : IAsesorMarcadorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public AsesorMarcadorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsesorMarcador, AsesorMarcador>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsesorMarcador, AsesorMarcadorDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<AsesorMarcadorDTO, AsesorMarcador>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        public IEnumerable<AsesorMarcadorDTO> Obtener()
        {
            return _unitOfWork.AsesorMarcadorRepository.Obtener();
        }
        public AsesorMarcadorDTO ObtenerPorIdPersonal(int idPersonal)
        {
            return _unitOfWork.AsesorMarcadorRepository.ObtenerPorIdPersonal(idPersonal);
        }
        public AsesorMarcadorDTO Actualizar(AsesorMarcadorDTO dto, string usuario)
        {
            try
            {
                AsesorMarcador? entidad = new();
                if (dto != null)
                {
                    if (dto.Id != 0)
                    {
                        entidad = _unitOfWork.AsesorMarcadorRepository.ObtenerPorId(dto.Id);
                        if (entidad != null && entidad.Id != 0)
                        {
                            entidad.MarcadorActivo = dto.MarcadorActivo;
                            entidad.UsuarioModificacion = usuario;
                            entidad.FechaModificacion = DateTime.Now;
                            var respuesta = _unitOfWork.AsesorMarcadorRepository.Update(entidad);
                            _unitOfWork.Commit();


                            return dto;
                        }
                        else
                            throw new BadRequestException("Entidad no encontrada");
                    }
                    else
                        throw new BadRequestException("Id Entidad 0");
                }
                else
                    throw new BadRequestException("Entidad Nula");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.AsesorMarcadorRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.AsesorMarcadorRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ReporteFinalMarcadorDTO> ObtenerReporteAsesorMarcadorAutomatico(FiltroReporteAsesorMarcadorDTO filtro)
        {
            try
            {
                filtro.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                filtro.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);


                var registrosDB = _unitOfWork.AsesorMarcadorRepository.ObtenerReporteMarcador(filtro);

                List<int> horas = new List<int>();

                var horasInicio = registrosDB.Where(x => x.FechaModificacion.Hour < 8).OrderBy(x => x.FechaModificacion).ToList();
                if (horasInicio.Count() > 0)
                {
                    var validarActivos = horasInicio.FirstOrDefault(x => x.MarcadorActivo == true);
                    if (validarActivos != null)
                    {
                        for (int i = validarActivos!.FechaModificacion.Hour; i < 8; i++)
                        {
                            horas.Add(i);
                        }
                    }
                }

                for (int i = 8; i < 19; i++)
                {
                    horas.Add(i);
                }

                var horasFin = registrosDB.Where(x => x.FechaModificacion.Hour > 18).OrderBy(x => x.FechaModificacion).ToList();
                if (horasFin.Count() > 0)
                {
                    for (int i = 19; i < horasFin.LastOrDefault()!.FechaModificacion.Hour; i++)
                    {
                        horas.Add(i);
                    }
                }

                var reporteTemp = new List<ReporteFinalMarcadorDTO>();

                var fechas = registrosDB.Select(x => x.FechaModificacion.ToString("yyyy-MM-dd")).ToList();
                fechas = fechas.Distinct().ToList();
                fechas.ForEach(fecha =>
                {

                    var personal = registrosDB.Where(x => x.FechaModificacion.ToString("yyyy-MM-dd") == fecha).Select(x => x.IdPersonal).ToList();
                    personal = personal.Distinct().ToList();

                    foreach (var idPersonal in personal)
                    {
                        var filtroRegistros = registrosDB.Where(x => x.FechaModificacion.ToString("yyyy-MM-dd") == fecha && x.IdPersonal == idPersonal).ToList();
                        filtroRegistros = filtroRegistros.OrderBy(x => x.FechaModificacion).ToList();
                        var resultado = ProcesarReporte(filtroRegistros, horas);
                        reporteTemp = reporteTemp.Concat(resultado).ToList();
                    }
                });
                var reporteFinal = new List<ReporteFinalMarcadorDTO>();

                foreach (var h in horas)
                {
                    var registroFinal = new ReporteFinalMarcadorDTO()
                    {
                        Hora = h,
                        NumVecesDetenido = 0,
                        TiempoTotalDetenido = 0,
                        TiempoPromedioDetencion = 0,
                        PerTiempoDetenido = 0,
                    };

                    var registroHora = reporteTemp.Where(x => x.Hora == h).ToList();
                    foreach (var item in registroHora)
                    {
                        registroFinal.NumVecesDetenido += item.NumVecesDetenido;
                        registroFinal.TiempoTotalDetenido += item.TiempoTotalDetenido;
                        registroFinal.TiempoTotalDetenido = Math.Round(registroFinal.TiempoTotalDetenido, 1);
                    }
                    if (registroFinal.NumVecesDetenido > 0)
                    {
                        registroFinal.TiempoPromedioDetencion = registroFinal.TiempoTotalDetenido / registroFinal.NumVecesDetenido;
                        registroFinal.TiempoPromedioDetencion = Math.Round(registroFinal.TiempoPromedioDetencion, 1);
                    }
                    if (registroFinal.TiempoTotalDetenido > 0)
                    {
                        registroFinal.PerTiempoDetenido = (registroFinal.TiempoTotalDetenido / 60) * 100;
                        registroFinal.PerTiempoDetenido = Math.Round(registroFinal.PerTiempoDetenido, 1);
                    }
                    reporteFinal.Add(registroFinal);
                }


                reporteFinal =  reporteFinal.OrderBy(x => x.Hora).ToList();
                return reporteFinal;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ReporteFinalPromedioDTO> ObtenerReporteAsesorTiempoPromedio(FiltroReporteAsesorMarcadorDTO filtro)
        {
            try
            {
                filtro.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                filtro.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);


                var registrosDB = _unitOfWork.AsesorMarcadorRepository.ObtenerReporteMarcadorTiemposPromedio(filtro);

                return registrosDB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<ReporteFinalMarcadorDTO> ProcesarReporte(List<ReporteAsesorMarcadorDTO> registrosDB, List<int> horas)
        {
            try
            {
                var reporteProcesado = new List<ReporteFinalMarcadorDTO>();
                ReporteAsesorMarcadorDTO? itemAnterior = null;

                foreach (var h in horas)
                {
                    List<ReporteAsesorMarcadorDTO> filtroRegistroHora = new();

                    filtroRegistroHora = registrosDB.Where(x => x.FechaModificacion.Hour == h).ToList();

                    if (filtroRegistroHora.Count() > 0)
                    {
                        var registro = new ReporteFinalMarcadorDTO()
                        {
                            Hora = h,
                            NumVecesDetenido = 0,
                            TiempoTotalDetenido = 0,
                            TiempoPromedioDetencion = 0,
                            PerTiempoDetenido = 0,
                        };

                        for (int i = 0; i < filtroRegistroHora.Count; i++)
                        {
                            if (itemAnterior != null)
                            {
                                if (i == 0)
                                {
                                    var primerItem = filtroRegistroHora.FirstOrDefault()!;
                                    if (primerItem.MarcadorActivo.GetValueOrDefault() == true && itemAnterior.MarcadorActivo.GetValueOrDefault() != true)
                                    {
                                        var horaDiferencia = new DateTime(primerItem.FechaModificacion.Year, primerItem.FechaModificacion.Month, primerItem.FechaModificacion.Day, h, 00, 00);

                                        var diferencia = primerItem.FechaModificacion - horaDiferencia;

                                        registro.TiempoTotalDetenido += diferencia.TotalMinutes;
                                    }
                                }
                                else
                                {
                                    if (filtroRegistroHora[i].MarcadorActivo.GetValueOrDefault() == true)
                                    {
                                        var diferencia = filtroRegistroHora[i].FechaModificacion - itemAnterior.FechaModificacion;
                                        registro.TiempoTotalDetenido += diferencia.TotalMinutes;
                                    }
                                }
                                if (filtroRegistroHora[i].MarcadorActivo.GetValueOrDefault() != itemAnterior.MarcadorActivo)
                                {
                                    if (filtroRegistroHora[i].MarcadorActivo.GetValueOrDefault() == false)
                                    {
                                        registro.NumVecesDetenido++;
                                    }
                                }
                            }
                            itemAnterior = filtroRegistroHora[i];
                        }

                        var ultimoItem = filtroRegistroHora.LastOrDefault()!;
                        if (ultimoItem.MarcadorActivo.GetValueOrDefault() == false && h != horas.LastOrDefault())
                        {
                            var horaDiferencia = new DateTime(ultimoItem.FechaModificacion.Year, ultimoItem.FechaModificacion.Month, ultimoItem.FechaModificacion.Day, h, 59, 59);
                            if (DateTime.Now.ToString("dd/MM/yyyy") == ultimoItem.FechaModificacion.ToString("dd/MM/yyyy"))
                            {
                                if (DateTime.Now.Hour == h)
                                {
                                    horaDiferencia = DateTime.Now;
                                }
                            }

                            var diferencia = horaDiferencia - ultimoItem.FechaModificacion;

                            registro.TiempoTotalDetenido += diferencia.TotalMinutes;
                        }

                        if (registro.NumVecesDetenido > 0)
                        {
                            registro.TiempoPromedioDetencion = registro.TiempoTotalDetenido / registro.NumVecesDetenido;
                        }
                        if (registro.TiempoTotalDetenido > 0)
                        {
                            registro.PerTiempoDetenido = (registro.TiempoTotalDetenido / 60) * 100;
                        }
                        reporteProcesado.Add(registro);
                    }
                    else
                    {
                        var registroNulo = new ReporteFinalMarcadorDTO()
                        {
                            Hora = h,
                            NumVecesDetenido = 0,
                            TiempoTotalDetenido = 0,
                            TiempoPromedioDetencion = 0,
                            PerTiempoDetenido = 0,
                        };
                        if (itemAnterior != null)
                        {
                            if (DateTime.Now.ToString("dd/MM/yyyy") == itemAnterior.FechaModificacion.ToString("dd/MM/yyyy"))
                            {
                                if (itemAnterior.MarcadorActivo.GetValueOrDefault() == false)
                                {
                                    if (DateTime.Now.Hour == h)
                                    {
                                        var diferencia = DateTime.Now - new DateTime(itemAnterior.FechaModificacion.Year, itemAnterior.FechaModificacion.Month, itemAnterior.FechaModificacion.Day, h, 0, 0);
                                        registroNulo.TiempoTotalDetenido = diferencia.TotalMinutes;
                                    }
                                    else if(DateTime.Now.Hour > h)
                                    {
                                        registroNulo.TiempoTotalDetenido = 60;
                                    }
                                }
                            }
                            else
                            {
                                registroNulo.TiempoTotalDetenido = itemAnterior.MarcadorActivo.GetValueOrDefault() ? 0 : 60;
                            }
                        }

                        if (itemAnterior != null)
                        {
                            itemAnterior = new ReporteAsesorMarcadorDTO()
                            {
                                IdPersonal = itemAnterior.IdPersonal,
                                MarcadorActivo = itemAnterior.MarcadorActivo,
                                FechaModificacion = new DateTime(itemAnterior.FechaModificacion.Year, itemAnterior.FechaModificacion.Month, itemAnterior.FechaModificacion.Day, h, 0, 0)
                            };
                        }
                        reporteProcesado.Add(registroNulo);
                    }
                }
                return reporteProcesado;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
