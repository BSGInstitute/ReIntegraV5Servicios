using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
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
    public class ProgramaGeneralProblemaFactorSubSolucionRepository : GenericRepository<TProgramaGeneralProblemaFactorSubSolucion>, IProgramaGeneralProblemaFactorSubSolucionRepository
    {
        private Mapper _mapper;

        public ProgramaGeneralProblemaFactorSubSolucionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralProblemaFactorSubSolucion, ProgramaGeneralProblemaFactorSubSolucion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProgramaGeneralProblemaFactorSubSolucion MapeoEntidad(ProgramaGeneralProblemaFactorSubSolucion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralProblemaFactorSubSolucion modelo = _mapper.Map<TProgramaGeneralProblemaFactorSubSolucion>(entidad);

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

        public TProgramaGeneralProblemaFactorSubSolucion Add(ProgramaGeneralProblemaFactorSubSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSubSolucion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralProblemaFactorSubSolucion);
                return ProgramaGeneralProblemaFactorSubSolucion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralProblemaFactorSubSolucion Update(ProgramaGeneralProblemaFactorSubSolucion entidad)
        {
            try
            {
                var ProgramaGeneralProblemaFactorSubSolucion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralProblemaFactorSubSolucion.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralProblemaFactorSubSolucion);
                return ProgramaGeneralProblemaFactorSubSolucion;
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


        public IEnumerable<TProgramaGeneralProblemaFactorSubSolucion> Add(IEnumerable<ProgramaGeneralProblemaFactorSubSolucion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralProblemaFactorSubSolucion> listado = new List<TProgramaGeneralProblemaFactorSubSolucion>();
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

        public IEnumerable<TProgramaGeneralProblemaFactorSubSolucion> Update(IEnumerable<ProgramaGeneralProblemaFactorSubSolucion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralProblemaFactorSubSolucion> listado = new List<TProgramaGeneralProblemaFactorSubSolucion>();
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
        /// Obtiene todos los registros de T_TipoFormacion.
        /// </summary>
        /// <returns> List<TipoFormacionDTO> </returns>
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Obtener()
        {
            try
            {
                List<ProgramaGeneralProblemaFactorSubSolucionDTO> rpta = new List<ProgramaGeneralProblemaFactorSubSolucionDTO>();
                var query = @"
                    SELECT
	                    Id, IdProgramaGeneralProblemaFactorSolucion,
                        Solucion,
                        Orden,
                        Nivel
                    FROM pla.T_ProgramaGeneralProblemaFactorSubSolucion
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaFactorSubSolucionDTO>>(resultado);

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
        /// <returns >TipoFormacion || null</returns>
        public ProgramaGeneralProblemaFactorSubSolucion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblemaFactorSolucion,
                        Solucion,
                        Orden,
                        Nivel,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactorSubSolucion
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ProgramaGeneralProblemaFactorSubSolucion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> ObtenerPorIdProgramaGeneralProblemaFactorSolucion(int idProgramaGeneralProblemaFactorSolucion)
        {
            try
            {
                List<ProgramaGeneralProblemaFactorSubSolucionDTO> rpta = new List<ProgramaGeneralProblemaFactorSubSolucionDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProgramaGeneralProblemaFactorSolucion,
                        Solucion,
                        Orden,
                        Nivel,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion
                    FROM pla.T_ProgramaGeneralProblemaFactorSubSolucion
                    WHERE IdProgramaGeneralProblemaFactorSolucion=@idProgramaGeneralProblemaFactorSolucion AND estado=1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProgramaGeneralProblemaFactorSolucion = idProgramaGeneralProblemaFactorSolucion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralProblemaFactorSubSolucionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


       
    }
}
