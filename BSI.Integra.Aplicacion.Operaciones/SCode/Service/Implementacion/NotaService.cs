using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: NotaService
    /// Autor: Gilmer Quipse
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_Nota
    /// </summary>
    public class NotaService : INotaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public NotaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TNotum, Notum>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene el listado de nota asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de Matricula Cabecera </param>
        /// <returns> List<NotaPresencialDTO> </returns> 
        public List<NotaPresencialDTO> ListadoNotaPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.NotaRepository.ListadoNotaPorMatriculaCabecera(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene el listado de promedio de notas asociados al IdMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"> Id de Matricula Cabecera </param>
        /// <returns> List<NotaPresencialPromedioDTO> </returns> 
        public List<NotaPresencialPromedioDTO> ListadoNotaPorMatriculaCabeceraPromedio(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.NotaRepository.ListadoNotaPorMatriculaCabeceraPromedio(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 30/01/2023
        /// <summary>
        /// Obtiene las notas de los cursos inscritos
        /// </summary>
        /// <param name="idMatriculaCabecera" name="IdPEspecifico"> Id de Matricula Cabecera, Id pespecifico del alumno</param>
        /// <returns> List<NotaPresencialPromedioDTO> </returns> 
        public List<NotaPresencialPromedioEspecificoDTO> ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(int IdMatriculaCabecera, int IdPEspecifico)
        {
            try
            {
                return _unitOfWork.NotaRepository.ListadoNotaPorMatriculaCabeceraPromedioIdEspecifico(IdMatriculaCabecera, IdPEspecifico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
