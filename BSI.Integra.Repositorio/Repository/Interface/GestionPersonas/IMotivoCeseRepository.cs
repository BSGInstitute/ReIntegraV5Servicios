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
    public interface IMotivoCeseRepository : IGenericRepository<TMotivoCese>
    {
        #region Metodos Base
        TMotivoCese Add(MotivoCese entidad);
        TMotivoCese Update(MotivoCese entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMotivoCese> Add(IEnumerable<MotivoCese> listadoEntidad);
        IEnumerable<TMotivoCese> Update(IEnumerable<MotivoCese> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MotivoCeseDTO> Obtener();
    }
}
