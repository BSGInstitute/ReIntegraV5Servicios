using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IRegistroCertificadoFisicoGeneradoService
    {
        #region Metodos Base
        RegistroCertificadoFisicoGenerado Add(RegistroCertificadoFisicoGenerado entidad);
        RegistroCertificadoFisicoGenerado Update(RegistroCertificadoFisicoGenerado entidad);
        bool Delete(int id, string usuario);

        List<RegistroCertificadoFisicoGenerado> Add(List<RegistroCertificadoFisicoGenerado> listadoEntidad);
        List<RegistroCertificadoFisicoGenerado> Update(List<RegistroCertificadoFisicoGenerado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
