using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IContadorBicRepository : IGenericRepository<TContadorBic>
    {
        #region Metodos Base
        TContadorBic Add(ContadorBic entidad);
        TContadorBic Update(ContadorBic entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TContadorBic> Add(IEnumerable<ContadorBic> listadoEntidad);
        IEnumerable<TContadorBic> Update(IEnumerable<ContadorBic> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPeru();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPeruBic1();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPeruBic2();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasContador();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasChile();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasChileBic1();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasChileBic2();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasMexico();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasMexicoBic1();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasMexicoBic2();
        List<ConfiguracionBICDTO> ObtenerConfiguracionBicAplicada();
        List<PersonalBicConfiguracionDTO> ObtenerConfiguracionBicPersonal();
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPorIdOportunida(int idOportunidad);
        List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPorIdOportunidadWhatsapp(int idOportunidad);
        List<OportunidadesACerrarBICDTO> ObtenerOportunidadesACerrarManualmente();
        List<OportunidadContadorBicDTO> ObtenerDatosParaBicPorPaisNuevaLogica(int idPais);
        bool ObtenerNuevoCalculoDiasBic();
    }
}
