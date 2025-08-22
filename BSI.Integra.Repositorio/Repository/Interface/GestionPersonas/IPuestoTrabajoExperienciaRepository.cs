using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoExperienciaRepository : IGenericRepository<TPuestoTrabajoExperiencium>
    {
        #region Metodos Base
        TPuestoTrabajoExperiencium Add(PuestoTrabajoExperiencia entidad);

        TPuestoTrabajoExperiencium Update(PuestoTrabajoExperiencia entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoExperiencium> Add(IEnumerable<PuestoTrabajoExperiencia> listadoEntidad);
        IEnumerable<TPuestoTrabajoExperiencium> Update(IEnumerable<PuestoTrabajoExperiencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoExperienciaPuestoTrabajo> ObtenerPuestoTrabajoExperiencia(int? idPerfilPuestoTrabajo);
    }
}
