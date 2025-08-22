using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICarreraPreRequisitoPgeneralRepository : IGenericRepository<TCarreraPreRequisitoPgeneral>
    {
        IEnumerable<CarreraPreRequisitoPgeneralDTO> ObtenerCarreraPreRequisitoPGeneralPorIdPGeneral(int idPGeneral);
        IEnumerable<CarreraPreRequisitoPgeneral> ObtenerPorIdPGeneral(int idPGeneral);
        //CarreraPreRequisitoPGeneral? ObtenerPorId(int id);
        CarreraPreRequisitoPgeneral? ObtenerPorIdPgeneralIdPgeneralPrerequisito(int idPgeneral, int idPgeneralPrerequisito);
    }
}
