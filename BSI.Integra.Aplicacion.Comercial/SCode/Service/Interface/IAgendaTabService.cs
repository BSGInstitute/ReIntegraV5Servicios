using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAgendaTabService
    {
        IEnumerable<AgendaTabDTO> Obtener();
        AgendaTabDTO Actualizar(AgendaTabAlternoDTO dto, string usuario);
        AgendaTabDTO Insertar(AgendaTabAlternoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
