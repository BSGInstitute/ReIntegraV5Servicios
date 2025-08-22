using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ParametroSeoPwRepository
    /// Autor Modificacion: Gilmer Qm.
    /// Fecha: 02/05/2023
    /// <summary>
    /// Gestión general de T_ParametroSeoPw
    /// </summary>
    public class ParametroSeoPwRepository : GenericRepository<TParametroSeoPw>, IParametroSeoPwRepository
    {
        private Mapper _mapper;
        public ParametroSeoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TParametroSeoPw, ParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de T_ParametroSeoPw
        /// </summary>
        /// <returns> List<ParametroSeoPwDTO> </returns>
        public IEnumerable<ParametroSeoPwDTO> ObtenerCombo()
        {
            try
            {
                IEnumerable<ParametroSeoPwDTO> rpta = new List<ParametroSeoPwDTO>();
                string _query = @"SELECT Id,Nombre,
                                           NumeroCaracteres
                                    FROM pla.T_ParametroSEO_PW
                                    WHERE Estado = 1
                                    ORDER BY Id DESC;";
                var queryRespuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ParametroSeoPwDTO>>(queryRespuesta)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<ParametroSeoPwDTO>> ObtenerComboAsync()
        {
            try
            {
                string _query = @"SELECT Id,Nombre,
                                           NumeroCaracteres
                                    FROM pla.T_ParametroSEO_PW
                                    WHERE Estado = 1
                                    ORDER BY Id DESC;";
                var queryRespuesta = await _dapperRepository.QueryDapperAsync(_query, null);

                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ParametroSeoPwDTO>>(queryRespuesta)!;
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
