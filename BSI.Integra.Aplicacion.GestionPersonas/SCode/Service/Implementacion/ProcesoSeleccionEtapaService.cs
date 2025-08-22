using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: ProcesoSeleccionEtapaService
    /// Autor: Eliot Arias F.
    /// Fecha: 26/10/2024
    /// <summary>
    /// Gestión general de gp.T_ProcesoSeleccionEtapa
    /// </summary>
    public class ProcesoSeleccionEtapaService : IProcesoSeleccionEtapaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProcesoSeleccionEtapaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TProcesoSeleccionEtapa, ProcesoSeleccionEtapa>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TProveedorTipoServicio, ProcesoSeleccionEtapaDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ProcesoSeleccionEtapa, ProcesoSeleccionEtapaDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Etapas del Proceso de Seleccion para combo
        /// </summary>
        /// <returns> IEnumerable<ProcesosSeleccionEtapaComboDTO> </returns>
        public IEnumerable<ProcesosSeleccionEtapaComboDTO> ObtenerComboProcesoSeleccionEtapa()
        {
            try
            {
                return _unitOfWork.ProcesoSeleccionEtapaRepository.ObtenerComboProcesoSeleccionEtapa();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
