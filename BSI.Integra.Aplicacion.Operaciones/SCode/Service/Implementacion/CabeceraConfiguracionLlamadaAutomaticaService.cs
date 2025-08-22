using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Nancy.Json;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: CabeceraConfiguracionLlamadaAutomaticaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CabeceraConfiguracionLlamadaAutomatica
    /// </summary>
    public class CabeceraConfiguracionLlamadaAutomaticaService : ICabeceraConfiguracionLlamadaAutomaticaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CabeceraConfiguracionLlamadaAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCabeceraConfiguracionLlamadaAutomatica, CabeceraConfiguracionLlamadaAutomatica>(MemberList.None).ReverseMap();
                cfg.CreateMap<CabeceraConfiguracionLlamadaAutomaticaDTO, CabeceraConfiguracionLlamadaAutomatica>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public CabeceraConfiguracionLlamadaAutomatica Add(CabeceraConfiguracionLlamadaAutomaticaDTO data,string Usuario)
        {
            try
            {
                CabeceraConfiguracionLlamadaAutomatica entidad = _mapper.Map<CabeceraConfiguracionLlamadaAutomatica>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.EstadoProceso = "EN PROCESO";
                entidad.Estado = true;

                var modelo = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Add(entidad);
                _unitOfWork.Commit();

                data.GenerarData.IdCabeceraConfiguracion = modelo.Id;
                data.GenerarData.IdPEspecifico = modelo.IdPespecifico;

                this.CrearRegistrosLlamadaPorTipoConfiguracion(modelo.IdIvrTipoConfiguracion, data.GenerarData);

                return _mapper.Map<CabeceraConfiguracionLlamadaAutomatica>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CabeceraConfiguracionLlamadaAutomatica Update(CabeceraConfiguracionLlamadaAutomaticaDTO data, string Usuario)
        {
            try
            {
                var repositorioCabeceraConfiguracionLlamadaAutomatica = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository;
                var entidad = _mapper.Map<CabeceraConfiguracionLlamadaAutomatica>(repositorioCabeceraConfiguracionLlamadaAutomatica.FirstById(data.Id));
                var nuevaEntidad = _mapper.Map<CabeceraConfiguracionLlamadaAutomatica>(data);

                nuevaEntidad.Estado = entidad.Estado;
                nuevaEntidad.FechaCreacion = entidad.FechaCreacion;
                nuevaEntidad.UsuarioCreacion= entidad.UsuarioCreacion;
                nuevaEntidad.UsuarioModificacion = Usuario;
                nuevaEntidad.FechaModificacion = DateTime.Now;
                nuevaEntidad.RowVersion = entidad.RowVersion;
                nuevaEntidad.EstadoProceso = entidad.EstadoProceso;

                var modelo = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Update(nuevaEntidad);
                _unitOfWork.Commit();

                data.GenerarData.IdCabeceraConfiguracion = modelo.Id;
                data.GenerarData.IdPEspecifico = modelo.IdPespecifico;

                repositorioCabeceraConfiguracionLlamadaAutomatica.EliminarRegistrosDetalle(Usuario, data.Id);
                this.CrearRegistrosLlamadaPorTipoConfiguracion(modelo.IdIvrTipoConfiguracion, data.GenerarData);

                return _mapper.Map<CabeceraConfiguracionLlamadaAutomatica>(modelo);
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
                _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CabeceraConfiguracionLlamadaAutomatica> Add(List<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CabeceraConfiguracionLlamadaAutomatica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CabeceraConfiguracionLlamadaAutomatica> Update(List<CabeceraConfiguracionLlamadaAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CabeceraConfiguracionLlamadaAutomatica>>(modelo);
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
                _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CabeceraConfiguracionLlamadaAutomatica
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaDTO> </returns>
        public bool CrearRegistrosLlamadaPorTipoConfiguracion(int IdTipoConfiguracion, FiltroGenerarDataLLamdaAutomaticaDTO dataGenerar)
        {
            try
            {
                switch (IdTipoConfiguracion)
                {
                    case 6:
                        _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.GenerarRegistrosRecordatorioClases(dataGenerar);
                        break;
                    case 7:
                        _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.GenerarRegistrosRecordatorioWebinar(dataGenerar);
                        break;
                    case 8:
                        _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.GenerarRegistrosRecordatorioCuotaCronograma(dataGenerar);
                        break;
                    case 9:
                        _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.GenerarRegistrosRecordatorioAsistencia(dataGenerar);
                        break;
                    case 10:
                        _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.GenerarRegistrosRecordatorioAvanceAcademicoAO(dataGenerar);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CabeceraConfiguracionLlamadaAutomatica
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaDTO> </returns>
        public IEnumerable<LlamadaAutomaticaConfiguracionDTO> ObtenerCabeceraConfiguracionLlamadaAutomatica()
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerCabeceraConfiguracionLlamadaAutomatica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionClases(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleCabeceraConfiguracionClases(IdCabecera, IdsSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionWebinar(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleCabeceraConfiguracionWebinar(IdCabecera, IdsSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionCuota(int IdCabecera)
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleCabeceraConfiguracionCuota(IdCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAsistencia(int IdCabecera, List<int> IdsSesion)
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleCabeceraConfiguracionAsistencia(IdCabecera, IdsSesion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public IEnumerable<DetalleCabeceraConfiguracionDTO> ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(int IdCabecera )
        {
            try
            {
                return _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(IdCabecera );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CabeceraConfiguracionLlamadaAutomatica para mostrarse en combo.
        /// </summary>
        /// <returns> List<CabeceraConfiguracionLlamadaAutomaticaComboDTO> </returns>
        public bool RealizarCalculoDeLlamadasDiaHoy()
        {
            try
            {
                var listaCabecerasEnProceso = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerCabecerasEnProceso();
                var serializerProceso = new JavaScriptSerializer();
                List<int> listaDetalleActualizar = new List<int>();
                foreach (var item in listaCabecerasEnProceso)
                {
                    var configuracion = serializerProceso.Deserialize<ConfiguracionCabeceraDTO>(item.CongelamientoConfiguracion); 
                    var listaResultad = this.ObtenerDetalleEjecucionHoy(configuracion, item.IdIvrTipoConfiguracion, item.Id);
                    listaDetalleActualizar.AddRange(listaResultad);
                }
                var listaIds = string.Join(",", listaDetalleActualizar);
                var result = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ActualizarLlamadaHoy(listaIds);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> ObtenerDetalleEjecucionHoy(ConfiguracionCabeceraDTO configuracion, int IdIvrTipoConfiguracion, int Id)
        {
            List<int> listaDetalleActualizar = new List<int>();
            try
            {
                
                DateTime FechaHoy = DateTime.Now.Date;
            
                if (IdIvrTipoConfiguracion == 6) //CLASES
                {
                    var listaDetalle = this.ObtenerDetalleCabeceraConfiguracionClases(Id, configuracion.ListaSeleccion);
                    listaDetalleActualizar = listaDetalle
                   .Where(item => FechaHoy == item.FechaSesion.Date.AddDays(-1 * configuracion.EnvioDiasAntes.Value))
                   .Select(item => item.Id)
                   .ToList();

                }
                else if (IdIvrTipoConfiguracion == 7) //WEBINAR
                {
                    var listaDetalle = this.ObtenerDetalleCabeceraConfiguracionWebinar(Id, configuracion.ListaSeleccion);
                    listaDetalleActualizar = listaDetalle
                   .Where(item => FechaHoy == item.FechaSesion.Date)
                   .Select(item => item.Id)
                   .ToList();

                }
                else if (IdIvrTipoConfiguracion == 8)//CUOTAS CRONOGRAMA
                {
                    var variante = configuracion.TipoCuota=="aVencer" ? -1 : 1;
                    var listaDetalle = this.ObtenerDetalleCabeceraConfiguracionCuota(Id);
                    listaDetalleActualizar = listaDetalle
                   .Where(item => FechaHoy == item.FechaSesion.Date.AddDays(configuracion.EnvioDiasAntes.Value *  variante))
                   .Select(item => item.Id)
                   .ToList();

                }
                else if(IdIvrTipoConfiguracion == 9)//ASISTENCIA
                {
                    var listaDetalle = this.ObtenerDetalleCabeceraConfiguracionAsistencia(Id, configuracion.ListaSeleccion);
                    listaDetalleActualizar = listaDetalle
                   .Where(item => FechaHoy == item.FechaSesion.Date.AddDays(configuracion.EnvioDiasAntes.Value))
                   .Select(item => item.Id)
                   .ToList();

                }
                else if (IdIvrTipoConfiguracion == 10) //AVANCE ACADEMICO AO
                {
                    var listaDetalle = this.ObtenerDetalleCabeceraConfiguracionAvanceAcademicoAO(Id);
                    listaDetalleActualizar = listaDetalle
                   .Where(item => FechaHoy == item.FechaSesion.Date)
                   .Select(item => item.Id)
                   .ToList();

                }

            }
            catch (Exception ex)
            {
            }
            return listaDetalleActualizar;
        }

        public DatoLlamadaDTO ObtenerDatoLlamada(int IdIvrEjecucion)
        {
            try
            {
                var datoLlamada = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDatoLlamada(IdIvrEjecucion);

                return datoLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el concluido del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarConcluido(int Id)
        {
            try
            {
                var rep = _unitOfWork.LlamadaAutomaticaDetalleCabeceraConfiguracionRepository;
                var entidad = rep.ObtenerLlamadaAutomaticaDetalleCabeceraConfiguracionPorId(Id);
                if (entidad != null)
                {
                    entidad.Concluido = true;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = rep.Update(entidad);
                    _unitOfWork.Commit();

                    return true;
                }
                else return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarIntento(int Id)
        {
            try
            {
                var rep = _unitOfWork.LlamadaAutomaticaDetalleCabeceraConfiguracionRepository;
                var entidad = rep.ObtenerLlamadaAutomaticaDetalleCabeceraConfiguracionPorId(Id);
                if (entidad != null)
                {
                    entidad.Intento = entidad.Intento + 1;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = rep.Update(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el intento y concluido del registro.
        /// </summary>
        /// <returns></returns>
        public bool ActualizarIntentoConcluido(int Id)
        {
            try
            {
                var rep = _unitOfWork.LlamadaAutomaticaDetalleCabeceraConfiguracionRepository;
                var entidad = rep.ObtenerLlamadaAutomaticaDetalleCabeceraConfiguracionPorId(Id);
                if (entidad != null)
                {
                    entidad.Concluido = true;
                    entidad.Intento = entidad.Intento + 1;
                    entidad.UsuarioModificacion = "Ivr-Procceso";
                    entidad.FechaModificacion = DateTime.Now;
                    var data = rep.Update(entidad);
                    _unitOfWork.Commit();
                    return true;
                }
                else return false;


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DetalleIvrDTO ObtenerDetalleParaIvr(string CelularAlumno)
        {
            try
            {
                var datoLlamada = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerDetalleParaIvr(CelularAlumno);
                return datoLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ObtenerRangoHoraEjecucionDialer(int IdIvrEjecucion) 
        {
            try
            {
                DateTime FechaHoy = DateTime.Now;
                var registroHoraDialer = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository.ObtenerRangoHoraEjecucionDialer(IdIvrEjecucion);
                if(registroHoraDialer != null)
                {
                    if(FechaHoy >= registroHoraDialer.HoraInicio && FechaHoy <= registroHoraDialer.HoraFin) 
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RealizarGenerarRegistroEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                var rep  = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository;
                var CabecerasDeIVR = rep.ObtenerCabecerasEnProcesoPorIdIvrEjecucion(IdIvrEjecucion)
                    .Where(objeto => 
                        objeto.IdIvrTipoConfiguracion == 8 
                    ).ToList(); //Solo toma las cabeceras que tengan la Opcion de Siempre.

                var serializerProceso = new JavaScriptSerializer();

                foreach (var item in CabecerasDeIVR)
                {
                    
                    var ConfiguracionCongelada = serializerProceso.Deserialize<ConfiguracionCabeceraDTO>(item.CongelamientoConfiguracion);
                    if (ConfiguracionCongelada.TipoEnvio== "siempre")
                    {
                        FiltroGenerarDataLLamdaAutomaticaDTO GenerarData = new FiltroGenerarDataLLamdaAutomaticaDTO();
                        if (ConfiguracionCongelada.ListaSeleccion != null)
                            GenerarData.IdsSesiones = string.Join(",", ConfiguracionCongelada.ListaSeleccion);
                        else GenerarData.IdsSesiones = "";
                        GenerarData.IdTipoModalidad = ConfiguracionCongelada.IdTipoModalidad;
                        GenerarData.IdCabeceraConfiguracion = item.Id;
                        GenerarData.IdPEspecifico = ConfiguracionCongelada.IdPEspecifico;
                        GenerarData.IsTodosWebinar = ConfiguracionCongelada.IsTodosWebinar;
                        GenerarData.DiasCalculoCuota = ConfiguracionCongelada.EnvioDiasAntes;
                        GenerarData.IsPorPrograma = ConfiguracionCongelada.IsPorPrograma;
                        this.CrearRegistrosLlamadaPorTipoConfiguracion(item.IdIvrTipoConfiguracion, GenerarData);
                    }
                  
                }


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool ActualizarProcesoCompletadoEjecucionDialer(int IdIvrEjecucion)
        {
            try
            {
                var rep = _unitOfWork.CabeceraConfiguracionLlamadaAutomaticaRepository;
                var CabecerasDeIVR = rep.ObtenerCabecerasSinPendientesALlamar(IdIvrEjecucion);
                var Serializer = new JavaScriptSerializer();
                var ListaCompletados = CabecerasDeIVR
                    .Where(item =>
                    {
                        var configuracionCongelada = Serializer.Deserialize<ConfiguracionCabeceraDTO>(item.CongelamientoConfiguracion);
                        return configuracionCongelada.TipoEnvio != "siempre";
                    })
                    .Select(item => item.Id)
                    .ToList();

                var listaString = string.Join(",", ListaCompletados);
                return rep.ActualizarProcesoCompletadoCabeceraConfiguracion(listaString);

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
