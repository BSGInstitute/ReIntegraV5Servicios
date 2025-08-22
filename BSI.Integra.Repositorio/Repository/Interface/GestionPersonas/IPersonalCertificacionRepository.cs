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
    public interface IPersonalCertificacionRepository : IGenericRepository<TPersonalCertificacion>
    {
        #region Metodos Base
        TPersonalCertificacion Add(PersonalCertificacion entidad);
        TPersonalCertificacion Update(PersonalCertificacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalCertificacion> Add(IEnumerable<PersonalCertificacion> listadoEntidad);
        IEnumerable<TPersonalCertificacion> Update(IEnumerable<PersonalCertificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalCertificacionDTO> ObtenerPersonalCertificacion(int idPersonal);
        PersonalCertificacion ObtenerPorId(int Id);
    }
}
