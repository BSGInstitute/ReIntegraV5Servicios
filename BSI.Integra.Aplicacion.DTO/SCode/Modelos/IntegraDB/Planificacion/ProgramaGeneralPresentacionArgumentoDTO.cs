using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class PresentacionArgumentoModalidadAlternoDTO
    {
        public int IdPresentacionArgumento { get; set; }
        public int IdPGeneral { get; set; }
        public string NombrePresentacionArgumento { get; set; }
        public string DescripcionPresentacionArgumento { get; set; }

        public int IdModalidadPresentacionArgumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public string NombreModalidad { get; set; }
        public int? IdArgumentoPresentacionArgumento { get; set; }
        public string DetalleArgumentoPresentacionArgumento { get; set; }
        public string SolucionArgumentoPresentacionArgumento { get; set; }
        public bool EstadoPresentacionArgumento { get; set; }
        public bool? EstadoArgumento { get; set; }
        public bool EsVisibleAgenda { get; set; }
    }


    public class ProgramaGeneralPresentacionArgumentoAgendaDTO
    {
        public int IdPresentacionArgumento { get; set; }
        public string NombrePresentacionArgumento { get; set; } = null!;
        public string DescripcionPresentacionArgumento { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
    }

    public class ProgramaGeneralPresentacionArgumentoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

    }
    public class ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO
    {
        public int IdPresentacionArgumento { get; set; }
        public string NombrePresentacionArgumento { get; set; } = null!;
        public string DescripcionPresentacionArgumento { get; set; }
        public int Respuesta { get; set; }
        public string Completado { get; set; } = null!;
        public List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> Argumentos { get; set; } = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO>();
    }

    
    public class AlumnoCodigosDescuentosDTO
    {
        public int IdAlumno { get; set; }
        public List<CodigoDTO> Descuentos { get; private set; }
    }
    public class CodigoDTO
    {
        public int Id { get; set; }
        public int PorcentajeDescuento { get; set; }
        public string CodigoDescuento { get; set; }
        public Boolean? Utilizado {  get; set; }
        public Boolean? Estado { get; set; }
        public string Correo {  get; set; }

    }

}
