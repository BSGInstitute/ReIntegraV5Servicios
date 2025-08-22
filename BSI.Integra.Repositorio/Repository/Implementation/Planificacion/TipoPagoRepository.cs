using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Repositorio: TipoPagoRepository
    /// Autor: Gilmer Qm
    /// Fecha: 08/06/2023
    /// <summary>
    /// Gestión general de T_TipoPago
    /// </summary>
    public class TipoPagoRepository : GenericRepository<TTipoPago>, ITipoPagoRepository
    {
        private Mapper _mapper;
        public TipoPagoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoPago, TipoPago>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm
        /// Fecha: 08/06/2023
        /// <summary>
        ///  Obtiene el combo
        /// </summary>
        /// <returns> IEnumerable<TipoPagoComboDTO> </returns>
        public async Task<IEnumerable<TipoPagoComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var _query = @"SELECT Id AS Id,
                                   Nombre AS Nombre,
                                   Cuotas AS Cuotas
                            FROM pla.T_TipoPago
                            WHERE Estado = 1;";
                var pgeneralDB = await _dapperRepository.QueryDapperAsync(_query, new { });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TipoPagoComboDTO>>(pgeneralDB);
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
