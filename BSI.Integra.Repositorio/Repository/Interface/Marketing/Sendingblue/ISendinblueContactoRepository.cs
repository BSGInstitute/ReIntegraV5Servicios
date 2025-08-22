using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BBSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.TSendingblueContactosDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueContactoRepository
    {
        #region Metodos Base
        bool Add(string entidad);
        TSendinblueContacto Update(CrearSendingblueContactos entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSendinblueContacto> Update(IEnumerable<CrearSendingblueContactos> listadoEntidad);
        IEnumerable<TSendinblueContacto> Update(IEnumerable<TSendinblueContacto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public TSendinblueContacto ObtenerCampaniaPorId(int id);
        public IEnumerable<TSendinblueContacto> ObtenerTodaslasCampanias();
        TSendinblueContacto ObtenerCampaniaPorEmail(string email);
    }
}
