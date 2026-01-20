using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: PGeneralVersionProgramaRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 09/06/2023
    /// <summary>
    /// Gestión general de T_PGeneralVersionPrograma
    /// </summary>
    public class PGeneralVersionProgramaRepository : GenericRepository<TPgeneralVersionPrograma>, IPGeneralVersionProgramaRepository
    {
        private Mapper _mapper;
        public PGeneralVersionProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralVersionPrograma, PgeneralVersionPrograma>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region
        private TPgeneralVersionPrograma MapeoEntidad(PgeneralVersionPrograma entidad)
        {
            try
            {
                TPgeneralVersionPrograma modelo = _mapper.Map<TPgeneralVersionPrograma>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralVersionPrograma Add(PgeneralVersionPrograma entidad)
        {
            try
            {
                var PgeneralVersionPrograma = MapeoEntidad(entidad);
                base.Insert(PgeneralVersionPrograma);
                return PgeneralVersionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralVersionPrograma Update(PgeneralVersionPrograma entidad)
        {
            try
            {
                var PgeneralVersionPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PgeneralVersionPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(PgeneralVersionPrograma);
                return PgeneralVersionPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las versiones del programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerVersionesProgramaPorPGeneralAsync(int idPGeneral)
        {
            try
            {
                string _query = @"SELECT Id,
                                       Nombre
                                FROM pla.V_VersionesPrograma
                                WHERE IdPgeneral = @IdPgeneral;";
                var pgeneralVersiones = await _dapperRepository.QueryDapperAsync(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(pgeneralVersiones) && !pgeneralVersiones.Contains("[]") && pgeneralVersiones != null)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(pgeneralVersiones);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 19/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene las versiones del programa por el IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> PK de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralVersionProgramaDetalleDTO> </returns>
        public IEnumerable<PGeneralVersionProgramaDetalleDTO> ObtenerPGeneralVersionProgramaDetallePorIdPGeneral(int idPGeneral)
        {
            try
            {
                IEnumerable<PGeneralVersionProgramaDetalleDTO> pgeneralVersionPrograma = new List<PGeneralVersionProgramaDetalleDTO>();
                var _query = @"SELECT IdPgeneralVersionPrograma,IdPGeneral,NombreVersion,IdVersionPrograma,Duracion,CreditoDisponibleTutorVirtual,CantidadWebinarAsignado,CantidadMesAccesoAdicionalWebinar FROM pla.V_TPGeneral_VersionPrograma WHERE  Estado = 1 and IdPGeneral = @IdPgeneral";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    pgeneralVersionPrograma = JsonConvert.DeserializeObject<IEnumerable<PGeneralVersionProgramaDetalleDTO>>(respuestaDapper);
                }
                return pgeneralVersionPrograma;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PgeneralVersionPrograma> </returns>
        public IEnumerable<PgeneralVersionPrograma> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   IdVersionPrograma,
                                   Duracion,
                                   CreditoDisponibleTutorVirtual,
                                   CantidadWebinarAsignado,
                                   CantidadMesAccesoAdicionalWebinar,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralVersionPrograma
                            WHERE Estado = 1
                                  AND IdPgeneral = @IdPgeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralVersionPrograma>>(respuestaDapper);
                }
                return new List<PgeneralVersionPrograma>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> IEnumerable<PgeneralVersionPrograma> </returns>
        public PgeneralVersionPrograma? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   IdVersionPrograma,
                                   Duracion,
                                   CreditoDisponibleTutorVirtual,
                                   CantidadWebinarAsignado,
                                   CantidadMesAccesoAdicionalWebinar,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralVersionPrograma
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                    return JsonConvert.DeserializeObject<PgeneralVersionPrograma>(respuestaDapper);

                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Flavio R. Mamani
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> IEnumerable<PgeneralVersionPrograma> </returns>
        public PgeneralVersionPrograma? ObtenerPorIdPgeneralIdVersionPrograma(int idPgeneral, int idVersionPrograma)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPgeneral,
                                   IdVersionPrograma,
                                   Duracion,
                                   CreditoDisponibleTutorVirtual,
                                   CantidadWebinarAsignado,
                                   CantidadMesAccesoAdicionalWebinar,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralVersionPrograma
                            WHERE Estado = 1
                                  AND IdPgeneral = @idPgeneral AND IdVersionPrograma = @idVersionPrograma;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPgeneral, idVersionPrograma });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                    return JsonConvert.DeserializeObject<PgeneralVersionPrograma>(respuestaDapper);

                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
