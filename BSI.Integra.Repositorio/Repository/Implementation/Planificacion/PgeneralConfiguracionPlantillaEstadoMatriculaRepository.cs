using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: PgeneralConfiguracionPlantillaEstadoMatriculaRepository
    /// Autor: Gilmer Qm
    /// Fecha: 17/07/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_PgeneralConfiguracionPlantillaEstadoMatricula
    /// </summary>
    public class PgeneralConfiguracionPlantillaEstadoMatriculaRepository : GenericRepository<TPgeneralConfiguracionPlantillaEstadoMatricula>, IPgeneralConfiguracionPlantillaEstadoMatriculaRepository
    {
        Mapper _mapper;
        public PgeneralConfiguracionPlantillaEstadoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralConfiguracionPlantillaEstadoMatricula, PgeneralConfiguracionPlantillaEstadoMatricula>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 17/07/2023
        /// Version: 1.0
        /// <param name="idPgeneralConfiguracionPlantillaDetalle"> (PK) de T_PgeneralConfiguracionPlantillaDetalle </param>
        /// <summary>
        /// Obtiene los registros asociados al IdPgeneralConfiguracionPlantillaDetalle
        /// </summary>
        public IEnumerable<PgeneralConfiguracionPlantillaEstadoMatricula> ObtenerPorIdPgeneralConfiguracionPlantillaDetalle(int idPgeneralConfiguracionPlantillaDetalle)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPgeneralConfiguracionPlantillaDetalle,
                                   IdEstadoMatricula,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PgeneralConfiguracionPlantillaEstadoMatricula
                            WHERE Estado = 1
                                  AND IdPgeneralConfiguracionPlantillaDetalle = @IdPgeneralConfiguracionPlantillaDetalle;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPgeneralConfiguracionPlantillaDetalle = idPgeneralConfiguracionPlantillaDetalle });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralConfiguracionPlantillaEstadoMatricula>>(resultado)!;
                return new List<PgeneralConfiguracionPlantillaEstadoMatricula>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
