using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReclamoService
    /// Autor: Gilmer Quispe.
    /// Fecha: 10/11/2022
    /// <summary>
    /// Gestión general de T_Reclamo
    /// </summary>
    public class ReclamoService : IReclamoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ReclamoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReclamo, Reclamo>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        public Reclamo Add(Reclamo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReclamoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Reclamo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene la lista de reclamo del alumno por el idmatricula
        /// </summary>
        /// <param name="idMatricula"> Codigo Matricula </param>
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> ListarReclamosAlumno(int idMatricula)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ListarReclamosAlumno(idMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> ListarReclamos()
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ListarReclamos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> ObtenerReclamosPorAlumno(int idMatricula)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ObtenerReclamosPorAlumno(idMatricula);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosAreasDTO> ListarReclamosAreas()
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ListarReclamosAreas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public ListarReclamosAreasDTO InsertarReclamosArea(ReclamoAreasDTO listadoBO)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.InsertarReclamosArea(listadoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> GenerarReporteReclamo(ReclamoFiltroDTO listadoBO)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.GenerarReporteReclamo(listadoBO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<registroTipoReclamoAlumnoDTO> ObtenerListaTipoReclamoAlumno()
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ObtenerListaTipoReclamoAlumno();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Quiñones
        /// Fecha: 30/01/2023
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public bool ConfirmarReclamo(int id, string usuario, string comentario)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ConfirmarReclamo(id, usuario, comentario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Quiñones
        /// Fecha: 30/01/2023
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public bool EnviarReclamo(int id, string usuario)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.EnviarReclamo(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 30/01/2023
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public bool EliminarReclamo(int id, string usuario)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.EliminarReclamo(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 27/04/2023
        /// <summary>
        /// Inserta la fecha de reprogamacion
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public bool ReclamoSinContacto(int id, string usuario)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ReclamoSinContacto(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 27/04/2023
        /// <summary>
        /// Inserta la fecha de reprogamacion
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public bool ResolverReclamoAreas(ReclamoSolucionDTO reclamo)
        {
            try
            {
                return _unitOfWork.ReclamoRepository.ResolverReclamoAreas(reclamo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de empresa.
        /// </summary>
        /// <param name="id">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        public Reclamo ObtenerPorId(int id)
        {

            try
            {
                return _unitOfWork.ReclamoRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Reclamo Update(Reclamo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReclamoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Reclamo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.ReclamoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    }
}
