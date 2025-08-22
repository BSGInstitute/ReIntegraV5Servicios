using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: SubAreaParametroSeoPwService
    /// Autor: Gilmer Quispe.
    /// Fecha: 09/05/2023
    /// <summary>
    /// Gestión general de T_SubAreaParametroSeoPw
    /// </summary>
    public class SubAreaParametroSeoPwService : ISubAreaParametroSeoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubAreaParametroSeoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubAreaParametroSeoPw, SubAreaParametroSeoPw>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 09/05/2023
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros asociados a SubAreaCapacitacion
        /// </summary>
        /// <param name="idSubAreaCapacitacion"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminarPorIdSubAreaCapacitacion(int idSubAreaCapacitacion, string usuario, List<SubAreaParametroSeoPwDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.SubAreaParametroSeoPwRepository.ObtenerPorIdSubAreaCapacitacion(idSubAreaCapacitacion).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdParametroSeoPw)));
                if (listaBorrar.Count() > 0)
                {
                    _unitOfWork.SubAreaParametroSeoPwRepository.Delete(listaBorrar.Select(x => x.Id).ToList(), usuario);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
