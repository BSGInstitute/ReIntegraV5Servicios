using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IPostulanteLogService
    {
        #region Metodos Base
        TPostulanteLog Add(PostulanteLog entidad);
        //TPostulanteLog Add(PostulanteLogv2 entidad);
        TPostulanteLog Update(PostulanteLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteLog> Add(IEnumerable<PostulanteLog> listadoEntidad);
        IEnumerable<TPostulanteLog> Update(IEnumerable<PostulanteLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteLog? ObtenerPorId(int id);
        Boolean GuardarPostulanteLog(Postulante postulanteRegistroNuevo, IntegraAspNetUser integraUser, bool esNuevoRegistro);
    }
}
