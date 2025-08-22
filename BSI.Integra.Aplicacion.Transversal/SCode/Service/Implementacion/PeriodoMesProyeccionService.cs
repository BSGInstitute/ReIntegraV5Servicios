using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Org.BouncyCastle.Ocsp;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PeriodoMesProyeccionService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PeriodoMesProyeccion
    /// </summary>
    public class PeriodoMesProyeccionService : IPeriodoMesProyeccionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PeriodoMesProyeccionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPeriodoMesProyeccion, PeriodoMesProyeccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PeriodoMesProyeccionDTO, PeriodoMesProyeccion>(MemberList.None).ReverseMap();
            }
           );

         
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PeriodoMesProyeccion Add(PeriodoMesProyeccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoMesProyeccionRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PeriodoMesProyeccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PeriodoMesProyeccion Update(PeriodoMesProyeccion entidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoMesProyeccionRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PeriodoMesProyeccion>(modelo);
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
                _unitOfWork.PeriodoMesProyeccionRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PeriodoMesProyeccion> Add(List<PeriodoMesProyeccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoMesProyeccionRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PeriodoMesProyeccion>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PeriodoMesProyeccion> Update(List<PeriodoMesProyeccion> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoMesProyeccionRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PeriodoMesProyeccion>>(modelo);
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
                _unitOfWork.PeriodoMesProyeccionRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<PeriodoMesProyeccion> InsertarPeriodoMesProyeccion(List<PeriodoMesProyeccionDTO> entidad, string Usuario)
        {
            try
            {
                List<PeriodoMesProyeccion> data = new List<PeriodoMesProyeccion>();
                List<PeriodoMesProyeccion> datae = new List<PeriodoMesProyeccion>();
                var datadel = new List<int>();
                var rep = _unitOfWork.PeriodoMesProyeccionRepository;
                foreach (var item in entidad)
                {
                    if (item.Id == 0)
                    {
                        PeriodoMesProyeccion d = _mapper.Map<PeriodoMesProyeccion>(item);
                        d.Id = 0;
                        d.UsuarioModificacion = Usuario;
                        d.UsuarioCreacion = Usuario;
                        d.FechaCreacion = DateTime.Now;
                        d.FechaModificacion = DateTime.Now;
                        d.Estado = true;
                        data.Add(d);
                    }
                    else
                    {
                        if (item.edit == true)
                        {
                            PeriodoMesProyeccion d = _mapper.Map<PeriodoMesProyeccion>(rep.FirstById(item.Id));
                            d.Cantidad = item.Cantidad;
                            d.Periodo = item.Periodo;
                            d.UsuarioModificacion = Usuario;
                            d.FechaModificacion = DateTime.Now;
                            d.Estado = true;
                            datae.Add(d);
                        }
                        else if(item.delete == true)
                        {
                            datadel.Add(item.Id);
                        }
                    }
                }
                var modelo = new List<TPeriodoMesProyeccion>();
                modelo.AddRange(_unitOfWork.PeriodoMesProyeccionRepository.Add(data));
                modelo.AddRange(_unitOfWork.PeriodoMesProyeccionRepository.Update(datae));
                _unitOfWork.PeriodoMesProyeccionRepository.Delete(datadel, Usuario);
                _unitOfWork.Commit();
                
                return _mapper.Map<List< PeriodoMesProyeccion >>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Actuliza datos en la tabla T_PeriodoMesProyeccion
        /// <returns></returns>
        public PeriodoMesProyeccion ActulizarPeriodoMesProyeccion(PeriodoMesProyeccionDTO entidad, string Usuario)
        {
            try
            {
                var rep = _unitOfWork.PeriodoMesProyeccionRepository;
                var entidadActual = _mapper.Map<PeriodoMesProyeccion>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.Periodo = entidad.Periodo;
                entidadActual.Cantidad = entidad.Cantidad;
               
                var modelo = _unitOfWork.PeriodoMesProyeccionRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<PeriodoMesProyeccion>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        /// Autor: Margiory Ramirez
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PeriodoMesProyeccion
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<PeriodoMesProyeccionDTO> ObtenerPeriodoMesProyeccion()
        {
            try
            {
                return _unitOfWork.PeriodoMesProyeccionRepository.ObtenerPeriodoMesProyeccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha: 08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PeriodoMesProyeccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PeriodoMesProyeccionCombo> ObtenerPeriodoMesProyeccionCombo()
        {
            try
            {
                return _unitOfWork.PeriodoMesProyeccionRepository.ObtenerPeriodoMesProyeccionCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


    }
}
