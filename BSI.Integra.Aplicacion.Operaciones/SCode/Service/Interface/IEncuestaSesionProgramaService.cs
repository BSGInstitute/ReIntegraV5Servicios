using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Interface
{
    public interface IEncuestaSesionProgramaService
    {
        #region Metodos Base
        EncuestaSesionPrograma Add(EncuestaSesionPrograma entidad);
        EncuestaSesionPrograma Update(EncuestaSesionPrograma entidad);
        bool Delete(int id, string usuario);
        List<EncuestaSesionPrograma> Add(List<EncuestaSesionPrograma> listadoEntidad);
        List<EncuestaSesionPrograma> Update(List<EncuestaSesionPrograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
 
}
