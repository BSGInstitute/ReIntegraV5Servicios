using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueRelacionAlmunoSBDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendingblueRelacionAlumnoSbRepository
    {
        #region Metodos Base
        bool Add(string entidad);
        TSendinblueRelacionAlmunoSb Update(CrearSendinblueRelacionAlmunoSB entidad);
        bool Delete(int id, string usuario);

        bool AddRange(string listadoEntidad);
        IEnumerable<TSendinblueRelacionAlmunoSb> Update(IEnumerable<CrearSendinblueRelacionAlmunoSB> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TSendinblueRelacionAlmunoSb ObtenerCampaniaPorId(int id);
        IEnumerable<TSendinblueRelacionAlmunoSb> ObtenerTodaslasCampanias();
    }
}
