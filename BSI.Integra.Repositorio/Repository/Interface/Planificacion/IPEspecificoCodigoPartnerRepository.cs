using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPEspecificoCodigoPartnerRepository : IGenericRepository<TPespecificoCodigoPartner>
    {
        #region Metodos Base
        TPespecificoCodigoPartner Add(PespecificoCodigoPartner entidad);
        TPespecificoCodigoPartner Update(PespecificoCodigoPartner entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoCodigoPartner> Add(IEnumerable<PespecificoCodigoPartner> listadoEntidad);
        IEnumerable<TPespecificoCodigoPartner> Update(IEnumerable<PespecificoCodigoPartner> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoCodigoPartner? ObtenerPorId(int id);
        List<PespecificoCodigoPartnerDTO> ObtenerPEspecificoCodigoPartner(int idPGeneral);
    }
}
