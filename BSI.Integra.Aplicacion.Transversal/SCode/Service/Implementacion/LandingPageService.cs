using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: LandingPageService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>s
    /// Gestión general de T_LandingPage
    /// </summary>
    public class LandingPageService : ILandingPageService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public LandingPageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TLandingPage, LandingPage>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        public LandingPage Add(LandingPageEnvio data)
        {
            try
            {

                var servicio = new PlantillaV2SeccionService(_unitOfWork);
                var repLandingPage = _unitOfWork.LandingPageRepository;
                LandingPage entidad = new LandingPage();
                entidad.Nombre = data.Nombre;
                entidad.IdPEspecifico = data.IdPEspecifico;
                entidad.IdTipo = data.IdTipo;
                entidad.IdFormularioSolicitud = data.IdFormularioSolicitud;
                entidad.IdProgramaGeneral = data.IdProgramaGeneral;
                entidad.IdPlantilla = data.IdPlantilla;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.EstilosCongelados = JsonConvert.SerializeObject(servicio.ObtenerTodo(data.IdPlantilla));
                entidad.Cabecera = data.Cabecera;
                entidad.Titulo = data.Titulo;
                entidad.Subtitulo = data.Subtitulo;
                entidad.Url = "https://bsginstitute.com/LandingPage/";
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.LandingPageRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LandingPage>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LandingPage Update(LandingPageEnvio data)
        {
            try
            {
                var servicio = new PlantillaV2SeccionService(_unitOfWork);
                var repLandingPage = _unitOfWork.LandingPageRepository;
                LandingPage entidad = new LandingPage();
                entidad = _mapper.Map<LandingPage>(repLandingPage.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.IdPEspecifico = data.IdPEspecifico;
                entidad.IdTipo = data.IdTipo;
                entidad.IdFormularioSolicitud = data.IdFormularioSolicitud;
                entidad.IdProgramaGeneral = data.IdProgramaGeneral;
                entidad.IdPlantilla = data.IdPlantilla;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.EstilosCongelados = JsonConvert.SerializeObject(servicio.ObtenerTodo(data.IdPlantilla));
                entidad.Cabecera = data.Cabecera;
                entidad.Titulo = data.Titulo;
                entidad.Subtitulo = data.Subtitulo;
                entidad.Url = "https://bsginstitute.com/LandingPage/";
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.LandingPageRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<LandingPage>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        #region Metodos Base

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.LandingPageRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LandingPage> Add(List<LandingPage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LandingPageRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LandingPage>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LandingPage> Update(List<LandingPage> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.LandingPageRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<LandingPage>>(modelo);
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
                _unitOfWork.LandingPageRepository.Delete(listadoIds, usuario);
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
        /// Obtiene registros de T_LandingPage para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<LandingPageCombo> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.LandingPageRepository.ObtenerCombo();
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
        /// Obtiene todos los registros de T_LandingPage
        /// </summary>
        /// <returns> List<LandingPageDTO> </returns>
        public IEnumerable<LandingPage> ObtenerLandingPage()
        {
            try
            {
                return _unitOfWork.LandingPageRepository.ObtenerLandingPage();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<LandingPageC> ObtenerLandingPageC()
        {
            try
            {
                return _unitOfWork.LandingPageRepository.ObtenerLandingPageC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
