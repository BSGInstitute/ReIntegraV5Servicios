using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using PdfSharp.Pdf.Filters;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConjuntoAnuncioService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConjuntoAnuncio
    /// </summary>
    public class ConjuntoAnuncioService : IConjuntoAnuncioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConjuntoAnuncioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConjuntoAnuncio, ConjuntoAnuncio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConjuntoAnuncio Add(ConjuntoAnuncioEnvioDTO data)
        {
            try
            {
                var repConjuntoAnuncio = _unitOfWork.ConjuntoAnuncioRepository;
                ConjuntoAnuncio entidad = new ConjuntoAnuncio();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.IdConjuntoAnuncioFacebook = data.IdConjuntoAnuncio_Facebook;
                entidad.FechaCreacionCampania = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;


                var modelo = _unitOfWork.ConjuntoAnuncioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConjuntoAnuncio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConjuntoAnuncio Update(ConjuntoAnuncioEnvioDTO data)
        {
            try
            {
                var repConjuntoAnuncio = _unitOfWork.ConjuntoAnuncioRepository;
                ConjuntoAnuncio entidad = new ConjuntoAnuncio();
                entidad.Id = data.Id;
                entidad.Nombre = data.Nombre;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.IdConjuntoAnuncioFacebook = data.IdConjuntoAnuncio_Facebook;
                entidad.FechaCreacionCampania = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.ConjuntoAnuncioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConjuntoAnuncio>(modelo);
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
                _unitOfWork.ConjuntoAnuncioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConjuntoAnuncio> Add(List<ConjuntoAnuncio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoAnuncioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConjuntoAnuncio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConjuntoAnuncio> Update(List<ConjuntoAnuncio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoAnuncioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConjuntoAnuncio>>(modelo);
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
                _unitOfWork.ConjuntoAnuncioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConjuntoAnuncio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConjuntoAnuncioRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: MargORY.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConjuntoAnuncio
        /// </summary>
        /// <returns> List<ConjuntoAnuncioDTO> </returns>
        public IEnumerable<ConjuntoAnuncioPanelDTO> ObtenerConjuntoAnuncio()
        {
            try
            {
                return _unitOfWork.ConjuntoAnuncioRepository.ObtenerConjuntoAnuncio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConjuntoAnuncioPanelDTO> ListarConjuntoAnuncios(FiltroPaginadorDTO filtro)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoAnuncioRepository.ListarConjuntoAnuncios(filtro);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConjuntoAnuncioPanelDTO>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<CoonjuntoAnuncioUrl> ObtenerConjuntoAnuncioUrl(int IdProgramaGeneral)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoAnuncioRepository.ObtenerConjuntoAnuncioUrl(IdProgramaGeneral);
                _unitOfWork.Commit();
                return _mapper.Map<List<CoonjuntoAnuncioUrl>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
