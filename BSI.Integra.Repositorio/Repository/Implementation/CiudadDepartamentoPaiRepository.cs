using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CiudadDepartamentoPaiRepository
    /// Autor: Jorge Gamero
    /// Fecha: 20/09/2024
    /// <summary>
    /// Gestión general de T_CiudadDepartamentoPais
    /// </summary>
    public class CiudadDepartamentoPaiRepository : GenericRepository<TCiudadDepartamentoPai>, ICiudadDepartamentoPaiRepository
    {
        private Mapper _mapper;

        public CiudadDepartamentoPaiRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCiudadDepartamentoPai, CiudadDepartamentoPai>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 20/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_CiudadDepartamentoPais por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> CiudadDepartamentoPai </returns>
        public IEnumerable<CiudadDepartamentoPai> ObtenerPorId(int idDepartamentoPais)
        {
            try
            {
                var rpta = new List<CiudadDepartamentoPai>();
                var query = @"SELECT * FROM conf.T_CiudadDepartamentoPais WHERE Estado = 1 AND IdDepartamentoPais = @IdDepartamentoPais";
                var resultado = _dapperRepository.QueryDapper(query, new { IdDepartamentoPais = idDepartamentoPais });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject< List<CiudadDepartamentoPai>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Gamero
        /// Fecha: 20/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM conf.T_CiudadDepartamentoPais WHERE Estado = 1 ORDER BY Nombre";
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
        /// Fecha: 25/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigo de ciudad por Id
        /// </summary> 
        /// <returns> CodigoCiudadDTO </returns>
        public CodigoCiudadDTO ObtenerCodigoPorId(int id)
        {
            try
            {
                var codigo = new CodigoCiudadDTO();
                var query = @"SELECT Codigo FROM conf.T_CiudadDepartamentoPais WHERE ID = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    codigo = JsonConvert.DeserializeObject<CodigoCiudadDTO>(resultado);
                }
                return codigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
