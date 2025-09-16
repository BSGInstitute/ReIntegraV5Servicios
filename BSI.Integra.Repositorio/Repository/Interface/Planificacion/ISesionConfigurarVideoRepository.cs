using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
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
    public interface ISesionConfigurarVideoRepository : IGenericRepository<TSesionConfigurarVideo>
    {
        #region Metodos Base
        TSesionConfigurarVideo Add(SesionConfigurarVideo entidad);
        TSesionConfigurarVideo Update(SesionConfigurarVideo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSesionConfigurarVideo> Add(IEnumerable<SesionConfigurarVideo> listadoEntidad);
        IEnumerable<TSesionConfigurarVideo> Update(IEnumerable<SesionConfigurarVideo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        void ActualizarPadreSesionConfiguracionVideo(int idConfiguracionAntiguo, int idConfiguracionNuevo);
        List<SesionConfigurarVideoDTO> ObtenerPorIdConfigurarVideoPrograma(int idConfigurarVideoPrograma);
        IntDTO EliminarSesionesConfiguracionVideo(int idPGeneral, string usuario);
        SesionConfigurarVideo ObtenerPorId(int id);
        IEnumerable<InformacionSesionConfigurarVideoDTO> ObtenerPorIdPGeneral(int idPGeneral);
        bool ActualizarDescargaReproduccionVideo(ActualizarDescargaReproduccionDTO dto, string usuario);
        ConfigurarConteodeVideosPorTipo ObtenerConteosdeVideosTipo(int idPGeneral);
    }
}
