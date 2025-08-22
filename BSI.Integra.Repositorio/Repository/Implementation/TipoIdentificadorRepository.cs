using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoIdentificadorRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de T_TipoIdentificador
    /// </summary>
    public class TipoIdentificadorRepository : GenericRepository<TCodigoCiiuIndustrium>, ITipoIdentificadorRepository
    {
        private Mapper _mapper;

        public TipoIdentificadorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCodigoCiiuIndustrium, TipoIdentificador>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TipoIdentificadorComboDTO> </returns>
        public IEnumerable<TipoIdentificadorComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<TipoIdentificadorComboDTO>();
                var query = @"SELECT Id,Nombre,IdPais FROM fin.T_TipoIdentificador WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoIdentificadorComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
