using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: OportunidadComercialService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 26/07/2022
    /// <summary>
    /// Gestión general de T_OportunidadComercial
    /// </summary>
    public class OportunidadComercialService : IOportunidadComercialService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public OportunidadComercialService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
