using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPGeneralTipoDescuentoService
    {
        BoolDTO ObtenerFlagPromocion(int idPGeneral, int idTipoDescuento);
        IEnumerable<int> ObtenerProgramaPorDescuento(int IdTipoDescuento);
        void EliminacionLogicoTipoDescuento(int IdTipoDescuento, string usuario, List<int> nuevos);
        bool AsociarPrograma(TipoDescuentoProgramaDTO dto, string usuario);
        public bool AsociarDescuentos(ProgramaTipoDescuentoDTO Json, string usuario);
        public List<int> ObtenerDescuentosPorPrograma(int idPGeneral);//A nivel de service ya puedes jugar con los datos a tu antojo, como juegas con los chicos 
    }
}
