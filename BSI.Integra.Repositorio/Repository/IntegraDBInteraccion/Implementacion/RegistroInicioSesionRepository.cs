using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Implementacion
{
    /// Repositorio: InteraccionIntegraRepository
    /// Autor: Max Mantilla R.
    /// Fecha: 03/06/2024
    /// <summary>
    /// Gestión de interacciones de integra
    /// </summary>

    public class RegistroInicioSesionRepository : GenericRepositoryInteraccion<TRegistroInicioSesion>, IRegistroInicioSesionRepository
    {
        private Mapper _mapper;
        public RegistroInicioSesionRepository(IntegraDBInteraccionContext context, IConnectionFactoryInteraccion connectionFactory, IDapperRepositoryInteraccion dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegistroInicioSesion, RegistroInicioSesion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los asesores que no sean coordinadores
        /// </summary>
        /// <returns> Lista de Objeto DTO : List<AsesorNombreFiltroDTO> </returns>
        public List<RegistroInicioSesionDTO> Obtener()
        {
            try
            {
                var asesores = new List<RegistroInicioSesionDTO>();
                var query = @"SELECT * FROM lgv.T_RegistroInicioSesion";
                var personalDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    asesores = JsonConvert.DeserializeObject<List<RegistroInicioSesionDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla R.
        /// Fecha: 28/05/2024
        /// Version: 1.0
        /// <summary>
        /// Registra el estado de la interacción de inicio de sesión de integra
        /// </summary>
        /// <returns> int </returns>
        public int RegistrarInicioSesion(RegistroInicioSesionLogueoDTO Model)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[lgv].[SP_RegistroInicioSesion_Registrar]", new
                {
                    IdPersonal = Model.IdPersonal,
                    Usuario = Model.Usuario,
                    Clave = Model.Clave,
                    IpPublica = Model.IpPublica,
                    IpLocal = Model.IpLocal,
                    DireccionMAC = Model.DireccionMac,
                    UsuarioRegistro = Model.Usuario,
                });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null" && query != null)
                {
                    var Valor = JsonConvert.DeserializeObject<IdDTO>(query)!;
                    return Valor.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
