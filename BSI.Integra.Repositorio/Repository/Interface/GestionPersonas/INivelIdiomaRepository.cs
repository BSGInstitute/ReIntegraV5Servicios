using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface INivelIdiomaRepository : IGenericRepository<TNivelIdioma>
    {
        #region Metodos Base
        TNivelIdioma Add(NivelIdioma entidad);
        TNivelIdioma Update(NivelIdioma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TNivelIdioma> Add(IEnumerable<NivelIdioma> listadoEntidad);
        IEnumerable<TNivelIdioma> Update(IEnumerable<NivelIdioma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<NivelIdiomaDTO> Obtener();
    }
}
