using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPuestoTrabajoService
    {

        IEnumerable<PuestoTrabajoEnviarDTO> Obtener();
        ComboPuestoTrabajo ObtenerCombos();
        PuestoTrabajoInsertDTO Insertar(PuestoTrabajoInsertDTO dto, string usuario);
        PuestoTrabajoInsertDTO Actualizar(PuestoTrabajoInsertDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        ObtenerExamenDTO ObtenerPerfilPuestoTrabajo(int? idPerfilPuestoTrabajo);
        IEnumerable<PuestoTrabajoVersionesDTO> ObtenerListaHistoricoPerfilPuestoTrabajo(int? idPuestoTrabajo);
        bool InsertarActualizarPerfilPuestoTrabajo(PerfilPuestoTrabajoInsertarActualizarDTO dto, string usuario);
        IEnumerable<PuestoTrabajoModuloSistemaDTO> ObtenerGridAsignacionInterfaz(int idPuestoTrabajo);
        bool InsertarActualizarInterfaz(AsignarInterfazDTO ListaAsignar , string usuarioP);
        bool AprobarRechazarVersionPerfilPuestoTrabajo(AprobacionRechazoPerfilPuestoTrabajoDTO dto, string usuario);
        IEnumerable<PersonalAprobacionDTO> EsPersonalAprobacionVersion(int idPersonal);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
