using System;
﻿using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;


namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IModuloSistemaV5Service
    {
        bool AsignarModulo(AsignarModuloV5DTO dto, string usuario);
        bool DesasignarModulo(AsignarModuloV5DTO dto, string usuario);
        public ModuloUrlDTO ObtenerNombreUrlModulos(string segmentoModulo);
    }
}
