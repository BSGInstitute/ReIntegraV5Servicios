using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalInformacionMedicaDTO
    {
        public string Alergia { get; set; }
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        //public int IdTipoSangre { get; set; }
        public string Precaucion { get; set; }
    }
}
