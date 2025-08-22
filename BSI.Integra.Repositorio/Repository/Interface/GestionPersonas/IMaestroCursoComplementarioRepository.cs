using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IMaestroCursoComplementarioRepository
    {
        #region Metodos Base
        TPuestoTrabajoCursoComplementario Add(CursoComplementario entidad);
        TPuestoTrabajoCursoComplementario Update(CursoComplementario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPuestoTrabajoCursoComplementario> Add(IEnumerable<CursoComplementario> listadoEntidad);
        IEnumerable<TPuestoTrabajoCursoComplementario> Update(IEnumerable<CursoComplementario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
