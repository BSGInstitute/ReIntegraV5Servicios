using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    /// Service: CentralLlamadaDireccionService
    /// Autor: Victor Hinojosa
    /// Fecha: 25/09/2024
    /// <summary>
    /// Gestión de Direccion de Central de Llamadas
    /// </summary>
    public class CentralLlamadaDireccionService : ICentralLlamadaDireccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CentralLlamadaDireccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCentralLlamadaDireccion, CentralLlamadaDireccion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Victor Hinojosa
        /// Fecha: 25/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene 
        /// </summary>
        /// <returns>List<CentralLlamadaDireccionDTO></CentralLlamadaDireccionDTO></returns>
        public IEnumerable<CentralLlamadaDireccionDTO> Obtener()
        {
            try
            {
                return _unitOfWork.CentralLlamadaDireccionRepository.Obtener();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DominioPbxDTO> ObtenerComboDominioPbx()
        {
            try
            {
                return _unitOfWork.CentralLlamadaDireccionRepository.ObtenerComboDominioPbx();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
