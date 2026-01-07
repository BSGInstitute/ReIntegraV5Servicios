
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using Org.BouncyCastle.Asn1.Cms;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComunicacionAcademicaDTO
    {
        //public List<MedioComunicacionDTO> MediosComunicacion { get; set; }
        //public List<BloqueHorarioDTO> Hola { get; set; }
    }

    public class ConfigurarPreferenciaDTO
    {
        public List<MedioComunicacionDTO> MediosComunicacion { get; set; }
        public List<BloqueHorarioDTO> BloqueHorario { get; set; }
        public List<BloqueHorarioDetalleDTO> BloqueHorarioDetalle { get; set; }
    }
    public class PreferenciaConfiguracionDTO
    {
        public int IdAlumno { get; set; }
        public List<VPreferenciaComunicacionAcademicaMedioComunicacionDTO> MediosComunicacion { get; set; }
        public List<PreferenciaComunicacionAcademicaHorarioDTO> BloqueHorario { get; set; }
    }
}
