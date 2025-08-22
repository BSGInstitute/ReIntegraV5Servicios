using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoCursoComplementarioRepository : IGenericRepository<TPuestoTrabajoCursoComplementario>
    {
        #region Metodos Base
        TPuestoTrabajoCursoComplementario Add(PuestoTrabajoCursoComplementario entidad);

        TPuestoTrabajoCursoComplementario Update(PuestoTrabajoCursoComplementario entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoCursoComplementario> Add(IEnumerable<PuestoTrabajoCursoComplementario> listadoEntidad);
        IEnumerable<TPuestoTrabajoCursoComplementario> Update(IEnumerable<PuestoTrabajoCursoComplementario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoCursoComplementarioDTO> ObtenerPuestoTrabajoCursoComplementario(int? idPerfilPuestoTrabajo);
    }
}
