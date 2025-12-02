using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPaqueteTutorVirtualBeneficioRepository : IGenericRepository<TPaqueteTutorVirtualPaisBeneficio>
    {
        #region Metodos Base
        TPaqueteTutorVirtualPaisBeneficio Add(PaqueteTutorVirtualBeneficio entidad);
        TPaqueteTutorVirtualPaisBeneficio Update(PaqueteTutorVirtualBeneficio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPaqueteTutorVirtualPaisBeneficio> Add(IEnumerable<PaqueteTutorVirtualBeneficio> listadoEntidad);
        IEnumerable<TPaqueteTutorVirtualPaisBeneficio> Update(IEnumerable<PaqueteTutorVirtualBeneficio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PaqueteTutorVirtualBeneficioDTO> Obtener();
        PaqueteTutorVirtualBeneficio? ObtenerPorId(int id);
        IEnumerable<PaqueteTutorVirtualBeneficioDTO> ObtenerPorIdPaquetePais(int idPaqueteTutorVirtualPais);
    }
}
