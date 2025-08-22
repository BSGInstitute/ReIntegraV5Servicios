using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Service: CertificadoTipoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 06/01/2023
    /// <summary>
    /// Gestión general de T_CertificadoTipo
    /// </summary>
    public class CertificadoTipoService : ICertificadoTipoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CertificadoTipoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoTipo, CertificadoTipo>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CertificadoTipo con Estado = 1.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CertificadoTipoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
