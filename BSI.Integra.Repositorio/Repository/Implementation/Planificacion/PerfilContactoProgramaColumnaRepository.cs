using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Repositorio: PerfilContactoProgramaColumnaRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 08/06/2023
    /// <summary>
    /// Gestión general de T_PerfilContactoProgramaColumna
    /// </summary>
    public class PerfilContactoProgramaColumnaRepository : GenericRepository<TPerfilContactoProgramaColumna>, IPerfilContactoProgramaColumnaRepository
    {
        private Mapper _mapper;
        public PerfilContactoProgramaColumnaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilContactoProgramaColumna, PerfilContactoProgramaColumna>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de T_PerfilContactoProgramaColumna
        /// </summary>
        /// <returns> List<PerfilContactoProgramaColumnaDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string _query = @"SELECT [Id],
                                       [Nombre]
                                FROM [pla].[T_PerfilContactoProgramaColumna]
                                WHERE Estado = 1;";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(queryRespuesta)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
