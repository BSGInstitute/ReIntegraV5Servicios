using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
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
    /// Repositorio: FormaCalculoEvaluacionRepository
    /// Autor: Gilmer Qm.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_FormaCalculoEvaluacion
    /// </summary>
    public class FormaCalculoEvaluacionRepository : GenericRepository<TFormaCalculoEvaluacion>, IFormaCalculoEvaluacionRepository
    {
        private Mapper _mapper;

        public FormaCalculoEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormaCalculoEvaluacion, FormaCalculoEvaluacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormaCalculoEvaluacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTO = new List<ComboDTO>();

                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_FormaCalculoEvaluacion
                            WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTO = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormaCalculoEvaluacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var comboDTO = new List<ComboDTO>();

                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_FormaCalculoEvaluacion
                            WHERE Estado = 1;";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTO = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
