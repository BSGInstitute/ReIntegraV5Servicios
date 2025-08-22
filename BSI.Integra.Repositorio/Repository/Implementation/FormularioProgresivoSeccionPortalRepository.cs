using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FormularioProgresivoSeccionPortalRepository
    /// Autor: Jorge Gamero
    /// Fecha: 27/02/2025
    /// <summary>
    /// Gestión general de T_FormularioProgresivoSeccionPortal
    /// </summary>
    public class FormularioProgresivoSeccionPortalRepository : GenericRepository<TFormularioProgresivoSeccionPortal>, IFormularioProgresivoSeccionPortalRepository
    {
        private Mapper _mapper;

        public FormularioProgresivoSeccionPortalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioProgresivoSeccionPortal, FormularioProgresivoSeccionPortal>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 27/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<FormularioProgresivoSeccionPortal> ObtenerRegistros()
        {
            try
            {
                var rpta = new List<FormularioProgresivoSeccionPortal>();
                var query = @"SELECT Id, Nombre
                    FROM mkt.T_FormularioProgresivoSeccionPortal WHERE Estado = 1 ORDER BY Id";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioProgresivoSeccionPortal>>(resultado);
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
