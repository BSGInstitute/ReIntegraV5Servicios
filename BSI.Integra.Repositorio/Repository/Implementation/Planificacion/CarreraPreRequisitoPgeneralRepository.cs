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
    /// Autor: Gretel Canasa.
    /// Fecha: 11/09/2023
    /// <summary>
    /// Gestión general de T_PGeneralExpositor
    /// </summary>
    public class CarreraPreRequisitoPgeneralRepository : GenericRepository<TCarreraPreRequisitoPgeneral>, ICarreraPreRequisitoPgeneralRepository
    {
        private Mapper _mapper;

        public CarreraPreRequisitoPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarreraPreRequisitoPgeneral, TCarreraPreRequisitoPgeneral>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gretel Canasa.
        /// Fecha: 11/09/2023
        /// <summary>
        ///  Obtiene la lista Descripción web General para un programa general registradas en el sistema(activos)
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral</param>
        /// <returns> IEnumerable<CarreraPreRequisitoPgeneralDTO> </returns>
        public IEnumerable<CarreraPreRequisitoPgeneralDTO> ObtenerCarreraPreRequisitoPGeneralPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdPGeneral_Prerequisito AS IdPgeneralPrerequisito
                             FROM pla.T_CarreraPreRequisitoPGeneral 
                             WHERE Estado = 1
                             AND IdPGeneral = @IdPGeneral;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CarreraPreRequisitoPgeneralDTO>>(resultado)!;
                }
                return new List<CarreraPreRequisitoPgeneralDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gretel Canasa.
        /// Fecha: 11/09/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<CarreraPreRequisitoPgeneral> </returns>
        public IEnumerable<CarreraPreRequisitoPgeneral> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                               IdPGeneral AS IdPgeneral,
                               IdPGeneral_Prerequisito AS IdPgeneralPrerequisito,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion,
                               IdMigracion  
                        FROM pla.T_CarreraPreRequisitoPGeneral 
                        WHERE Estado = 1
                        AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CarreraPreRequisitoPgeneral>>(respuestaDapper);
                }
                return new List<CarreraPreRequisitoPgeneral>();
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
        //        public PGeneralExpositor? ObtenerPorId(int id)
        //        {
        //            try
        //            {
        //                var _query = @"SELECT Id,
        //                                   IdPGeneral AS IdPgeneral,
        //                                   IdExpositor,
        //                                   Posicion,
        //                                   Estado,
        //                                   UsuarioCreacion,
        //                                   UsuarioModificacion,
        //                                   FechaCreacion,
        //                                   FechaModificacion,
        //                                   RowVersion,
        //                                   IdMigracion
        //                            FROM pla.T_PGeneralExpositor
        //                            WHERE Estado = 1
        //                                  AND Id = @Id;";
        //                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
        //                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
        //                {
        //                    return JsonConvert.DeserializeObject<PGeneralExpositor>(respuestaDapper)!;
        //                }
        //                return null;
        //            }
        //            catch (Exception Ex)
        //            {
        //                throw new Exception(Ex.Message);
        //            }
        //        }
        /// Autor: Gretel Canasa
        /// Fecha: 11/09/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name = "idPgeneral" > (PK)IdPGeneral </ param >
        /// < param name= "idPgeneralPrerequisito" ></ param >
        /// < returns > CarreraPreRequisitoPgeneral </ returns >
        public CarreraPreRequisitoPgeneral? ObtenerPorIdPgeneralIdPgeneralPrerequisito(int idPgeneral, int idPgeneralPrerequisito)
        {
            try
            {
                var _query = @"
                            SELECT Id,
                                   IdPGeneral AS IdPgeneral,
                                   IdPGeneral_Prerequisito AS IdPgeneralPrerequisito,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion  
                            FROM pla.T_CarreraPreRequisitoPGeneral 
                            WHERE Estado = 1
                            AND IdPGeneral = @idPgeneral AND IdPgeneralPrerequisito=@idPgeneralPrerequisito;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPgeneral, idPgeneralPrerequisito });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<CarreraPreRequisitoPgeneral>(respuestaDapper)!;
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
