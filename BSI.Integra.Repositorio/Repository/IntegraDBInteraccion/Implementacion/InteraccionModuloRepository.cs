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
    /// Gestión de interacciones de integra
    /// </summary>
    public class InteraccionModuloRepository : GenericRepositoryInteraccion<TInteraccionModulo>, IInteraccionModuloRepository
    {
        private Mapper _mapper;
        public InteraccionModuloRepository(IntegraDBInteraccionContext context, IConnectionFactoryInteraccion connectionFactory, IDapperRepositoryInteraccion dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInteraccionModulo, InteraccionModulo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Max Mantilla R.
        /// Fecha: 28/05/2024
        /// Version: 1.0
        /// <summary>
        /// Registra el estado de la interacción de inicio de sesión de integra
        /// </summary>
        /// <returns> int </returns>
        public bool RegistrarInteraccionModulo(RegistroInteraccionModuloDTO Model)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[ibck].[SP_InteraccionModulo_Registrar]", new
                {
                    IdUsuario = Model.IdUsuario,
                    UrlAnterior = Model.UrlAnterior,
                    UrlActual = Model.UrlActual,
                    IpPublica = Model.IpPublica,
                    IpLocal = Model.IpLocal,
                    DireccionMAC = Model.DireccionMac,
                    ControlTipo = Model.ControlTipo,
                    ControlNombre = Model.ControlNombre,
                    Contenido = Model.Contenido,
                    NombreModulo = Model.NombreModulo,
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
