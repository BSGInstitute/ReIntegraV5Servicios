using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProgramaGeneralArgumentoService
    {
        private IUnitOfWork _unitOfWork;
        public ProgramaGeneralArgumentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Obtener()
        {
            try
            {
                //    var respuesta = _unitOfWork.CourierRepository.ObtenerCourier();
                //    return _mapper.Map<List<CourierDTO>>(respuesta);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
