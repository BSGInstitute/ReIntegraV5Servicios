using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion
{
        /// Service: SolicitudTipoReporteService
        /// Autor: Gilmer Quispe.
        /// Fecha: 21/12/2022
        /// <summary>
        /// Gestión general de T_SolicitudTipoReportes
        /// </summary>
        public class PreguntaEncuestaCategoriaService : IPreguntaEncuestaCategoriaService
        {
            private IUnitOfWork _unitOfWork;
            private Mapper _mapper;

            public PreguntaEncuestaCategoriaService(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;

                var config = new MapperConfiguration(cfg => cfg.CreateMap<TPreguntaEncuestaCategorium, PreguntaEncuestaCategoria>(MemberList.None).ReverseMap());
                _mapper = new Mapper(config);
            }
            #region Metodos Base
            public PreguntaEncuestaCategoria Add(PreguntaEncuestaCategoria entidad)
            {
                try
                {
                    var modelo = _unitOfWork.PreguntaEncuestaCategoriaRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<PreguntaEncuestaCategoria>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public PreguntaEncuestaCategoria Update(PreguntaEncuestaCategoria entidad)
            {
                try
                {
                    var modelo = _unitOfWork.PreguntaEncuestaCategoriaRepository.Update(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<PreguntaEncuestaCategoria>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public bool Delete(int id, string usuario)
            {
                try
                {
                    _unitOfWork.PreguntaEncuestaCategoriaRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<PreguntaEncuestaCategoria> Add(List<PreguntaEncuestaCategoria> listadoEntidad)
            {
                try
                {
                    var modelo = _unitOfWork.PreguntaEncuestaCategoriaRepository.Add(listadoEntidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<PreguntaEncuestaCategoria>>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<PreguntaEncuestaCategoria> Update(List<PreguntaEncuestaCategoria> listadoEntidad)
            {
                try
                {
                    var modelo = _unitOfWork.PreguntaEncuestaCategoriaRepository.Update(listadoEntidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<List<PreguntaEncuestaCategoria>>(modelo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public bool Delete(List<int> listadoIds, string usuario)
            {
                try
                {
                    _unitOfWork.PreguntaEncuestaCategoriaRepository.Delete(listadoIds, usuario);
                    _unitOfWork.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        #endregion
            /// Autor: Gilmer Quispe
            /// Fecha: 21/12/2022
            /// Version: 1.0
            /// <summary>
            /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
            /// </summary>
            /// <param name="id"> Id de la entidad </param>
            /// <returns> SolicitudTipoReporte </returns>
            public PreguntaEncuestaCategoria ObtenerPorId(int id)
            {
                try
                {
                    return _unitOfWork.PreguntaEncuestaCategoriaRepository.ObtenerPorId(id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            /// Autor: Joseph Llanque
            /// Fecha: 25/12/2022
            /// Version: 1.0
            /// <summary>
            /// Obtiene todos los registros de la tabla
            /// </summary> 
            /// <returns> IEnumerable<ComboDTO> </returns>
            public IEnumerable<ComboDTO> ObtenerCombo()
            {
                try
                {
                    return _unitOfWork.PreguntaEncuestaCategoriaRepository.ObtenerCombo();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            /// Autor: Joseph Llanque
            /// Fecha: 25/12/2022
            /// Version: 1.0
            /// <summary>
            /// Obtiene todos los registros de la tabla
            /// </summary> 
            /// <returns> IEnumerable<ComboDTO> </returns>
            public List<PreguntaEncuestaCategoriaDTO> ObtenerCategoriaEncuesta()
            {
                try
                {
                    return _unitOfWork.PreguntaEncuestaCategoriaRepository.ObtenerCategoriaEncuesta();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public List<PreguntaCategoriaAsincronicaDTO> ObtenerPreguntaCategoriaAsincronica()
            {
                try
                {
                    return _unitOfWork.PreguntaEncuestaCategoriaRepository.ObtenerPreguntaCategoriaAsincronica();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

    }
}
