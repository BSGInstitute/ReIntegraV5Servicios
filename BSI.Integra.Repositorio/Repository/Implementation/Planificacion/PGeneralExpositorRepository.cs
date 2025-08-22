using AutoMapper;
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
    /// Repositorio: PGeneralExpositorRepository
    /// Autor: GIlmer Quispe.
    /// Fecha: 19/06/2023
    /// <summary>
    /// Gestión general de T_PGeneralExpositor
    /// </summary>
    public class PGeneralExpositorRepository : GenericRepository<TPgeneralExpositor>, IPGeneralExpositorRepository
    {
        public PGeneralExpositorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista Descripción web General para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<PGeneralExpositorDTO> </returns>
        public IEnumerable<PGeneralExpositorDTO> ObtenerPGeneralExpositorPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdExpositor,
                                   Posicion,
                                   IdModalidadCurso
                            FROM pla.T_PGeneralExpositor
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralExpositorDTO>>(resultado)!;
                }
                return new List<PGeneralExpositorDTO>();
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
        /// <returns> IEnumerable<PGeneralExpositor> </returns>
        public IEnumerable<PGeneralExpositor> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdExpositor,
                                   Posicion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdModalidadCurso
                            FROM pla.T_PGeneralExpositor
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PGeneralExpositor>>(respuestaDapper);
                }
                return new List<PGeneralExpositor>();
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
        /// <returns> PGeneralExpositor </returns>
        public PGeneralExpositor? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdExpositor,
                                   Posicion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralExpositor
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PGeneralExpositor>(respuestaDapper)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 11/08/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="idPgeneral"> (PK) IdPGeneral</param>
        /// <param name="idExpositor"></param>
        /// <returns> PGeneralExpositor </returns>
        public PGeneralExpositor? ObtenerPorIdPgeneralIdExpositor(int idPgeneral, int idExpositor)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdExpositor,
                                   Posicion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdModalidadCurso 
                            FROM pla.T_PGeneralExpositor
                            WHERE Estado = 1
                                  AND IdPGeneral = @idPgeneral AND IdExpositor=@idExpositor AND IdModalidadCurso IS NULL;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPgeneral, idExpositor });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PGeneralExpositor>(respuestaDapper)!;
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Jeremy Pacheco Garcia
        /// Fecha: 04/08/2025
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="idPgeneral"> (PK) IdPGeneral</param>
        /// <param name="idExpositor"></param>
        /// /// <param name="idModalidadCurso"></param>
        /// <returns> PGeneralExpositor </returns>
        public PGeneralExpositor? ObtenerPorIdPgeneralIdExpositorModalidad(int idPgeneral, int idExpositor, int idModalidadCurso)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdExpositor,
                                   Posicion,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion,
                                   IdModalidadCurso
                            FROM pla.T_PGeneralExpositor
                            WHERE Estado = 1
                                  AND IdPGeneral = @idPgeneral AND IdExpositor=@idExpositor AND IdModalidadCurso = @idModalidadCurso;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPgeneral, idExpositor, idModalidadCurso });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PGeneralExpositor>(respuestaDapper)!;
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
