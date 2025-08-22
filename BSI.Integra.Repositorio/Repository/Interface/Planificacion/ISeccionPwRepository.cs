using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ISeccionPwRepository : IGenericRepository<TSeccionPw>
    {
        IEnumerable<SeccionPlantillaContenidoDTO> ObtenerSeccionesPorIdPlantillaPW(int idPlantillaPw);
        IEnumerable<SeccionPw> ObtenerPorIdPlantillaPw(int idPlantillaPw);
        SeccionPw ObtenerPorId(int id);
        IEnumerable<SeccionPwFiltroPlantillaPwDTO> ObtenerPlantillaSeccionesPorIdPlantillaPW(int idPlantillaPw);
    }
}
