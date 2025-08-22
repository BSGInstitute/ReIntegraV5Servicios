namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfiguracionDatoRemarketingDTO
    {
        public int Id { get; set; }
        public int IdAgendaTab { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; }
        public List<int> ListaIdTipoDato { get; set; }
        public List<int> ListaIdTipoCategoriaOrigen { get; set; }
        public List<int> ListaCategoriaOrigen { get; set; }
        public List<int> ListaProbabilidadRegistro { get; set; }
    }

    public class ConfiguracionDatoRemarketingAEliminarDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
    }

    public class ConfiguracionDatoRemarketingGrillaDTO
    {
        public int Id { get; set; }
        public int IdAgendaTab { get; set; }
        public string NombreAgendaTab { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Vigente { get; set; }
        public int? IdTipoDato { get; set; }
        public string NombreTipoDato { get; set; }
        public int? IdTipoCategoriaOrigen { get; set; }
        public string NombreTipoCategoriaOrigen { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string NombreCategoriaOrigen { get; set; }
        public int? IdProbabilidadRegistroPw { get; set; }
        public string NombreProbabilidadRegistroPw { get; set; }
    }

    public class ConfiguracionDatoRemarketingAgrupadoGrillaDTO
    {
        public int Id { get; set; }
        public int IdAgendaTab { get; set; }
        public string NombreAgendaTab { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool Vigente { get; set; }
        public List<ConfiguracionDatoRemarketingTipoDatoGrillaDTO> ListaTipoDato { get; set; }
        public List<ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO> ListaTipoCategoriaOrigen { get; set; }
        public List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ListaCategoriaOrigen { get; set; }
        public List<ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO> ListaProbabilidadRegistroPw { get; set; }
    }

    public class ConfiguracionDatoRemarketingAgendaTabVentasDTO
    {
        public int IdAgendaTab { get; set; }
        public string NombreAgendaTab { get; set; }
    }

    public class ConfiguracionDatoRemarketingTipoDatoGrillaDTO
    {
        public int IdTipoDato { get; set; }
        public string NombreTipoDato { get; set; }
    }


    //public class ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO
    //{
    //    public int IdTipoCategoriaOrigen { get; set; }
    //    public string NombreTipoCategoriaOrigen { get; set; }
    //}

    //public class ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO
    //{
    //    public int IdCategoriaOrigen { get; set; }
    //    public string NombreCategoriaOrigen { get; set; }
    //    public int IdTipoCategoriaOrigen { get; set; }
    //}

    public class ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO
    {
        public int IdProbabilidadRegistroPw { get; set; }
        public string NombreProbabilidadRegistroPw { get; set; }
    }

    public class ComboConfiguracionDatoRemarketingDTO
    {
        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ListaComboConfiguracionDatoRemarketingAgendaTab { get; set; }
        public List<ConfiguracionDatoRemarketingTipoDatoGrillaDTO> ListaComboConfiguracionDatoRemarketingTipoDato { get; set; }
        public List<ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO> ListaComboConfiguracionDatoRemarketingTipoCategoriaOrigen { get; set; }
        public List<ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO> ListaComboConfiguracionDatoRemarketingCategoriaOrigen { get; set; }
        public List<ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO> ListaComboConfiguracionDatoRemarketingProbabilidadRegistroPw { get; set; }
    }

}




