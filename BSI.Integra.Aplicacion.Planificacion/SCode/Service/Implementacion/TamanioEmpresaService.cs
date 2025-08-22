using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: TamanioEmpresaService
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/12/2022
    /// <summary>
    /// Gestión general de TTamanioEmpresa
    /// </summary>
    public class TamanioEmpresaService : ITamanioEmpresaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public TamanioEmpresaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TTamanioEmpresa, TamanioEmpresa>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros con el estado = 1
        /// </summary> 
        /// <returns> List<TamanioEmpresaComboDTO> </returns>
        public List<TamanioEmpresaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.TamanioEmpresaRepository.ObtenerCombo().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
