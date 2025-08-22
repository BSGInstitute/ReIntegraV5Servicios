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
    public interface IParentescoPersonalRepository : IGenericRepository<TParentescoPersonal>
    {
        #region Metodos Base
        TParentescoPersonal Add(ParentescoPersonal entidad);
        TParentescoPersonal Update(ParentescoPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TParentescoPersonal> Add(IEnumerable<ParentescoPersonal> listadoEntidad);
        IEnumerable<TParentescoPersonal> Update(IEnumerable<ParentescoPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ParentescoPersonalDTO> Obtener();
    }
}
