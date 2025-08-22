using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoEncuestumRepository
    /// Autor: Jorge Gamero
    /// Fecha: 26/13/2025
    /// <summary>
    /// Gestión general de T_TipoEncuesta
    /// </summary>
    public class TipoEncuestumRepository : GenericRepository<TTipoEncuestum>, ITipoEncuestumRepository
    {
        private Mapper _mapper;

        public TipoEncuestumRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEncuestum, TipoEncuestum>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla T_TipoEncuesta
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"	SELECT Id, Nombre FROM pla.T_TipoEncuesta WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 26/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la vista V_TModalidadCurso_Filtro
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerComboTipoModalidad()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"	SELECT Id, Nombre FROM pla.V_TModalidadCurso_Filtro WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
