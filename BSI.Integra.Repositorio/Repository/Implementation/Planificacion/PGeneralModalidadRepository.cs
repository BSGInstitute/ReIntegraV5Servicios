using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PGeneralModalidadRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 15/06/2023
    /// Autor: GIlmer Quispe.
    /// Fecha: 19/06/2023
    /// <summary>
    /// Gestión general de T_PgeneralModalidad
    /// Gestión general de T_PGeneralModalidad
    /// </summary>
    public class PGeneralModalidadRepository : GenericRepository<TPgeneralModalidad>, IPGeneralModalidadRepository
    {
        private Mapper _mapper;

        public PGeneralModalidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralModalidad, PgeneralModalidad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralModalidad MapeoEntidad(PgeneralModalidad entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralModalidad pGeneralModalidad = _mapper.Map<TPgeneralModalidad>(entidad);

                return pGeneralModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralModalidad Add(PgeneralModalidad entidad)
        {
            try
            {
                var pGeneralModalidad = MapeoEntidad(entidad);
                Insert(pGeneralModalidad);
                return pGeneralModalidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralModalidad Update(PgeneralModalidad entidad)
        {
            try
            {
                var pGeneralModalidad = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                pGeneralModalidad.RowVersion = entidadExistente.RowVersion;

                Update(pGeneralModalidad);
                return pGeneralModalidad;
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

        public IEnumerable<TPgeneralModalidad> Add(IEnumerable<PgeneralModalidad> listadoEntidad)
        {
            try
            {
                List<TPgeneralModalidad> listado = new List<TPgeneralModalidad>();
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

        public IEnumerable<TPgeneralModalidad> Update(IEnumerable<PgeneralModalidad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralModalidad> listado = new List<TPgeneralModalidad>();
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
        /// Autor: Jonathan Caipo
        /// Fecha: 07/06/2023
        /// Version: 1.0
        /// <summary>
        /// Listar modalidades del curso
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> Lista DTO - List<PGeneralModalidadDTO> </returns>
        public IEnumerable<PGeneralModalidadDTO> ListarModalidadesCurso(int idPGeneral)
        {
            try
            {
                var query = @"SELECT IdModalidadCurso FROM pla.T_PGeneralModalidad WHERE IdPgeneral = @idPGeneral AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralModalidadDTO>>(resultado)!;
                }
                return new List<PGeneralModalidadDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PGMR-LMC-001@Error en ObtenerCombo() {ex.Message}", ex);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista de T_PGeneralModalidad asociados al PGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<PGeneralModalidadDTO> </returns>
        public IEnumerable<PGeneralModalidadDTO> ObtenerPGeneralModalidadPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdModalidadCurso
                            FROM pla.T_PGeneralModalidad
                            WHERE Estado = 1
                                  AND IdPgeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralModalidadDTO>>(respuestaDapper);
                }
                return new List<PGeneralModalidadDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> IEnumerable<PGeneralModalidadDTO> </returns>
        public PgeneralModalidad? ObtenerPorIdPGeneralYIdModalidadCurso(int idPGeneral, int idModalidadCurso)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdModalidadCurso,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralModalidad
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral AND IdModalidadCurso=@IdModalidadCurso;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { IdPGeneral = idPGeneral, IdModalidadCurso = idModalidadCurso });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("null"))
                {
                    return JsonConvert.DeserializeObject<PgeneralModalidad>(respuestaDapper)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}

