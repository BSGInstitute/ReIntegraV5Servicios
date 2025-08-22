namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AsociacionGrupoFiltroCentroCostoDTO
    {
        public List<CentroCostoSubAreaDTO> ListaCentroCosto { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }

    public class AsociacionGrupoFiltroPGeneralDTO
    {
        public List<PGeneralSubAreaDTO> ListaPGeneral { get; set; }
        public string Usuario { get; set; }
        public int IdGrupo { get; set; }
    }
}


