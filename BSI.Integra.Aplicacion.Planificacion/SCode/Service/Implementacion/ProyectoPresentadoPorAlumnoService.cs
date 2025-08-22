using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using CommandLine;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    public class ProyectoPresentadoPorAlumnoService: IProyectoPresentadoPorAlumnoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        
        public ProyectoPresentadoPorAlumnoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// 
        /// Autor: Edmundo Llaza
        /// Fecha 2023-07-31
        /// <summary>
        /// Función que trae data para llenar los combos Coordinadores, Docente, CentroCosto y PEspecifico
        /// </summary>
        /// <returns>Retorma una lista</returns>
        public ObtenerDataComboDTO ObtenerCombosModulo()
        {
            
            try
            {
                var comboDataCombo = new ObtenerDataComboDTO();
                comboDataCombo.ObtenerCoordinadorasDocente = _unitOfWork.PersonalRepository.ObtenerCoordinadorasDocente();
                comboDataCombo.ObtenerNombreProveedorParaHonorario = _unitOfWork.ProveedorRepository.ObtenerNombreProveedorParaHonorario().ToList();
                comboDataCombo.ObtenerCombo = _unitOfWork.CentroCostoRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerProgramaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerProgramaEspecifico().ToList();


                return comboDataCombo;
            }
            catch 
            {
                throw;
            }

        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-23
        /// Versión: 1.0
        /// <summary>
        /// Generar un reporte con los nombre de los programas especificos según el valor ingresado
        /// </summary>
        /// <param name="valor"></param>
        /// <returns> Lista de objetoDTO: List<FiltroDTO> </returns>
        public List<ComboDTO> ObtenerPorNombreAutocomplete(string valor)
        {
            try
            {
                return _unitOfWork.PEspecificoRepository.ObtenerPorNombreAutocomplete(valor.ToString());
            }
            catch
            {
                throw; 
            }
        }
    }
}
