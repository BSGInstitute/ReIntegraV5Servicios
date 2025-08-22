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
    public interface ISistemaPensionarioRepository : IGenericRepository<TSistemaPensionario>
    {
        #region Metodos Base
        TSistemaPensionario Add(SistemaPensionario entidad);
        TSistemaPensionario Update(SistemaPensionario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSistemaPensionario> Add(IEnumerable<SistemaPensionario> listadoEntidad);
        IEnumerable<TSistemaPensionario> Update(IEnumerable<SistemaPensionario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SistemaPensionarioDTO> Obtener();
    }
}
