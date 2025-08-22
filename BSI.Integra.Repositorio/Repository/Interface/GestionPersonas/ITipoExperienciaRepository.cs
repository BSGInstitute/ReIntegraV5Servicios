using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoExperienciaRepository
    {

        #region Metodos Base
        TTipoExperiencium Add(TipoExperiencia entidad);
        TTipoExperiencium Update(TipoExperiencia entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTipoExperiencium> Add(IEnumerable<TipoExperiencia> listadoEntidad);
        IEnumerable<TTipoExperiencium> Update(IEnumerable<TipoExperiencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoExperienciaDTO> Obtener();
        TipoExperiencia? ObtenerPorId(int idGradoEstudio);
    }
}
