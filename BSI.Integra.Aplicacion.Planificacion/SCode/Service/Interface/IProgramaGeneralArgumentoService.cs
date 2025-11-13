using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralArgumentoService
    {
        List<ProgramaGeneralArgumentoDTO> ObtenerInformacionProgramaGeneralArgumentoTodo(int IdPGeneral);
        ArgumentoMotivacionProgramaGeneralDTO ObtenerArgumentoMotivacionByIdPGeneral(int idPGeneral, string motivacion);
        ProgramaGeneralArgumentoDTO ObtenerInformacionProgramaGeneralArgumento(int idProgramaGeneralArgumento);
        IEnumerable<ProgramaGeneralArgumentoDTO> Obtener();
        public ProgramaGeneralArgumentoDTO Insertar(ProgramaGeneralArgumentoDTO entidad, string usuario);

        ProgramaGeneralArgumentoDTO Actualizar(ProgramaGeneralArgumentoDTO entidad, string usuario);


        IEnumerable<ProgramaGeneralArgumentoMotivacionDTO> ObtenerMotivaciones(int IdPGeneral);
        bool Eliminar(int id, string usuario);
        List<OportunidadMotivacionSeleccionViewDTO> ObtenerMotivacionSeleccionByIdOportunidad(int idOportunidad);
        bool InsertarArgumentoMotivacionSeleccion(ProgramaArgumentoMotivacionSeleccionDTO data, string usuario);

        ///object InsertarArgumentoMotivacionSeleccion(ProgramaArgumentoMotivacionSeleccionDTO data);
        Task<List<ProgramaGeneralArgumentoDTO>> ObtenerArgumentoMotivacion(int idOportunidadl);
        Task<List<ConfiguracionProblemaJerarquicaDTO>> ObtenerProblemaCliente(int idPGeneral, int? idAlumno=null );
        List<MotivacionDiccionarioViewDTO> ObtenerMotivacionesTodoDiccionario();
    }
}
