using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CategoriaAlumnoService
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_CategoriaAlumno
    /// </summary>
    public class CategoriaAlumnoService : ICategoriaAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CategoriaAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaAlumno, CategoriaAlumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Categoria Alumno
        /// </summary>
        /// <returns></returns>
        public List<CategoriaAlumnoDTO> ObtenerCategoriaAlumno()
        {
            try
            {
                return _unitOfWork.CategoriaAlumnoRepository.ObtenerCategoriaAlumno();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de pago por matriculaCabecera
        /// </summary>
        /// <param name="matriculaCabecera"></param>
        /// <returns></returns>
        public List<FechaPagoDTO> ObtenerFechaPago(int matriculaCabecera)
        {
            try
            {
                return _unitOfWork.CategoriaAlumnoRepository.ObtenerFechaPago(matriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
