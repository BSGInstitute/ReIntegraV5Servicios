using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PGeneralTipoDescuentoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_PGeneralTipoDescuento
    /// </summary>
    public class PGeneralTipoDescuentoService : IPGeneralTipoDescuentoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PGeneralTipoDescuentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralTipoDescuento, PGeneralTipoDescuento>(MemberList.None).ReverseMap();
                cfg.CreateMap<PGeneralTipoDescuento, TipoDescuentoProgramaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPgeneralTipoDescuento, TipoDescuentoProgramaDTO>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Flag Promocion asociado a un Programa General y un Tipo Descuento
        /// </summary>
        /// <param name="idPGeneral">Id del Programa General</param>
        /// <param name="idTipoDescuento">Id del Tipo Descuento</param>
        /// <returns> BoolDTO </returns>
        public BoolDTO ObtenerFlagPromocion(int idPGeneral, int idTipoDescuento)
        {
            try
            {
                return _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerFlagPromocion(idPGeneral, idTipoDescuento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene por id todos los ProgramasPorDescuento
        /// </summary>
        /// <param name="IdTipoDescuento"></param>
        /// <returns></returns>
        public IEnumerable<int> ObtenerProgramaPorDescuento(int IdTipoDescuento)
        {
            return _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerProgramaPorDescuento(IdTipoDescuento);
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos los Descuentos asociados a un TipoDescuento por programa
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns></returns>
        public void EliminacionLogicoTipoDescuento(int IdTipoDescuento, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerPorId(IdTipoDescuento).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y.Equals(x.IdPgeneral)));
                _unitOfWork.PGeneralTipoDescuentoRepository.Delete(listaBorrar.Select(x => x.Id).ToList(), usuario);
                _unitOfWork.Commit();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Klebert Layme
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Asocia un tipo descuento programa en PGeneralTipoDescuento
        /// </summary>
        /// <param name="id">Id del area de trabajo</param>
        /// <returns> TipoDescuentoProgramaDTO </returns>
        public bool AsociarPrograma(TipoDescuentoProgramaDTO dto, string usuario)
        {
            try
            {
                PGeneralTipoDescuento descuento = new();
                if (dto != null)
                {
                    var descuentosPromocion = _unitOfWork.DescuentoPromocionRepository.Obtener();
                    var descuentoPGeneral = _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerPorId(dto.IdTipoDescuento);

                    IPGeneralTipoDescuentoService pGeneralTipoDescuentoService = new PGeneralTipoDescuentoService(_unitOfWork);
                    pGeneralTipoDescuentoService.EliminacionLogicoTipoDescuento(dto.IdTipoDescuento, usuario, dto.IdPgeneral);

                    foreach (var item in dto.IdPgeneral)
                    {
                        bool flag = (descuentosPromocion.Any(x => x.IdTipoDescuento == dto.IdTipoDescuento));

                        if (_unitOfWork.PGeneralTipoDescuentoRepository.Exist(x => x.IdTipoDescuento == dto.IdTipoDescuento && x.IdPgeneral == item))
                        {
                            descuento = descuentoPGeneral.FirstOrDefault(x => x.IdPgeneral == item);
                            descuento.FlagPromocion = flag;
                            descuento.UsuarioModificacion = usuario;
                            descuento.FechaModificacion = DateTime.Now;
                            _unitOfWork.PGeneralTipoDescuentoRepository.Update(descuento);
                            _unitOfWork.Commit();
                        }
                        else
                        {
                            descuento = new PGeneralTipoDescuento();
                            descuento.IdPgeneral = item;
                            descuento.IdTipoDescuento = dto.IdTipoDescuento;
                            descuento.FlagPromocion = flag;
                            descuento.UsuarioCreacion = usuario;
                            descuento.UsuarioModificacion = usuario;
                            descuento.FechaCreacion = DateTime.Now;
                            descuento.FechaModificacion = DateTime.Now;
                            descuento.Estado = true;
                            _unitOfWork.PGeneralTipoDescuentoRepository.Add(descuento);
                            _unitOfWork.Commit();
                        }
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 31/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los ids de los descuentos por programa
        /// </summary>
        /// <param name="idPGeneral">Id del programa general</param>
        /// <returns> Lista de ids </returns>
        public List<int> ObtenerDescuentosPorPrograma(int idPGeneral)
        {
            try
            {
                return _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerPorIdPGeneral(idPGeneral).Select(x => x.IdTipoDescuento.Value).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 10/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los ids de los descuentos por programa
        /// </summary>
        /// <param name="jsonDTO">Objeto programa tipo descuento</param>
        /// <param name="usuario">usuario registro</param>
        /// <returns> Lista de ids </returns>
        public bool AsociarDescuentos(ProgramaTipoDescuentoDTO jsonDTO, string usuario)
        {
            try
            {
                var descuentosPromocion = _unitOfWork.DescuentoPromocionRepository.Obtener();
                using (TransactionScope scope = new TransactionScope())
                {
                    var listaBorrar = _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerPorIdPGeneral(jsonDTO.IdPgeneral).ToList();
                    listaBorrar.RemoveAll(x => jsonDTO.Descuentos.Any(y => y == x.IdTipoDescuento));
                    if (listaBorrar != null && listaBorrar.Count() > 0)
                    {
                        _unitOfWork.PGeneralTipoDescuentoRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                        _unitOfWork.Commit();
                    }
                    var listaInsertar = new List<PGeneralTipoDescuento>();
                    var listaActualizar = new List<PGeneralTipoDescuento>();
                    foreach (var idTipoDescuento in jsonDTO.Descuentos)
                    {
                        bool flag = false;
                        if (descuentosPromocion.Any(x => x.IdTipoDescuento == idTipoDescuento))
                        {
                            flag = true;
                        }
                        PGeneralTipoDescuento descuento;
                        if (_unitOfWork.PGeneralTipoDescuentoRepository.Exist(x => x.IdTipoDescuento == idTipoDescuento && x.IdPgeneral == jsonDTO.IdPgeneral))
                        {
                            descuento = _unitOfWork.PGeneralTipoDescuentoRepository.ObtenerPorIdTipoDescuentoYIdPGeneral(jsonDTO.IdPgeneral, idTipoDescuento)!;
                            if (descuento.FlagPromocion != flag)
                            {
                                descuento.IdPgeneral = jsonDTO.IdPgeneral;
                                descuento.IdTipoDescuento = idTipoDescuento;
                                descuento.FlagPromocion = flag;
                                descuento.UsuarioModificacion = usuario;
                                descuento.FechaModificacion = DateTime.Now;
                                listaActualizar.Add(descuento);
                            }
                        }
                        else
                        {
                            descuento = new PGeneralTipoDescuento()
                            {
                                IdPgeneral = jsonDTO.IdPgeneral,
                                IdTipoDescuento = idTipoDescuento,
                                FlagPromocion = flag,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                Estado = true
                            };
                            listaInsertar.Add(descuento);
                        }
                    }
                    if (listaInsertar.Count() > 0)
                    {
                        _unitOfWork.PGeneralTipoDescuentoRepository.Add(listaInsertar);
                        _unitOfWork.Commit();
                    }
                    if (listaActualizar.Count() > 0)
                    {
                        _unitOfWork.PGeneralTipoDescuentoRepository.Update(listaActualizar);
                        _unitOfWork.Commit();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
