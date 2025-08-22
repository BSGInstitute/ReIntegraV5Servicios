using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OrigenService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Origen
    /// </summary>
    public class OrigenService : IOrigenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OrigenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOrigen, Origen>(MemberList.None).ReverseMap();
                cfg.CreateMap<OrigenDTO, Origen>(MemberList.None).ReverseMap();
            }
         );
          
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Origen Add(OrigenDTO entidad,string Usuario)
        {
            try
            {

                Origen data = _mapper.Map<Origen>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;

                var modelo = _unitOfWork.OrigenRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<Origen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Origen Update(OrigenDTO entidad,string Usuario)
        {


            try
            {
                var rep = _unitOfWork.OrigenRepository;
                var entidadActual = _mapper.Map<Origen>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Nombre = entidad.Nombre;
                entidadActual.Descripcion = entidad.Descripcion;
                entidadActual.Prioridad= entidad.Prioridad;
                var modelo = _unitOfWork.OrigenRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<Origen>(modelo);
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
                _unitOfWork.OrigenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Origen> Add(List<Origen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Origen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Origen> Update(List<Origen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OrigenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Origen>>(modelo);
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
                _unitOfWork.OrigenRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_Origen para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_Origen
        /// </summary>
        /// <returns> List<OrigenDTO> </returns>
        public IEnumerable<OrigenDTO> ObtenerOrigen()
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Origen
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> List<TarifarioDetalleAgendaDTO> </returns>
        public List<TarifarioDetalleAgendaDTO> ObtenerTarifariosDetallesAgenda(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerTarifariosDetallesAgenda(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Origen
        /// </summary>
        /// <param name="idMatriculaCabecera">Id Matricula Cabecera</param>
        /// <returns> List<TarifarioDetalleAgendaDTO> </returns>
        public List<VersionprogramaDTO> obtenerversionAlumno(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.OrigenRepository.obtenerversionAlumno(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id de categoria origen por idOrigen
        /// </summary>
        /// <param name="idOrigen">Id del origen</param>
        /// <returns>Objeto de clase OrigenIdCategoriaOrigenDTO</returns>
        public OrigenIdCategoriaOrigenDTO IdCategoriaOrigenPorOrigen(int idOrigen)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerIdCategoriaOrigenPorOrigen(idOrigen);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna una lista de los Origenes para ser usados en los filtros  "RegistrarOportunidad"
        /// </summary>
        /// <param name="idOrigen">Id del origen</param>
        /// <returns>Objeto de clase OrigenIdCategoriaOrigenDTO</returns>
        public List<ComboFiltroDTO> ObtenerOrigeneParaRegistrarOportunidad(string Area)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerOrigeneParaRegistrarOportunidad(Area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Origenes Por CategoriaOrigen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerOrigenPorCategoriaOrigen(int idCategoriaOrigenInbox, int idCategoriaOrigenCorreo, int idCategoriaOrigenComentarios)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerOrigenPorCategoriaOrigen(idCategoriaOrigenInbox, idCategoriaOrigenCorreo, idCategoriaOrigenComentarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 17/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id, nombre de un origen filtrado por nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public List<ComboFiltroDTO> ObtenerOrigenChat(string nombre)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerOrigenChat(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 03/11/2022
        /// Version: 1.0
        /// <summary>T
        /// Obtiene ComboDTO
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerTodoFiltro()
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerTodoFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de la T_Tarifario
        /// </summary>
        /// <returns></returns>
        /// <exception>List<TarifarioDTO></exception>
        public List<TarifarioDTO> ObtenerTarifarios()
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerTarifarios();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tarifario de tallado en lista por idTarifario
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <returns></returns>
        /// <exception>List<TarifarioDetalleConfiguracionDTO></exception>
        public List<TarifarioDetalleConfiguracionDTO> ObtenerTarifariosDetalles(int idTarifario)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerTarifariosDetalles(idTarifario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 08/11/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un Tarifario
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        /// <exception">List<TarifarioDTO></exception>
        public List<TarifarioDTO> InsertarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                return _unitOfWork.OrigenRepository.InsertarTarifario(objeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Tarifario mediante TarifarioNuevoDTO objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<TarifarioDTO> ActualizarTarifario(TarifarioNuevoDTO objeto)
        {
            try
            {
                return _unitOfWork.OrigenRepository.ActualizarTarifario(objeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 09/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario identificado por idTarifario, usuario
        /// </summary>
        /// <param name="idTarifario"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public List<TarifarioDTO> EliminarTarifario(int idTarifario, string usuario)
        {
            try
            {
                return _unitOfWork.OrigenRepository.EliminarTarifario(idTarifario, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina el Tarifario País según Id y Usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<TarifarioDetalleDTO> EliminarTarifarioDetallePais(int id, string usuario)
        {
            try
            {
                return _unitOfWork.OrigenRepository.EliminarTarifarioDetallePais(id, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 23/11/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina Tarifario Detalle por concepto y usuario
        /// </summary>
        /// <param name="concepto"></param>
        /// <param name="usuario"></param>
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<TarifarioDetalleDTO> EliminarTarifarioDetalle(string concepto, string usuario)
        {
            try
            {
                return _unitOfWork.OrigenRepository.EliminarTarifarioDetalle(concepto, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de Origen Filtro de reclamo
        /// </summary> 
        /// <returns> List<TarifarioDetalleDTO> </returns>
        public List<ComboFiltroDTO> ObtenerCombosOrigen()
        {
            try
            {
                return _unitOfWork.OrigenRepository.ObtenerCombosOrigen();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
