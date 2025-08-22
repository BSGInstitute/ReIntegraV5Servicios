using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstructuraEspecificaService
    {
        DatosEstructuraCurricularDTO CongelarEstructuraEspecifica(int IdCronograma, string Usuario);
        List<CapitulosSesionesProgramaDTO> ConseguirEstructuraPorPrograma(int idProgramaGeneral);
        bool CongelarEstructuraAlumno(object datos, string usuario);
    }
}
