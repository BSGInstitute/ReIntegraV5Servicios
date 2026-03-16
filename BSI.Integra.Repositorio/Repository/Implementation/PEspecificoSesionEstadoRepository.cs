using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class PEspecificoSesionEstadoRepository : GenericRepository<TPespecificoSesionEstado>, IPEspecificoSesionEstadoRepository
    {
        private Mapper _mapper;

        public PEspecificoSesionEstadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoSesionEstado, PEspecificoSesionEstado>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstado, PEspecificoSesionEstadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PEspecificoSesionEstado, TPespecificoSesionEstado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPespecificoSesionEstado MapeoEntidad(PEspecificoSesionEstado entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoSesionEstado modelo = _mapper.Map<TPespecificoSesionEstado>(entidad);

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

        public TPespecificoSesionEstado Add(PEspecificoSesionEstado entidad)
        {
            try
            {
                var PEspecificoSesionEstado = MapeoEntidad(entidad);
                base.Insert(PEspecificoSesionEstado);
                return PEspecificoSesionEstado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoSesionEstado Update(PEspecificoSesionEstado entidad)
        {
            try
            {
                var PEspecificoSesionEstado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecificoSesionEstado.RowVersion = entidadExistente.RowVersion;

                base.Update(PEspecificoSesionEstado);
                return PEspecificoSesionEstado;
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


        public IEnumerable<TPespecificoSesionEstado> Add(IEnumerable<PEspecificoSesionEstado> listadoEntidad)
        {
            try
            {
                List<TPespecificoSesionEstado> listado = new List<TPespecificoSesionEstado>();
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

        public IEnumerable<TPespecificoSesionEstado> Update(IEnumerable<PEspecificoSesionEstado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoSesionEstado> listado = new List<TPespecificoSesionEstado>();
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
        /// Obtiene todos los registros de T_PEspecificoSesionEstado.
        /// </summary>
        /// <returns> List<PEspecificoSesionEstadoDTO> </returns>
        public IEnumerable<PEspecificoSesionEstadoDTO> Obtener()
        {
            try
            {
                List<PEspecificoSesionEstadoDTO> rpta = new List<PEspecificoSesionEstadoDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM pla.T_PEspecificoSesionEstado
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PEspecificoSesionEstadoDTO>>(resultado);

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
        /// <returns >PEspecificoSesionEstado || null</returns>
        public PEspecificoSesionEstado? ObtenerPorId(int id)
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
                    FROM pla.T_PEspecificoSesionEstado
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PEspecificoSesionEstado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public void ActualizarEstadoCurso(EstadoCursoDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_TPEspecificoSesion_ActualizarPEspecificoSesionEstado";
                var parametros = new
                {
                    Id = dto.Id,
                    IdPEspecificoSesionEstado = dto.IdPEspecificoSesionEstado,
                    UsuarioModificacion = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarEstadoCurso() {ex.Message}", ex);
            }
        }

        public void ActualizarEstadoObservacion(EstadoCursoObservacionDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_TPEspecificoSesion_ActualizarPEspecificoSesionEstadoObservacionDetalle";
                var parametros = new
                {
                    Id = dto.Id,
                    IdPEspecificoSesionEstadoObservacionDetalle = dto.IdPEspecificoSesionEstadoObservacionDetalle,
                    UsuarioModificacion = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarEstadoObservacion() {ex.Message}", ex);
            }
        }
    }
}
