using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionManualService
    {
        #region Metodos Base
        #endregion
        public object AsignarAsesor(AsignarAsesorManualDTO AsignarAsesor, string Usuario);
        object CerrarOportunidadOD(List<int> IdOportunidades, string Usuario);
        public bool EnvioWhats(int idOportunidad, int idPais, int idPersonal, int IdCategoriaOrigen);
        public bool EnvioWhatsAppTercerDiaSinContacto(int idOportunidad, int idPais, int idPersonal, int IdCategoriaOrigen, ContadorBic contador);
        public object AsignarAsesorFechaProgramacion(AsignarAsesorManuaWhatsapplDTO AsignarAsesor, string Usuario);

    }
}

