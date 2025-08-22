using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: CertificadoTipoProgramaRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión general de T_CertificadoTipoPrograma
    /// </summary>
    public class CertificadoTipoProgramaRepository : GenericRepository<TCertificadoTipoPrograma>, ICertificadoTipoProgramaRepository
    {
        private Mapper _mapper;

        public CertificadoTipoProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoTipoPrograma, CertificadoTipoPrograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CertificadoTipoPrograma con Estado = 1.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<ComboDTO>();
                var query = @"SELECT Id,
                                   NombreProgramaCertificado AS Nombre
                            FROM ope.T_CertificadoTipoPrograma
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
