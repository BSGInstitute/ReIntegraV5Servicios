using AutoMapper;

using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class ModuloSistemaV5Service: IModuloSistemaV5Service
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ModuloSistemaV5Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<ModuloSistemaPaqueteV5DTO, TModuloSistemaPaqueteV5>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary>
        /// <param name="dto">ModuloSistemaPaqueteV5DTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialAccionDTO</returns>
        public bool AsignarModulo(AsignarModuloV5DTO dto, string usuario)
        {
            try
            {
                var resultado = _unitOfWork.ModuloSistemaV5Repository.AsignarModulo(dto, usuario);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 31/10/2023
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo Material de Accion
        /// </summary> 
        /// <param name="dto">ModuloSistemaPaqueteV5DTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>MaterialAccionDTO</returns>
        public bool DesasignarModulo(AsignarModuloV5DTO dto, string usuario)
        {
            try
            {
                var resultado = _unitOfWork.ModuloSistemaV5Repository.DesasignarModulo(dto, usuario);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 06/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre del módulo por el segmento de su url
        /// </summary> 
        /// <param name="segmentoModulo">Segmento de la url del módulo</param>
        /// <returns>ModuloUrlDTO</returns>
        public ModuloUrlDTO ObtenerNombreUrlModulos(string segmentoModulo)
        {
            try
            {
                var resultado = _unitOfWork.ModuloSistemaV5Repository.ObtenerNombreUrlModulos(segmentoModulo);
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
