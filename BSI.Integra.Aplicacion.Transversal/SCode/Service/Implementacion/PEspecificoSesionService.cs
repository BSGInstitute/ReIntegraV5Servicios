using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PEspecificoSesionService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_PEspecifico
    /// </summary>
    public class PEspecificoSesionService : IPEspecificoSesionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PEspecificoSesionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesion, PEspecificoSesion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPespecifico, PEspecifico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Daniel Huaita Carpio
        /// Fecha: 08/02/2023
        /// Version: 2.0
        /// <summary>
        /// Se obtiene la lista de sesiones por el idPespecifico
        /// </summary>
        /// <param  name="idPespecifico">Id del pespecifico (PK de la tabla pla.T_PEspecifico)</param>
        /// <returns>Cadena con la lista de cursos</returns>
        public List<PEspecificoSesionRecuperacionDTO> ObtenerSesionesPorPEspecifico(int idPespecifico, int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.PEspecificoSesionRepository.ObtenerSesionesPorPEspecifico(idPespecifico, idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la proxima sesion segun el idPEspecifico y un tipo
        /// </summary>
        /// <param name="idPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="tipo">Tipo Programa Especifico</param>
        /// <returns> List<EspecificoFechasInicioDTO> </returns>
        public List<PEspecificoSesionFechaHoraInicioDTO> ObtenerFechaHoraInicioSesionPorIdPEspecifico(List<int> idPEspecifico, int tipo)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> fechasHoraInicio = new();
                if (tipo == 1)
                    fechasHoraInicio = _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioPorIdsPEspecificoPadre(idPEspecifico);
                else if (tipo == 2)
                    fechasHoraInicio = _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioPorIdsPEspecifico(idPEspecifico);
                else
                    fechasHoraInicio = _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioSinSesionPorIdsPEspecifico(idPEspecifico);

                if (fechasHoraInicio.Any())
                    return fechasHoraInicio;
                else if (tipo == 2)
                    return _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioSinSesionPorIdsPEspecifico(idPEspecifico);
                else
                    return fechasHoraInicio;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 30/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la proxima sesion segun el idPEspecifico y un tipo
        /// </summary>
        /// <param name="idPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="tipo">Tipo Programa Especifico</param>
        /// <returns> List<EspecificoFechasInicioDTO> </returns>
        public async Task<List<PEspecificoSesionFechaHoraInicioDTO>> ObtenerFechaHoraInicioSesionPorIdPEspecificoAsync(List<int> idPEspecifico, int tipo)
        {
            try
            {
                List<PEspecificoSesionFechaHoraInicioDTO> fechasHoraInicio = new();

                if (tipo == 1)
                    fechasHoraInicio = await _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioPorIdsPEspecificoPadreAsync(idPEspecifico);
                else if (tipo == 2)
                    fechasHoraInicio = await _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioPorIdsPEspecificoAsync(idPEspecifico);
                else
                    fechasHoraInicio = await _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioSinSesionPorIdsPEspecificoAsync(idPEspecifico);

                if (fechasHoraInicio.Any())
                    return fechasHoraInicio;
                else if (tipo == 2)
                    return await _unitOfWork.PEspecificoSesionRepository.ObtenerFechaHoraInicioSinSesionPorIdsPEspecificoAsync(idPEspecifico);
                else
                    return fechasHoraInicio;
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 22/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cronograma Individual Pespecifico
        /// </summary>
        /// <param name="programaEspecifico"></param>
        /// <returns></returns>
        public IEnumerable<PespecificoSesionCompuestoDTO> ObtenerCronogramaIndividualPorPEspecificoAlterno(DatosProgramaEspecificoDTO programaEspecifico)
        {
            try
            {
                var listaSesiones = _unitOfWork.PEspecificoSesionRepository.ObtenerInformacionProgramaEspecificoSesion(programaEspecifico.Id);
                var rpta = listaSesiones.Select(Sesiones => new PespecificoSesionCompuestoDTO
                {
                    Id = Sesiones.Id,
                    FechaHoraInicio = Sesiones.FechaHoraInicio,
                    Duracion = Sesiones.Duracion,
                    DuracionTotal = string.IsNullOrEmpty(programaEspecifico.Duracion) ? 0 : Convert.ToDecimal(programaEspecifico.Duracion),
                    Curso = programaEspecifico.Nombre,
                    IdExpositor = Sesiones.IdExpositor,
                    IdProveedor = Sesiones.IdProveedor,
                    IdAmbiente = Sesiones.IdAmbiente,
                    IdCiudad = programaEspecifico.IdCiudad,
                    PEspecificoHijoId = programaEspecifico.Id,
                    Tipo = programaEspecifico.Tipo,
                    Comentario = Sesiones.Comentario,
                    EsSesionInicial = Sesiones.Id == programaEspecifico.IdSesion_Inicio,
                    MostrarPDF = true
                });
                return rpta;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene cronograma Individual Pespecifico
        /// </summary>
        /// <param name="programaEspecifico"></param>
        /// <returns></returns>
        public VerificarFechaSesionDTO VerificarFechaSesion(int idSesion, DateTime fecha)
        {
            try
            {
                var sesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(idSesion);
                fecha.AddHours(-5);
                DateTime FechaInicio = fecha, FechaFin = fecha.AddHours(Convert.ToDouble(sesion.Duracion));

                var expositor = _unitOfWork.ExpositorRepository.ObtenerPorId(sesion.IdExpositor.Value);

                Ambiente? ambiente = null;

                if (sesion.IdAmbiente.HasValue)
                    ambiente = _unitOfWork.AmbienteRepository.ObtenerPorId(sesion.IdAmbiente.Value);
                VerificarFechaSesionDTO resultado = new();

                var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(sesion.IdPespecifico);
                //var ListaFeriados = _unitOfWork.FeriadoRepository.GetBy(s => (s.Frecuencia == 0 && s.Dia.Day == fecha.Day && s.Dia.Month == fecha.Month && s.IdTroncalCiudad == pEspecifico.IdCiudad) ||
                //                                    (s.Frecuencia == 1 && s.Dia.Day == fecha.Day && s.Dia.Month == fecha.Month && s.Dia.Year == fecha.Year && s.IdTroncalCiudad == pEspecifico.IdCiudad))
                //                        .ToList();

                //if (ListaFeriados.Count != 0)
                //    return new { Feriados = ListaFeriados };

                var listaCrucesExpositor = _unitOfWork.PEspecificoSesionRepository.GetBy(s => s.Id != idSesion
                        && (s.FechaHoraInicio <= FechaInicio && FechaInicio <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)).AddSeconds(-1))
                        || (s.FechaHoraInicio.AddSeconds(1) <= FechaFin && FechaFin <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)))
                    ).Where(s => expositor == null ? false : expositor.Id == s.IdExpositor);

                var listaCrucesAmbiente = _unitOfWork.PEspecificoSesionRepository.GetBy(s => s.Id != idSesion
                        && ((s.FechaHoraInicio <= FechaInicio && FechaInicio <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion)).AddSeconds(-1))
                        || (s.FechaHoraInicio.AddSeconds(1) <= FechaFin && FechaFin <= s.FechaHoraInicio.AddHours(Convert.ToDouble(s.Duracion))))
                    ).Where(s => ambiente == null || ambiente.Virtual ? false : ambiente.Id == s.IdAmbiente);

                resultado.CrucesExpositor = (from cruces in listaCrucesExpositor
                                             join pespecificos in _unitOfWork.PEspecificoRepository.GetAll() on cruces.IdPespecifico equals pespecificos.Id
                                             join ambientes in _unitOfWork.AmbienteRepository.GetAll() on cruces.IdAmbiente equals ambientes.Id into ambientesB
                                             join expositores in _unitOfWork.ExpositorRepository.GetAll() on cruces.IdExpositor equals expositores.Id into expositoresB
                                             from expositorData in expositoresB.DefaultIfEmpty()
                                             from ambientesData in ambientesB.DefaultIfEmpty()
                                             select new CruceFechaSesionDTO
                                             {
                                                 SesionId = cruces.Id,
                                                 SesionFechaInicio = cruces.FechaHoraInicio.ToString(),
                                                 SesionFechaFin = cruces.FechaHoraInicio.AddHours(Convert.ToDouble(cruces.Duracion)).ToString(),
                                                 SesionComentario = cruces.Comentario,
                                                 PespecificoId = pespecificos.Id,
                                                 PespecificoNombre = pespecificos.Nombre,
                                                 AmbienteId = ambientesData == null ? "_" : ambientesData.Id.ToString(),
                                                 AmbienteNombre = ambientesData == null ? "(No tiene Ambiente)" : ambientesData.Nombre,
                                                 ExpositorId = expositorData == null ? 0 : expositorData.Id,
                                                 ExpositorNombre = expositorData == null ? "(No tiene Expositor)" : expositorData.PrimerNombre
                                             }).ToList();

                resultado.CrucesAmbiente = (from cruces in listaCrucesAmbiente
                                            join pespecificos in _unitOfWork.PEspecificoRepository.GetAll() on cruces.IdPespecifico equals pespecificos.Id
                                            join ambientes in _unitOfWork.AmbienteRepository.GetAll() on cruces.IdAmbiente equals ambientes.Id into ambientesB
                                            join expositores in _unitOfWork.ExpositorRepository.GetAll() on cruces.IdExpositor equals expositores.Id into expositoresB
                                            from expositorData in expositoresB.DefaultIfEmpty()
                                            from ambientesData in ambientesB.DefaultIfEmpty()
                                            select new CruceFechaSesionDTO
                                            {
                                                SesionId = cruces.Id,
                                                SesionFechaInicio = cruces.FechaHoraInicio.ToString(),
                                                SesionFechaFin = cruces.FechaHoraInicio.AddHours(Convert.ToDouble(cruces.Duracion)).ToString(),
                                                SesionComentario = cruces.Comentario,
                                                PespecificoId = pespecificos.Id,
                                                PespecificoNombre = pespecificos.Nombre,
                                                AmbienteId = ambientesData == null ? "_" : ambientesData.Id.ToString(),
                                                AmbienteNombre = ambientesData == null ? "(No tiene Ambiente)" : ambientesData.Nombre,
                                                ExpositorId = expositorData == null ? 0 : expositorData.Id,
                                                ExpositorNombre = expositorData == null ? "(No tiene Expositor)" : expositorData.PrimerNombre
                                            }).ToList();

                return resultado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Valida los cruces entre fechas y docentes en los cronogramas
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> DTO - ActualizarPEspecificoSesionDTO - respuesta </returns>
        public (bool EstadoCruce, IEnumerable<CruceSesionPEspecificoDTO?> Cruces, string? Detalle) ActualizarDatosCronogramaSesiones(InformacionCronogramaSesionesDTO dto, string usuario)
        {
            try
            {
                var sesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(dto.Id);
                if (sesion == null || sesion.Id == 0)
                {
                    throw new BadRequestException("#PeSS-ADCS-001@No existe pespecifico sesion");
                }
                double duracion = Convert.ToDouble(sesion.Duracion);
                var fechasCruces = _unitOfWork.PEspecificoSesionRepository.ValidarCrucesSesiones(dto, duracion);
                if (fechasCruces.Count() > 0)
                {
                    return (true, fechasCruces, null);
                }
                else
                {
                    string detalle = string.Empty;
                    if (dto.IdAmbiente != null)
                    {
                        sesion.IdAmbiente = (dto.IdAmbiente == -1) ? null : dto.IdAmbiente;
                    }
                    if (dto.IdProveedor != null)
                    {
                        if (dto.AplicarCambios && dto.IdProveedor != sesion.IdProveedor)
                        {
                            var furs = _unitOfWork.FurRepository.ObtenerFurProgramaEspecifico(sesion.IdPespecifico, true);
                            foreach (var item in furs)
                            {
                                var fur = _unitOfWork.FurRepository.ObtenerPorId(item.Id)!;
                                var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(fur.IdProducto!.Value, dto.IdProveedor.Value);
                                if (detalleFur != null)
                                {
                                    var producto = _unitOfWork.ProductoRepository.ObtenerProductoCuentaContable(fur.IdProducto.Value).FirstOrDefault();
                                    fur.UsuarioSolicitud = usuario;
                                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                                    fur.Cuenta = detalleFur.CuentaDescripcion;
                                    fur.IdProveedor = dto.IdProveedor;
                                    fur.IdProducto = fur.IdProducto.Value;
                                    fur.Descripcion = producto.DescripcionProducto;
                                    fur.IdProductoPresentacion = producto.IdProductoPresentacion;
                                    fur.PrecioUnitarioMonedaOrigen = detalleFur.PrecioOrigen;
                                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.PrecioOrigen * fur.Cantidad);
                                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * fur.Cantidad);
                                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * fur.Cantidad;
                                    fur.PagoDolares = detalleFur.PrecioDolares * fur.Cantidad;
                                    fur.IdMonedaPagoReal = detalleFur.IdMonedaPago;
                                    fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                                    fur.UsuarioModificacion = usuario;
                                    fur.FechaModificacion = DateTime.Now;
                                    _unitOfWork.FurRepository.Update(fur);
                                    _unitOfWork.Commit();
                                }
                                else
                                {
                                    detalle += $" * No existe asociación entre Producto: {fur.IdProducto!.Value} y Proveedor: {dto.IdProveedor.Value};";
                                }
                            }
                        }
                        sesion.IdProveedor = (dto.IdProveedor == -1) ? null : dto.IdProveedor;
                    }
                    if (dto.GrupoSesion != null)
                        sesion.GrupoSesion = dto.GrupoSesion;

                    sesion.UsuarioModificacion = usuario;
                    sesion.FechaModificacion = DateTime.Now;
                    if (sesion.FechaHoraInicio != dto.FechaHoraInicio)
                    {

                        if (dto.AplicarCambios)
                        {
                            var furs = _unitOfWork.FurRepository.ObtenerFurProgramaEspecifico(sesion.IdPespecifico, false);
                            if (furs.Count() > 0)
                            {
                                foreach (var item in furs)
                                {
                                    var fur = _unitOfWork.FurRepository.ObtenerPorId(item.Id);
                                    if (fur != null)
                                    {
                                        if (fur.IdFurFaseAprobacion1 == ValorEstatico.IdFurProyectado
                                            || fur.IdFurFaseAprobacion1 == ValorEstatico.IdFurEstadoPorAprobar)
                                        {
                                            var nroSemana = _unitOfWork.FurRepository.ObtenerNumeroSemana(dto.FechaHoraInicio);
                                            fur.FechaLimite = dto.FechaHoraInicio;
                                            fur.NumeroSemana = nroSemana;
                                            fur.UsuarioModificacion = usuario;
                                            fur.FechaModificacion = DateTime.Now;
                                            _unitOfWork.FurRepository.Update(fur);
                                            _unitOfWork.Commit();
                                        }
                                    }
                                }
                            }
                        }
                        EnviarNotificacionCambioFechaSesion(dto, sesion, usuario);
                        sesion.UrlWebex = string.Empty;
                        sesion.CuentaWebex = null;
                    }
                    sesion.FechaHoraInicio = dto.FechaHoraInicio;
                    sesion.MostrarPortalWeb = dto.MostrarPortalWeb;
                    _unitOfWork.PEspecificoSesionRepository.Update(sesion);
                    _unitOfWork.Commit();

                    if (dto.IdModalidadCurso != null && dto.IdModalidadCurso != -1)
                    {
                        _unitOfWork.PEspecificoSesionRepository.ActualizarModalidadSesion(sesion.IdPespecifico, sesion.Grupo, dto.IdModalidadCurso.Value, usuario);
                    }

                    if (sesion.Id == _unitOfWork.PEspecificoSesionRepository.ObtenerSesionInicial(sesion.IdPespecifico, sesion.Grupo))
                    {
                        Proveedor proveedor = new Proveedor();
                        if (sesion.IdProveedor != null)
                        {
                            proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(sesion.IdProveedor.Value)!;
                        }
                        PespecificoParticipacionExpositor pEspecificoParticipacionExpositor = new PespecificoParticipacionExpositor();
                        if (_unitOfWork.PespecificoParticipacionExpositorRepository.Exist(w => w.IdPespecifico == sesion.IdPespecifico && w.Grupo == sesion.Grupo))
                        {
                            pEspecificoParticipacionExpositor = _unitOfWork.PespecificoParticipacionExpositorRepository.ObtenerPorIdPespecificoYGrupo(sesion.IdPespecifico, sesion.Grupo)!;
                            pEspecificoParticipacionExpositor.IdExpositorGrupo = sesion.IdExpositor;
                            pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                            pEspecificoParticipacionExpositor.ExpositorGrupo = sesion.IdProveedor == null ? null : proveedor.Nombre1 + " " + proveedor.Nombre2 + " " + proveedor.ApePaterno + " " + proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;
                        }
                        else
                        {
                            pEspecificoParticipacionExpositor.IdPespecifico = sesion.IdPespecifico;
                            pEspecificoParticipacionExpositor.IdExpositorGrupo = sesion.IdExpositor;
                            pEspecificoParticipacionExpositor.ExpositorGrupo = sesion.IdProveedor == null ? null : proveedor.Nombre1 + " " + proveedor.Nombre2 + " " + proveedor.ApePaterno + " " + proveedor.ApeMaterno;
                            pEspecificoParticipacionExpositor.IdProveedorPlanificacionGrupo = sesion.IdProveedor;
                            pEspecificoParticipacionExpositor.Grupo = sesion.Grupo;
                            pEspecificoParticipacionExpositor.Estado = true;
                            pEspecificoParticipacionExpositor.FechaCreacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.FechaModificacion = DateTime.Now;
                            pEspecificoParticipacionExpositor.UsuarioCreacion = usuario;
                            pEspecificoParticipacionExpositor.UsuarioModificacion = usuario;
                        }
                        _unitOfWork.PespecificoParticipacionExpositorRepository.Update(pEspecificoParticipacionExpositor);
                        _unitOfWork.Commit();
                    }
                    return (false, null, detalle);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviarNotificacionCambioFechaSesion(InformacionCronogramaSesionesDTO dto, PEspecificoSesion sesion, string usuario)
        {
            try
            {
                DateTime fechaDeHoy = DateTime.Now;
                TimeSpan diferencia = dto.FechaHoraInicio - fechaDeHoy;
                int diferenciaEnDias = diferencia.Days;

                PEspecifico pEspecificoHijo = _unitOfWork.PEspecificoRepository.ObtenerPorId(sesion.IdPespecifico)!;
                PEspecifico? pEspecificoPadre = null;

                if (pEspecificoHijo.CursoIndividual != true)
                {
                    var resultado = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPorPEspecificoHijoId(sesion.IdPespecifico);
                    pEspecificoPadre = _unitOfWork.PEspecificoRepository.ObtenerPorId(resultado.PespecificoPadreId)!;
                }

                if (diferenciaEnDias < 4 || !string.IsNullOrEmpty(sesion.UrlWebex))
                {
                    List<string> correosPersonalizados = new List<string>
                    {
                        "wvalencia@bsginstitute.com",
                        "wruiz@bsginstitute.com",
                        "sruiz@bsginstitute.com",
                        "salarconq@bsginstitute.com",
                        "lcarpio@bsginstitute.com"
                    };
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "gcanasac@bsginstitute.com",
                        "fmamanif@bsginstitute.com"
                    };

                    string mensaje = "<h3> NOTIFICACIÓN MODIFICACIÓN DE SESIONES! </h3> <br>\n\n";
                    mensaje += "<br>\n\n";
                    mensaje += "Se modifico la siguiente Sesion: <br>\n\n";
                    mensaje += "<br>\n\n";
                    if (pEspecificoHijo.CursoIndividual != true)
                    {
                        mensaje += "Nombre Programa: " + pEspecificoPadre!.Nombre + "<br>\n\n";
                        mensaje += "Id Pespecifico: " + pEspecificoPadre.Id + "<br>\n\n";
                        mensaje += "Nombre Curso: " + pEspecificoHijo.Nombre + "<br>\n\n";
                        mensaje += "Id Curso: " + pEspecificoHijo.Id + "<br>\n\n";
                    }
                    else
                    {
                        mensaje += "Nombre Programa: " + pEspecificoHijo.Nombre + "<br>\n\n";
                        mensaje += "Id Pespecifico: " + pEspecificoHijo.Id + "<br>\n\n";
                    }
                    mensaje += "Fecha Sesion Anterior: " + sesion.FechaHoraInicio.ToString("yyyy/MM/dd hh:mm:ss") + "<br>\n\n";
                    mensaje += "Fecha Sesion Modificada: " + dto.FechaHoraInicio.ToString("yyyy/MM/dd hh:mm:ss") + "<br>\n\n";
                    mensaje += "Fecha Modificación: " + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "<br>\n\n";
                    mensaje += "Usuario Modificación: " + usuario + "<br>\n\n";

                    var mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = "soportetecnico@bsginstitute.com",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = "Notificación: Se cambio la fecha de Sesion Programada",
                        Message = mensaje,
                        Cc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        Bcc = ""
                    };
                    try
                    {
                        var mailService = new TMK_MailService();
                        mailService.SetData(mailDataPersonalizado);
                        mailService.SendMessageTask();
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public bool ActualizarFechaParaSesionRecorrerFechas(PEspecificoSesion pEspecificoSesion, string usuario)
        {
            var feriados = _mapper.Map<List<FeriadoDTO>>(_unitOfWork.FeriadoRepository.ObtenerPorTipo(0));
            var pEspecificoPadre = (from sesion in _unitOfWork.PEspecificoSesionRepository.GetAll()
                                    join pEspeficoHijo in _unitOfWork.PespecificoPadrePespecificoHijoRepository.GetAll() on sesion.IdPespecifico equals pEspeficoHijo.PespecificoHijoId
                                    join pEspecifico in _unitOfWork.PEspecificoRepository.GetAll() on pEspeficoHijo.PespecificoPadreId equals pEspecifico.Id
                                    where sesion.Id == pEspecificoSesion.Id
                                    select pEspecifico).FirstOrDefault();

            if (pEspecificoPadre == null) //Tiene que ser Curso Individual
            {
                pEspecificoPadre = (from sesiones in _unitOfWork.PEspecificoSesionRepository.GetAll()
                                    join PEspecifico in _unitOfWork.PEspecificoRepository.GetAll() on sesiones.IdPespecifico equals PEspecifico.Id
                                    where sesiones.Id == pEspecificoSesion.Id
                                    select PEspecifico).FirstOrDefault();

                if (!pEspecificoPadre.CursoIndividual.Value)
                    throw new BadRequestException("Algo anda mal! Este Curso no es individual");
            }

            var frecuenciaList = (from tablaFrecuencia in _unitOfWork.PespecificoFrecuenciaRepository.GetAll()
                                  where tablaFrecuencia.IdPespecifico == pEspecificoPadre.Id
                                  select tablaFrecuencia).ToList();

            if (frecuenciaList.Count() != 1)
                throw new BadRequestException("El Programa Especifico no tiene frecuencia o tiene mas de una");

            var frecuencia = frecuenciaList.FirstOrDefault();
            var FrecuenciaGeneral = _unitOfWork.FrecuenciaRepository.ObtenerPorId(frecuencia.IdFrecuencia.Value);
            var detelleFrecuencia = (from frecueciaDetalle in _unitOfWork.PespecificoFrecuenciaDetalleRepository.GetAll()
                                     where frecueciaDetalle.IdPespecificoFrecuencia == frecuencia.Id
                                     select frecueciaDetalle).ToList();

            var diasFrecuencia = detelleFrecuencia.Select(s => s.DiaSemana).ToArray();
            IQueryable<TPespecificoSesion> listaSesiones;
            if (!pEspecificoPadre.CursoIndividual.Value)
                listaSesiones = (from cursos in _unitOfWork.PespecificoPadrePespecificoHijoRepository.GetAll()
                                 join sesiones in _unitOfWork.PEspecificoSesionRepository.GetAll() on cursos.PespecificoHijoId equals sesiones.IdPespecifico
                                 where cursos.PespecificoPadreId == pEspecificoPadre.Id && sesiones.SesionAutoGenerada
                                 select sesiones);
            else
                listaSesiones = (from sesiones in _unitOfWork.PEspecificoSesionRepository.GetAll()
                                 where sesiones.IdPespecifico == pEspecificoPadre.Id && sesiones.SesionAutoGenerada
                                 select sesiones);

            var sesionesNoCambian = listaSesiones.Where(s => s.FechaHoraInicio <= pEspecificoSesion.FechaHoraInicio).OrderBy(s => s.FechaHoraInicio).ToList();
            var sesionesCambiar = listaSesiones.Where(s => s.FechaHoraInicio > pEspecificoSesion.FechaHoraInicio).OrderBy(s => s.FechaHoraInicio).ToList();



            //Obtenemos lista de sesiones con sus cursos
            List<EsquemaSesionesDTO> estructuraSesiones = new List<EsquemaSesionesDTO>();
            estructuraSesiones = (from sesiones in sesionesCambiar
                                  join TablaPEspecifico in _unitOfWork.PEspecificoRepository.GetAll() on sesiones.IdPespecifico equals TablaPEspecifico.Id
                                  select new EsquemaSesionesDTO
                                  {
                                      Curso = _mapper.Map<InformacionPespecificoHijoDTO>(TablaPEspecifico),
                                      Dia = (byte)sesiones.FechaHoraInicio.DayOfWeek,
                                      Duracion = sesiones.Duracion,
                                      FechaAsignar = sesiones.FechaHoraInicio,
                                      SesionId = sesiones.Id
                                  }).ToList();

            int DetalleFrecuenciaAModificar = (sesionesNoCambian.Count) % frecuencia.NroSesiones;
            var fechaAsignar = pEspecificoSesion.FechaHoraInicio;

            int diaAsigar = (int)fechaAsignar.DayOfWeek;

            for (int i = 0; i < diasFrecuencia.Length; i++)
            {
                if (diasFrecuencia[i] >= diaAsigar)
                {
                    DetalleFrecuenciaAModificar = i;
                    break;
                }
            }

            if (detelleFrecuencia[DetalleFrecuenciaAModificar].DiaSemana == (byte)pEspecificoSesion.FechaHoraInicio.DayOfWeek)
            {
                DateTime fechaNueva = pEspecificoSesion.FechaHoraInicio.AddHours(Convert.ToDouble(pEspecificoSesion.Duracion));
                DateTime fechasesion =
                    new DateTime(pEspecificoSesion.FechaHoraInicio.Year, pEspecificoSesion.FechaHoraInicio.Month, pEspecificoSesion.FechaHoraInicio.Day, detelleFrecuencia[DetalleFrecuenciaAModificar].HoraDia.Hours, detelleFrecuencia[DetalleFrecuenciaAModificar].HoraDia.Minutes, 0);
                if (fechaNueva.AddSeconds(1) > fechasesion)
                {
                    DetalleFrecuenciaAModificar = (DetalleFrecuenciaAModificar + 1) % diasFrecuencia.Length;
                }
            }
            DateTime fechaTemp = fechaAsignar;

            using (TransactionScope scope = new TransactionScope())
            {
                //Cambiar fecha la sesion que se seleccion
                for (int i = 0; i < estructuraSesiones.Count; i++)
                {
                    if ((DetalleFrecuenciaAModificar) % diasFrecuencia.Length == 0 && i != 0)
                    {
                        fechaAsignar = fechaTemp.AddDays(FrecuenciaGeneral.NumDias);
                        fechaTemp = fechaAsignar;
                    }
                    fechaAsignar = ObtenerFechaAsignar(estructuraSesiones[i].Curso, fechaAsignar, diasFrecuencia[DetalleFrecuenciaAModificar], diasFrecuencia, feriados);

                    fechaAsignar = fechaAsignar.Date + detelleFrecuencia[(DetalleFrecuenciaAModificar + i) % diasFrecuencia.Length].HoraDia;
                    estructuraSesiones[i].FechaAsignar = fechaAsignar;

                    //Actualiza la sesion
                    var sesion = _unitOfWork.PEspecificoSesionRepository.FirstBy(s => s.Id == estructuraSesiones[i].SesionId);
                    sesion.FechaHoraInicio = estructuraSesiones[i].FechaAsignar.Value;
                    sesion.FechaModificacion = DateTime.Now;
                    sesion.UsuarioModificacion = usuario;
                }
                scope.Complete();
            }
            return true;
        }
        private DateTime GetNextWeekday(DateTime inicio, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)inicio.DayOfWeek + 7) % 7;
            return inicio.AddDays(daysToAdd);
        }
        private bool FechaEsFeriadoPorCiudad(DateTime fecha, IEnumerable<FeriadoDTO> feriados, int idCiudad)
        {
            var listaFeriados = feriados.Where(s => s.IdTroncalCiudad == idCiudad); //buscar todos los feriados de la ciudad
            foreach (var feriado in listaFeriados)
            {
                if (feriado.Frecuencia == 1 && //si feriado es unico
                    feriado.Dia.Year == fecha.Year &&
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
                if (feriado.Frecuencia == 0 && //si feriado es anual
                    feriado.Dia.Month == fecha.Month &&
                    feriado.Dia.Day == fecha.Day)
                    return true;
            }
            return false;
        }
        public bool EstablecerSesionInicial(int idProgramaEspecificoSesion, string usuario)
        {
            try
            {
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(idProgramaEspecificoSesion);
                var programaEspecificoPadre = _unitOfWork.PespecificoPadrePespecificoHijoRepository.ObtenerPorPEspecificoHijoId(programaEspecificoSesion.IdPespecifico);

                int idPespecifico = 0;
                if (programaEspecificoSesion.Grupo == 1)
                {
                    if (programaEspecificoPadre != null && programaEspecificoPadre.Id != 0)
                        idPespecifico = programaEspecificoPadre.PespecificoPadreId;
                    else
                        idPespecifico = programaEspecificoSesion.IdPespecifico;

                    var pEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idPespecifico);
                    pEspecifico.IdSesionInicio = programaEspecificoSesion.Id;
                    pEspecifico.UsuarioModificacion = usuario;
                    pEspecifico.FechaModificacion = DateTime.Now;
                    _unitOfWork.PEspecificoRepository.Update(pEspecifico);
                    _unitOfWork.Commit();
                }

                if (programaEspecificoPadre != null)
                {
                    var listaIdSesiones = _unitOfWork.PEspecificoSesionRepository.ObtenerIdSesiones(programaEspecificoPadre.PespecificoPadreId, programaEspecificoSesion.Grupo);
                    var sesiones = _unitOfWork.PEspecificoSesionRepository.ObtenerPorIds(listaIdSesiones);
                    foreach (var item in sesiones)
                    {
                        item.EsSesionInicio = false;
                        item.UsuarioModificacion = usuario;
                        item.FechaModificacion = DateTime.Now;
                    }
                    _unitOfWork.PEspecificoSesionRepository.Update(sesiones);
                    _unitOfWork.Commit();
                }
                else
                {
                    var listaIdSesionesIndividuales = _unitOfWork.PEspecificoSesionRepository.ObtenerIdSesionesIndividuales(programaEspecificoSesion.IdPespecifico, programaEspecificoSesion.Grupo);
                    var sesionesIndividuales = _unitOfWork.PEspecificoSesionRepository.ObtenerPorIds(listaIdSesionesIndividuales);
                    foreach (var item in sesionesIndividuales)
                    {
                        item.EsSesionInicio = false;
                        item.UsuarioModificacion = usuario;
                        item.FechaModificacion = DateTime.Now;
                    }
                    _unitOfWork.PEspecificoSesionRepository.Update(sesionesIndividuales);
                    _unitOfWork.Commit();
                }

                var entidad = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(idProgramaEspecificoSesion);
                entidad.EsSesionInicio = true;
                entidad.UsuarioModificacion = usuario;
                entidad.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoSesionRepository.Update(entidad);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public DateTime ObtenerFechaAsignar(InformacionPespecificoHijoDTO curso, DateTime fechaAsignar, byte dia, byte[] diasFrecuencia, IEnumerable<FeriadoDTO> listaFeriados)
        {
            int cont = Array.IndexOf(diasFrecuencia, dia);
            DateTime fechaTemp;

            while (true)
            {
                fechaTemp = fechaAsignar;
                fechaAsignar = ObtenerProximoDiaSemana(fechaAsignar, (DayOfWeek)diasFrecuencia[cont]);
                if (fechaAsignar.DayOfYear == fechaTemp.DayOfYear && cont == (cont + 1) % diasFrecuencia.Length)
                {
                    fechaAsignar = fechaAsignar.AddDays(1);
                    continue;
                }

                //si es feriado continuar con la siguiente sesion
                if (FechaEsFeriadoPorCiudad(fechaAsignar, listaFeriados, curso.IdCiudad.Value))
                {
                    cont = (cont + 1) % diasFrecuencia.Length;
                    fechaAsignar = fechaAsignar.AddDays(1);
                }
                else
                    break;
            }

            return fechaAsignar;
        }
        private DateTime ObtenerProximoDiaSemana(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }
        public bool CancelarWebinar(CancelarWebinarDTO dto, string usuario)
        {
            try
            {
                PEspecificoSesion Objeto = new PEspecificoSesion();
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(dto.IdPEspecificoSesion);
                programaEspecificoSesion.FechaCancelacionWebinar = DateTime.Now;
                programaEspecificoSesion.ComentarioCancelacionWebinar = dto.ComentarioCancelacion;
                programaEspecificoSesion.UsuarioModificacion = usuario;
                programaEspecificoSesion.EsWebinarConfirmado = dto.Confirmo;
                _unitOfWork.PEspecificoSesionRepository.Update(programaEspecificoSesion);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool EliminarSesion(int idProgramaEspecifico, int idProgramaEspecificoSesion, string usuario)
        {
            try
            {
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(idProgramaEspecifico);
                if (programaEspecifico == null)
                {
                    throw new BadRequestException("No existe pespecifico programaEspecifico");
                }
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(idProgramaEspecificoSesion);
                if (programaEspecificoSesion == null)
                {
                    throw new BadRequestException("No existe pespecifico sesion");
                }
                programaEspecifico.Duracion = (Convert.ToDecimal(programaEspecifico.Duracion) - programaEspecificoSesion.Duracion).ToString();
                programaEspecifico.UsuarioModificacion = usuario;
                programaEspecifico.FechaModificacion = DateTime.Now;
                _unitOfWork.PEspecificoRepository.Update(programaEspecifico);
                _unitOfWork.PEspecificoSesionRepository.Delete(idProgramaEspecificoSesion, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 20/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Confirma webinar
        /// </summary>
        /// <param name="dto"> parametros entrada </param>
        /// <returns> bool </returns>
        public bool ConfirmarWebinar(ConfirmacionWebinarDTO dto, string usuario)
        {
            try
            {
                var programaEspecificoSesion = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(dto.IdPEspecificoSesion);
                programaEspecificoSesion.EsWebinarConfirmado = dto.Confirmo;
                programaEspecificoSesion.UsuarioModificacion = usuario;
                _unitOfWork.PEspecificoSesionRepository.Update(programaEspecificoSesion);
                _unitOfWork.Commit();
                return (true);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 20/07/2023
        /// Versión: 1.0
        /// <summary>
        /// Confirma webinar
        /// </summary>
        /// <param name="dto"> parametros entrada </param>
        /// <returns> bool </returns>
        public List<DetalleSesionesAlumnosDTO> DetalleSesionesPorAlumnosFiltrado(SesionFiltroDTO detalleSesionesFiltro)
        {
            return _unitOfWork.PEspecificoSesionRepository.ObtenerDetalleSesionesPorAlumnosFiltrado(detalleSesionesFiltro).ToList();
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioClases(int IdPEspecifico, int IdTipoModalidad)
        {
            try
            {
                return _unitOfWork.PEspecificoSesionRepository.ObtenerSesionesRecordatorioClases(IdPEspecifico, IdTipoModalidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las sesiones vigentes para recordatorio de clases
        /// </summary>
        /// <param name="IdPEspecifico">Lista de Id PEspecifico</param>
        /// <param name="IdTipoModalidad">Lista de Id Tipo Modalidad</param>
        /// <returns> List<PEspecificoSesionesRecordatorioClases> </returns>
        public List<PEspecificoSesionesRecordatorioClases> ObtenerSesionesRecordatorioWebinar(int IdPEspecifico, int IdTipoModalidad)
        {
            try
            {
                return _unitOfWork.PEspecificoSesionRepository.ObtenerSesionesRecordatorioWebinar(IdPEspecifico, IdTipoModalidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
