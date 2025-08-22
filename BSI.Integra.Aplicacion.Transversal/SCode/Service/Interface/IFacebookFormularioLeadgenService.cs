using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IFacebookFormularioLeadgenService
    {
        #region Metodos Base
        FacebookFormularioLeadgen Add(FacebookFormularioLeadgen entidad);
        FacebookFormularioLeadgen Update(FacebookFormularioLeadgen entidad);
        bool Delete(int id, string usuario);

        List<FacebookFormularioLeadgen> Add(List<FacebookFormularioLeadgen> listadoEntidad);
        List<FacebookFormularioLeadgen> Update(List<FacebookFormularioLeadgen> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        //public List<InteraccionesPorPasoProgramasDTO> Reportebot(bool estaRegistrado, string fechaInicio, string fechaFin);
        public List<InteraccionesPorPasoProgramasDTO> Reportebot(FiltroBotDTO filtro);
    }
}
