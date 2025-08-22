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
    /// Repositorio: TamanioEmpresaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de TamanioEmpresa
    /// </summary>
    public class TamanioEmpresaRepository : GenericRepository<TTamanioEmpresa>, ITamanioEmpresaRepository
    {
        private Mapper _mapper;

        public TamanioEmpresaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTamanioEmpresa, TamanioEmpresa>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TamanioEmpresaComboDTO> </returns>
        public IEnumerable<TamanioEmpresaComboDTO> ObtenerCombo()
        {
            try
            {
                var rpta = new List<TamanioEmpresaComboDTO>();
                var query = @"SELECT Id,Nombre FROM pla.T_TamanioEmpresa WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TamanioEmpresaComboDTO>>(resultado);
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
