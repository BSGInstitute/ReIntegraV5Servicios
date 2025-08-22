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
    public interface IEntidadSistemaPensionarioRepository : IGenericRepository<TEntidadSistemaPensionario>
    {
        #region Metodos Base
        TEntidadSistemaPensionario Add(EntidadSistemaPensionario entidad);
        TEntidadSistemaPensionario Update(EntidadSistemaPensionario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEntidadSistemaPensionario> Add(IEnumerable<EntidadSistemaPensionario> listadoEntidad);
        IEnumerable<TEntidadSistemaPensionario> Update(IEnumerable<EntidadSistemaPensionario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EntidadSistemaPensionarioDTO> Obtener();
    }
}
