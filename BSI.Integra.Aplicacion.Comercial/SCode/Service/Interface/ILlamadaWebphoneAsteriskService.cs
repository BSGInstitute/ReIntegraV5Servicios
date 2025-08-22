using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ILlamadaWebphoneAsteriskService
    {
        LlamadaWebphoneAsterisk Insertar(NuevaLlamadaActividadDTO obj, string url, string usuario);
        ValorIntDTO ModificarLlamadaWebphone(int idLlamada, string url, string nombreUsuario, int duracionContesto, int nroBytes);
        void RegularizarContadorCdrId();
    }
}
