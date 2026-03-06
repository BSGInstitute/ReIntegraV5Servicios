using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class PEspecificoSesionEstadoObservacionRepository : GenericRepository<TPEspecificoSesionEstadoObservacion>, IPEspecificoSesionEstadoObservacionRepository
    {
        private Mapper _mapper;

        public PEspecificoSesionEstadoObservacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPEspecificoSesionEstadoObservacion, PEspecificoSesionEstadoObservacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacion, PEspecificoSesionEstadoObservacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacion, TPEspecificoSesionEstadoObservacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPEspecificoSesionEstadoObservacion MapeoEntidad(PEspecificoSesionEstadoObservacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPEspecificoSesionEstadoObservacion modelo = _mapper.Map<TPEspecificoSesionEstadoObservacion>(entidad);

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

        public TPEspecificoSesionEstadoObservacion Add(PEspecificoSesionEstadoObservacion entidad)
        {
            try
            {
                var PEspecificoSesionEstadoObservacion = MapeoEntidad(entidad);
                base.Insert(PEspecificoSesionEstadoObservacion);
                return PEspecificoSesionEstadoObservacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPEspecificoSesionEstadoObservacion Update(PEspecificoSesionEstadoObservacion entidad)
        {
            try
            {
                var PEspecificoSesionEstadoObservacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecificoSesionEstadoObservacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PEspecificoSesionEstadoObservacion);
                return PEspecificoSesionEstadoObservacion;
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


        public IEnumerable<TPEspecificoSesionEstadoObservacion> Add(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad)
        {
            try
            {
                List<TPEspecificoSesionEstadoObservacion> listado = new List<TPEspecificoSesionEstadoObservacion>();
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

        public IEnumerable<TPEspecificoSesionEstadoObservacion> Update(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPEspecificoSesionEstadoObservacion> listado = new List<TPEspecificoSesionEstadoObservacion>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PEspecificoSesionEstadoObservacion.
        /// </summary>
        /// <returns> List<PEspecificoSesionEstadoObservacionDTO> </returns>
        public IEnumerable<PEspecificoSesionEstadoObservacionDTO> Obtener()
        {
            try
            {
                List<PEspecificoSesionEstadoObservacionDTO> rpta = new List<PEspecificoSesionEstadoObservacionDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM pla.T_PEspecificoSesionEstadoObservacion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEstadoObservacionDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >PEspecificoSesionEstadoObservacion || null</returns>
        public PEspecificoSesionEstadoObservacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_PEspecificoSesionEstadoObservacion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PEspecificoSesionEstadoObservacion>(resultado)!;
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
