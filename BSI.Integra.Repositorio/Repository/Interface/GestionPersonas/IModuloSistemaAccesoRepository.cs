using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IModuloSistemaAccesoRepository : IGenericRepository<TModuloSistemaAccesoV5>
    {
        #region Metodos Base
        TModuloSistemaAccesoV5 Add(ModuloSistemaAccesoV5 entidad);

        TModuloSistemaAccesoV5 Update(ModuloSistemaAccesoV5 entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TModuloSistemaAccesoV5> Add(IEnumerable<ModuloSistemaAccesoV5> listadoEntidad);
        IEnumerable<TModuloSistemaAccesoV5> Update(IEnumerable<ModuloSistemaAccesoV5> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
