using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaEgresoAprobadoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de T_CajaEgresoAprobado
    /// </summary>
    public class TipoIdentificadorService : ITipoIdentificadorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoIdentificadorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCajaEgresoAprobado, CajaEgresoAprobado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TipoIdentificadorComboDTO> </returns>
        public List<TipoIdentificadorComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoIdentificadorRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

