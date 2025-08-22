using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionRegularAutomaticaService
    {
        #region Metodos Base
        AsignacionRegular Add(AsignacionRegular entidad);
        AsignacionRegular Update(AsignacionRegular entidad);
        bool Delete(int id, string usuario);

        List<AsignacionRegular> Add(List<AsignacionRegular> listadoEntidad);
        List<AsignacionRegular> Update(List<AsignacionRegular> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
         Task<bool> AsignacionAutomatizadaAsesorWhatsapp(string Usuario, CancellationToken token);
         Task<bool> AsignacionAutomatizadaAsesor(string Usuario, CancellationToken token);
        public bool EnvioCorreoAsignacion(string mensaje);
        Task<bool> EjecutarAsignacionDatosAutomatizada2(string Usuario);
        Task<bool> EjecutarAsignacionDatosAutomatizada(string Usuario);
        public bool EnvioCorreoValidado(string mensaje);









    }
}