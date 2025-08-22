using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IMaterialPespecificoService
    {
        MaterialPespecificoCombosDTO ObtenerCombos();
        IEnumerable<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMateriales(FiltroMaterialDTO dto);
        bool AprobarMaterialVersion(int id, string usuario);
        bool DesaprobarMaterialVersion(int id, string usuario);
        List<MaterialPespecificoDTO> ObtenerPorIdPEspecifico(int idPEspecifico);
        ComboMaterialPespecificoDTO ObtenerComboMaterial(); 
        List<ComboDTO> Insertar(MaterialPespecificoDTO materialPespecifico, string usuario);
        List<ComboDTO> Actualizar(MaterialPespecificoDTO materialPespecificoDTO, string usuario);
        bool Eliminar(int id, string usuario);
        Task <List<ResultadoMaterialPEspecificoDetalleDTO>>ObtenerMaterialesGestionEnvioAsync(FiltroMaterialDTO filtroMaterial);
        bool NotificarMaterialVersionAlumnoPorCorreo(List<int> idMaterialPEspecificoDetalle, string nombreUsuario);
        bool NotificarMaterialVersionAlumnoImpresoPorCorreoAProveedor(int id, string usuario);
        List<MaterialPespecificoDetalle> ObtenerMaterialesAlumnoDigital(int idMaterialPEspecifico);
        List<PEspecificoFurDetalleDTO> ObtenerFursAsociadosPorIdPEspecifico(int idPEspecifico);
        bool AsociarActualizarFur(AsociarActualizarFurMaterialVersionDTO materialPespecificoDetalle, string usuario);
        AsociarActualizarFurMaterialVersionDTO ObtenerFurAsociadoPorIdPEspecificoDetalle(int idMaterialPEspecificoDetalle);
        Task<EntregaMaterialDTO> ObtenerCriteriosMaterialesProgramaEspecifico(FiltroMaterialDTO Filtro);
        bool NotificarListaMaterialVersionAlumnoPorCorreo(List<int> listaIdMaterialPEspecificoDetalle, string usuario);
        Task<bool> AprobarRechazarRegistroEntrega(AprobarRechazarRegistroEntregaMaterialDTO Registro);
        bool ActualizarFurRegistroMaterial(FurRegistroMaterialDTO FurMaterial);
    }
}
;