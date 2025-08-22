using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.IntegracionConIntegraDB
{
    public class RelacionFolderConListaDTO
    {
        public class RelacionFolderConListaSQL
        {
            public int Id { get; set; }
            public int ListaId { get; set; }
            public string Nombre { get; set; }
            public string NombreLista { get; set; }
            public int TotalSuscrito { get; set; }
            public int TotalExcluido { get; set; }
            public int UnicoSuscrito { get; set; }
            public int IdSendinblueCarpeta { get; set; }
            public int IdSendinblueLista { get; set; }
        }
        public class RelacionFolderConLista
        {
            public CarpetaSendingblueHelper Carpeta { get; set; }
            public List<ListasSendingblueHelper> Listas { get; set; }
        }
        public class CarpetaSendingblueHelper
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
        }
        public class ListasSendingblueHelper
        {
            public int ListaId { get; set; }
            public string NombreLista { get; set; }
            public int TotalSuscrito { get; set; }
            public int TotalExcluido { get; set; }
            public int UnicoSuscrito { get; set; }
            public int IdSendinblueCarpeta { get; set; }
            public int IdSendinblueLista { get; set; }
        }
    }
    
}
