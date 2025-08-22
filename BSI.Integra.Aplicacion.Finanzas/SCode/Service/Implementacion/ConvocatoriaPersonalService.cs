using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ConvocatoriaPersonalService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ConvocatoriaPersonal
    /// </summary>
    public class ConvocatoriaPersonalService : IConvocatoriaPersonalService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConvocatoriaPersonalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConvocatoriaPersonal, ConvocatoriaPersonal>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConvocatoriaPersonalRecibidoDTO, ConvocatoriaPersonal>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public ConvocatoriaPersonalDTO Add(ConvocatoriaPersonalRecibidoDTO data,string Usuario)
        {
            try
            {
                ConvocatoriaPersonal entidad = _mapper.Map<ConvocatoriaPersonal>(data);
    
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.ConvocatoriaPersonalRepository.Add(entidad);
                _unitOfWork.Commit();
                var respuesta = _mapper.Map<ConvocatoriaPersonal>(modelo);
                var retornar = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerConvocatoriasRegistradaById(respuesta.Id);

                string? IdsNivelEstudio = null; string? IdsExperiencia = null; string? IdsIdioma = null;

                if (data.IdNivelEstudio != null && data.IdNivelEstudio.Count() > 0) IdsNivelEstudio = string.Join(", ", data.IdNivelEstudio);
                if (data.IdExperiencia != null && data.IdExperiencia.Count() > 0) IdsExperiencia = string.Join(", ", data.IdExperiencia);
                if (data.IdIdiomaInsert != null && data.IdIdiomaInsert.Count() > 0) IdsIdioma = JsonConvert.SerializeObject(data.IdIdiomaInsert);

                var insertDetalle = _unitOfWork.ConvocatoriaPersonalRepository.InsertarDetalleConvocatorias(retornar.Id, Usuario, IdsNivelEstudio, IdsExperiencia, IdsIdioma);

                retornar.IdIdioma = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsIdioma(retornar.Id);
                retornar.IdNivelEstudio = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsNivelEstudio(retornar.Id);
                retornar.IdExperiencia = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsExperiencia(retornar.Id);

                return retornar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConvocatoriaPersonalDTO Update(ConvocatoriaPersonalRecibidoDTO data, string Usuario)
        {
            try
            {
                var repositorioConvocatoriaPersonal = _unitOfWork.ConvocatoriaPersonalRepository;
                var entidadActual = _mapper.Map<ConvocatoriaPersonal>(repositorioConvocatoriaPersonal.FirstById(data.Id));
                var entidadNueva = _mapper.Map<ConvocatoriaPersonal>(data);
                entidadNueva.UsuarioCreacion = entidadActual.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = Usuario;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.FechaCreacion = entidadActual.FechaCreacion;
                entidadNueva.Estado = entidadActual.Estado;

                var modelo = _unitOfWork.ConvocatoriaPersonalRepository.Update(entidadNueva);
                _unitOfWork.Commit();
                var respuesta = _mapper.Map<ConvocatoriaPersonal>(modelo);
                var retornar = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerConvocatoriasRegistradaById(respuesta.Id);

                string? IdsNivelEstudio = null; string? IdsExperiencia = null; string? IdsIdioma = null;

                if (data.IdNivelEstudio != null && data.IdNivelEstudio.Count() > 0) IdsNivelEstudio = string.Join(", ", data.IdNivelEstudio);
                if (data.IdExperiencia != null && data.IdExperiencia.Count() > 0) IdsExperiencia = string.Join(", ", data.IdExperiencia);
                if (data.IdIdiomaInsert != null && data.IdIdiomaInsert.Count() > 0) IdsIdioma = JsonConvert.SerializeObject(data.IdIdiomaInsert);

                var insertDetalle = _unitOfWork.ConvocatoriaPersonalRepository.InsertarDetalleConvocatorias(retornar.Id, Usuario, IdsNivelEstudio, IdsExperiencia, IdsIdioma);

                retornar.IdIdioma = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsIdioma(retornar.Id);
                retornar.IdNivelEstudio = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsNivelEstudio(retornar.Id);
                retornar.IdExperiencia = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsExperiencia(retornar.Id);
                return retornar;
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
                _unitOfWork.ConvocatoriaPersonalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConvocatoriaPersonal> Add(List<ConvocatoriaPersonal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConvocatoriaPersonalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConvocatoriaPersonal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConvocatoriaPersonal> Update(List<ConvocatoriaPersonal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConvocatoriaPersonalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConvocatoriaPersonal>>(modelo);
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
                _unitOfWork.ConvocatoriaPersonalRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConvocatoriaPersona
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        public List<ConvocatoriaPersonalDTO> ObtenerConvocatoriasRegistradas()
        {
            try
            {
                var resultado = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerConvocatoriasRegistradas();

                //Parallel.ForEach(resultado, item =>
                //{
                //    item.IdIdioma = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsIdioma(item.Id);
                //    item.IdNivelEstudio = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsNivelEstudio(item.Id);
                //    item.IdExperiencia = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsExperiencia(item.Id);
                //});

                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleConvocatoriaDTO ObtenerDetalleConvocatorias(int idConvocatoria)
        {
            try
            {
                DetalleConvocatoriaDTO respuesta = new DetalleConvocatoriaDTO();

                respuesta.IdIdioma = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsIdioma(idConvocatoria);
                respuesta.IdNivelEstudio = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsNivelEstudio(idConvocatoria);
                respuesta.IdExperiencia = _unitOfWork.ConvocatoriaPersonalRepository.ObtenerIdsExperiencia(idConvocatoria);

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los demas combos 
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        public object ObtenerTodosCombosConvotoriaPersonal()
        {
            try
            {
                var ComboEstadoConvocatoria = _unitOfWork.EstadoConvocatoriaRepository.ObtenerCombo();
                var ComboModalidadTrabajo = _unitOfWork.ModalidadTrabajoRepository.ObtenerCombo();
                var ComboCategoriaAsignacion = _unitOfWork.CategoriaAsignacionRepository.ObtenerCombo();
                var ComboExperiencia = _unitOfWork.ExperienciaRepository.ObtenerCombo();
                var ComboNivelEstudio = _unitOfWork.NivelEstudioRepository.ObtenerCombo();
                var ComboIdioma = _unitOfWork.IdiomaRepository.ObtenerIdiomaNivelCombo();
                var ComboTipoContrato = _unitOfWork.TipoContratoRepository.ObtenerCombo();

                return new { 
                    ComboEstadoConvocatoria, 
                    ComboModalidadTrabajo , 
                    ComboCategoriaAsignacion, 
                    ComboExperiencia, 
                    ComboNivelEstudio, 
                    ComboIdioma,
                    ComboTipoContrato
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 26/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConvocatoriaPersonal para Combo Postulante
        /// </summary>
        /// <returns> IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> </returns>
        public IEnumerable<ConvocatoriaPersonalComboPostulanteDTO> ObtenerComboComvocatoriaPersonal()
        {
            try
            {
                return _unitOfWork.ConvocatoriaPersonalRepository.ObtenerComboComvocatoriaPersonal();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }

}
