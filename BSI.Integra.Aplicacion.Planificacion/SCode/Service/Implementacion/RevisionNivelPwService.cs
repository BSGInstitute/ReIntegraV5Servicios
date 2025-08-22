using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{

    /// Service: PlantillaPwService
    /// Autor: Gilmer Qm.
    /// Fecha: 23/06/2023
    /// <summary>
    /// Gestión general de T_RevisionNivel_Pw
    /// </summary>
    public class RevisionNivelPwService : IRevisionNivelPwService
    {
        IUnitOfWork _unitOfWork;
        public RevisionNivelPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Versio: 1.0
        /// <summary>
        /// Obtiene todos los registros de RevisionNivel_Pw por el IdRevision
        /// </summary>
        /// <returns> IEnumerable<RevisionNivelPwDTO> </returns>
        public List<RevisionNivelPwDTO> ObtenerPorIdRevisionPw(int idRevisionPw)
        {
            return _unitOfWork.RevisionNivelPwRepository.ObtenerPorIdRevisionPw(idRevisionPw).ToList();
        }
    }
}
