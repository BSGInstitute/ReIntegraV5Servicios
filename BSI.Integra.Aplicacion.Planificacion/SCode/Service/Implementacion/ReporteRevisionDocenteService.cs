using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ReporteRevisionDocenteService: IReporteRevisionDocenteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;


        public ReporteRevisionDocenteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPartnerPw, PartnerPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public CombosReporteRevisionDocenteDTO ObtenerComboModulo()
        {
            var comboRevisionDocente = new CombosReporteRevisionDocenteDTO();
            comboRevisionDocente.AreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerCombo().OrderBy(x => x.Nombre).ToList();
            comboRevisionDocente.SubAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().OrderBy(x => x.Nombre).ToList();
            comboRevisionDocente.PGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramaSubAreaFiltro().OrderBy(x => x.Nombre).ToList();
            comboRevisionDocente.Proveedor = _unitOfWork.ProveedorRepository.ObtenerProveedorFiltro().ToList();

            return comboRevisionDocente;

        }
    }
}
