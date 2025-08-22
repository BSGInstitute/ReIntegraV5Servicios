using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Entidades.IntegraDBInteraccion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDBInteraccion;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.DapperRepository;
using BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.IntegraDBInteraccion.Implementacion
{
    /// Repositorio: InteraccionIntegraRepository
    /// Autor: Max Mantilla R.
    /// Fecha: 03/06/2024
    /// <summary>
    /// Gestión de estado de la interacción de inicio de sesión en integra
    /// </summary>

    public class RegistroInicioSesionEstadoRepository : GenericRepositoryInteraccion<TRegistroInicioSesionEstado>, IRegistroInicioSesionEstadoRepository
    {
        private Mapper _mapper;

        public RegistroInicioSesionEstadoRepository(IntegraDBInteraccionContext context, IConnectionFactoryInteraccion connectionFactory, IDapperRepositoryInteraccion dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegistroInicioSesionEstado, RegistroInicioSesionEstado>(MemberList.None).ReverseMap();
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
        public List<RegistroInicioSesionEstadoDTO> Obtener()
        {
            try
            {
                var asesores = new List<RegistroInicioSesionEstadoDTO>();
                var query = @"SELECT * FROM lgv.T_RegistroInicioSesionEstado";
                var personalDB = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(personalDB) && !personalDB.Contains("[]"))
                {
                    asesores = JsonConvert.DeserializeObject<List<RegistroInicioSesionEstadoDTO>>(personalDB);
                }
                return asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Max Mantilla R.
        /// Fecha: 03/06/2024
        /// Version: 1.0
        /// <summary>
        /// Registra el estado de la interacción de inicio de sesión en integra
        /// </summary>
        /// <returns> bool </returns>
        public bool RegistrarInicioSesionEstado(RegistroInicioSesionEstadoLogueoDTO Model)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[lgv].[SP_RegistroInicioSesionEstado_Registrar]", new
                {
                    IdRegistroInicioSesion = Model.IdRegistroInicioSesion,
                    TokenGenerada = Model.TokenGenerada,
                    InicioSesionCorrecta = Model.InicioSesionCorrecta,
                    Descripcion = Model.Descripcion,
                    Usuario = Model.Usuario,
                });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
