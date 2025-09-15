using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{

    public class CriterioCalificacionFaseDTO
    {
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Usuario { get; set; }
    }
    public class CriterioCalificacionFaseCreateDTO
    {
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Usuario { get; set; }
    }

    public class CriterioCalificacionFaseUpdateDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Usuario { get; set; }
    }
}