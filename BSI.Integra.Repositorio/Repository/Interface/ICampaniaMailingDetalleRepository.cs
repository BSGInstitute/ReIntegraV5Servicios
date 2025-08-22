using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampaniaMailingDetalleRepository : IGenericRepository<TCampaniaMailingDetalle>
    {
        ValorIntDTO? ObtenerIdCampaniaMailing(string codMailing);
        CampaniaMailingDTO Obtener(int id);
    }
}
