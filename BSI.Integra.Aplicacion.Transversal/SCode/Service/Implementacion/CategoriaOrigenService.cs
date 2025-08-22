using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CategoriaOrigenService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_CategoriaOrigen
    /// </summary>
    public class CategoriaOrigenService : ICategoriaOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CategoriaOrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaOrigen, CategoriaOrigen>(MemberList.None).ReverseMap();
                cfg.CreateMap<CategoriasOrigenDTO, CategoriaOrigen>(MemberList.None).ReverseMap();
            }
            );

        
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CategoriaOrigen Add(CategoriasOrigenDTO entidad,string Usuario)
        {
            try
            {

                CategoriaOrigen data = _mapper.Map<CategoriaOrigen>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;
                var modelo = _unitOfWork.CategoriaOrigenRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<CategoriaOrigen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CategoriaOrigen Update(CategoriasOrigenDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.CategoriaOrigenRepository;
                var entidadActual = _mapper.Map<CategoriaOrigen>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.IdTipoDato = entidad.IdTipoDato;
                entidadActual.IdTipoCategoriaOrigen = entidad.IdTipoCategoriaOrigen;
                entidadActual.Meta = entidad.Meta;
                entidadActual.IdProveedorCampaniaIntegra = entidad.IdProveedorCampaniaIntegra;
                entidadActual.IdFormularioProcedencia = entidad.IdFormularioProcedencia;
                entidadActual.Considerar = entidad.Considerar;
                entidadActual.CodigoOrigen = entidad.CodigoOrigen;
                
                var modelo = _unitOfWork.CategoriaOrigenRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<CategoriaOrigen>(modelo);
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
                _unitOfWork.CategoriaOrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaOrigen> Add(List<CategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaOrigenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CategoriaOrigen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaOrigen> Update(List<CategoriaOrigen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CategoriaOrigenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CategoriaOrigen>>(modelo);
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
                _unitOfWork.CategoriaOrigenRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CategoriaOrigen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>
        public IEnumerable<CategoriaOrigenDTO> ObtenerCategoriaOrigen()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Meiss Ramirez Neyra
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoInteracccion
        /// </summary>
        /// <returns> List<TipoInteraccionFiltroDT> </returns>
        public IEnumerable<TipoInteraccionFiltroDTO> TiposInteraccionPorProcedenciaFiltro()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.TiposInteraccionPorProcedenciaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// Autor: Margiory Meiss Ramirez Neyra
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaOrigen
        /// </summary>
        /// <returns> List<TipoCategoriaOrigenFiltroDTO> </returns>

        public IEnumerable<TipoCategoriaOrigenFiltroDTO> TipoCategoriaOrigenFiltroTodo()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.TipoCategoriaOrigenFiltroTodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// Autor: Margiory Meiss Ramirez Neyra
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el filtro de CategoriaOrigenfiltroDTO
        /// </summary>
        /// <returns> List<ComboFiltroDTO> </returns>
        public IEnumerable<ComboFiltroDTO> ObtenerCateoriaOrigenFiltro()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCateoriaOrigenFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// Autor: Margiory Meiss Ramirez Neyra
        /// Fecha: 22/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el filtro de CategoriaOrigenfiltroDTO
        /// </summary>
        /// <returns> List<ComboFiltroDTO> </returns>
        public IEnumerable<ComboFiltroDTO> ObtenerFiltroCategoriaOrigen()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerFiltroCategoriaOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Remirez Neyra.
        /// Fecha: 04/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de CategoriaOrigen donde el nombre contenda la palabra remarketing
        /// </summary>
        /// <returns> List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> </returns>
        public List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ObtenerRemarketingCategoriaOrigen()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerRemarketingCategoriaOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la categoria origen subcategoria dato
        /// </summary>
        /// <param name="idCategoriaOrigen">Id de la categoria origen (PK de la tabla mkt.T_CategoriaOrigen)</param>
        /// <param name="idTipoFormulario">Id del tipo de formulario (PK de la tabla mkt.T_TipoFormulario)</param>
        /// <returns>Objeto de clase CategoriaOrigenSubCategoriaDatoDTO</returns>
        public CategoriaOrigenSubCategoriaDatoDTO CategoriaOrigenSubCategoriaDato(int idCategoriaOrigen, int idTipoFormulario)
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenSubCategoriaDato(idCategoriaOrigen, idTipoFormulario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 12/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerCategoriaOrigenPorNombre(string nombre)
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenPorNombre(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaOrigen
        /// </summary>
        /// <returns> List<CategoriaOrigenDTO> </returns>
        public IEnumerable<ComboFiltroDTO> ObtenerCategoriaFiltro()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos de T_CategoriaOrigen por el Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> CategoriaOrigenDTO </returns>
        public CategoriaOrigen ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoriaOrigenFiltroGrupoDTO> ObtenerCategoriaFiltroGrupo()
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaFiltroGrupo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<CategoriaOrigeCombonDTO> ObtenerCategoriaPorTipoCategoria(string TipoDato)
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaPorTipoCategoria(TipoDato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Richardson Mantilla Rodríguez.
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las credenciales del Asesor, para conectarse al Servicio Imap.
        /// </summary>
        /// <param name="id">Id deTipoCategoriaOrigen</param>
        /// <returns> int </returns>
        public int ObtenerTipoCategoriaOrigenPorId(int id)
        {
            try
            {
                return _unitOfWork.CategoriaOrigenRepository.ObtenerTipoCategoriaOrigenPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
    }
}
