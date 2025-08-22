using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPersonalEncargadoDeEnvioDeConsultumRepository : IGenericRepository<TPersonalEncargadoDeEnvioDeConsultum>
    {

        #region Metodos Base
        TPersonalEncargadoDeEnvioDeConsultum Add(PersonalEncargadoDeEnvioDeConsultum entidad);
        TPersonalEncargadoDeEnvioDeConsultum Update(PersonalEncargadoDeEnvioDeConsultum entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalEncargadoDeEnvioDeConsultum> Add(IEnumerable<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad);
        IEnumerable<TPersonalEncargadoDeEnvioDeConsultum> Update(IEnumerable<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<PersonalEncargadoDeEnvioDeConsultumObtenerDTO> ObtenerDias(int IdConfiguracionDeEnvioParaWhatsApp);
    }
}