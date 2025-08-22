using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoPuntajeCalificacionRepository : GenericRepository<TPuestoTrabajoPuntajeCalificacion>, IPuestoTrabajoPuntajeCalificacionRepository
    {
        private Mapper _mapper;

        public PuestoTrabajoPuntajeCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoPuntajeCalificacion, PuestoTrabajoPuntajeCalificacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoPuntajeCalificacion MapeoEntidad(PuestoTrabajoPuntajeCalificacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPuestoTrabajoPuntajeCalificacion modelo = _mapper.Map<TPuestoTrabajoPuntajeCalificacion>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoPuntajeCalificacion Add(PuestoTrabajoPuntajeCalificacion entidad)
        {
            try
            {
                var PuestoTrabajoPuntajeCalificacion = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajoPuntajeCalificacion);
                return PuestoTrabajoPuntajeCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoPuntajeCalificacion Update(PuestoTrabajoPuntajeCalificacion entidad)
        {
            try
            {
                var PuestoTrabajoPuntajeCalificacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoPuntajeCalificacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoPuntajeCalificacion);
                return PuestoTrabajoPuntajeCalificacion;
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


        public IEnumerable<TPuestoTrabajoPuntajeCalificacion> Add(IEnumerable<PuestoTrabajoPuntajeCalificacion> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoPuntajeCalificacion> listado = new List<TPuestoTrabajoPuntajeCalificacion>();
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

        public IEnumerable<TPuestoTrabajoPuntajeCalificacion> Update(IEnumerable<PuestoTrabajoPuntajeCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoPuntajeCalificacion> listado = new List<TPuestoTrabajoPuntajeCalificacion>();
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
        public List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> ObtenerNombreEvaluacionPuntaje()
        {
            try
            {
                List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> EvaluacionGrupo = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
                var query = "SELECT CalificacionTotal, IdEvaluacion, NombreEvaluacion, IdGrupo, NombreGrupo, IdComponente, NombreComponente FROM [gp].[V_ObtenerEvaluacionesProcesoSeleccion] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacion(int? idPerfilPuestoTrabajo, int? idExamenTest)
        {
            try
            {
                PuestoTrabajoPuntajeCalificacion EvaluacionGrupo = new PuestoTrabajoPuntajeCalificacion();
                var query = "SELECT Id,IdPerfilPuestoTrabajo,IdExamenTest,IdGrupoComponenteEvaluacion,IdExamen,CalificaPorCentil,PuntajeMinimo,IdProcesoSeleccionRango,EsCalificable FROM gp.T_PuestoTrabajoPuntajeCalificacion WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @idPerfilPuestoTrabajo AND  IdExamenTest  = @idExamenTest AND IdGrupoComponenteEvaluacion IS NULL AND IdExamen IS NULL";
                var res = _dapperRepository.FirstOrDefault(query, new { idPerfilPuestoTrabajo , idExamenTest });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<PuestoTrabajoPuntajeCalificacion>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionAndIdGrupoComponenteEvaluacion(int? idPerfilPuestoTrabajo, int? idExamenTest,int? idGrupo)
        {
            try
            {
                PuestoTrabajoPuntajeCalificacion EvaluacionGrupo = new PuestoTrabajoPuntajeCalificacion();
                var query = "SELECT Id,IdPerfilPuestoTrabajo,IdExamenTest,IdGrupoComponenteEvaluacion,IdExamen,CalificaPorCentil,PuntajeMinimo,IdProcesoSeleccionRango,EsCalificable FROM gp.T_PuestoTrabajoPuntajeCalificacion WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @idPerfilPuestoTrabajo AND  IdExamenTest  = @idExamenTest AND IdGrupoComponenteEvaluacion = @idGrupo AND IdExamen IS NULL";
                var res = _dapperRepository.FirstOrDefault(query, new { idPerfilPuestoTrabajo, idExamenTest ,idGrupo });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<PuestoTrabajoPuntajeCalificacion>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoPuntajeCalificacion ObtenerPorIdPerfilPuestoTrabajoAndIdEvaluacionANDIdComponente(int? idPerfilPuestoTrabajo, int? idExamenTest , int? idComponente)
        {
            try
            {
                PuestoTrabajoPuntajeCalificacion EvaluacionGrupo = new PuestoTrabajoPuntajeCalificacion();
                var query = "SELECT Id,IdPerfilPuestoTrabajo,IdExamenTest,IdGrupoComponenteEvaluacion,IdExamen,CalificaPorCentil,PuntajeMinimo,IdProcesoSeleccionRango,EsCalificable FROM gp.T_PuestoTrabajoPuntajeCalificacion WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @idPerfilPuestoTrabajo AND  IdExamenTest  = @idExamenTest AND IdGrupoComponenteEvaluacion IS NULL AND IdExamen = @idComponente";
                var res = _dapperRepository.FirstOrDefault(query, new { idPerfilPuestoTrabajo, idExamenTest , idComponente });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<PuestoTrabajoPuntajeCalificacion>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PuestoTrabajoPuntajeCalificacion BuscarPorIdPerfilAndIdEvaluacionAndIdGrupoAndIdComponente(int? idPerfilPuestoTrabajo, int? idEvaluacion, int? idGrupo, int? idComponente)
        {
            try
            {
                PuestoTrabajoPuntajeCalificacion EvaluacionGrupo = new PuestoTrabajoPuntajeCalificacion();
                var query = "SELECT Id,IdPerfilPuestoTrabajo,IdExamenTest,IdGrupoComponenteEvaluacion,IdExamen,CalificaPorCentil,PuntajeMinimo,IdProcesoSeleccionRango,EsCalificable FROM gp.T_PuestoTrabajoPuntajeCalificacion WHERE Estado = 1 AND IdPerfilPuestoTrabajo = @idPerfilPuestoTrabajo AND  IdExamenTest  = @idEvaluacion AND idGrupo =@idGrupo  AND IdExamen = @idComponente";
                var res = _dapperRepository.FirstOrDefault(query, new { idPerfilPuestoTrabajo, idEvaluacion,idGrupo, idComponente });
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    EvaluacionGrupo = JsonConvert.DeserializeObject<PuestoTrabajoPuntajeCalificacion>(res);
                }
                return EvaluacionGrupo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
