using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFacebookFormularioLeadgenRepository : IGenericRepository<TFacebookFormularioLeadgen>
    {
        #region Metodos Base
        TFacebookFormularioLeadgen Add(FacebookFormularioLeadgen entidad);
        TFacebookFormularioLeadgen Update(FacebookFormularioLeadgen entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFacebookFormularioLeadgen> Add(IEnumerable<FacebookFormularioLeadgen> listadoEntidad);
        IEnumerable<TFacebookFormularioLeadgen> Update(IEnumerable<FacebookFormularioLeadgen> listadoEntidad);

        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public DatosCiudadDTO ObtenerDatosCiudadMexico(string nombre);
    }
}
