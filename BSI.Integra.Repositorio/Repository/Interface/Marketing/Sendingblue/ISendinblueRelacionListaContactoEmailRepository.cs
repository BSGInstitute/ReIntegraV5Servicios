using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinblueRelacionListaContactoEmailRepository
    {
        TSendinblueRelacionListaContactoEmail Add(SendinblueRelacionListaContactoEmailDTO entidad);
        IEnumerable<TSendinblueRelacionListaContactoEmail> Add(IEnumerable<SendinblueRelacionListaContactoEmailDTO> listadoEntidad);
        TSendinblueRelacionListaContactoEmail Update(SendinblueRelacionListaContactoEmailDTO entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSendinblueRelacionListaContactoEmail> Update(IEnumerable<SendinblueRelacionListaContactoEmailDTO> listadoEntidad);
        IEnumerable<TSendinblueRelacionListaContactoEmail> Update(IEnumerable<TSendinblueRelacionListaContactoEmail> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
    }
}
