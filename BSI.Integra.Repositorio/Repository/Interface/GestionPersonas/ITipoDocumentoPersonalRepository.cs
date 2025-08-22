using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoDocumentoPersonalRepository : IGenericRepository<TTipoDocumentoPersonal>
    {
        #region Metodos Base
        TTipoDocumentoPersonal Add(TipoDocumentoPersonal entidad);
        TTipoDocumentoPersonal Update(TipoDocumentoPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoDocumentoPersonal> Add(IEnumerable<TipoDocumentoPersonal> listadoEntidad);
        IEnumerable<TTipoDocumentoPersonal> Update(IEnumerable<TipoDocumentoPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoDocumentoPersonalDTO> Obtener();
        IEnumerable<TipoDocumentoPersonalComboDTO> ObtenerComboDocumentos();
    }
}
