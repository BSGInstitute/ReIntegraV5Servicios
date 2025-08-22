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
    /// Repositorio: ProgramaAreaRelacionadaRepository
    /// Autor: GIlmer Quispe.
    /// Fecha: 19/06/2023
    /// <summary>
    /// Gestión general de T_ProgramaAreaRelacionada
    /// </summary>
    public class ProgramaAreaRelacionadaRepository : GenericRepository<TProgramaAreaRelacionadum>, IProgramaAreaRelacionadaRepository
    {
        public ProgramaAreaRelacionadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }


        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista de T_ProgramaAreaRelacionada asociados al PGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<ProgramaAreaRelacionadaDTO> </returns>
        public IEnumerable<ProgramaAreaRelacionadaDTO> ObtenerProgramaAreaRelacionadaPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdAreaCapacitacion
                            FROM pla.T_ProgramaAreaRelacionada
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaAreaRelacionadaDTO>>(respuestaDapper);
                }
                return new List<ProgramaAreaRelacionadaDTO>();
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
        /// <returns> IEnumerable<ProgramaAreaRelacionadum> </returns>
        public IEnumerable<ProgramaAreaRelacionadum> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdAreaCapacitacion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_ProgramaAreaRelacionada
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaAreaRelacionadum>>(respuestaDapper);
                }
                return new List<ProgramaAreaRelacionadum>();
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
        ///  Obtiene el registrpo por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> ProgramaAreaRelacionadum </returns>
        public ProgramaAreaRelacionadum? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdAreaCapacitacion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_ProgramaAreaRelacionada
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<ProgramaAreaRelacionadum>(respuestaDapper)!;
                }
                return null;
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
        ///  Obtiene el registrpo por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> ProgramaAreaRelacionadum </returns>
        public ProgramaAreaRelacionadum? ObtenerPorIdPgeneralIdAreaCapacitacion(int idPgeneral, int idAreaCapacitacion)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdAreaCapacitacion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_ProgramaAreaRelacionada
                            WHERE Estado = 1
                                  AND IdPGeneral = @idPgeneral AND IdAreaCapacitacion=@idAreaCapacitacion;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPgeneral, idAreaCapacitacion });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<ProgramaAreaRelacionadum>(respuestaDapper)!;
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
