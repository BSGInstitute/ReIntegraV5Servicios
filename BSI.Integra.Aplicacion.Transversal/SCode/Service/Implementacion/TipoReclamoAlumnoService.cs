using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoReclamoAlumnoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/12/2022
    /// <summary>
    /// Gestión general de T_TipoReclamoAlumno
    /// </summary>
    public class TipoReclamoAlumnoService : ITipoReclamoAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public TipoReclamoAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoReclamoAlumno, TipoReclamoAlumno>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todo el registro de Tipo Reclamo de Alumno
        /// </summary>
        /// <returns> List<ComboFiltroDTO> </returns> 
        public List<ComboFiltroDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoReclamoAlumnoRepository.ObtenerCombo().ToList();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
