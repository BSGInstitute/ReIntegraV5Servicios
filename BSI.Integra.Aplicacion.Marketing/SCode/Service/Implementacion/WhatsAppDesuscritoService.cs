using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppDesuscritoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/12/2022
    /// <summary>
    /// Gestión general de T_WhatsAppDesuscrito
    /// </summary>
    public class WhatsAppDesuscritoService : IWhatsAppDesuscritoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppDesuscritoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppDesuscrito, WhatsAppDesuscrito>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/12/2022
        /// Version: 1.0
        /// <summary>
        /// Evalua si existe un registro por el numero de telefono
        /// </summary>
        /// <param name="numero">Id de la Oportunidad</param>
        /// <returns> bool </returns>
        public bool ExistePorNumeroTelefono(string numero)
        {
            try
            {
                return _unitOfWork.WhatsAppDesuscritoRepository.ExistePorNumeroTelefono(numero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
