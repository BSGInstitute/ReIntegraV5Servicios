using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRegistroCertificadoFisicoGeneradoRepository
    {
        #region Metodos Base
        TRegistroCertificadoFisicoGenerado Add(RegistroCertificadoFisicoGenerado entidad);
        TRegistroCertificadoFisicoGenerado Update(RegistroCertificadoFisicoGenerado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRegistroCertificadoFisicoGenerado> Add(IEnumerable<RegistroCertificadoFisicoGenerado> listadoEntidad);
        IEnumerable<TRegistroCertificadoFisicoGenerado> Update(IEnumerable<RegistroCertificadoFisicoGenerado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


    }
}
