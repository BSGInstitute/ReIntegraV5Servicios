using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.Classes;
using Microsoft.Data.SqlClient;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: ResumenGrabacionOnlineService
    /// Autor: Jorge Gamero
    /// Fecha: 10/02/2025
    /// <summary>
    /// Gestión general de T_ResumenGrabacionOnline
    /// </summary>
    public class ResumenGrabacionOnlineService : IResumenGrabacionOnlineService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ResumenGrabacionOnlineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TResumenGrabacionOnline, ResumenGrabacionOnline>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ResumenGrabacionOnline
        /// </summary>
        /// <returns> IEnumerable<ResumenGrabacionOnlineDTO> </returns>
        public IEnumerable<ResumenGrabacionOnlineDTO> ObtenerResumenGrabacionOnline()
        {
            try
            {
                return _unitOfWork.ResumenGrabacionOnlineRepository.ObtenerResumenGrabacionOnline();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 10/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ResumenGrabacionOnline filtrado por Id
        /// </summary>
        /// <returns> ResumenGrabacionOnlineDTO </returns>
        public ResumenGrabacionOnlineDTO ObtenerResumenGrabacionOnlinePorId(int id)
        {
            try
            {
                return _unitOfWork.ResumenGrabacionOnlineRepository.ObtenerResumenGrabacionOnlinePorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 11/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ProcesamientoTipoGenerar filtrado por Id
        /// </summary>
        /// <returns> ProcesamientoTipoGenerarDTO </returns>
        public ProcesamientoTipoGenerarDTO ObtenerProcesamientoTipoGenerarPorId(int id)
        {
            try
            {
                return _unitOfWork.ResumenGrabacionOnlineRepository.ObtenerProcesamientoTipoGenerarPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}