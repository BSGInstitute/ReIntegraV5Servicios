using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: LinkedinResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de testimonios de LinkedIn de BSG Institute.
    /// Módulo 100% manual (sin API externa). CRUD para carga desde modales,
    /// consultas de administración vía SP y gestión de visibilidad.
    /// </summary>
    public class LinkedinResenaService : ILinkedinResenaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        private const string USUARIO_FALLBACK = "SISTEMA";

        public LinkedinResenaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TLinkedinResena, LinkedinResena>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region CRUD

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un testimonio y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de testimonio a insertar.</param>
        /// <returns>LinkedinResena</returns>
        public LinkedinResena Add(LinkedinResena entidad)
        {
            var modelo = _unitOfWork.LinkedinResenaRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<LinkedinResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un testimonio y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de testimonio con los datos actualizados.</param>
        /// <returns>LinkedinResena</returns>
        public LinkedinResena Update(LinkedinResena entidad)
        {
            var modelo = _unitOfWork.LinkedinResenaRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<LinkedinResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un testimonio por su Id.</summary>
        /// <param name="id">Id del testimonio a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.LinkedinResenaRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de testimonios en bloque.</summary>
        /// <param name="listadoEntidad">Listado de testimonios a insertar.</param>
        /// <returns>List de LinkedinResena</returns>
        public List<LinkedinResena> Add(List<LinkedinResena> listadoEntidad)
        {
            var modelo = _unitOfWork.LinkedinResenaRepository.Add(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<LinkedinResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un listado de testimonios en bloque.</summary>
        /// <param name="listadoEntidad">Listado de testimonios con los datos actualizados.</param>
        /// <returns>List de LinkedinResena</returns>
        public List<LinkedinResena> Update(List<LinkedinResena> listadoEntidad)
        {
            var modelo = _unitOfWork.LinkedinResenaRepository.Update(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<LinkedinResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un listado de testimonios por sus Ids.</summary>
        /// <param name="listadoIds">Listado de Ids de los testimonios a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            _unitOfWork.LinkedinResenaRepository.Delete(listadoIds, usuario);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Consultas de Administración

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_LinkedinResenaObtenerDatos (modo Grilla).</summary>
        /// <param name="filtro">Filtros de la grilla (visibilidad, país, fechas, paginación).</param>
        /// <returns>LinkedinResenaGrillaPaginadaDTO</returns>
        public LinkedinResenaGrillaPaginadaDTO ObtenerGrilla(LinkedinResenaGrillaFiltroDTO filtro)
        {
            return _unitOfWork.LinkedinResenaRepository.ObtenerGrilla(filtro);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con testimonios activos para el combo de filtros.</summary>
        /// <returns>List de LinkedinResenaPaisComboDTO</returns>
        public List<LinkedinResenaPaisComboDTO> ObtenerPaisesCombo()
        {
            return _unitOfWork.LinkedinResenaRepository.ObtenerPaisesCombo();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>List de LinkedinResenaCiudadComboDTO</returns>
        public List<LinkedinResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais)
        {
            return _unitOfWork.LinkedinResenaRepository.ObtenerCiudadesCombo(idPais);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca los testimonios indicados como visibles (Mostrar=true).</summary>
        /// <param name="dto">Ids de testimonios y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaVisible(LinkedinResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.LinkedinResenaRepository.MarcarResenaVisible(
                dto.IdsLinkedinResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca los testimonios indicados como ocultos (Mostrar=false).</summary>
        /// <param name="dto">Ids de testimonios y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaOculta(LinkedinResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.LinkedinResenaRepository.MarcarResenaOculta(
                dto.IdsLinkedinResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        #endregion
    }
}
