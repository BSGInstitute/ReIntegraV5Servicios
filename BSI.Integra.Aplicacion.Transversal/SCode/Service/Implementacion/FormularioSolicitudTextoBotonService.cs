using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FormularioSolicitudTextoBotonService
    /// Autor: Margiory Ramirez Neyra .
    /// Fecha: 15/09/2022
    /// <summary>
    /// Gestión general de T_FormularioSolicitudTextoBoton
    /// </summary>
    public class FormularioSolicitudTextoBotonService : IFormularioSolicitudTextoBotonService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioSolicitudTextoBotonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioSolicitudTextoBoton  , FormularioSolicitudTextoBoton>(MemberList.None).ReverseMap();
                cfg.CreateMap<FormularioSolicitudTextoBotonDTO, FormularioSolicitudTextoBoton>(MemberList.None).ReverseMap();


            }
         );
       
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FormularioSolicitudTextoBoton Add(FormularioSolicitudTextoBotonDTO entidad, string Usuario)
        {


            try
            {

                FormularioSolicitudTextoBoton data = _mapper.Map<FormularioSolicitudTextoBoton>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.FormularioSolicitudTextoBotonRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<FormularioSolicitudTextoBoton>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormularioSolicitudTextoBoton Update(FormularioSolicitudTextoBotonDTO entidad,string Usuario)
        {
            try
            {

                var rep = _unitOfWork.FormularioSolicitudTextoBotonRepository;
                var entidadActual = _mapper.Map<FormularioSolicitudTextoBoton>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.TextoBoton = entidad.TextoBoton;
                entidadActual.PorDefecto = entidad.PorDefecto;
                entidadActual.Descripcion = entidad.Descripcion;
                var modelo = _unitOfWork.FormularioSolicitudTextoBotonRepository.Update(entidadActual);
                _unitOfWork.Commit();
                   return _mapper.Map<FormularioSolicitudTextoBoton>(modelo);
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
                _unitOfWork.FormularioSolicitudTextoBotonRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioSolicitudTextoBoton> Add(List<FormularioSolicitudTextoBoton> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioSolicitudTextoBotonRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioSolicitudTextoBoton>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioSolicitudTextoBoton> Update(List<FormularioSolicitudTextoBoton> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioSolicitudTextoBotonRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioSolicitudTextoBoton>>(modelo);
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
                _unitOfWork.FormularioSolicitudTextoBotonRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// Autor: Margiory Ramirez Neyra .
        /// Fecha: 15/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormularioSolicitudTextoBoto para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FormularioSolicitudTextoBotonRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_FormularioSolicitudTextoBoto
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<FormularioSolicitudTextoBotonDTO> ObtenerFormularioSolicitudTextoBoton()
        {
            try
            {
                return _unitOfWork.FormularioSolicitudTextoBotonRepository.ObtenerFormularioSolicitudTextoBoton();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 18/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los filtros de T_FormularioSolicitudTextoBoton
        /// </summary>
        /// <returns> List<FormularioSolicitudTextoBotonDTO> </returns>
        public IEnumerable<FormularioSolicitudTextoBotonFiltroDTO> ObtenerFiltroFormularioSolicitudTextoBoton()
        {
            try
            {
                return _unitOfWork.FormularioSolicitudTextoBotonRepository.ObtenerFiltroFormularioSolicitudTextoBoton();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
