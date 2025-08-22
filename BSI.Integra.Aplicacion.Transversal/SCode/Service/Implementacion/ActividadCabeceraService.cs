using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ActividadCabeceraService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 22/08/2022
    /// <summary>
    /// Gestión general de T_ActividadCabecera
    /// </summary>
    public class ActividadCabeceraService : IActividadCabeceraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ActividadCabeceraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTipoInteracccion, ActividadCabecera>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ActividadCabecera Add(ActividadCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadCabeceraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ActividadCabecera>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActividadCabecera Update(ActividadCabecera entidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadCabeceraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ActividadCabecera>(modelo);
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
                _unitOfWork.ActividadCabeceraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadCabecera> Add(List<ActividadCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadCabeceraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ActividadCabecera>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadCabecera> Update(List<ActividadCabecera> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ActividadCabeceraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ActividadCabecera>>(modelo);
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
                _unitOfWork.ActividadCabeceraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public List<ComboDTO> ObtenerFiltro()
        {
            try
            {
                return _unitOfWork.ActividadCabeceraRepository.ObtenerFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadCabeceraDTO> ObtenerTodoActividadAutomatica()
        {
            try
            {
                return _unitOfWork.ActividadCabeceraRepository.ObtenerTodoActividadAutomatica();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarActividadCabecera(int id, string usuario)
        {
            try
            {

                var _repoReprogramacionCab = _unitOfWork.ReprogramacionCabeceraRepository;
                var _repActividadCabecera = _unitOfWork.ActividadCabeceraRepository;
                var ActividadCabecera = _repActividadCabecera.GetBy(x => x.Id == id).FirstOrDefault();
                var _repActividadCabeceraTipoDato = _unitOfWork.TipoDatoRepository;

                var ReprogramacionesDB = _repoReprogramacionCab.ObtenerReprogramacionCabPorActividadCab(id).ToList();
                for (var d = 0; d < ReprogramacionesDB.Count; ++d)
                    _repoReprogramacionCab.Delete(ReprogramacionesDB[d].Id, usuario);

                _repActividadCabecera.Delete(id, usuario);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public bool InsertarActividadCabecera(ListaActividadDTO ObjetoDTO, string usuario)
        {
            try
            {

                var _repActividadCabecera = _unitOfWork.ActividadCabeceraRepository;
                var _repoReprogramacionCab = _unitOfWork.ReprogramacionCabeceraRepository;
                var NuevaActividadCabecera = new ActividadCabecera();


                if (ObjetoDTO.ActividadBase == "LLAMADA")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraLlamada.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraLlamada.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = ObjetoDTO.ActividadCabeceraLlamada.DuracionEstimada;
                    NuevaActividadCabecera.ReproManual = ObjetoDTO.ActividadCabeceraLlamada.ReproManual;
                    NuevaActividadCabecera.ReproAutomatica = ObjetoDTO.ActividadCabeceraLlamada.ReproAutomatica;
                    NuevaActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraLlamada.Idplantilla;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraLlamada.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = ObjetoDTO.ActividadCabeceraLlamada.ValidaLlamada;
                    NuevaActividadCabecera.IdPlantillaSpeech = ObjetoDTO.ActividadCabeceraLlamada.IdPlantillaSpeech;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = ObjetoDTO.ActividadCabeceraLlamada.NumeroMaximoLlamadas;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;
                    NuevaActividadCabecera.Activo = false;
                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = usuario;
                    NuevaActividadCabecera.UsuarioModificacion = usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraLlamada.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo = ObjetoDTO.ActividadCabeceraLlamada.EsEnvioMasivo;

                    var resp = _repActividadCabecera.Add(NuevaActividadCabecera);

        
                    if (ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones != null)
                    {
                        var Reprogramaciones = ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones;
                        for (var r = 0; r < Reprogramaciones.Count; ++r)
                        {
                            TReprogramacionCabecera ReprogramacionNueva = new TReprogramacionCabecera();
                            ReprogramacionNueva.IdActividadCabecera = resp.Id;
                            ReprogramacionNueva.IdCategoriaOrigen = Reprogramaciones[r].IdCategoriaOrigen;
                            ReprogramacionNueva.MaxReproPorDia = Reprogramaciones[r].MaxReproPorDia;
                            ReprogramacionNueva.IntervaloSigProgramacionMin = Reprogramaciones[r].IntervaloSigProgramacionMin;
                            ReprogramacionNueva.Estado = true;
                            ReprogramacionNueva.UsuarioCreacion = usuario;
                            ReprogramacionNueva.UsuarioModificacion = usuario;
                            ReprogramacionNueva.FechaCreacion = DateTime.Now;
                            ReprogramacionNueva.FechaModificacion = DateTime.Now;
                            _repoReprogramacionCab.Insert(ReprogramacionNueva);
                        }
                    }

                }
                else if (ObjetoDTO.ActividadBase == "WHATSAPP INDIVIDUAL" || ObjetoDTO.ActividadBase == "CORREO INDIVIDUAL")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraIndividual.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraIndividual.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = 0;
                    NuevaActividadCabecera.ReproManual = false;
                    NuevaActividadCabecera.ReproAutomatica = false;
                    NuevaActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraIndividual.IdPlantilla;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraIndividual.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = false;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = 0;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;

                    NuevaActividadCabecera.IdConjuntoLista = null;
                    NuevaActividadCabecera.IdFrecuencia = null;
                    NuevaActividadCabecera.FechaInicioActividad = null;
                    NuevaActividadCabecera.FechaFinActividad = null;
                    NuevaActividadCabecera.DiaFrecuenciaMensual = null;
                    NuevaActividadCabecera.EsRepetitivo = null;
                    NuevaActividadCabecera.HoraInicio = null;
                    NuevaActividadCabecera.HoraFin = null;
                    NuevaActividadCabecera.CantidadIntevaloTiempo = null;
                    NuevaActividadCabecera.IdTiempoIntervalo = null;
                    NuevaActividadCabecera.Activo = false;

                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = usuario;
                    NuevaActividadCabecera.UsuarioModificacion = usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraIndividual.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo = ObjetoDTO.ActividadCabeceraIndividual.EsEnvioMasivo;
                    var resp = _repActividadCabecera.Add(NuevaActividadCabecera);

                }
                else if (ObjetoDTO.ActividadBase == "SEGMENTO FACEBOOK" || ObjetoDTO.ActividadBase == "SEGMENTO ADWORDS" || ObjetoDTO.ActividadBase == "MAILING MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "MAILING MASIVO MARKETING" || ObjetoDTO.ActividadBase == "MAILING" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "SMS MASIVO")
                {
                    NuevaActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraMasivo.Nombre;
                    NuevaActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraMasivo.Descripcion;
                    NuevaActividadCabecera.DuracionEstimada = 0;
                    NuevaActividadCabecera.ReproManual = false;
                    NuevaActividadCabecera.ReproAutomatica = false;
                    NuevaActividadCabecera.Idplantilla = 1;
                    NuevaActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraMasivo.IdActividadBase;
                    NuevaActividadCabecera.ValidaLlamada = false;
                    NuevaActividadCabecera.NumeroMaximoLlamadas = 0;
                    NuevaActividadCabecera.FechaCreacion2 = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion2 = DateTime.Now;

                    NuevaActividadCabecera.IdConjuntoLista = ObjetoDTO.ActividadCabeceraMasivo.IdConjuntoLista;
                    NuevaActividadCabecera.IdFrecuencia = ObjetoDTO.ActividadCabeceraMasivo.IdFrecuencia;
                    NuevaActividadCabecera.FechaInicioActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaInicioActividad;
                    NuevaActividadCabecera.FechaFinActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaFinActividad;
                    NuevaActividadCabecera.DiaFrecuenciaMensual = ObjetoDTO.ActividadCabeceraMasivo.DiaFrecuenciaMensual;
                    NuevaActividadCabecera.EsRepetitivo = ObjetoDTO.ActividadCabeceraMasivo.EsRepetitivo;
                    NuevaActividadCabecera.HoraInicio = ObjetoDTO.ActividadCabeceraMasivo.HoraInicio;
                    NuevaActividadCabecera.HoraFin = ObjetoDTO.ActividadCabeceraMasivo.HoraFin;
                    NuevaActividadCabecera.CantidadIntevaloTiempo = ObjetoDTO.ActividadCabeceraMasivo.CantidadIntevaloTiempo;
                    NuevaActividadCabecera.IdTiempoIntervalo = ObjetoDTO.ActividadCabeceraMasivo.IdTiempoIntervalo;
                    NuevaActividadCabecera.Activo = ObjetoDTO.ActividadCabeceraMasivo.Activo;

                    NuevaActividadCabecera.Estado = true;
                    NuevaActividadCabecera.UsuarioCreacion = usuario;
                    NuevaActividadCabecera.UsuarioModificacion = usuario;
                    NuevaActividadCabecera.FechaCreacion = DateTime.Now;
                    NuevaActividadCabecera.FechaModificacion = DateTime.Now;
                    NuevaActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraMasivo.IdPersonalAreaTrabajo;
                    NuevaActividadCabecera.EsEnvioMasivo = ObjetoDTO.ActividadCabeceraMasivo.EsEnvioMasivo;


                    var resp = _repActividadCabecera.Add(NuevaActividadCabecera);
                    _unitOfWork.Commit();

                    var _repActividadCabeceraDiaSemana = _unitOfWork.ActividadCabeceraDiaSemanaRepository;

                    if (ObjetoDTO.ActividadCabeceraMasivo.Semanal != null && ObjetoDTO.ActividadCabeceraMasivo.Semanal.Count() != 0)
                    {
                        var Dias = ObjetoDTO.ActividadCabeceraMasivo.Semanal;
                        for (var r = 0; r < Dias.Count; ++r)
                        {
                            ActividadCabeceraDiaSemana actividadCabeceraDiaSemana = new ActividadCabeceraDiaSemana();
                            actividadCabeceraDiaSemana.IdActividadCabecera = resp.Id;
                            actividadCabeceraDiaSemana.IdDiaSemana = Dias[r];
                            actividadCabeceraDiaSemana.Estado = true;
                            actividadCabeceraDiaSemana.UsuarioCreacion = usuario;
                            actividadCabeceraDiaSemana.UsuarioModificacion = usuario;
                            actividadCabeceraDiaSemana.FechaCreacion = DateTime.Now;
                            actividadCabeceraDiaSemana.FechaModificacion = DateTime.Now;

                            _repActividadCabeceraDiaSemana.Insert(actividadCabeceraDiaSemana);
                        }
                    }
                }
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }


        public bool ActualizarActividadCabecera(ListaActividadDTO ObjetoDTO, string usuario)
        {

            try
            {
                var _repActividadCabecera = _unitOfWork.ActividadCabeceraRepository;
                var _repoReprogramacionCab = _unitOfWork.ReprogramacionCabeceraRepository;
                var _repActividadCabeceraDiaSemana = _unitOfWork.ActividadCabeceraDiaSemanaRepository;
                var ActividadCabecera = _repActividadCabecera.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
               

                if (ActividadCabecera == null) throw new Exception("No se encuentra el registro que se quiere actualizar ¿Id? correcto");

                if (ObjetoDTO.ActividadBase == "LLAMADA")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraLlamada.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraLlamada.Descripcion;
                    ActividadCabecera.DuracionEstimada = ObjetoDTO.ActividadCabeceraLlamada.DuracionEstimada;
                    ActividadCabecera.ReproManual = ObjetoDTO.ActividadCabeceraLlamada.ReproManual;
                    ActividadCabecera.ReproAutomatica = ObjetoDTO.ActividadCabeceraLlamada.ReproAutomatica;
                    ActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraLlamada.Idplantilla;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraLlamada.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = ObjetoDTO.ActividadCabeceraLlamada.ValidaLlamada;
                    ActividadCabecera.IdPlantillaSpeech = ObjetoDTO.ActividadCabeceraLlamada.IdPlantillaSpeech;
                    ActividadCabecera.NumeroMaximoLlamadas = ObjetoDTO.ActividadCabeceraLlamada.NumeroMaximoLlamadas;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;
                    ActividadCabecera.FechaCreacion2 = DateTime.Now;
                    ActividadCabecera.Estado = true;
                    ActividadCabecera.UsuarioModificacion = usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraLlamada.IdPersonalAreaTrabajo;

                    _repActividadCabecera.Update(ActividadCabecera);

                    
                    //reinsercion de nuevas Reprogramaciones
                    var ReprogramacionesDB = _repoReprogramacionCab.ObtenerReprogramacionCabPorActividadCab(ActividadCabecera.Id);
                    for (var d = 0; d < ReprogramacionesDB.Count; ++d)
                        _repoReprogramacionCab.Delete(ReprogramacionesDB[d].Id, usuario);

                    if (ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones != null)
                    {
                        var Reprogramaciones = ObjetoDTO.ActividadCabeceraLlamada.Reprogramaciones;
                        for (var r = 0; r < Reprogramaciones.Count; ++r)
                        {
                            TReprogramacionCabecera Reprogramacion = new TReprogramacionCabecera();
                            Reprogramacion.IdActividadCabecera = ActividadCabecera.Id;
                            Reprogramacion.IdCategoriaOrigen = Reprogramaciones[r].IdCategoriaOrigen;
                            Reprogramacion.MaxReproPorDia = Reprogramaciones[r].MaxReproPorDia;
                            Reprogramacion.IntervaloSigProgramacionMin = Reprogramaciones[r].IntervaloSigProgramacionMin;
                            Reprogramacion.Estado = true;
                            Reprogramacion.UsuarioCreacion = usuario;
                            Reprogramacion.UsuarioModificacion = usuario;
                            Reprogramacion.FechaCreacion = DateTime.Now;
                            Reprogramacion.FechaModificacion = DateTime.Now;

                            _repoReprogramacionCab.Insert(Reprogramacion);
                        }
                    }

                }
                else if (ObjetoDTO.ActividadBase == "WHATSAPP INDIVIDUAL" || ObjetoDTO.ActividadBase == "CORREO INDIVIDUAL")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraIndividual.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraIndividual.Descripcion;
                    ActividadCabecera.DuracionEstimada = 0;
                    ActividadCabecera.ReproManual = false;
                    ActividadCabecera.ReproAutomatica = false;
                    ActividadCabecera.Idplantilla = ObjetoDTO.ActividadCabeceraIndividual.IdPlantilla;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraIndividual.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = false;
                    ActividadCabecera.IdPlantillaSpeech = null;
                    ActividadCabecera.NumeroMaximoLlamadas = 0;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;
                    ActividadCabecera.UsuarioModificacion = usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraIndividual.IdPersonalAreaTrabajo;

                    _repActividadCabecera.Update(ActividadCabecera);

                }
                else if (ObjetoDTO.ActividadBase == "SEGMENTO FACEBOOK" || ObjetoDTO.ActividadBase == "SEGMENTO ADWORDS" || ObjetoDTO.ActividadBase == "MAILING MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "MAILING MASIVO MARKETING" || ObjetoDTO.ActividadBase == "MAILING" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO" || ObjetoDTO.ActividadBase == "WHATSAPP MASIVO OPERACIONES" || ObjetoDTO.ActividadBase == "SMS MASIVO")
                {
                    ActividadCabecera.Nombre = ObjetoDTO.ActividadCabeceraMasivo.Nombre;
                    ActividadCabecera.Descripcion = ObjetoDTO.ActividadCabeceraMasivo.Descripcion;
                    ActividadCabecera.DuracionEstimada = 0;
                    ActividadCabecera.ReproManual = false;
                    ActividadCabecera.ReproAutomatica = false;
                    ActividadCabecera.Idplantilla = 1;
                    ActividadCabecera.IdActividadBase = ObjetoDTO.ActividadCabeceraMasivo.IdActividadBase;
                    ActividadCabecera.ValidaLlamada = false;
                    ActividadCabecera.NumeroMaximoLlamadas = 0;
                    ActividadCabecera.FechaCreacion2 = DateTime.Now;
                    ActividadCabecera.FechaModificacion2 = DateTime.Now;

                    ActividadCabecera.IdConjuntoLista = ObjetoDTO.ActividadCabeceraMasivo.IdConjuntoLista;
                    ActividadCabecera.IdFrecuencia = ObjetoDTO.ActividadCabeceraMasivo.IdFrecuencia;
                    ActividadCabecera.FechaInicioActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaInicioActividad;
                    ActividadCabecera.FechaFinActividad = ObjetoDTO.ActividadCabeceraMasivo.FechaFinActividad;
                    ActividadCabecera.DiaFrecuenciaMensual = ObjetoDTO.ActividadCabeceraMasivo.DiaFrecuenciaMensual;
                    ActividadCabecera.EsRepetitivo = ObjetoDTO.ActividadCabeceraMasivo.EsRepetitivo;
                    ActividadCabecera.HoraInicio = ObjetoDTO.ActividadCabeceraMasivo.HoraInicio;
                    ActividadCabecera.HoraFin = ObjetoDTO.ActividadCabeceraMasivo.HoraFin;
                    ActividadCabecera.CantidadIntevaloTiempo = ObjetoDTO.ActividadCabeceraMasivo.CantidadIntevaloTiempo;
                    ActividadCabecera.IdTiempoIntervalo = ObjetoDTO.ActividadCabeceraMasivo.IdTiempoIntervalo;
                    ActividadCabecera.Activo = ObjetoDTO.ActividadCabeceraMasivo.Activo;
                    ActividadCabecera.UsuarioModificacion = usuario;
                    ActividadCabecera.FechaModificacion = DateTime.Now;
                    ActividadCabecera.IdPersonalAreaTrabajo = ObjetoDTO.ActividadCabeceraMasivo.IdPersonalAreaTrabajo;


                    _repActividadCabecera.Update(ActividadCabecera);

                    var DiaSemana = _repActividadCabeceraDiaSemana.GetBy(w => w.Estado == true && w.IdActividadCabecera == ActividadCabecera.Id).ToList();
                    for (var d = 0; d < DiaSemana.Count; ++d)
                        _repActividadCabeceraDiaSemana.Delete(DiaSemana[d].Id, usuario);

                    if (ObjetoDTO.ActividadCabeceraMasivo.Semanal != null && ObjetoDTO.ActividadCabeceraMasivo.Semanal.Count() != 0)
                    {
                        var Dias = ObjetoDTO.ActividadCabeceraMasivo.Semanal;
                        for (var r = 0; r < Dias.Count; ++r)
                        {
                            ActividadCabeceraDiaSemana actividadCabeceraDiaSemana = new ActividadCabeceraDiaSemana();
                            actividadCabeceraDiaSemana.IdActividadCabecera = ActividadCabecera.Id;
                            actividadCabeceraDiaSemana.IdDiaSemana = Dias[r];
                            actividadCabeceraDiaSemana.Estado = true;
                            actividadCabeceraDiaSemana.UsuarioCreacion = usuario;
                            actividadCabeceraDiaSemana.UsuarioModificacion = usuario;
                            actividadCabeceraDiaSemana.FechaCreacion = DateTime.Now;
                            actividadCabeceraDiaSemana.FechaModificacion = DateTime.Now;

                            _repActividadCabeceraDiaSemana.Insert(actividadCabeceraDiaSemana);
                        }
                    }

                }

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception Ex)
            {
                return false;
            }

        }


        public List<ComboDTO> ObtenerActividadesBaseMasivo()
        {
            try
            {
                return _unitOfWork.ActividadCabeceraRepository.ObtenerActividadesBaseMasivo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadCabeceraDTO> ObtenerActividadPorId(int IdActividadCabecera)
        {
            try
            {
                return _unitOfWork.ActividadCabeceraRepository.ObtenerActividadPorId(IdActividadCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ActividadCabeceraDiaSemana> ObtenerActividadDiaPorID(int idActividadCabecera)
        {
            try
            {
                return _unitOfWork.ActividadCabeceraDiaSemanaRepository.ObtenerActividadDiaPorID(idActividadCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }



    public class ActividadCabeceraUtil
    {
        private static Dictionary<int, Dictionary<int, int>> diccionarioActividadesCabecera = new Dictionary<int, Dictionary<int, int>>();

        ///<value>0</value>
        private static readonly int valorVacio = 0;

        /// <summary>
        /// Construye diccionario
        /// </summary>
        /// <param name="llave"> Clave de diccionario </param>
        /// <param name="valor"> Valor de diccionario </param>
        /// <returns> Vacio </returns>
        private static Dictionary<int, int> BuilderDictionary(int llave, int valor)
        {
            var dic = new Dictionary<int, int>
            {
                { llave, valor }
            };
            return dic;
        }

        /// Autor: ----------
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Inicializa Actividades Cabecera
        /// </summary>
        /// <returns> Vacio </returns>
        private static void InitActividadesCabecera()
        {
            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadBNC,
                BuilderDictionary(ValorEstatico.IdTipoDatoHistorico, ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis));

            diccionarioActividadesCabecera[ValorEstatico.IdFaseOportunidadBNC].Add(ValorEstatico.IdTipoDatoLanzamiento, ValorEstatico.IdActividadCabeceraLlamadaContactoInicial);

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadBNC1,
                 BuilderDictionary(ValorEstatico.IdTipoDatoLanzamiento, ValorEstatico.IdActividadCabeceraLlamadaContactoPredictiva));


            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadIT,
                BuilderDictionary(
                    valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRevisionInfo));

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadIP,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaCierre));

            diccionarioActividadesCabecera.Add(
                ValorEstatico.IdFaseOportunidadPF,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirmacionRegistroPW));

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadIC,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirmacionPago));

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadIS,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirEnvioDoc));

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadRN,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirSeguimientoRN));

            diccionarioActividadesCabecera.Add(ValorEstatico.IdFaseOportunidadRN2,
                BuilderDictionary(valorVacio, ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis));
        }

        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener Actividad Cabecera
        /// </summary>
        /// <param name="idFase"> Id de Fase </param>
        /// <param name="idTipodato"> Id de tipo de dato </param>
        /// <param name="idPersonaAreaTrabajo"> Id de Área de trabajo de personal </param>
        /// <param name="idActividadCabecera"> Id de actividad cabecera </param>
        /// <param name="probabilidad"> Valor de probabilidad </param>
        /// <returns> Diccionario clave valor : Dictionary<int, int> </returns>
        public static int ObtenerActividadCabecera(int idFase, int idTipodato, int? idPersonaAreaTrabajo, int? idActividadCabecera, decimal? probabilidad = -1)
        {
            if (idPersonaAreaTrabajo.HasValue)
            {
                if (idPersonaAreaTrabajo == ValorEstatico.IdPersonalAreaTrabajoOperaciones)
                {
                    if (idActividadCabecera.HasValue)
                        return idActividadCabecera.Value;
                    else
                        return ValorEstatico.IdActividadCabeceraLlamadaSeguimiento;
                }
            }

            if (probabilidad.Equals(-1)) return ValorEstatico.IdActividadCabeceraPrimerContactoClienteProbMedia;

            if (diccionarioActividadesCabecera == null || (diccionarioActividadesCabecera != null && diccionarioActividadesCabecera.Count == 0))
            {
                InitActividadesCabecera();
            }
            //Buscamos la actividad Cabecera
            if (!diccionarioActividadesCabecera.ContainsKey(idFase))
                return ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis;
            else
            {
                var dic = diccionarioActividadesCabecera[idFase];
                if (dic.ContainsKey(idTipodato))
                    return dic[idTipodato];
                else if (dic.ContainsKey(valorVacio))
                    return dic[valorVacio];
                return ValorEstatico.IdActividadCabeceraLlamadaConfirInteresProHis;
            }
        }


    }
}
