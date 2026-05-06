using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: ComputrabajoResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de reseñas de empleador en Computrabajo.
    /// Módulo 100% manual (sin API externa). CRUD para carga desde modales,
    /// consultas de administración vía SP y gestión de visibilidad.
    /// Computrabajo NO tiene API pública — captura manual periódica (quincenal).
    /// </summary>
    public class ComputrabajoResenaService : IComputrabajoResenaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        private const string USUARIO_FALLBACK = "SISTEMA";

        public ComputrabajoResenaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TComputrabajoResena, ComputrabajoResena>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region CRUD

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de reseña a insertar.</param>
        /// <returns>ComputrabajoResena</returns>
        public ComputrabajoResena Add(ComputrabajoResena entidad)
        {
            var modelo = _unitOfWork.ComputrabajoResenaRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<ComputrabajoResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de reseña con los datos actualizados.</param>
        /// <returns>ComputrabajoResena</returns>
        public ComputrabajoResena Update(ComputrabajoResena entidad)
        {
            var modelo = _unitOfWork.ComputrabajoResenaRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<ComputrabajoResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.ComputrabajoResenaRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        /// <param name="listadoEntidad">Listado de reseñas a insertar.</param>
        /// <returns>List de ComputrabajoResena</returns>
        public List<ComputrabajoResena> Add(List<ComputrabajoResena> listadoEntidad)
        {
            var modelo = _unitOfWork.ComputrabajoResenaRepository.Add(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<ComputrabajoResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        /// <param name="listadoEntidad">Listado de reseñas con los datos actualizados.</param>
        /// <returns>List de ComputrabajoResena</returns>
        public List<ComputrabajoResena> Update(List<ComputrabajoResena> listadoEntidad)
        {
            var modelo = _unitOfWork.ComputrabajoResenaRepository.Update(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<ComputrabajoResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        /// <param name="listadoIds">Listado de Ids de las reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            _unitOfWork.ComputrabajoResenaRepository.Delete(listadoIds, usuario);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Consultas de Administración

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_ComputrabajoResenaObtenerDatos.</summary>
        /// <param name="filtro">Filtros de la grilla (visibilidad, país, tipo empleado, fechas, paginación).</param>
        /// <returns>ComputrabajoResenaGrillaPaginadaDTO</returns>
        public ComputrabajoResenaGrillaPaginadaDTO ObtenerGrilla(ComputrabajoResenaGrillaFiltroDTO filtro)
        {
            return _unitOfWork.ComputrabajoResenaRepository.ObtenerGrilla(filtro);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con reseñas activas para el combo de filtros.</summary>
        /// <returns>List de ComputrabajoResenaPaisComboDTO</returns>
        public List<ComputrabajoResenaPaisComboDTO> ObtenerPaisesCombo()
        {
            return _unitOfWork.ComputrabajoResenaRepository.ObtenerPaisesCombo();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>List de ComputrabajoResenaCiudadComboDTO</returns>
        public List<ComputrabajoResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais)
        {
            return _unitOfWork.ComputrabajoResenaRepository.ObtenerCiudadesCombo(idPais);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaVisible(ComputrabajoResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.ComputrabajoResenaRepository.MarcarResenaVisible(
                dto.IdsComputrabajoResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaOculta(ComputrabajoResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.ComputrabajoResenaRepository.MarcarResenaOculta(
                dto.IdsComputrabajoResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        #endregion
    }
}
