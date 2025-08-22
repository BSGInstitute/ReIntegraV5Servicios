using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public class PerfilPuestoTrabajoPersonalAprobacionRepository : GenericRepository<TPerfilPuestoTrabajoPersonalAprobacion>, IPerfilPuestoTrabajoPersonalAprobacionRepository
    {
        private Mapper _mapper;

        public PerfilPuestoTrabajoPersonalAprobacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoPersonalAprobacion, TPerfilPuestoTrabajoPersonalAprobacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPerfilPuestoTrabajoPersonalAprobacion MapeoEntidad(PerfilPuestoTrabajoPersonalAprobacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPerfilPuestoTrabajoPersonalAprobacion modelo = _mapper.Map<TPerfilPuestoTrabajoPersonalAprobacion>(entidad);

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

        public TPerfilPuestoTrabajoPersonalAprobacion Add(PerfilPuestoTrabajoPersonalAprobacion entidad)
        {
            try
            {
                var PerfilPuestoTrabajoPersonalAprobacion = MapeoEntidad(entidad);
                base.Insert(PerfilPuestoTrabajoPersonalAprobacion);
                return PerfilPuestoTrabajoPersonalAprobacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPerfilPuestoTrabajoPersonalAprobacion Update(PerfilPuestoTrabajoPersonalAprobacion entidad)
        {
            try
            {
                var PerfilPuestoTrabajoPersonalAprobacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PerfilPuestoTrabajoPersonalAprobacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PerfilPuestoTrabajoPersonalAprobacion);
                return PerfilPuestoTrabajoPersonalAprobacion;
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


        public IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> Add(IEnumerable<PerfilPuestoTrabajoPersonalAprobacion> listadoEntidad)
        {
            try
            {
                List<TPerfilPuestoTrabajoPersonalAprobacion> listado = new List<TPerfilPuestoTrabajoPersonalAprobacion>();
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

        public IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> Update(IEnumerable<PerfilPuestoTrabajoPersonalAprobacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPerfilPuestoTrabajoPersonalAprobacion> listado = new List<TPerfilPuestoTrabajoPersonalAprobacion>();
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

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >TipoFormacion || null</returns>
        public PerfilPuestoTrabajoPersonalAprobacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
                        IdPuestoTrabajo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PerfilPuestoTrabajoPersonalAprobacion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id=id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PerfilPuestoTrabajoPersonalAprobacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public PerfilPuestoTrabajoPersonalAprobacion? ObtenerPorIdPersonalAndIdPuestoTrabajo(int idPersonal,int idPuestoTrabajo)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
                        IdPuestoTrabajo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PerfilPuestoTrabajoPersonalAprobacion
                    WHERE Id=@idPersonal AND  IdPuestoTrabajo = @idPuestoTrabajo AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal = idPersonal,idPuestoTrabajo= idPuestoTrabajo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PerfilPuestoTrabajoPersonalAprobacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// <summary>
		/// Obtiene lista de personal configurado para aprobar versiones de perfil de puesto de trabajo
		/// </summary>
		/// <returns></returns>
		public List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO> ObtenerPersonalConfigurado()
        {
            try
            {
                List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO> lista = new List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO>();
                var query = "SELECT Id, IdPersonal, Personal, IdPuestoTrabajo, PuestoTrabajo FROM [gp].[V_TPerfilPuestoTrabajoPersonalAprobacion_ObtenerPersonalConfigurado] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO>>(res);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<PerfilPuestoTrabajoPersonalAprobacion>? ObtenerbyIdPersonal(int idPersonal)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdPersonal,
                        IdPuestoTrabajo,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_PerfilPuestoTrabajoPersonalAprobacion
                    WHERE IdPersonal=@idPersonal AND estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal=idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject< List<PerfilPuestoTrabajoPersonalAprobacion>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
