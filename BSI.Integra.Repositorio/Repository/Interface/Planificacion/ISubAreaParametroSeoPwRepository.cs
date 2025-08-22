using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ISubAreaParametroSeoPwRepository : IGenericRepository<TSubAreaParametroSeoPw>
    {
        #region Metodos Base
        TSubAreaParametroSeoPw Add(SubAreaParametroSeoPw entidad);
        TSubAreaParametroSeoPw Update(SubAreaParametroSeoPw entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSubAreaParametroSeoPw> Add(IEnumerable<SubAreaParametroSeoPw> listadoEntidad);
        IEnumerable<TSubAreaParametroSeoPw> Update(IEnumerable<SubAreaParametroSeoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public bool ExistePorIdParametroSeoPwIdSubAreaCapacitacion(int idParametroSeoPw, int idSubAreaCapacitacion);
        public SubAreaParametroSeoPw ObtenerPorIdParametroSeoPwIdSubAreaCapacitacion(int idParametroSeoPw, int idSubAreaCapacitacion);
        public IEnumerable<SubAreaParametroSeoPw> ObtenerPorIdSubAreaCapacitacion(int idSubAreaCapacitacion);
        IEnumerable<ParametroContenidoDTO> ObtenerParametroContenidoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion);
    }
}
