using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: SuscripcionProgramaGeneralRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha:08/06/2023
    /// <summary>
    /// Gestión general de T_SuscripcionProgramaGeneral
    /// </summary>
    public class SuscripcionProgramaGeneralRepository : GenericRepository<TSuscripcionProgramaGeneral>, ISuscripcionProgramaGeneralRepository
    {
        private Mapper _mapper;
        public SuscripcionProgramaGeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSuscripcionProgramaGeneral, SuscripcionProgramaGeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor Modificacion: Gilmer Qm.
        /// Fecha: 08/06/2023
        /// <summary>
        ///  Obtiene el combo
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var _query = @"SELECT Id AS Id,
                               Titulo AS Nombre
                        FROM pla.T_SuscripcionProgramaGeneral
                        WHERE Estado = 1;";
                var respuestaDapper = await _dapperRepository.QueryDapperAsync(_query, null);
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(respuestaDapper);
                }

                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista de T_SuscripcionProgramaGeneral asociados al PGeneral
        /// </summary>
        /// <param name="idPgeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<SuscripcionProgramaGeneralDTO> </returns>
        public IEnumerable<SuscripcionProgramaGeneralDTO> ObtenerSuscripcionProgramaGeneralPorIdPGeneral(int idPgeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                       IdPGeneral AS IdPgeneral,
                                       Titulo,
                                       Descripcion,
                                       OrdenBeneficio
                                FROM pla.V_SuscripcionProgramaGeneral
                                WHERE Estado = 1
                                      AND IdPGeneral = @idPgeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPGeneral = idPgeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SuscripcionProgramaGeneralDTO>>(respuestaDapper)!;
                }
                return new List<SuscripcionProgramaGeneralDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de suscripciones (activo) con Id, Nombre  registradas en el sistema 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerComboPorIdPgeneral(int idPGeneral)
        {
            try
            {
                List<ComboDTO> suscripciones = new List<ComboDTO>();
                var query = "SELECT Id,Nombre FROM pla.V_Suscripciones_Filtro WHERE Estado = 1 and IdPGeneral = @idPGeneral";
                var respuestaDapper = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(respuestaDapper)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#SPFR-OSPPN-001@Error en ObtenerSuscripcionesPorProgramaNombre() {ex.Message}", ex);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<SuscripcionProgramaGeneral> </returns>
        public IEnumerable<SuscripcionProgramaGeneral> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral,
                                   Titulo,
                                   Descripcion,
                                   OrdenBeneficio,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_SuscripcionProgramaGeneral
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<SuscripcionProgramaGeneral>>(respuestaDapper);
                }
                return new List<SuscripcionProgramaGeneral>();
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
        /// <returns> SuscripcionProgramaGeneral </returns>
        public SuscripcionProgramaGeneral? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   IdPGeneral,
                                   Titulo,
                                   Descripcion,
                                   OrdenBeneficio,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_SuscripcionProgramaGeneral
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<SuscripcionProgramaGeneral>(respuestaDapper)!;
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
