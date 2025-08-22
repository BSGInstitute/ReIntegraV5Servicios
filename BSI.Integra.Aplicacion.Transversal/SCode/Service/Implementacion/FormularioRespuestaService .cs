using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FormularioRespuestaService
    /// Autor: Margiory ramirez Neyra .
    /// Fecha: 13/09/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FormularioRespuestaService : IFormularioRespuestaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioRespuestaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioRespuestum, FormularioRespuesta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FormularioRespuesta Add(FormularioRespuestaDatoDTO data)
        {
            try
            {
                FormularioRespuesta entidad = new FormularioRespuesta();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.Codigo = data.Codigo;
                entidad.IdPgeneral = data.IdPgeneral;
                entidad.ProgramaGeneral = data.ProgramaGeneral;
                entidad.UsuarioCreacion = data.UsuarioModificacion;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                var modelo = _unitOfWork.FormularioRespuestaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormularioRespuesta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormularioRespuesta Update(FormularioRespuestaDatoDTO data)
        {
            try

            {
                var repFormulario = _unitOfWork.FormularioRespuestaRepository;
                FormularioRespuesta entidad = new FormularioRespuesta();
                entidad = _mapper.Map<FormularioRespuesta>(repFormulario.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Codigo = data.Codigo;
                entidad.IdPgeneral = data.IdPgeneral;
                entidad.ProgramaGeneral = data.ProgramaGeneral;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.FechaModificacion = DateTime.Now;



                var modelo = repFormulario.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormularioRespuesta>(modelo);
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
                _unitOfWork.FormularioRespuestaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioRespuesta> Add(List<FormularioRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioRespuestaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioRespuesta> Update(List<FormularioRespuesta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioRespuestaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioRespuesta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.TipoDatoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Formulariorespuesta para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FormularioRespuestaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de  T_Formulariorespuesta 
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<FormularioRespuestaDTO> ObtenerFormularioRespuesta()
        {
            try
            {
                return _unitOfWork.FormularioRespuestaRepository.ObtenerFormularioRespuesta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de  T_Formulariorespuesta 
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>

        public IEnumerable<FormularioRespuestaFiltroDTO> ObtenerFiltroFormularioRespuestum()
        {

            try
            {
                return _unitOfWork.FormularioRespuestaRepository.ObtenerFiltroFormularioRespuestum();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de  T_PGeneral para combo Dato.
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>


        public IEnumerable<ProgramaGeneralDatoDTO> ObtenerComboDato()
        {

            try
            {
                return _unitOfWork.FormularioRespuestaRepository.ObtenerComboDato();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
