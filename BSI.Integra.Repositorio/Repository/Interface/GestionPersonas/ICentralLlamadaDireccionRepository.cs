using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICentralLlamadaDireccionRepository : IGenericRepository<TCentralLlamadaDireccion>
    {
      IEnumerable<CentralLlamadaDireccionDTO> Obtener();
      IEnumerable<DominioPbxDTO> ObtenerComboDominioPbx();

    }
}
