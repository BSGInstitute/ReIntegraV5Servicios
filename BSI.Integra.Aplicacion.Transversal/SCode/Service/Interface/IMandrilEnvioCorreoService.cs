using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMandrilEnvioCorreoService
    {
        #region Metodos Base
        MandrilEnvioCorreo Add(MandrilEnvioCorreo entidad);
        MandrilEnvioCorreo Update(MandrilEnvioCorreo entidad);
        bool Delete(int id, string usuario);
        List<MandrilEnvioCorreo> Add(List<MandrilEnvioCorreo> listadoEntidad);
        List<MandrilEnvioCorreo> Update(List<MandrilEnvioCorreo> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
