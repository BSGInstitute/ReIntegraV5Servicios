using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Implementacion
{
    public class PerfilPuestoTrabajoPersonalAprobacionService : IPerfilPuestoTrabajoPersonalAprobacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public PerfilPuestoTrabajoPersonalAprobacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PerfilPuestoTrabajoPersonalAprobacion, PerfilPuestoTrabajoPersonalAprobacionDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20-01-2025
        /// <summary>
        /// PerfilPuestoTrabajoPersonalAprobacionService
        /// </summary>
        /// <returns> Lista DatosCombosPersonalAprobacionDTO </returns>
        public DatosCombosPersonalAprobacionDTO ObtenerCombos()
        {

            var listaPersonal = _unitOfWork.PersonalRepository.ObtenerDatosPersonal();
            var listaPuestoTrabajo = _unitOfWork.PuestoTrabajoRepository.ObtenerCombo();
            DatosCombosPersonalAprobacionDTO resultado = new DatosCombosPersonalAprobacionDTO
            {
                ListaPersonal = listaPersonal,
                ListaPuestoTrabajo = listaPuestoTrabajo
            };
            return resultado;

        }
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20-01-2025
        /// <summary>
        /// PerfilPuestoTrabajoPersonalAprobacionService
        /// </summary>
        /// <returns> Lista PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO </returns>
        public IEnumerable<PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO> ObtenerPerfilPuestoTrabajoPersonalAprobacion()
        {

            try
            {
                
                var listaPerfilPuestoTrabajoPersonalAprobacion = _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.ObtenerPersonalConfigurado();
                var resultado = listaPerfilPuestoTrabajoPersonalAprobacion.GroupBy(x => new { x.IdPersonal, x.Personal }).Select(x => new PerfilPuestoTrabajoPersonalAprobacionAgrupadoDTO
                {
                    IdPersonal = x.Key.IdPersonal,
                    Personal = x.Key.Personal,
                    ListaPuestoTrabajo = x.GroupBy(y => y.IdPuestoTrabajo).Select(y => y.Key).ToList(),
                    PuestoTrabajo = x.GroupBy(z => z.PuestoTrabajo).Select(z => z.Key).ToList(),
                }).ToList();
                return resultado;
            }
            catch (Exception)
            {
                throw;
            }

        }
        /// Autor: Villanueva Torres Marco Jose
        /// Fecha: 20-01-2025
        /// Version: 1.0
        /// <summary>
        /// Registra un nuevo TipoFormacion
        /// </summary>
        /// <param name="dto">TipoFormacionDTO</param>
        /// <param name="usuario">Usuario Registro</param>
        /// <returns>TipoFormacionDTO</returns>
        public bool Insertar(PerfilPuestoTrabajoPersonalAprobacionDTO dto, string usuario)
        {   
            try
            {
                foreach (var personal in dto.ListaPersonal)
                {
                    foreach (var puestoTrabajo in dto.ListaPuestoTrabajo)
                    {
                        PerfilPuestoTrabajoPersonalAprobacion configuracionPersonal;
                        //configuracionPersonal = _mapper.Map<PerfilPuestoTrabajoPersonalAprobacion>(_unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.FirstBy(x => x.IdPersonal == personal && x.IdPuestoTrabajo == puestoTrabajo));
                        configuracionPersonal = _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.ObtenerPorIdPersonalAndIdPuestoTrabajo(personal, puestoTrabajo);
                        if (configuracionPersonal != null)
                        {
                            configuracionPersonal.UsuarioModificacion = usuario;
                            configuracionPersonal.FechaModificacion = DateTime.Now;
                            _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.Update(configuracionPersonal);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            configuracionPersonal = new PerfilPuestoTrabajoPersonalAprobacion();
                            configuracionPersonal.IdPersonal = personal;
                            configuracionPersonal.IdPuestoTrabajo = puestoTrabajo;
                            configuracionPersonal.Estado = true;
                            configuracionPersonal.UsuarioCreacion = usuario;
                            configuracionPersonal.UsuarioModificacion = usuario;
                            configuracionPersonal.FechaCreacion = DateTime.Now;
                            configuracionPersonal.FechaModificacion = DateTime.Now;
                            _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.Add(configuracionPersonal);
                            _unitOfWork.Commit();
                        }
                    }
                }
              
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Metodo Actualizar
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 15/04/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una inserción o actualizacion basica a la tabla y sus detalles
        /// </summary>   
        /// <param name="PerfilPuestoTrabajoPersonalAprobacion"> parametros de la nueva PerfilPuestoTrabajoPersonalAprobacion y sus detalles </param>

        public bool Actualizar(PerfilPuestoTrabajoPersonalAprobacionDTO dto, string usuario)
        {
            try
            {
                foreach (var personal in dto.ListaPersonal)
                {
                    //var configuracionPersonal = _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.GetBy(x => x.IdPersonal == personal).ToList();
                    var configuracionPersonal = _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.ObtenerbyIdPersonal(personal);
                    foreach (var conf in configuracionPersonal)
                    {
                        var listaConfiguracion = dto.ListaPersonal.Contains(personal) && dto.ListaPuestoTrabajo.Contains(conf.IdPuestoTrabajo);
                        if (!listaConfiguracion)
                        {
                            _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.Delete(conf.Id, usuario);
                            _unitOfWork.Commit() ;
                        }
                    }
                }

                var res = Insertar(dto,usuario);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Metodo Eliminar.
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 20-01-2025
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(EliminarPuestoTrabajoDTO dto, string usuario)
        {
            try
            {

                var configuracion = _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.ObtenerbyIdPersonal(dto.IdPersonal);
                var listaEliminar = configuracion
                    .Where(x => dto.IdsPuestoTrabajo.Contains(x.IdPuestoTrabajo))
                    .ToList();

                _unitOfWork.PerfilPuestoTrabajoPersonalAprobacionRepository.Delete(listaEliminar.Select(X => X.Id), usuario);
                _unitOfWork.Commit();


                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
