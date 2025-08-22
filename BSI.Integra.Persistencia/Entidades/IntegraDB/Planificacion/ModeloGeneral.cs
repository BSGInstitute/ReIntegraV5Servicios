using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ModeloGeneral : BaseIntegraEntity
    {
        public string Nombre { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public int PeVersion { get; set; }
        public int? IdPadre { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<ModeloGeneralEscala> ModeloGeneralEscala { get; set; }
        public List<ModeloGeneralCargo> ModeloGeneralCargo { get; set; }
        public List<ModeloGeneralIndustria> ModeloGeneralIndustria { get; set; }
        public List<ModeloGeneralAFormacion> ModeloGeneralAFormacion { get; set; }
        public List<ModeloGeneralATrabajo> ModeloGeneralATrabajo { get; set; }
        public List<ModeloGeneralCategoriaDato> ModeloGeneralCategoriaDato { get; set; }
        public List<ModeloGeneralTipoDato> ModeloGeneralTipoDato { get; set; }
    }
}
