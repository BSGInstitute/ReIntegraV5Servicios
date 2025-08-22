using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ParametroEvaluacionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 01/06/2023
    /// <summary>
    /// Gestión general de T_ParametroEvaluacion
    /// </summary>
    public class ParametroEvaluacionService : IParametroEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ParametroEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TParametroEvaluacion, ParametroEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
    }
}
