using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class ConfigurarProcesoSeleccionService : IConfigurarProcesoSeleccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfigurarProcesoSeleccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPuestoTrabajo, PuestoTrabajo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        public IEnumerable<ComboDTO> ObtenerComboPuestoTrabajo()
        {
            try
            {
                return _unitOfWork.PuestoTrabajoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ComboDTO> ObtenerComboSedeTrabajo()
        {
            try
            {
                return _unitOfWork.SedeTrabajoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<CriterioEvaluacionProcesoDTO> ObtenerComboCriterioSeleccion()
        {
            try
            {
                return _unitOfWork.CriterioEvaluacionProcesoRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProcesoSeleccionRangoDTO> ObtenerProcesoSeleccionRango()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionRangoRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProcesoSeleccionEtapaDTO> ObtenerProcesoSeleccionEtapa(int IdProcesoSeleccion)
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(IdProcesoSeleccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ConfigurarProcesoSeleccionDTO> ObtenerProcesoSeleccion()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionRepository.ObtenerConfiguracionProcesoSeleccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ComboDTO> ObtenerExamenes()
        {
            try
            {
                return _unitOfWork.ExamenRepository.ObtenerExamenes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EstructuraBasicaDTO> ObtenerExamenesNoAsociados(int IdProcesoSeleccion)
        {
            try
            {
                return _unitOfWork.ExamenRepository.ObtenerExamenNoAsignadoProcesoSeleccion(IdProcesoSeleccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ExamenAsignadoProcesoDTO> ObtenerExamenesAsociados(int IdProcesoSeleccion)
        {
            try
            {
                return _unitOfWork.ExamenRepository.ObtenerExamenAsignadoProcesoSeleccion(IdProcesoSeleccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Actualizar(ProcesoSeleccionAgrupadoInsertarModificarDTO dto, string usuario)
        {
            try
            {
                List<EvaluacionAsignadoProcesoDTO> listaEvaluaciones = dto.listaEvaluaciones.Concat(dto.listaEvaluacionesEvaluador).ToList();
                ProcesoSeleccion? procesoSeleccion = _unitOfWork.ProcesoSeleccionRepository.ObtenerPorId(dto.ConfiguracionProcesoSeleccion.Id.Value);
                List<ConfiguracionAsignacionEvaluacion>? listaEvaluacion = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.ObtenerPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                List<ConfiguracionAsignacionExamen>? listaExamen = _unitOfWork.ConfiguracionAsignacionExamenRepository.ObtenerPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                List<ProcesoSeleccionEtapaDTO> listaEtapa = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                List<ConfiguracionAsignacionExamen> EliminarExamen = new List<ConfiguracionAsignacionExamen>();
                List<int> IdExistente = new List<int>();
                List<ConfiguracionAsignacionEvaluacion> EliminarEvaluacion = new List<ConfiguracionAsignacionEvaluacion>();
                List<int> IdExistenteEvaluacion = new List<int>();
                List<ProcesoSeleccionEtapaDTO> EliminarEtapa = new List<ProcesoSeleccionEtapaDTO>();
                List<int> IdExistenteEtapa = new List<int>();
                List<EtapaProcesoSeleccionDTO> ListaIdsEtapa = new List<EtapaProcesoSeleccionDTO>();
                var count = 1;

                using (TransactionScope scope = new TransactionScope())
                {
                    procesoSeleccion.Nombre = dto.ConfiguracionProcesoSeleccion.Nombre;
                    procesoSeleccion.IdPuestoTrabajo = dto.ConfiguracionProcesoSeleccion.IdPuestoTrabajo.Value;
                    procesoSeleccion.Codigo = dto.ConfiguracionProcesoSeleccion.Codigo;
                    procesoSeleccion.Url = dto.ConfiguracionProcesoSeleccion.Url;
                    procesoSeleccion.Activo = dto.ConfiguracionProcesoSeleccion.Activo;
                    procesoSeleccion.IdSede = dto.ConfiguracionProcesoSeleccion.IdSede;
                    procesoSeleccion.FechaInicioProceso = dto.ConfiguracionProcesoSeleccion.FechaInicioProceso;
                    procesoSeleccion.FechaFinProceso = dto.ConfiguracionProcesoSeleccion.FechaFinProceso;
                    procesoSeleccion.UsuarioModificacion = usuario;
                    procesoSeleccion.FechaModificacion = DateTime.Now;
                    _unitOfWork.ProcesoSeleccionRepository.Update(procesoSeleccion);
                    _unitOfWork.Commit();



                    foreach (var item in dto.listaEtapas)
                    {
                        ProcesoSeleccionEtapa etapa = new ProcesoSeleccionEtapa();
                        var etapaNuevo = listaEtapa.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (etapaNuevo != null && item.Id > 0)
                        {
                            etapa = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorId(item.Id.Value);
                            etapa.Nombre = item.Nombre;
                            //etapa.IdExamen = item.IdExamen;
                            etapa.IdProcesoSeleccion = procesoSeleccion.Id;
                            etapa.NroOrden = item.NroOrden;
                            etapa.UsuarioModificacion = usuario;
                            etapa.FechaModificacion = DateTime.Now;
                            var dataItem = _unitOfWork.ProcesoSeleccionEtapaRepository.Update(etapa);
                            _unitOfWork.Commit();
                            IdExistenteEtapa.Add(dataItem.Id);
                        }
                        else
                        {
                            etapa = new ProcesoSeleccionEtapa();
                            etapa.Nombre = item.Nombre;
                            etapa.IdProcesoSeleccion = procesoSeleccion.Id;
                            etapa.NroOrden = item.NroOrden;

                            etapa.Estado = true;
                            etapa.UsuarioCreacion = usuario;
                            etapa.UsuarioModificacion = usuario;
                            etapa.FechaCreacion = DateTime.Now;
                            etapa.FechaModificacion = DateTime.Now;
                            var dataItem = _unitOfWork.ProcesoSeleccionEtapaRepository.Add(etapa);
                            _unitOfWork.Commit();
                            etapa.Id = dataItem.Id;
                            IdExistenteEtapa.Add(dataItem.Id);

                            EtapaProcesoSeleccionDTO EtapaProceso = new EtapaProcesoSeleccionDTO();
                            if (item.Id < 0)
                            {
                                EtapaProceso.IdEtapa = item.Id;
                                EtapaProceso.IdProcesoSeleccionEtapa = dataItem.Id;
                            }
                            ListaIdsEtapa.Add(EtapaProceso);
                        }

                    }

                    foreach (var ev in listaEvaluaciones)
                    {

                        var evaluacionNuevo = listaEvaluacion.Where(x => x.IdEvaluacion == ev.IdEvaluacion && x.IdProcesoSeleccion == dto.ConfiguracionProcesoSeleccion.Id).FirstOrDefault();
                        if (evaluacionNuevo != null && evaluacionNuevo.Id != 0 && evaluacionNuevo.Id != null)
                        {
                            ConfiguracionAsignacionEvaluacion evaluacion = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacion(dto.ConfiguracionProcesoSeleccion.Id.Value, ev.IdEvaluacion.Value); ;
                            evaluacion.IdProcesoSeleccion = procesoSeleccion.Id;
                            evaluacion.IdEvaluacion = ev.IdEvaluacion.Value;
                            evaluacion.NroOrden = ev.NroOrden.Value;
                            evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() : ev.IdProcesoSeleccionEtapa;

                            evaluacion.UsuarioModificacion = usuario;
                            evaluacion.FechaModificacion = DateTime.Now;
                            _unitOfWork.DetachAll();
                            var dataItem2 = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.Update(evaluacion);

                            _unitOfWork.Commit();
                            IdExistenteEvaluacion.Add(dataItem2.Id);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            ConfiguracionAsignacionEvaluacion evaluacion = new ConfiguracionAsignacionEvaluacion();
                            evaluacion.IdProcesoSeleccion = procesoSeleccion.Id;
                            evaluacion.IdEvaluacion = ev.IdEvaluacion.Value;
                            evaluacion.NroOrden = ev.NroOrden.Value;
                            evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() : ev.IdProcesoSeleccionEtapa;

                            evaluacion.Estado = true;
                            evaluacion.UsuarioCreacion = usuario;
                            evaluacion.UsuarioModificacion = usuario;
                            evaluacion.FechaCreacion = DateTime.Now;
                            evaluacion.FechaModificacion = DateTime.Now;
                            var dataItem2 = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.Add(evaluacion);
                            _unitOfWork.Commit();
                            IdExistenteEvaluacion.Add(dataItem2.Id);
                            _unitOfWork.Commit();
                        }
                    }

                    EliminarExamen = _unitOfWork.ConfiguracionAsignacionExamenRepository.ObtenerPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                    EliminarExamen.RemoveAll(x => IdExistente.Contains(x.Id));

                    foreach (var eliminar in EliminarExamen)
                    {
                        _unitOfWork.ConfiguracionAsignacionExamenRepository.Delete(eliminar.Id, usuario);
                        _unitOfWork.Commit();
                    }


                    EliminarEtapa = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                    EliminarEtapa.RemoveAll(x => IdExistenteEtapa.Contains(x.Id));
                    foreach (var eliminar in EliminarEtapa)
                    {
                        _unitOfWork.ProcesoSeleccionEtapaRepository.Delete(eliminar.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    EliminarEvaluacion = _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.ObtenerPorIdProcesoSeleccion(dto.ConfiguracionProcesoSeleccion.Id.Value);
                    EliminarEvaluacion.RemoveAll(x => IdExistenteEvaluacion.Contains(x.Id));
                    foreach (var eliminar in EliminarEvaluacion)
                    {
                        _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.Delete(eliminar.Id, usuario);
                        _unitOfWork.Commit();
                    }
                    scope.Complete();

                }

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool InsertarProcesoSeleccionConfiguracion(ProcesoSeleccionAgrupadoInsertarModificarDTO dto, string usuario)
        {
            try
            {
                IEnumerable<EvaluacionAsignadoProcesoDTO> listaEvaluaciones = dto.listaEvaluaciones.Concat(dto.listaEvaluacionesEvaluador);
                ProcesoSeleccion procesoSeleccion = new ProcesoSeleccion();
                List<EtapaProcesoSeleccionDTO> ListaIdsEtapa = new List<EtapaProcesoSeleccionDTO>();
                var count = 1;

                using (TransactionScope scope = new TransactionScope())
                {
                    procesoSeleccion.Nombre = dto.ConfiguracionProcesoSeleccion.Nombre;
                    procesoSeleccion.IdPuestoTrabajo = dto.ConfiguracionProcesoSeleccion.IdPuestoTrabajo.Value;
                    procesoSeleccion.Codigo = dto.ConfiguracionProcesoSeleccion.Codigo;
                    procesoSeleccion.Url = dto.ConfiguracionProcesoSeleccion.Url;
                    procesoSeleccion.Activo = dto.ConfiguracionProcesoSeleccion.Activo;
                    procesoSeleccion.IdSede = dto.ConfiguracionProcesoSeleccion.IdSede;
                    procesoSeleccion.FechaInicioProceso = dto.ConfiguracionProcesoSeleccion.FechaInicioProceso;
                    procesoSeleccion.FechaFinProceso = dto.ConfiguracionProcesoSeleccion.FechaFinProceso;

                    procesoSeleccion.Estado = true;
                    procesoSeleccion.UsuarioCreacion = usuario;
                    procesoSeleccion.UsuarioModificacion = usuario;
                    procesoSeleccion.FechaCreacion = DateTime.Now;
                    procesoSeleccion.FechaModificacion = DateTime.Now;
                    var dataItem = _unitOfWork.ProcesoSeleccionRepository.Add(procesoSeleccion);
                    _unitOfWork.Commit();
                    dto.ConfiguracionProcesoSeleccion.Id = dataItem.Id;


                    foreach (var item in dto.listaEtapas)
                    {
                        ProcesoSeleccionEtapa etapa = new ProcesoSeleccionEtapa();
                        etapa.Nombre = item.Nombre;
                        etapa.IdProcesoSeleccion = dataItem.Id;
                        etapa.NroOrden = item.NroOrden;

                        etapa.Estado = true;
                        etapa.UsuarioCreacion = usuario;
                        etapa.UsuarioModificacion = usuario;
                        etapa.FechaCreacion = DateTime.Now;
                        etapa.FechaModificacion = DateTime.Now;
                        var dataItem2 = _unitOfWork.ProcesoSeleccionEtapaRepository.Add(etapa);

                        _unitOfWork.Commit();
                        etapa.IdProcesoSeleccion = dataItem2.Id;
                        EtapaProcesoSeleccionDTO EtapaProceso = new EtapaProcesoSeleccionDTO();
                        if (item.Id < 0)
                        {
                            EtapaProceso.IdEtapa = item.Id;
                            EtapaProceso.IdProcesoSeleccionEtapa = dataItem2.Id;
                        }
                        ListaIdsEtapa.Add(EtapaProceso);
                    }

                    foreach (var ev in listaEvaluaciones)
                    {
                        ConfiguracionAsignacionEvaluacion evaluacion = new ConfiguracionAsignacionEvaluacion();
                        evaluacion.IdProcesoSeleccion = dataItem.Id;
                        evaluacion.IdEvaluacion = ev.IdEvaluacion.Value;
                        evaluacion.NroOrden = ev.NroOrden.Value;
                        if (ev.IdProcesoSeleccionEtapa < 0)
                        {
                            evaluacion.IdProcesoSeleccionEtapa = ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault();
                        }
                        else
                        {
                            evaluacion.IdProcesoSeleccionEtapa = null;
                        }
                        //evaluacion.IdProcesoSeleccionEtapa = ev.IdProcesoSeleccionEtapa < 0 ? ListaIdsEtapa.Where(x => x.IdEtapa == ev.IdProcesoSeleccionEtapa).Select(x => x.IdProcesoSeleccionEtapa).FirstOrDefault() :0;

                        evaluacion.Estado = true;
                        evaluacion.UsuarioCreacion = usuario;
                        evaluacion.UsuarioModificacion = usuario;
                        evaluacion.FechaCreacion = DateTime.Now;
                        evaluacion.FechaModificacion = DateTime.Now;
                        _unitOfWork.ConfiguracionAsignacionEvaluacionRepository.Add(evaluacion);
                        _unitOfWork.Commit();
                    }

                    scope.Complete();
                }

                return true;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool EliminarProcesoSeleccionConfiguracion(int id, string usuario)
        {

            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                List<int> ListaExamen = _unitOfWork.ConfiguracionAsignacionExamenRepository.ObtenerPorIdsProcesoSeleccion(id);


                foreach (int IdEliminar in ListaExamen)
                {
                    var respuesta = _unitOfWork.ConfiguracionAsignacionExamenRepository.Delete(IdEliminar, usuario);
                    _unitOfWork.Commit();
                }
                var obtenerId = _unitOfWork.ProcesoSeleccionRepository.ObtenerPorId(id);
                if (obtenerId != null)
                {
                    _unitOfWork.ProcesoSeleccionRepository.Delete(id, usuario);
                }
                else
                {
                    throw new Exception("El registro que se desea eliminar no existe ¿Id correcto?");
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public EvaluacionesAsociacionDTO ObtenerEvaluacionesAsociacion(int IdProcesoSeleccion)
        {
            try
            {
                ExamenTest evaluacionRep = new ExamenTest();
                ProcesoSeleccionEtapa repEtapaRep = new ProcesoSeleccionEtapa();

                var listaEvaluacionNoAsociado = _unitOfWork.ExamenTestRepository.ObtenerEvaluacionNoAsignadoProcesoSeleccion(IdProcesoSeleccion);
                var listaEvaluacionAsociado = _unitOfWork.ExamenTestRepository.ObtenerEvaluacionAsignadoProcesoSeleccion(IdProcesoSeleccion).OrderBy(x => x.NroOrden).ToList();
                var listaEtapaProcesoSeleccion = _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerEtapaPorIdProcesoSeleccion(IdProcesoSeleccion);

                EvaluacionesAsociacionDTO resultado = new EvaluacionesAsociacionDTO();
                resultado.listaEvaluacionNoAsociado = listaEvaluacionNoAsociado.Where(x => x.EsCalificadoPorPostulante == true).ToList();
                resultado.listaEvaluacionAsociado = listaEvaluacionAsociado.Where(x => x.EsCalificadoPorPostulante == true).ToList();
                resultado.listaEvaluacionNoAsociadoEvaluador = listaEvaluacionNoAsociado.Where(x => x.EsCalificadoPorPostulante == false).ToList();
                resultado.listaEvaluacionAsociadoEvaluador = listaEvaluacionAsociado.Where(x => x.EsCalificadoPorPostulante == false).ToList();
                resultado.listaEtapa = listaEtapaProcesoSeleccion;

                return resultado;

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public EvaluacionPuntajeDTO ObtenerEvaluacionPuntaje(int IdProcesoSeleccion)
        {
            try
            {

                var listaEvaluacion = _unitOfWork.ExamenTestRepository.ObtenerNombreEvaluacionPuntaje(IdProcesoSeleccion);
                var ListaCalificacionTotal = listaEvaluacion.Where(x => x.CalificacionTotal == true).ToList();
                var ListaCalificacionAgrupadaIndependiente = listaEvaluacion.Where(x => x.CalificacionTotal == false).ToList();
                var ListaIndependiente = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo == null || x.IdGrupo == 0).ToList();
                var ListaGrupo = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo != null && x.IdGrupo != 0).ToList();
                var ListaComponente = ListaGrupo.Where(x => x.IdComponente != null && x.IdComponente != 0).ToList();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionTotal = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionAgrupada = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                List<NombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionIndependiente = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                List<NombreEvaluacionAgrupadaComponenteDTO> listacomponentesIndependientes = new List<NombreEvaluacionAgrupadaComponenteDTO>();

                //Separa todas las Calificaciones Totales, se agrupa las evaluaciones y se colocan en nuevoCalificacionTotal
                if (ListaCalificacionTotal.Count() > 0)
                {
                    foreach (var item in ListaCalificacionTotal)
                    {
                        item.IdComponente = null;
                        item.IdGrupo = null;
                        item.NombreComponente = null;
                        item.NombreGrupo = null;
                        item.CalificaAgrupadoNoIndependiente = false;
                    }
                    nuevoCalificacionTotal = ListaCalificacionTotal.GroupBy(u => (u.IdProcesoSeleccion, u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion, u.IdComponente, u.NombreComponente, u.Puntaje))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,
                            IdGrupo = group.Key.IdGrupo
                            ,
                            IdComponente = group.Key.IdComponente
                            ,
                            IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,
                            NombreComponente = group.Key.NombreComponente
                            ,
                            NombreGrupo = group.Key.NombreGrupo
                            ,
                            NombreEvaluacion = group.Key.NombreEvaluacion
                        ,
                            CalificacionTotal = true
                        ,
                            Puntaje = group.Key.Puntaje
                        ,
                            CalificaPorCentil = false
                        ,
                            IdProcesoSeleccionRango = 0
                        ,
                            EsCalificable = false
                        }).ToList();
                }

                //Separa todas las Calificaciones por Componente, se agrupa los Componentes y se colocan en nuevoCalificacionIndependiente
                if (ListaIndependiente.Count() > 0)
                {
                    foreach (var item in ListaIndependiente)
                    {
                        item.IdGrupo = null;
                        item.NombreGrupo = null;
                    }
                    nuevoCalificacionIndependiente = ListaIndependiente.GroupBy(u => (u.IdProcesoSeleccion, u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion, u.IdComponente, u.NombreComponente, u.Puntaje))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,
                            IdGrupo = group.Key.IdGrupo
                            ,
                            IdComponente = group.Key.IdComponente
                            ,
                            IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,
                            NombreComponente = group.Key.NombreComponente
                            ,
                            NombreGrupo = group.Key.NombreGrupo
                            ,
                            NombreEvaluacion = group.Key.NombreEvaluacion
                            ,
                            CalificacionTotal = false
                            ,
                            Puntaje = group.Key.Puntaje
                            ,
                            CalificaPorCentil = false
                            ,
                            CalificaAgrupadoNoIndependiente = false
                            ,
                            IdProcesoSeleccionRango = 0
                            ,
                            EsCalificable = false
                        }).ToList();
                }
                //Separa todos las opciones 
                if (ListaComponente.Count() > 0)
                {
                    listacomponentesIndependientes = ListaComponente.GroupBy(u => (u.IdProcesoSeleccion, u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion, u.IdComponente, u.NombreComponente, u.Puntaje))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,
                            IdGrupo = group.Key.IdGrupo
                            ,
                            IdComponente = group.Key.IdComponente
                            ,
                            IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,
                            NombreComponente = group.Key.NombreComponente
                            ,
                            NombreGrupo = group.Key.NombreGrupo
                            ,
                            NombreEvaluacion = group.Key.NombreEvaluacion
                            ,
                            CalificacionTotal = false
                            ,
                            Puntaje = group.Key.Puntaje
                            ,
                            CalificaPorCentil = false
                            ,
                            CalificaAgrupadoNoIndependiente = true
                            ,
                            IdProcesoSeleccionRango = 0
                            ,
                            EsCalificable = false
                        }).ToList();
                }


                //Separa todas las Calificaciones por Grupo, se agrupa los Grupos y se colocan en nuevoCalificacionAgrupada
                if (ListaGrupo.Count() > 0)
                {
                    foreach (var item in ListaGrupo)
                    {
                        item.IdComponente = null;
                        item.NombreComponente = null;
                    }
                    nuevoCalificacionAgrupada = ListaGrupo.GroupBy(u => (u.IdProcesoSeleccion, u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion, u.IdComponente, u.NombreComponente, u.Puntaje))
                        .Select(group => new NombreEvaluacionAgrupadaComponenteDTO
                        {
                            IdEvaluacion = group.Key.IdEvaluacion
                            ,
                            IdGrupo = group.Key.IdGrupo
                            ,
                            IdComponente = group.Key.IdComponente
                            ,
                            IdProcesoSeleccion = group.Key.IdProcesoSeleccion
                            ,
                            NombreComponente = group.Key.NombreComponente
                            ,
                            NombreGrupo = group.Key.NombreGrupo
                            ,
                            NombreEvaluacion = group.Key.NombreEvaluacion
                            ,
                            CalificacionTotal = false
                            ,
                            Puntaje = group.Key.Puntaje
                            ,
                            CalificaPorCentil = false
                            ,
                            CalificaAgrupadoNoIndependiente = true
                            ,
                            IdProcesoSeleccionRango = 0
                            ,
                            EsCalificable = false
                        }).ToList();
                }



                List<NombreEvaluacionAgrupadaComponenteDTO> listaPuntajeCalificacionTotal = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                listaPuntajeCalificacionTotal = nuevoCalificacionTotal.Concat(nuevoCalificacionAgrupada).Concat(nuevoCalificacionIndependiente).ToList();
                List<NombreEvaluacionAgrupadaComponenteDTO> listaPuntajeCalificacionComponente = new List<NombreEvaluacionAgrupadaComponenteDTO>();
                listaPuntajeCalificacionComponente = nuevoCalificacionTotal.Concat(nuevoCalificacionAgrupada).Concat(nuevoCalificacionIndependiente).Concat(listacomponentesIndependientes).ToList();
                foreach (var item in listaPuntajeCalificacionTotal)
                {
                    if (item.IdEvaluacion != null && item.IdGrupo == null && item.IdComponente == null)
                    {

                        ProcesoSeleccionPuntajeCalificacion evaluacionPje = new ProcesoSeleccionPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacion(item.IdProcesoSeleccion, item.IdEvaluacion);
                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdGrupo != null)
                    {

                        ProcesoSeleccionPuntajeCalificacion evaluacionPje = new ProcesoSeleccionPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamen(item.IdProcesoSeleccion, item.IdEvaluacion, item.IdGrupo);

                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                    if (item.IdComponente != null)
                    {
                        ProcesoSeleccionPuntajeCalificacion evaluacionPje = new ProcesoSeleccionPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacion(item.IdProcesoSeleccion, item.IdEvaluacion, item.IdComponente);

                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }
                    }
                }

                foreach (var item in listaPuntajeCalificacionComponente)
                {
                    if (item.IdComponente != null && item.IdGrupo != null)
                    {
                        ProcesoSeleccionPuntajeCalificacion evaluacionPje = new ProcesoSeleccionPuntajeCalificacion();
                        evaluacionPje = _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdEvaluacionIdGrupoComponenteEvaluacionIdExamen(item.IdProcesoSeleccion, item.IdEvaluacion, item.IdGrupo, item.IdComponente);

                        if (evaluacionPje != null && evaluacionPje.Id != 0)
                        {
                            item.Puntaje = evaluacionPje.PuntajeMinimo;
                            item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
                            item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
                            item.EsCalificable = evaluacionPje.EsCalificable;
                        }

                    }
                }

                List<NombreEvaluacionesAgrupadaIndependienteDTO> listaNombreEvaluacion = new List<NombreEvaluacionesAgrupadaIndependienteDTO>();
                foreach (var item in listaPuntajeCalificacionTotal)
                {
                    if (listaPuntajeCalificacionTotal.Count > 0)
                    {
                        listaNombreEvaluacion.Add(new NombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id = item.IdEvaluacion.Value
                            ,
                            Nombre = item.NombreEvaluacion
                            ,
                            CalificacionTotal = item.CalificacionTotal
                            ,
                            CalificaAgrupadoNoIndependiente = item.CalificaAgrupadoNoIndependiente
                        });
                    }

                }
                var listaEvaluaciones = listaNombreEvaluacion.GroupBy(u => (u.Id, u.Nombre, u.CalificacionTotal, u.CalificaAgrupadoNoIndependiente))
                        .Select(group => new NombreEvaluacionesAgrupadaIndependienteDTO
                        {
                            Id = group.Key.Id
                            ,
                            Nombre = group.Key.Nombre
                            ,
                            CalificacionTotal = group.Key.CalificacionTotal
                            ,
                            CalificaAgrupadoNoIndependiente = group.Key.CalificaAgrupadoNoIndependiente
                        }).ToList();
                //List<NombreEvaluacionAgrupadaComponenteDTO2>? listaComponente = _unitOfWork.ConfiguracionAsignacionExamenRepository.obtenerComponentesEvaluacion();
                EvaluacionPuntajeDTO resultado = new EvaluacionPuntajeDTO();
                resultado.listaEvaluacionesPuntajeCalificacion = listaPuntajeCalificacionTotal;
                resultado.listaEvaluaciones = listaEvaluaciones;
                resultado.listacomponentes = listaPuntajeCalificacionComponente;

                return resultado;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool ActualizarProcesoSeleccionConfiguracionCalificacion(PuntajeEvaluacionAgrupadaComponenteDTO Json, string usuario)
        {

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in Json.ListaPuntaje)
                    {
                        ProcesoSeleccionPuntajeCalificacion puntaje = _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdEvaluacionIdGrupoComponenteEvaluacionIdExamen(item.IdProcesoSeleccion, item.IdEvaluacion, item.IdGrupo, item.IdComponente);
                        if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
                        {

                            puntaje.CalificaPorCentil = item.CalificaPorCentil;
                            puntaje.PuntajeMinimo = item.Puntaje;
                            puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                            puntaje.EsCalificable = item.EsCalificable;

                            puntaje.UsuarioModificacion = usuario;
                            puntaje.FechaModificacion = DateTime.Now;
                            _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.Update(puntaje);
                          
                        }
                        else
                        {
                            puntaje = new ProcesoSeleccionPuntajeCalificacion();
                            puntaje.IdProcesoSeleccion = item.IdProcesoSeleccion;
                            puntaje.IdExamen = item.IdComponente;
                            puntaje.IdExamenTest = item.IdEvaluacion;
                            puntaje.IdGrupoComponenteEvaluacion = item.IdGrupo;
                            puntaje.CalificaPorCentil = item.CalificaPorCentil;
                            puntaje.PuntajeMinimo = item.Puntaje;
                            puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
                            puntaje.EsCalificable = item.EsCalificable;

                            puntaje.Estado = true;
                            puntaje.UsuarioCreacion = usuario;
                            puntaje.FechaCreacion = DateTime.Now;
                            puntaje.UsuarioModificacion = usuario;
                            puntaje.FechaModificacion = DateTime.Now;
                            _unitOfWork.ProcesoSeleccionPuntajeCalificacionRepository.Add(puntaje);
                           
                        }

                    }
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }


    }
}
