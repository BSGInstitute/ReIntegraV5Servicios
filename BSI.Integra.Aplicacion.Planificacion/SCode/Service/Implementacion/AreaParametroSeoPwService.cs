using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class AreaParametroSeoPwService : IAreaParametroSeoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaParametroSeoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TAreaParametroSeoPw, AreaParametroSeoPw>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TAreaParametroSeoPw, AreaParametroSeoPwDTO>(MemberList.None).ReverseMap();
                    cfg.CreateMap<AreaParametroSeoPw, AreaParametroSeoPwDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el combo de T_ParametroSeoPw
        /// </summary> 
        /// <returns> List<ParametroSeoPwComboDTO> </returns>
        public IEnumerable<AreaParametrosSeoPorIdAreaDTO> ObtenerAreaParametrosSeoPorIdArea(int IdTag)
        {
            try
            {
                return _unitOfWork.AreaParametroSeoPwRepository.ObtenerAreaParametrosSeoPorIdArea(IdTag).ToList();
            }
            catch
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id Area de Capacitacion
        /// </summary>
        /// <param name="idPGeneral">Id del area de Capacitacion</param>

        /// <returns> AreaCapacitacion </returns>
        public List<AreaParametroSeoPw> ObtenerPorId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AreaParametroSeoPwRepository.ObtenerPorId(id);
                if (respuesta != null)
                {
                    return _mapper.Map<List<AreaParametroSeoPw>>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id Area de Capacitacion
        /// </summary>
        /// <param name="idPGeneral">Id del area de Capacitacion</param>

        /// <returns> AreaCapacitacion </returns>
        public AreaParametroSeoPw ObtenertodoId(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AreaParametroSeoPwRepository.ObtenerPorId(id);
                if (respuesta != null)
                {
                    return _mapper.Map<AreaParametroSeoPw>(respuesta);
                }
                else
                {
                    throw new BadRequestException($"No existe la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EliminarPorIdAreaCapacitacion(string usuario, int idAreaCapacitacion, List<AreaParametrosSeoPorIdAreaDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.AreaParametroSeoPwRepository.ObtenerPorIdAreaCapacitacion(idAreaCapacitacion).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdParametroSeopw)));
                _unitOfWork.AreaParametroSeoPwRepository.Delete(listaBorrar.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
