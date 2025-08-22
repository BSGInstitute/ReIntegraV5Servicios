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
    public interface IPersonalMotivoTiempoInactividadRepository : IGenericRepository<TPersonalMotivoTiempoInactividad>
    {
        #region Metodos Base
        TPersonalMotivoTiempoInactividad Add(PersonalMotivoTiempoInactividad entidad);
        TPersonalMotivoTiempoInactividad Update(PersonalMotivoTiempoInactividad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalMotivoTiempoInactividad> Add(IEnumerable<PersonalMotivoTiempoInactividad> listadoEntidad);
        IEnumerable<TPersonalMotivoTiempoInactividad> Update(IEnumerable<PersonalMotivoTiempoInactividad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalTiempoInactivoHistoricoDTO> ObtenerPeriodoInactivoHistorico(int idPersonal);
    }
}
