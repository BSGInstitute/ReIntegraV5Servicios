using AutoMapper;
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
    public class ProcesoSeleccionPuntajeCalificacionRepository : GenericRepository<TProcesoSeleccionPuntajeCalificacion>, IProcesoSeleccionPuntajeCalificacionRepository
    {
        private Mapper _mapper;
        public ProcesoSeleccionPuntajeCalificacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProcesoSeleccionPuntajeCalificacion, ProcesoSeleccionPuntajeCalificacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ProcesoSeleccionPuntajeCalificacion, TProcesoSeleccionPuntajeCalificacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TProcesoSeleccionPuntajeCalificacion MapeoEntidad(ProcesoSeleccionPuntajeCalificacion entidad)
        {
            try
            {
                TProcesoSeleccionPuntajeCalificacion modelo = _mapper.Map<TProcesoSeleccionPuntajeCalificacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProcesoSeleccionPuntajeCalificacion Add(ProcesoSeleccionPuntajeCalificacion entidad)
        {
            try
            {
                var ProcesoSeleccionPuntajeCalificacion = MapeoEntidad(entidad);
                Insert(ProcesoSeleccionPuntajeCalificacion);
                return ProcesoSeleccionPuntajeCalificacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TProcesoSeleccionPuntajeCalificacion Update(ProcesoSeleccionPuntajeCalificacion entidad)
        {
            try
            {
                var ProcesoSeleccionPuntajeCalificacion = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProcesoSeleccionPuntajeCalificacion.RowVersion = entidadExistente.RowVersion;

                Update(ProcesoSeleccionPuntajeCalificacion);
                return ProcesoSeleccionPuntajeCalificacion;
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
        public IEnumerable<TProcesoSeleccionPuntajeCalificacion> Add(IEnumerable<ProcesoSeleccionPuntajeCalificacion> listadoEntidad)
        {
            try
            {
                List<TProcesoSeleccionPuntajeCalificacion> listado = new List<TProcesoSeleccionPuntajeCalificacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TProcesoSeleccionPuntajeCalificacion> Update(IEnumerable<ProcesoSeleccionPuntajeCalificacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProcesoSeleccionPuntajeCalificacion> listado = new List<TProcesoSeleccionPuntajeCalificacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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


        public ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamen(int? IdProcesoSeleccion, int? IdEvaluacion, int? IdGrupo)
        {
            try
            {
                ProcesoSeleccionPuntajeCalificacion rpta = new ProcesoSeleccionPuntajeCalificacion();
                var query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion=@IdGrupo AND IdExamen IS NULL ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProcesoSeleccion, IdEvaluacion, IdGrupo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccionPuntajeCalificacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamen()", ex);
            }
        }



        public ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacion(int? idProcesoSeleccion, int? idEvaluacion, int? idComponente)
        {
            try
            {
                ProcesoSeleccionPuntajeCalificacion rpta = new ProcesoSeleccionPuntajeCalificacion();
                var query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @idProcesoSeleccion AND IdExamenTest = @idEvaluacion AND IdGrupoComponenteEvaluacion IS NULL  AND IdExamen = @idComponente ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idProcesoSeleccion, idEvaluacion, idComponente });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccionPuntajeCalificacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacion()", ex);
            }
        }

        public ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdExamenIdGrupoComponenteEvaluacionPorComponente(int? idProcesoSeleccion, int? idEvaluacion, int? idGrupo, int? idComponente)
        {
            ProcesoSeleccionPuntajeCalificacion rpta = new ProcesoSeleccionPuntajeCalificacion();
            var query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @idProcesoSeleccion AND IdExamenTest = @idEvaluacion AND IdGrupoComponenteEvaluacion IS NULL  AND IdExamen = @idComponente AND IdGrupo=@idGrupo ";
            var resultado = _dapperRepository.FirstOrDefault(query, new { idProcesoSeleccion, idEvaluacion, idComponente, idGrupo });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                return JsonConvert.DeserializeObject<ProcesoSeleccionPuntajeCalificacion>(resultado)!;
            }
            return null;
        }
        public ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacion(int? IdProcesoSeleccion, int? IdEvaluacion)
        {
            try
            {
                ProcesoSeleccionPuntajeCalificacion rpta = new ProcesoSeleccionPuntajeCalificacion();
                var query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion IS NULL AND IdExamen IS NULL ";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProcesoSeleccion, IdEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccionPuntajeCalificacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdProcesoSeleccionIdEvaluacion()", ex);
            }
        }


        public ProcesoSeleccionPuntajeCalificacion? ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdEvaluacionIdGrupoComponenteEvaluacionIdExamen(int? IdProcesoSeleccion, int? IdEvaluacion, int? IdGrupo, int? idComponente)
        {
            try
            {
                ProcesoSeleccionPuntajeCalificacion rpta = new ProcesoSeleccionPuntajeCalificacion();
                var query = "";
                if (IdGrupo != null && idComponente != null)
                {
                    query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion =@IdGrupo  AND IdExamen = @idComponente ";
                }
                if (idComponente == null && IdGrupo == null)
                {
                    query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion IS NULL  AND IdExamen IS NULL ";
                }
                if (idComponente == null && IdGrupo != null)
                {
                    query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion =@IdGrupo  AND IdExamen IS NULL ";
                }
                if (idComponente != null && IdGrupo == null)
                {
                    query = @"
                    SELECT
	                    Id,
						IdProcesoSeleccion,
						IdExamenTest,
						IdGrupoComponenteEvaluacion,
						IdExamen,
						CalificaPorCentil,
						PuntajeMinimo,
						IdProcesoSeleccionRango,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
                        EsCalificable
                    FROM gp.T_ProcesoSeleccionPuntajeCalificacion
                    WHERE Estado = 1 AND IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamenTest = @IdEvaluacion AND IdGrupoComponenteEvaluacion IS NULL  AND IdExamen = @idComponente ";
                }
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProcesoSeleccion, IdEvaluacion, IdGrupo, idComponente });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProcesoSeleccionPuntajeCalificacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorIdProcesoSeleccionIdEvaluacionIdGrupoIdEvaluacionIdGrupoComponenteEvaluacionIdExamen()", ex);
            }
        }
    }

}
