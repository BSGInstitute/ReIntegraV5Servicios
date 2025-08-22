using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: CertificadoTipoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión general de T_CertificadoTipo
    /// </summary>
    public class CertificadoTipoRepository : GenericRepository<TCertificadoTipo>, ICertificadoTipoRepository
    {
        private Mapper _mapper;

        public CertificadoTipoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoTipo, CertificadoTipo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CertificadoTipo con Estado = 1.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<ComboDTO>();
                var query = @"SELECT Id,
                                   Nombre
                            FROM ope.T_CertificadoTipo
                            WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
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
