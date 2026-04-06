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
    public class PEspecificoSesionEstadoObservacionDetalleRepository : GenericRepository<TPespecificoSesionEstadoObservacionDetalle>, IPEspecificoSesionEstadoObservacionDetalleRepository
    {
        private Mapper _mapper;

        public PEspecificoSesionEstadoObservacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesionEstadoObservacionDetalle, PEspecificoSesionEstadoObservacionDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacionDetalle, PEspecificoSesionEstadoObservacionDetalleDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstadoObservacionDetalle, TPespecificoSesionEstadoObservacionDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoSesionEstadoObservacionDetalle MapeoEntidad(PEspecificoSesionEstadoObservacionDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoSesionEstadoObservacionDetalle modelo = _mapper.Map<TPespecificoSesionEstadoObservacionDetalle>(entidad);

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

        public TPespecificoSesionEstadoObservacionDetalle Add(PEspecificoSesionEstadoObservacionDetalle entidad)
        {
            try
            {
                var PEspecificoSesionEstadoObservacionDetalle = MapeoEntidad(entidad);
                base.Insert(PEspecificoSesionEstadoObservacionDetalle);
                return PEspecificoSesionEstadoObservacionDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoSesionEstadoObservacionDetalle Update(PEspecificoSesionEstadoObservacionDetalle entidad)
        {
            try
            {
                var PEspecificoSesionEstadoObservacionDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecificoSesionEstadoObservacionDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(PEspecificoSesionEstadoObservacionDetalle);
                return PEspecificoSesionEstadoObservacionDetalle;
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


        public IEnumerable<TPespecificoSesionEstadoObservacionDetalle> Add(IEnumerable<PEspecificoSesionEstadoObservacionDetalle> listadoEntidad)
        {
            try
            {
                List<TPespecificoSesionEstadoObservacionDetalle> listado = new List<TPespecificoSesionEstadoObservacionDetalle>();
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

        public IEnumerable<TPespecificoSesionEstadoObservacionDetalle> Update(IEnumerable<PEspecificoSesionEstadoObservacionDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoSesionEstadoObservacionDetalle> listado = new List<TPespecificoSesionEstadoObservacionDetalle>();
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
        /// Obtiene todos los registros de T_PEspecificoSesionEstadoObservacionDetalle.
        /// </summary>
        /// <returns> List<PEspecificoSesionEstadoObservacionDetalleDTO> </returns>
        public IEnumerable<PEspecificoSesionEstadoObservacionDetalleDTO> Obtener()
        {
            try
            {
                List<PEspecificoSesionEstadoObservacionDetalleDTO> rpta = new List<PEspecificoSesionEstadoObservacionDetalleDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre AS Contenido,IdPEspecificoSesionEstadoObservacion
                    FROM pla.T_PEspecificoSesionEstadoObservacionDetalle
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEstadoObservacionDetalleDTO>>(resultado);

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
        /// <returns >PEspecificoSesionEstadoObservacionDetalle || null</returns>
        public IEnumerable<PEspecificoSesionEstadoObservacionDetalle> ObtenerPorId(int id)
        {
            try
            {
                var query = @"
            SELECT
                Id,
                Nombre,
                IdPEspecificoSesionEstadoObservacion AS IdPespecificoSesionEstadoObservacion,
                Orden,
                Estado,
                UsuarioCreacion,
                UsuarioModificacion,
                FechaCreacion,
                FechaModificacion,
                RowVersion
            FROM pla.T_PEspecificoSesionEstadoObservacionDetalle
            WHERE IdPEspecificoSesionEstadoObservacion = @id
              AND Estado = 1
            ORDER BY Orden ASC";

                var resultado = _dapperRepository.QueryDapper(query, new { id });

                if (!string.IsNullOrWhiteSpace(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<PEspecificoSesionEstadoObservacionDetalle>>(resultado)
                           ?? new List<PEspecificoSesionEstadoObservacionDetalle>();
                }

                return new List<PEspecificoSesionEstadoObservacionDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-DET-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

    }
}
