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
    public interface IEstadoCivilRepository
    {
        #region Metodos Base
        TEstadoCivil Add(EstadoCivil entidad);
        TEstadoCivil Update(EstadoCivil entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEstadoCivil> Add(IEnumerable<EstadoCivil> listadoEntidad);
        IEnumerable<TEstadoCivil> Update(IEnumerable<EstadoCivil> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EstadoCivilDTO> Obtener();
    }
}
