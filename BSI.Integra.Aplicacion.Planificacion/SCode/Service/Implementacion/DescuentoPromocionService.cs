using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class DescuentoPromocionService : IDescuentoPromocionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DescuentoPromocionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TDescuentoPromocion, DescuentoPromocion>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TDescuentoPromocion, DescuentoPromocionDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<DescuentoPromocion, DescuentoPromocionDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Klebert Layme
        /// Fecha: 25/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCapacitacion
        /// </summary>
        /// <returns> List<AreaCapacitacionDTO> </returns>
        public IEnumerable<DescuentoPromocionDTO> Obtener()
        {
            try
            {
                return _unitOfWork.DescuentoPromocionRepository.Obtener();
            }
            catch
            {
                throw;
            }
        }
    }
}
