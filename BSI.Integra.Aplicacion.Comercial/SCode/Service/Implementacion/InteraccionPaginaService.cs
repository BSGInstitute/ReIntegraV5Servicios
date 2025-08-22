using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: InteraccionPaginaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 27/12/2022
    /// <summary>
    /// Gestión general de T_InteraccionPagina
    /// </summary>
    public class InteraccionPaginaService : IInteraccionPaginaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public InteraccionPaginaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInteraccionPagina, InteraccionPagina>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obteniene todas las InteraccionPaginaes del Alumno
        /// </summary>
        /// <param name="idAlumno"> Id del alumno </param>
        /// <returns> List<InteraccionPaginaAlumnoDTO> </returns>
        public List<InteraccionAlumnoDTO> ObtenerInteraccionesPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.InteraccionPaginaRepository.ObtenerInteraccionesPorAlumno(idAlumno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
