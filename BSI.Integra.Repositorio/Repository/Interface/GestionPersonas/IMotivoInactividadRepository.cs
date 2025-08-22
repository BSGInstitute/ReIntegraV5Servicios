using BSI.Integra.Aplicacion.DTO;
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
    public interface IMotivoInactividadRepository : IGenericRepository<TMotivoInactividad>
    {
        #region Metodos Base
        TMotivoInactividad Add(MotivoInactividad entidad);
        TMotivoInactividad Update(MotivoInactividad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMotivoInactividad> Add(IEnumerable<MotivoInactividad> listadoEntidad);
        IEnumerable<TMotivoInactividad> Update(IEnumerable<MotivoInactividad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> Obtener();

    }
}
