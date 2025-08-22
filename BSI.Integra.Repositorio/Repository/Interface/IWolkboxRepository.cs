using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWolkboxRepository
    {
        void InsertarWolkboxTokenLog(WolkboxTokenLogDTO dto);
        void ReasignarTokenWolkboxPersonal(int idWolkboxToken);
        WolkboxTokenDTO? ObtenerWolkboxTokenPorIdPersonal(int idPersonal);
    }
}
