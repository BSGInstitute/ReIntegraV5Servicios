using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoEnvioAutomaticoService
    /// Autor: Jonathan Caipo
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_TipoEnvioAutomatico
    /// </summary>
    public class TipoEnvioAutomaticoService : ITipoEnvioAutomaticoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoEnvioAutomaticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEnvioAutomatico, TipoEnvioAutomatico>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Tipos de Envio para ser usados en filtros
        /// </summary>
        /// <param></param>
        /// <returns> Lista </returns>
        public List<FiltroDTO> ObtenerTodoCombo()
        {
            try
            {
                return _unitOfWork.TipoEnvioAutomaticoRepository.ObtenerTodoCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
