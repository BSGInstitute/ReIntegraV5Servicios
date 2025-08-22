using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ActividadDetalleService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ActividadDetalle
    /// </summary>
    public class ActividadDetalleService : IActividadDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _mapperActividad;

        public ActividadDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TActividadDetalle, ActividadDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<ActividadDetalleDTO, ActividadDetalle>(MemberList.None);
            });
            _mapper = new Mapper(config);
            _mapperActividad = new Mapper(config);
        }

        #region Metodos Base
        public ActividadDetalle Add(ActividadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ActividadDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActividadDetalle Update(ActividadDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ActividadDetalle>(modelo);
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
                _unitOfWork.ActividadDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadDetalle> Add(List<ActividadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ActividadDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadDetalle> Update(List<ActividadDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ActividadDetalle>>(modelo);
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
                _unitOfWork.ActividadDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ActividadDetalle
        /// </summary>
        /// <returns> List<ActividadDetalleDTO> </returns>
        public IEnumerable<ActividadDetalleDTO> ObtenerActividadDetalle()
        {
            try
            {
                return _unitOfWork.ActividadDetalleRepository.ObtenerActividadDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalleDTO </returns>
        public ActividadDetalle ObtenerPorId(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.ActividadDetalleRepository.ObtenerPorId(idActividadDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna una lista de actividades compuestas filtradas por un idactividad detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> List<CompuestoActividadEjecutadaDTO> </returns>
        public List<CompuestoActividadEjecutadaDTO> ObtenerAgendaActividades(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.ActividadDetalleRepository.ObtenerAgendaActividades(idActividadDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Mapea de un ActividadDetalleDTO a ActividadDetalle
        /// </summary>
        /// <returns> ActividadDetalle </returns>
        public ActividadDetalle MapeoEntidadDesdeDTO(ActividadDetalleDTO dto)
        {
            try
            {
                var entidad = _mapper.Map<ActividadDetalle>(dto);
                if (entidad != null) entidad.Estado = true;
                return entidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaAgenda(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ActividadDetalleRepository.ReporteActividadOcurrenciaAgenda(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CompuestoActividadEjecutadaDTO ObtenerAgendaRealizadaRegistroTiempoReal(int idActividadDetalle)
        {
            List<CompuestoActividadEjecutadaDTO> temp = ObtenerAgendaActividades(idActividadDetalle);

            var result = (from p in temp
                          group p by new
                          {
                              p.Id,
                              p.CentroCosto,
                              p.Contacto,
                              p.CodigoFase,
                              p.NombreTipoDato,
                              p.Origen,
                              p.FechaProgramada,
                              p.FechaReal,
                              p.Duracion,
                              p.Actividad,
                              p.Ocurrencia,
                              p.Comentario,
                              p.Asesor,
                              p.IdContacto,
                              p.IdOportunidad,
                              p.ProbActual,
                              p.Ca_nombre,//ca_nombre
                              p.IdCategoria,
                              p.FaseInicial,
                              p.FaseMaxima,
                              p.TotalOportunidades,
                              p.UnicoTimbrado,
                              p.UnicoContesto,
                              p.UnicoEstadoLlamada,
                              p.NumeroLlamadas,
                              p.Estado,
                              p.UnicoClasificacion,
                              p.UnicoFechaLlamada,
                              p.NombreGrupo,
                              p.IdFaseOportunidadInicial,
                              p.FechaModificacion

                          } into g
                          select new CompuestoActividadesEjecutadasTempDTO
                          {
                              Id = g.Key.Id,
                              CentroCosto = g.Key.CentroCosto,
                              Contacto = g.Key.Contacto,
                              CodigoFase = g.Key.CodigoFase,
                              NombreTipoDato = g.Key.NombreTipoDato,
                              Origen = g.Key.Origen,
                              FechaProgramada = g.Key.FechaProgramada,
                              FechaReal = g.Key.FechaReal,
                              Duracion = g.Key.Duracion,
                              Actividad = g.Key.Actividad,
                              Ocurrencia = g.Key.Ocurrencia,
                              Comentario = g.Key.Comentario,
                              Asesor = g.Key.Asesor,
                              IdContacto = g.Key.IdContacto,
                              IdOportunidad = g.Key.IdOportunidad,
                              ProbActual = g.Key.ProbActual,
                              Ca_nombre = g.Key.Ca_nombre,
                              IdCategoria = g.Key.IdCategoria,
                              FaseInicial = g.Key.FaseInicial,
                              FaseMaxima = g.Key.FaseMaxima,
                              TotalOportunidades = g.Key.TotalOportunidades,
                              UnicoTimbrado = g.Key.UnicoTimbrado,
                              UnicoContesto = g.Key.UnicoContesto,
                              UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
                              NumeroLlamadas = g.Key.NumeroLlamadas,
                              Estado = g.Key.Estado,
                              NombreGrupo = g.Key.NombreGrupo,
                              IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                              FechaModificacion = g.Key.FechaModificacion,

                              lista = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdLlamada,
                                  DuracionTimbrado = o.DuracionTimbrado,
                                  DuracionContesto = o.DuracionContesto,
                                  EstadoLlamada = o.EstadoLlamada,
                                  FechaLlamada = o.FechaLlamadaIntegra,
                                  FechaLlamadaFin = o.FechaLlamadaFin,
                                  SubEstadoLlamada = o.SubEstadoLlamadaIntegra,

                              }).OrderByDescending(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                              llamadasTresCX = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTresCX,
                                  DuracionContesto = o.TiempoContestoTresCx.ToString(),
                                  DuracionTimbrado = o.TiempoTimbradoTresCx.ToString(),
                                  EstadoLlamada = o.EstadoLlamadaTresCX,
                                  FechaLlamada = o.FechaIncioLlamadaTresCX,
                                  FechaLlamadaFin = o.FechaFinLlamadaTresCX,
                                  SubEstadoLlamada = o.SubEstadoLlamadaTresCX,

                              }).OrderBy(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()

                          });

            var item = result.FirstOrDefault();

            CompuestoActividadEjecutadaDTO item_detalle = new CompuestoActividadEjecutadaDTO()
            {

                Id = item.Id,
                CentroCosto = item.CentroCosto,
                Contacto = item.Contacto,
                CodigoFase = item.CodigoFase,
                NombreTipoDato = item.NombreTipoDato,
                Origen = item.Origen,
                FechaProgramada = item.FechaProgramada,
                FechaReal = item.FechaReal,
                Duracion = item.Duracion,
                Actividad = item.Actividad,
                Ocurrencia = item.Ocurrencia,
                Comentario = item.Comentario,
                Asesor = item.Asesor,
                IdContacto = item.IdContacto,
                IdOportunidad = item.IdOportunidad,
                ProbActual = item.ProbActual,
                Ca_nombre = item.Ca_nombre,
                IdCategoria = item.IdCategoria,
                FaseInicial = item.FaseInicial,
                FaseMaxima = item.FaseMaxima,
                TotalOportunidades = item.TotalOportunidades,
                UnicoTimbrado = item.UnicoTimbrado,
                UnicoContesto = item.UnicoContesto,
                UnicoEstadoLlamada = item.UnicoEstadoLlamada,
                Estado = item.Estado,
                NombreGrupo = item.NombreGrupo,

            };

            if (item.lista != null && item.lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
            {

                item_detalle.NumeroLlamadas = item.lista.Count().ToString();
                item.lista = item.lista.OrderBy(x => x.FechaLlamada).ToList();

                item_detalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                item_detalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                item_detalle.FechaLlamada = String.Concat(item.lista.Select(o => "<strong >I: </strong >" + o.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />"));

            }
            else
            {
                string date = item.UnicoFechaLlamada == null ? "" : item.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
                item_detalle.NumeroLlamadas = "1";
                item_detalle.DuracionTimbrado = item.UnicoEstadoLlamada + " <strong >- TT:</strong >" + item.UnicoTimbrado + "  <strong >TC:</strong >" + item.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + item.UnicoClasificacion + "</strong><br />";

            }
            item_detalle.MinutosIntervale = 0;
            item_detalle.MinutosTotalContesto = 0;
            item_detalle.MinutosTotalTimbrado = 0;
            item_detalle.MinutosTotalPerdido = -1;

            item_detalle.TiemposTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
            item_detalle.EstadosTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
            var listaActividades = ReporteActividadOcurrenciaAgenda(item.IdOportunidad);
            item_detalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 /*ValorEstatico.IdEstadoOcurrenciaEjecutado*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 /*ValorEstatico.IdEstadoOcurrenciaNoEjecutado*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 /*ValorEstatico.IdEstadoOcurrenciaAsignacionManual*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.NombreGrabacionTresCX = "-";
            item_detalle.NombreGrabacionIntegra = "-";
            return item_detalle;
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de actividades
        /// </summary>
        /// <param name="idOportunidad">Id de la Oportunidad</param>
        /// <returns> List<ReporteActividadOcurrenciaDTO> </returns>
        public async Task<CompuestoActividadEjecutadaDTO> ObtenerAgendaRealizadaRegistroTiempoRealAsync(int idActividadDetalle)
        {
            List<CompuestoActividadEjecutadaDTO> temp = await _unitOfWork.ActividadDetalleRepository.ObtenerAgendaActividadesAsync(idActividadDetalle);
            var result = (from p in temp
                          group p by new
                          {
                              p.Id,
                              p.CentroCosto,
                              p.Contacto,
                              p.CodigoFase,
                              p.NombreTipoDato,
                              p.Origen,
                              p.FechaProgramada,
                              p.FechaReal,
                              p.Duracion,
                              p.Actividad,
                              p.Ocurrencia,
                              p.Comentario,
                              p.Asesor,
                              p.IdContacto,
                              p.IdOportunidad,
                              p.ProbActual,
                              p.Ca_nombre,//ca_nombre
                              p.IdCategoria,
                              p.FaseInicial,
                              p.FaseMaxima,
                              p.TotalOportunidades,
                              p.UnicoTimbrado,
                              p.UnicoContesto,
                              p.UnicoEstadoLlamada,
                              p.NumeroLlamadas,
                              p.Estado,
                              p.UnicoClasificacion,
                              p.UnicoFechaLlamada,
                              p.NombreGrupo,
                              p.IdFaseOportunidadInicial,
                              p.FechaModificacion

                          } into g
                          select new CompuestoActividadesEjecutadasTempDTO
                          {
                              Id = g.Key.Id,
                              CentroCosto = g.Key.CentroCosto,
                              Contacto = g.Key.Contacto,
                              CodigoFase = g.Key.CodigoFase,
                              NombreTipoDato = g.Key.NombreTipoDato,
                              Origen = g.Key.Origen,
                              FechaProgramada = g.Key.FechaProgramada,
                              FechaReal = g.Key.FechaReal,
                              Duracion = g.Key.Duracion,
                              Actividad = g.Key.Actividad,
                              Ocurrencia = g.Key.Ocurrencia,
                              Comentario = g.Key.Comentario,
                              Asesor = g.Key.Asesor,
                              IdContacto = g.Key.IdContacto,
                              IdOportunidad = g.Key.IdOportunidad,
                              ProbActual = g.Key.ProbActual,
                              Ca_nombre = g.Key.Ca_nombre,
                              IdCategoria = g.Key.IdCategoria,
                              FaseInicial = g.Key.FaseInicial,
                              FaseMaxima = g.Key.FaseMaxima,
                              TotalOportunidades = g.Key.TotalOportunidades,
                              UnicoTimbrado = g.Key.UnicoTimbrado,
                              UnicoContesto = g.Key.UnicoContesto,
                              UnicoEstadoLlamada = g.Key.UnicoEstadoLlamada,
                              NumeroLlamadas = g.Key.NumeroLlamadas,
                              Estado = g.Key.Estado,
                              NombreGrupo = g.Key.NombreGrupo,
                              IdFaseOportunidadInicial = g.Key.IdFaseOportunidadInicial,
                              FechaModificacion = g.Key.FechaModificacion,

                              lista = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdLlamada,
                                  DuracionTimbrado = o.DuracionTimbrado,
                                  DuracionContesto = o.DuracionContesto,
                                  EstadoLlamada = o.EstadoLlamada,
                                  FechaLlamada = o.FechaLlamadaIntegra,
                                  FechaLlamadaFin = o.FechaLlamadaFin,
                                  SubEstadoLlamada = o.SubEstadoLlamadaIntegra,

                              }).OrderByDescending(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                              llamadasTresCX = g.Select(o => new CompuestoActividadesEjecutadasTemp_DetalleDTO
                              {
                                  Id = o.IdTresCX,
                                  DuracionContesto = o.TiempoContestoTresCx.ToString(),
                                  DuracionTimbrado = o.TiempoTimbradoTresCx.ToString(),
                                  EstadoLlamada = o.EstadoLlamadaTresCX,
                                  FechaLlamada = o.FechaIncioLlamadaTresCX,
                                  FechaLlamadaFin = o.FechaFinLlamadaTresCX,
                                  SubEstadoLlamada = o.SubEstadoLlamadaTresCX,

                              }).OrderBy(o => o.FechaLlamada).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList()

                          });

            var item = result.FirstOrDefault();
            var taskListaActividades = _unitOfWork.ActividadDetalleRepository.ReporteActividadOcurrenciaAgendaAsync(item.IdOportunidad);

            CompuestoActividadEjecutadaDTO item_detalle = new CompuestoActividadEjecutadaDTO()
            {
                Id = item.Id,
                CentroCosto = item.CentroCosto,
                Contacto = item.Contacto,
                CodigoFase = item.CodigoFase,
                NombreTipoDato = item.NombreTipoDato,
                Origen = item.Origen,
                FechaProgramada = item.FechaProgramada,
                FechaReal = item.FechaReal,
                Duracion = item.Duracion,
                Actividad = item.Actividad,
                Ocurrencia = item.Ocurrencia,
                Comentario = item.Comentario,
                Asesor = item.Asesor,
                IdContacto = item.IdContacto,
                IdOportunidad = item.IdOportunidad,
                ProbActual = item.ProbActual,
                Ca_nombre = item.Ca_nombre,
                IdCategoria = item.IdCategoria,
                FaseInicial = item.FaseInicial,
                FaseMaxima = item.FaseMaxima,
                TotalOportunidades = item.TotalOportunidades,
                UnicoTimbrado = item.UnicoTimbrado,
                UnicoContesto = item.UnicoContesto,
                UnicoEstadoLlamada = item.UnicoEstadoLlamada,
                Estado = item.Estado,
                NombreGrupo = item.NombreGrupo,

            };

            if (item.lista != null && item.lista.Select(s => s.DuracionTimbrado).FirstOrDefault() != null)
            {

                item_detalle.NumeroLlamadas = item.lista.Count().ToString();
                item.lista = item.lista.OrderBy(x => x.FechaLlamada).ToList();

                item_detalle.DuracionTimbrado = String.Concat(item.lista.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
                item_detalle.EstadoLlamada = String.Concat(item.lista.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
                item_detalle.FechaLlamada = String.Concat(item.lista.Select(o => "<strong >I: </strong >" + o.FechaLlamada.Value.ToString("HH:mm:ss") + "<strong> T: </strong >" + o.FechaLlamadaFin.Value.ToString("HH:mm:ss") + "<br />"));

            }
            else
            {
                string date = item.UnicoFechaLlamada == null ? "" : item.UnicoFechaLlamada.Value.ToString("yyyy/MM/dd HH:mm");
                item_detalle.NumeroLlamadas = "1";
                item_detalle.DuracionTimbrado = item.UnicoEstadoLlamada + " <strong >- TT:</strong >" + item.UnicoTimbrado + "  <strong >TC:</strong >" + item.UnicoContesto + " <strong >-</strong > " + date + "<br /><strong id='estadoNuevoT'>Nuevo Estado: </strong ><strong id='estadoNuevoC'>" + item.UnicoClasificacion + "</strong><br />";

            }
            item_detalle.MinutosIntervale = 0;
            item_detalle.MinutosTotalContesto = 0;
            item_detalle.MinutosTotalTimbrado = 0;
            item_detalle.MinutosTotalPerdido = -1;

            item_detalle.TiemposTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >TT:</strong > / " + o.DuracionTimbrado + " <strong >TC:</strong > " + o.DuracionContesto + "<br />"));
            item_detalle.EstadosTresCX = String.Concat(item.llamadasTresCX.Select(o => " <strong >Tipo: " + o.EstadoLlamada + "</strong><br>SubTipo: " + o.SubEstadoLlamada + "<br />"));
            var listaActividades = await taskListaActividades;
            item_detalle.TotalEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 1 /*ValorEstatico.IdEstadoOcurrenciaEjecutado*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalNoEjecutadas = listaActividades.Where(x => x.IdEstadoOcurrencia == 2 /*ValorEstatico.IdEstadoOcurrenciaNoEjecutado*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.TotalAsignacionManual = listaActividades.Where(x => x.IdEstadoOcurrencia == 7 /*ValorEstatico.IdEstadoOcurrenciaAsignacionManual*/ && x.IdFaseActual == item.IdFaseOportunidadInicial && x.FechaReal < item.FechaModificacion.Value).Count();
            item_detalle.NombreGrabacionTresCX = "-";
            item_detalle.NombreGrabacionIntegra = "-";
            return item_detalle;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_ActividadDetalle asociados a un Id.
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> ActividadDetalle Entidad </returns>
        public ActividadDetalle ObtenerEntidadActividadDetallePorId(int idActividadDetalle)
        {
            try
            {
                return _unitOfWork.ActividadDetalleRepository.ObtenerEntidadActividadDetallePorId(idActividadDetalle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
