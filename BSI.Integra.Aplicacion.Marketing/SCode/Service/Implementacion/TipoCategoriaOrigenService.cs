using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: TipoCategoriaOrigenService
    /// Autor: Margiory Meiss Ramirez Neyra.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoCategoriaOrigen
    /// </summary>
    public class TipoCategoriaOrigenService : ITipoCategoriaOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TipoCategoriaOrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCategoriaOrigen, TipoCategoriaOrigen>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoCategoriaOrigenDTO, TipoCategoriaOrigen>(MemberList.None).ReverseMap();
            }
           );


            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public TipoCategoriaOrigen Add(TipoCategoriaOrigenDTO entidad, string Usuario)
        {
            try
            {
                TipoCategoriaOrigen data = _mapper.Map<TipoCategoriaOrigen>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.TipoCategoriaOrigenRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCategoriaOrigen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoCategoriaOrigen Update(TipoCategoriaOrigenDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.TipoCategoriaOrigenRepository;
                var entidadActual = _mapper.Map<TipoCategoriaOrigen>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.Meta = entidad.Meta;
                entidadActual.OportunidadMaxima = entidad.OportunidadMaxima;

                var modelo = _unitOfWork.TipoCategoriaOrigenRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<TipoCategoriaOrigen>(modelo);
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
                _unitOfWork.TipoCategoriaOrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCategoriaOrigen> Add(List<TipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCategoriaOrigenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCategoriaOrigen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoCategoriaOrigen> Update(List<TipoCategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.TipoCategoriaOrigenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<TipoCategoriaOrigen>>(modelo);
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
                _unitOfWork.TipoCategoriaOrigenRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCategoriaOrigen
        /// </summary>
        /// <returns> List<TipoCategoriaOrigenDTO> </returns>
        public IEnumerable<TipoCategoriaOrigenDTO> ObtenerTipoCategoriaOrigen()
        {
            try
            {
                return _unitOfWork.TipoCategoriaOrigenRepository.ObtenerTipoCategoriaOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoCategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TipoCategoriaOrigenRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 08/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Filtros  Tipo Categoria Origen 
        /// </summary>
        /// <param name="idAsesor">Id del Asesor</param>
        /// <returns> List<TipoCategoriaOrigenFiltroDTO> </returns>

        public IEnumerable<TipoCategoriaOrigenFiltroDTO> ObtenerFiltroTipoCategoriaOrigen()
        {
            try
            {
                return _unitOfWork.TipoCategoriaOrigenRepository.ObtenerFiltroTipoCategoriaOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Meiss Ramirez Neyra.
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Filtros  Tipo Categoria Origen 
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>

        public IEnumerable<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.TipoCategoriaOrigenRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


