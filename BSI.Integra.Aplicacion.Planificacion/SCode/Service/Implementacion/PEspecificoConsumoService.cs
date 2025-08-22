using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: PEspecificoConsumoService
    /// Autor: Jonathan Caipo
    /// Fecha: 09/06/2023
    /// <summary>
    /// Gestión general de T_PespecificoConsumo
    /// </summary>
    public class PEspecificoConsumoService : IPEspecificoConsumoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PEspecificoConsumoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPespecificoConsumo, PEspecificoConsumo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta sesiones FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool InsertarFurSesiones(List<PEspecificoConsumoDTO> dto, string usuario)
        {
            try
            {
                if (dto == null || dto.Count() == 0)
                {
                    return false;
                }
                var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(dto.FirstOrDefault()!.IdProducto, dto.FirstOrDefault()!.IdProveedor);
                if (detalleFur == null)
                {
                    throw new BadRequestException("No existe detalle FUR");
                }
                var producto = _unitOfWork.ProductoRepository.ObtenerPorId(dto.FirstOrDefault()!.IdProducto);
                if (producto == null || producto.Id == 0)
                {
                    throw new BadRequestException("Producto no existente");
                }
                var planContable = _unitOfWork.PlanContableRepository.ObtenerPlanContablePorCuenta(long.Parse(producto.CuentaGeneral));
                if (planContable == null || planContable.Id == 0)
                {
                    throw new BadRequestException("Plan contable no existente");
                }

                foreach (var item in dto)
                {
                    PEspecificoConsumo pEspecificoConsumo = new PEspecificoConsumo()
                    {
                        IdPespecificoSesion = item.IdPespecificoSesion,
                        IdHistoricoProductoProveedor = item.IdHistoricoProductoProveedor,
                        Cantidad = item.Cantidad,
                        Factor = item.Factor,
                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    _unitOfWork.PEspecificoConsumoRepository.Add(pEspecificoConsumo);

                    int numeroSemana = _unitOfWork.FurRepository.ObtenerNumeroSemana(item.FechaHoraInicio);

                    Fur fur = new Fur();
                    fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                    fur.IdPespecifico = item.IdPespecifico;
                    fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                    fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                    fur.Descripcion = planContable.Descripcion;
                    fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * item.Cantidad);
                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * item.Cantidad);
                    fur.FechaLimite = item.FechaHoraInicio;
                    fur.IdCiudad = item.Ciudad;
                    fur.IdEmpresa = item.IdEmpresa;
                    fur.IdCentroCosto = item.IdCentroCosto;
                    fur.IdProveedor = item.IdProveedor;
                    fur.Cuenta = detalleFur.CuentaDescripcion;
                    fur.IdProducto = item.IdProducto;
                    fur.NumeroSemana = numeroSemana;
                    fur.Cantidad = item.Cantidad;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.UsuarioSolicitud = usuario;
                    fur.Estado = true;
                    fur.UsuarioCreacion = usuario;
                    fur.UsuarioModificacion = usuario;
                    fur.FechaCreacion = DateTime.Now;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                    fur.IdFurFaseAprobacion1 = 1;
                    fur.IdPersonalAreaTrabajo = item.AreaTrabajo;
                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * item.Cantidad;
                    fur.PagoDolares = detalleFur.PrecioDolares * item.Cantidad;
                    fur.Antiguo = 0;
                    fur.OcupadoSolicitud = false;
                    fur.OcupadoRendicion = false;
                    fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                    _unitOfWork.FurRepository.Add(fur);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Inserta Porgrama FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool InsertarFurPrograma(FurProgramaDTO dto, string usuario)
        {
            try
            {
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorId(dto.IdPespecifico);
                if (programaEspecifico == null || programaEspecifico.Id == 0)
                {
                    throw new BadRequestException($"Programa especifico no existente {dto.IdPespecifico}");
                }
                if (programaEspecifico.IdSesionInicio == null || programaEspecifico.IdSesionInicio == 0)
                {
                    throw new BadRequestException($"Programa especifico sin sesion inicio {dto.IdPespecifico}");
                }
                var pespecificoSesionInicio = _unitOfWork.PEspecificoSesionRepository.ObtenerPorId(programaEspecifico.IdSesionInicio.Value);
                var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(dto.IdProducto, dto.IdProveedor);
                if (pespecificoSesionInicio == null || pespecificoSesionInicio.Id == 0)
                {
                    throw new BadRequestException($"Sesion de inicio no existente");
                }
                var semana = _unitOfWork.FurRepository.ObtenerNumeroSemana(pespecificoSesionInicio.FechaHoraInicio);
                var producto = _unitOfWork.ProductoRepository.ObtenerPorId(dto.IdProducto);
                var planContable = _unitOfWork.PlanContableRepository.ObtenerPlanContablePorCuenta(long.Parse(producto.CuentaGeneral));

                Fur fur = new();
                fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                fur.IdPespecifico = dto.IdPespecifico;
                fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                fur.NumeroCuenta = detalleFur.NumeroCuenta;
                fur.Descripcion = planContable.Descripcion;
                fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * dto.Cantidad);
                fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * dto.Cantidad);
                fur.FechaLimite = pespecificoSesionInicio.FechaHoraInicio;
                fur.IdCiudad = dto.Ciudad;
                fur.IdEmpresa = dto.IdEmpresa;
                fur.IdCentroCosto = programaEspecifico.IdCentroCosto;
                fur.IdProveedor = dto.IdProveedor;
                fur.Cuenta = detalleFur.CuentaDescripcion;
                fur.IdProducto = dto.IdProducto;
                fur.NumeroSemana = semana;
                fur.Cantidad = dto.Cantidad;
                fur.Descripcion = detalleFur.Descripcion;
                fur.UsuarioSolicitud = usuario;
                fur.Estado = true;
                fur.UsuarioCreacion = usuario;
                fur.UsuarioModificacion = usuario;
                fur.FechaCreacion = DateTime.Now;
                fur.FechaModificacion = DateTime.Now;
                fur.IdMonedaProveedor = detalleFur.IdMoneda;
                fur.IdFurFaseAprobacion1 = 1;
                fur.IdPersonalAreaTrabajo = dto.AreaTrabajo;
                fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                fur.Monto = fur.PrecioTotalMonedaOrigen;
                fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * dto.Cantidad;
                fur.PagoDolares = detalleFur.PrecioDolares * dto.Cantidad;
                fur.Antiguo = 0;
                fur.OcupadoSolicitud = false;
                fur.OcupadoRendicion = false;
                fur.IdMonedaPagoRealizado = detalleFur.IdMoneda;
                _unitOfWork.FurRepository.Add(fur);
                _unitOfWork.Commit();
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Elimina sesión FUR
        /// </summary>
        /// <param name="idFurSesion"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public bool EliminarSesionFur(int idFurSesion, string usuario)
        {
            try
            {
                if (_unitOfWork.FurRepository.Exist(idFurSesion))
                {
                    _unitOfWork.FurRepository.Delete(idFurSesion, usuario);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 09/06/2023
		/// Version: 1.0
        /// <summary>
        /// Actualiza sesión FUR
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool ActualizarSesionFur(FurSesionFiltroDTO dto, string usuario)
        {
            try
            {
                var producto = _unitOfWork.ProductoRepository.ObtenerPorId(dto.IdProducto).Nombre;
                var proveedor = _unitOfWork.ProveedorRepository.ObtenerPorId(dto.IdProveedor);
                var proveedorNombre = string.Concat(proveedor.RazonSocial, " ", proveedor.Nombre1, " ", proveedor.Nombre2, " ", proveedor.ApePaterno, " ", proveedor.ApeMaterno).Trim();
                var detalleFur = _unitOfWork.HistoricoProductoProveedorRepository.ObtenerDetalleFUR(dto.IdProducto, dto.IdProveedor);
                var fur = _unitOfWork.FurRepository.ObtenerPorId(dto.Id);

                if (fur != null)
                {
                    fur.IdFurTipoPedido = detalleFur.IdFurTipoPedido;
                    fur.IdProductoPresentacion = detalleFur.IdProductoPresentacion;
                    fur.IdMonedaPagoReal = detalleFur.IdMoneda;
                    fur.NumeroCuenta = detalleFur.NumeroCuenta;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.PrecioUnitarioMonedaOrigen = detalleFur.Precio;
                    fur.PrecioTotalMonedaOrigen = Convert.ToDecimal(detalleFur.Precio * dto.Cantidad);
                    fur.PrecioUnitarioDolares = detalleFur.PrecioDolares;
                    fur.PrecioTotalDolares = Convert.ToDecimal(detalleFur.PrecioDolares * dto.Cantidad);
                    fur.IdCiudad = dto.Ciudad;
                    fur.IdEmpresa = dto.IdEmpresa;
                    fur.IdProveedor = dto.IdProveedor;
                    fur.Cuenta = detalleFur.CuentaDescripcion;
                    fur.IdProducto = dto.IdProducto;
                    fur.NumeroSemana = dto.Semana;
                    fur.Cantidad = dto.Cantidad;
                    fur.Descripcion = detalleFur.Descripcion;
                    fur.UsuarioModificacion = usuario;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdMonedaProveedor = detalleFur.IdMoneda;
                    fur.IdFurFaseAprobacion1 = 1;
                    fur.IdPersonalAreaTrabajo = dto.AreaTrabajo;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    fur.PagoMonedaOrigen = detalleFur.PrecioOrigen * dto.Cantidad;
                    fur.PagoDolares = detalleFur.PrecioDolares * dto.Cantidad;
                    fur.MontoProyectado = fur.PrecioTotalMonedaOrigen;
                    fur.Monto = fur.PrecioTotalMonedaOrigen;
                    _unitOfWork.FurRepository.Update(fur);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
