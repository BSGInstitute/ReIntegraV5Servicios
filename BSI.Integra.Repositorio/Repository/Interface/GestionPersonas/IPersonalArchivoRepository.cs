using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalArchivoRepository : IGenericRepository<TPersonalArchivo>
    {
        #region Metodos Base
        TPersonalArchivo Add(PersonalArchivo entidad);
        TPersonalArchivo Update(PersonalArchivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalArchivo> Add(IEnumerable<PersonalArchivo> listadoEntidad);
        IEnumerable<TPersonalArchivo> Update(IEnumerable<PersonalArchivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PersonalArchivo? ObtenerPorId(int idPersonalArchivo);
        string SubirDocumentosPersonal(byte[] archivo, string tipo, string nombreArchivo);
    }
}
