using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Net;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ContadorBicService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 29/08/2023
    /// <summary>
    /// Gestión general de ContadorBic
    /// </summary>
    public class ContadorBicService : IContadorBicService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private const int _DiasMexico = 3;
        private const int _DiasChile = 3;
        private const int _DiasColombia = 3;

        private const int _ConteoBic1 = 3;
        private const int _ConteoBic2 = 5;


        private List<Oportunidad> _OportunidadesBic = new();
        public ContadorBicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContadorBic, ContadorBic>(MemberList.None).ReverseMap();
                cfg.CreateMap<TContadorBic, ContadorBicDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ContadorBicDTO, ContadorBic>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public int CalcularDiasParaBIC()
        {
            try
            {
                var listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasContador();
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => o.Id)
                            .Select(grp => new { OporId = grp.Key, ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList() })
                            .ToList();

                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                List<ContadorBic> contadorBics = new List<ContadorBic>();

                foreach (var configuracion in listaConfiguracionBIC)
                {
                    int dias = configuracion.Dias;

                    var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    foreach (var bloque in bloques)
                    {
                        bloque.Contador = 0;
                    }
                    foreach (var oportunidad in oportunidadesAgrupadas)
                    {
                        var fechaActual = DateTime.Now;
                        var bloqueInicio = bloques.FirstOrDefault(x => x.Nombre == "Mañana")!.HoraInicio;
                        var fechaInicioContado = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, bloqueInicio.Hours, bloqueInicio.Minutes, bloqueInicio.Seconds);
                        List<DateTime> fechas = oportunidad.ListaFechas.Where(s => s < fechaInicioContado).ToList();
                        var nombreTurnoUltimo = "";
                        DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                        foreach (var fecha in fechas)
                        {
                            TimeSpan horaFecha = fecha.TimeOfDay;
                            foreach (var bloque in bloques)
                            {
                                if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                {
                                    if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                                    else
                                    {
                                        nombreTurnoUltimo = bloque.Nombre;
                                        fechaUltima = fecha.Date;
                                        bloque.Contador++;
                                        break;
                                    }
                                }
                            }
                        }
                        ContadorBic contadorBic = new();
                        foreach (var bloque in bloques)
                        {
                            if (bloque.Nombre == "Mañana") contadorBic.DiasSinContactoManhana = bloque.Contador ?? 0;
                            else contadorBic.DiasSinContactoTarde = bloque.Contador ?? 0;
                            bloque.Contador = 0;
                        }
                        try
                        {
                            int idOportunidad = int.Parse(oportunidad.OporId);
                            contadorBic.IdOportunidad = idOportunidad;
                            contadorBic.Estado = true;
                            contadorBic.FechaCreacion = DateTime.Now;
                            contadorBic.FechaModificacion = DateTime.Now;
                            contadorBic.UsuarioCreacion = "EjecutarBicV5";
                            contadorBic.UsuarioModificacion = "EjecutarBicv5";
                            contadorBics.Add(contadorBic);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                try
                {
                    _unitOfWork.ContadorBicRepository.Add(contadorBics);
                    _unitOfWork.Commit();
                }
                catch (Exception e)
                {
                    return 0;
                }
                return contadorBics.Count();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 14/08/2025
        /// Versión: 1.0
        /// <summary>
        /// Genera un nuevo calculo de contador bic alterno, si ya se realizó el calculo no lo vuelve a realizar
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public StringDTO CalcularDiasParaBICAlterno()
        {

            StringDTO resultado = new StringDTO();
            bool nuevoCalculo = _unitOfWork.ContadorBicRepository.ObtenerNuevoCalculoDiasBic();
            if (nuevoCalculo == true)
            {

                int cantidadDatosProcesados = CalcularDiasParaBIC();
                resultado.Valor = "Proceso finalizado con exito. Se procesaron " + cantidadDatosProcesados + " registros.";
            }
            else
            {
                resultado.Valor = "Ya se realizó el calculo de bics para hoy.";
        }
            return resultado;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<ContadorBic> CalcularDiasParaBICPorIdOportunidad(int idOportunidad)
        {
            try
            {
                ContadorBic contadorBic;
                var listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasPorIdOportunida(idOportunidad);
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => o.Id)
                            .Select(grp => new { OporId = grp.Key, ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList() })
                            .ToList();

                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                List<ContadorBic> idsOportunidades = new List<ContadorBic>();

                foreach (var configuracion in listaConfiguracionBIC)
                {
                    int dias = configuracion.Dias;

                    var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    foreach (var bloque in bloques)
                    {
                        bloque.Contador = 0;
                    }
                    foreach (var oportunidad in oportunidadesAgrupadas)
                    {
                        List<DateTime> fechas = oportunidad.ListaFechas;
                        var nombreTurnoUltimo = "";
                        DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                        foreach (var fecha in fechas)
                        {
                            TimeSpan horaFecha = fecha.TimeOfDay;
                            foreach (var bloque in bloques)
                            {
                                if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                {
                                    if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                                    else
                                    {
                                        nombreTurnoUltimo = bloque.Nombre;
                                        fechaUltima = fecha.Date;
                                        bloque.Contador++;
                                        break;
                                    }
                                }
                            }
                        }
                        contadorBic = new();
                        foreach (var bloque in bloques)
                        {
                            if (bloque.Nombre == "Mañana") contadorBic.DiasSinContactoManhana = bloque.Contador ?? 0;
                            else contadorBic.DiasSinContactoTarde = bloque.Contador ?? 0;
                            bloque.Contador = 0;
                        }
                        try
                        {
                            //int idOportunidad = ;
                            contadorBic.IdOportunidad = int.Parse(oportunidad.OporId);
                            contadorBic.Estado = true;
                            contadorBic.FechaCreacion = DateTime.Now;
                            contadorBic.FechaModificacion = DateTime.Now;
                            contadorBic.UsuarioCreacion = "EjecutarBic";
                            contadorBic.UsuarioModificacion = "EjecutarBic";
                            idsOportunidades.Add(contadorBic);
                            //_unitOfWork.ContadorBicRepository.Add(contadorBic);
                            //_unitOfWork.Commit();
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                return idsOportunidades;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el calculo de contador bic
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<ContadorBic> CalcularDiasParaBICPorIdOportunidadWhatsapp(int idOportunidad)
        {
            try
            {
                ContadorBic contadorBic;
                var listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasPorIdOportunidadWhatsapp(idOportunidad);
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => o.Id)
                            .Select(grp => new { OporId = grp.Key, ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList() })
                            .ToList();

                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                List<ContadorBic> idsOportunidades = new List<ContadorBic>();

                foreach (var configuracion in listaConfiguracionBIC)
                {
                    int dias = configuracion.Dias;

                    var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    foreach (var bloque in bloques)
                    {
                        bloque.Contador = 0;
                    }
                    foreach (var oportunidad in oportunidadesAgrupadas)
                    {
                        List<DateTime> fechas = oportunidad.ListaFechas;
                        var nombreTurnoUltimo = "";
                        DateTime fechaUltima = new DateTime(2019, 1, 1).Date;
                        foreach (var fecha in fechas)
                        {
                            TimeSpan horaFecha = fecha.TimeOfDay;
                            foreach (var bloque in bloques)
                            {
                                if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                {
                                    if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date)) break;
                                    else
                                    {
                                        nombreTurnoUltimo = bloque.Nombre;
                                        fechaUltima = fecha.Date;
                                        bloque.Contador++;
                                        break;
                                    }
                                }
                            }
                        }
                        contadorBic = new();
                        foreach (var bloque in bloques)
                        {
                            if (bloque.Nombre == "Mañana") contadorBic.DiasSinContactoManhana = bloque.Contador ?? 0;
                            else contadorBic.DiasSinContactoTarde = bloque.Contador ?? 0;
                            bloque.Contador = 0;
                        }
                        try
                        {
                            //int idOportunidad = ;
                            contadorBic.IdOportunidad = int.Parse(oportunidad.OporId);
                            contadorBic.Estado = true;
                            contadorBic.FechaCreacion = DateTime.Now;
                            contadorBic.FechaModificacion = DateTime.Now;
                            contadorBic.UsuarioCreacion = "EjecutarBic";
                            contadorBic.UsuarioModificacion = "EjecutarBic";
                            idsOportunidades.Add(contadorBic);
                            //_unitOfWork.ContadorBicRepository.Add(contadorBic);
                            //_unitOfWork.Commit();
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }
                return idsOportunidades;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<int> EjecutarBIC(int idPaisSede)
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> listaOportunidades = new();
                if (idPaisSede == 0)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasPeru();
                }
                else if (idPaisSede == 56)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasChile();
                }
                else if (idPaisSede == 52)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasMexico();
                }
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => new
                {
                    o.Id,
                    o.IdCodigoPais,
                    o.IdPersonal_Asignado,
                    o.IdFaseOportunidad
                }).Select(grp => new OportunidadAgrupadaDTO
                {
                    IdOportunidad = int.Parse((grp.Key.Id)),
                    ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList(),
                    IdPais = grp.Key.IdCodigoPais,
                    IdPersonalAsignado = grp.Key.IdPersonal_Asignado,
                    IdFaseOportunidad = grp.Key.IdFaseOportunidad
                }).ToList();
                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                var configuracionBicPersonal = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicPersonal();
                List<int> idsOportunidadesACerrar = new List<int>();
                List<ContadorBicLog> contadoresNuevos = new();
                List<ContadorBicLog> contadoresActualizar = new();
                try
                {
                    foreach (var configuracion in listaConfiguracionBIC)
                    {
                        int dias = configuracion.Dias;

                        var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                        foreach (var bloque in bloques)
                        {
                            bloque.Contador = 0;
                        }

                        foreach (var itemOportunidad in oportunidadesAgrupadas.ToList())
                        {
                            List<DateTime> fechas = itemOportunidad.ListaFechas;

                            var nombreTurnoUltimo = "";
                            DateTime fechaUltima = new DateTime(2019, 1, 1).Date;

                            ContadorBicLog? contadorBicLog = _unitOfWork.ContadorBicLogRepository.ObtenerUltimoRegistroPorIdOportunidad(itemOportunidad.IdOportunidad);
                            if (contadorBicLog != null)
                            {
                                contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                                contadorBicLog.FechaCalculo = DateTime.Now;
                                contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                                contadorBicLog.FechaModificacion = DateTime.Now;
                                contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                            }
                            else
                            {
                                contadorBicLog = new();
                                contadorBicLog.IdOportunidad = itemOportunidad.IdOportunidad;
                                contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                                contadorBicLog.FechaCalculo = DateTime.Now;
                                contadorBicLog.Estado = true;
                                contadorBicLog.UsuarioCreacion = "ContadorBic_v5";
                                contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                                contadorBicLog.FechaCreacion = DateTime.Now;
                                contadorBicLog.FechaModificacion = DateTime.Now;
                                contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                            }
                            foreach (var fecha in fechas)
                            {
                                TimeSpan horaFecha = fecha.TimeOfDay;
                                foreach (var bloque in bloques)
                                {
                                    if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                    {
                                        if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date))
                                            break;
                                        else
                                        {
                                            nombreTurnoUltimo = bloque.Nombre;
                                            fechaUltima = fecha.Date;
                                            bloque.Contador++;

                                            var itemLogDetalle = contadorBicLog.ContadorBicLogDetalles.FirstOrDefault(s => s.FechaLogContacto.Date == fechaUltima);
                                            if (itemLogDetalle == null)
                                            {
                                                itemLogDetalle = new ContadorBicLogDetalle();
                                                itemLogDetalle.Estado = true;
                                                itemLogDetalle.UsuarioCreacion = "ContadorBic_v5";
                                                itemLogDetalle.UsuarioModificacion = "ContadorBic_v5";
                                                itemLogDetalle.FechaCreacion = DateTime.Now;
                                                itemLogDetalle.FechaModificacion = DateTime.Now;
                                                itemLogDetalle.FechaLogContacto = fechaUltima;
                                                itemLogDetalle.EstadoContactoManhana = false;
                                                itemLogDetalle.EstadoContactoTarde = false;
                                                if (bloque.Nombre == "Mañana")
                                                    itemLogDetalle.EstadoContactoManhana = true;
                                                else
                                                    itemLogDetalle.EstadoContactoTarde = true;
                                                contadorBicLog.ContadorBicLogDetalles.Add(itemLogDetalle);
                                            }
                                            else
                                            {
                                                itemLogDetalle.FechaLogContacto = fechaUltima;
                                                if (bloque.Nombre == "Mañana")
                                                    itemLogDetalle.EstadoContactoManhana = true;
                                                else
                                                    itemLogDetalle.EstadoContactoTarde = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            /* Inicio Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                            bool convertirABic = false;
                            foreach (var bloque in bloques)
                            {
                                if (bloque.Nombre == "Mañana")
                                    contadorBicLog.SinContactoManhana = bloque.Contador ?? 0;
                                else
                                    contadorBicLog.SinContactoTarde = bloque.Contador ?? 0;

                                //Casos especiales para el conteo de dias para BIC
                                if ((!itemOportunidad.IdPais.HasValue && bloque.Contador >= dias)
                                    || (
                                        itemOportunidad.IdPais.HasValue
                                        && (
                                            (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisMexico && bloque.Contador >= _DiasMexico)
                                            || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisChile && bloque.Contador >= _DiasChile)
                                            || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisColombia && bloque.Contador >= _DiasColombia)
                                            || (itemOportunidad.IdPais.Value != ValorEstatico.IdPaisMexico
                                                && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisChile
                                                && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisColombia
                                                && bloque.Contador >= dias)
                                            )
                                        )
                                    )
                                {
                                    convertirABic = true;
                                }
                                if (convertirABic == false && configuracionBicPersonal.Count() > 0)
                                {
                                    foreach (var item in configuracionBicPersonal)
                                    {
                                        var listaAsesores = item.IdAsesores.Split(',');
                                        foreach (var item1 in listaAsesores)
                                        {
                                            if (item.IdPais != null)
                                            {
                                                if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                                                    && bloque.Contador >= item.DiasBic
                                                    && item.IdPais == itemOportunidad.IdPais)
                                                {
                                                    convertirABic = true;
                                                }
                                            }
                                            else
                                            {
                                                if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                                                    && bloque.Contador >= item.DiasBic)
                                                {
                                                    convertirABic = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                //if (itemOportunidad.IdPersonalAsignado == 5213 && bloque.Contador >= 4)
                                //{
                                //    convertirABic = true;
                                //} //Casos especiales del pilot de JC para que al tercer dia se cierren a BIC
                                //if ((itemOportunidad.IdPersonalAsignado == 4707 && bloque.Contador >= 3) //Para asesores seleccionados
                                //   || (itemOportunidad.IdPersonalAsignado == 5112 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5190 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5294 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5546 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 4477 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 4567 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5544 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5454 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5228 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5636 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 4363 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5110 && bloque.Contador >= 3)
                                //   || (itemOportunidad.IdPersonalAsignado == 5305 && bloque.Contador >= 3))
                                //{
                                //    convertirABic = true;
                                //}
                                bloque.Contador = 0;
                            }
                            /* Fin Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                            if (convertirABic)
                            {
                                try
                                {
                                    ProcesoConvertiBic(itemOportunidad.IdOportunidad, "CerradoBIC");
                                    idsOportunidadesACerrar.Add(itemOportunidad.IdOportunidad);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (contadorBicLog.Id != 0)
                                    {
                                        contadoresActualizar.Add(contadorBicLog);
                                    }
                                    else
                                    {
                                        contadoresNuevos.Add(contadorBicLog);
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (_OportunidadesBic.Count() > 0)
                    {
                        _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                try
                {
                    if (contadoresActualizar.Count() > 0)
                    {
                        var ids = contadoresActualizar.Select(x => x.Id).ToList();
                        var detalle = _unitOfWork.ContadorBicLogDetalleRepository.ObtenerPorIdsContadorLog(ids);
                        if (detalle.Count() > 0)
                        {
                            _unitOfWork.ContadorBicLogDetalleRepository.Delete(detalle.Select(x => x.Id), "ContadorBic_v5");
                            _unitOfWork.Commit();
                        }
                        _unitOfWork.ContadorBicLogRepository.Update(contadoresActualizar);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                try
                {
                    if (contadoresNuevos.Count() > 0)
                    {
                        _unitOfWork.ContadorBicLogRepository.Add(contadoresNuevos);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                /*Se omite nueva logica de cierre bic por ordenes de gerencia (2025-08-14)*/
                /*_OportunidadesBic = new List<Oportunidad>();
                List<OportunidadContadorBicDTO> oportunidadContadorBics = _unitOfWork.ContadorBicRepository.ObtenerDatosParaBicPorPaisNuevaLogica(idPaisSede);
                foreach (var oportunidadContadorBic in oportunidadContadorBics)
                {
                    ProcesoConvertiBic(oportunidadContadorBic.IdOportunidad, "CerradoBIC-NL");
                    idsOportunidadesACerrar.Add(oportunidadContadorBic.IdOportunidad);

                }
                if (_OportunidadesBic != null && _OportunidadesBic.Count() > 0)
                {
                    _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                    _unitOfWork.Commit();
                }
                */
                return idsOportunidadesACerrar;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<int> EjecutarBIC1(int idPaisSede)
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> listaOportunidades = new();
                if (idPaisSede == 0)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasPeruBic1();
                }
                else if (idPaisSede == 56)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasChileBic1();
                }
                else if (idPaisSede == 52)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasMexicoBic1();
                }
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => new
                {
                    o.Id,
                    o.IdCodigoPais,
                    o.IdPersonal_Asignado,
                    o.IdFaseOportunidad
                }).Select(grp => new OportunidadAgrupadaDTO
                {
                    IdOportunidad = int.Parse((grp.Key.Id)),
                    ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList(),
                    IdPais = grp.Key.IdCodigoPais,
                    IdPersonalAsignado = grp.Key.IdPersonal_Asignado,
                    IdFaseOportunidad = grp.Key.IdFaseOportunidad
                }).ToList();
                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                var configuracionBicPersonal = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicPersonal();
                List<int> idsOportunidadesACerrar = new List<int>();
                List<ContadorBicLog> contadoresNuevos = new();
                List<ContadorBicLog> contadoresActualizar = new();
                try
                {
                    //int dias = configuracion.Dias;

                    //var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                    //foreach (var bloque in bloques)
                    //{
                    //    bloque.Contador = 0;
                    //}

                    foreach (var itemOportunidad in oportunidadesAgrupadas.ToList())
                    {
                        //List<DateTime> fechas = itemOportunidad.ListaFechas;

                        //var nombreTurnoUltimo = "";
                        //DateTime fechaUltima = new DateTime(2019, 1, 1).Date;

                        //ContadorBicLog? contadorBicLog = _unitOfWork.ContadorBicLogRepository.ObtenerUltimoRegistroPorIdOportunidad(itemOportunidad.IdOportunidad);
                        //if (contadorBicLog != null)
                        //{
                        //    contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                        //    contadorBicLog.FechaCalculo = DateTime.Now;
                        //    contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                        //    contadorBicLog.FechaModificacion = DateTime.Now;
                        //    contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                        //}
                        //else
                        //{
                        //    contadorBicLog = new();
                        //    contadorBicLog.IdOportunidad = itemOportunidad.IdOportunidad;
                        //    contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                        //    contadorBicLog.FechaCalculo = DateTime.Now;
                        //    contadorBicLog.Estado = true;
                        //    contadorBicLog.UsuarioCreacion = "ContadorBic_v5";
                        //    contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                        //    contadorBicLog.FechaCreacion = DateTime.Now;
                        //    contadorBicLog.FechaModificacion = DateTime.Now;
                        //    contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                        //}
                        //foreach (var fecha in fechas)
                        //{
                        //    TimeSpan horaFecha = fecha.TimeOfDay;
                        //    foreach (var bloque in bloques)
                        //    {
                        //        if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                        //        {
                        //            if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date))
                        //                break;
                        //            else
                        //            {
                        //                nombreTurnoUltimo = bloque.Nombre;
                        //                fechaUltima = fecha.Date;
                        //                bloque.Contador++;

                        //                var itemLogDetalle = contadorBicLog.ContadorBicLogDetalles.FirstOrDefault(s => s.FechaLogContacto.Date == fechaUltima);
                        //                if (itemLogDetalle == null)
                        //                {
                        //                    itemLogDetalle = new ContadorBicLogDetalle();
                        //                    itemLogDetalle.Estado = true;
                        //                    itemLogDetalle.UsuarioCreacion = "ContadorBic_v5";
                        //                    itemLogDetalle.UsuarioModificacion = "ContadorBic_v5";
                        //                    itemLogDetalle.FechaCreacion = DateTime.Now;
                        //                    itemLogDetalle.FechaModificacion = DateTime.Now;
                        //                    itemLogDetalle.FechaLogContacto = fechaUltima;
                        //                    itemLogDetalle.EstadoContactoManhana = false;
                        //                    itemLogDetalle.EstadoContactoTarde = false;
                        //                    if (bloque.Nombre == "Mañana")
                        //                        itemLogDetalle.EstadoContactoManhana = true;
                        //                    else
                        //                        itemLogDetalle.EstadoContactoTarde = true;
                        //                    contadorBicLog.ContadorBicLogDetalles.Add(itemLogDetalle);
                        //                }
                        //                else
                        //                {
                        //                    itemLogDetalle.FechaLogContacto = fechaUltima;
                        //                    if (bloque.Nombre == "Mañana")
                        //                        itemLogDetalle.EstadoContactoManhana = true;
                        //                    else
                        //                        itemLogDetalle.EstadoContactoTarde = true;
                        //                }
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}
                        /* Inicio Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                        bool convertirABic = false;
                        if (itemOportunidad.ListaFechas.Count() >= _ConteoBic1)
                        {
                            convertirABic = true;
                        }
                        //foreach (var bloque in bloques)
                        //{
                        //    if (bloque.Nombre == "Mañana")
                        //        contadorBicLog.SinContactoManhana = bloque.Contador ?? 0;
                        //    else
                        //        contadorBicLog.SinContactoTarde = bloque.Contador ?? 0;

                        //    //Casos especiales para el conteo de dias para BIC
                        //    if ((!itemOportunidad.IdPais.HasValue && bloque.Contador >= dias)
                        //        || (
                        //            itemOportunidad.IdPais.HasValue
                        //            && (
                        //                (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisMexico && bloque.Contador >= _DiasMexico)
                        //                || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisChile && bloque.Contador >= _DiasChile)
                        //                || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisColombia && bloque.Contador >= _DiasColombia)
                        //                || (itemOportunidad.IdPais.Value != ValorEstatico.IdPaisMexico
                        //                    && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisChile
                        //                    && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisColombia
                        //                    && bloque.Contador >= dias)
                        //                )
                        //            )
                        //        )
                        //    {
                        //        convertirABic = true;
                        //    }
                        //    if (convertirABic == false && configuracionBicPersonal.Count() > 0)
                        //    {
                        //        foreach (var item in configuracionBicPersonal)
                        //        {
                        //            var listaAsesores = item.IdAsesores.Split(',');
                        //            foreach (var item1 in listaAsesores)
                        //            {
                        //                if (item.IdPais != null)
                        //                {
                        //                    if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                        //                        && bloque.Contador >= item.DiasBic
                        //                        && item.IdPais == itemOportunidad.IdPais)
                        //                    {
                        //                        convertirABic = true;
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                        //                        && bloque.Contador >= item.DiasBic)
                        //                    {
                        //                        convertirABic = true;
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    bloque.Contador = 0;
                        //}



                        /* Fin Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                        if (convertirABic)
                        {
                            try
                            {
                                ProcesoConvertiBic1(itemOportunidad.IdOportunidad, "CerradoBIC1");//comentado temporalmente para validar por Marco Kilimajer
                                idsOportunidadesACerrar.Add(itemOportunidad.IdOportunidad);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            //try
                            //{
                            //    if (contadorBicLog.Id != 0)
                            //    {
                            //        contadoresActualizar.Add(contadorBicLog);
                            //    }
                            //    else
                            //    {
                            //        contadoresNuevos.Add(contadorBicLog);
                            //    }
                            //}
                            //catch
                            //{
                            //    continue;
                            //}
                        }
                    }
                    
                }
                catch
                {
                }
                try
                {
                    if (_OportunidadesBic.Count() > 0)
                    {
                        _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                //try
                //{
                //    if (contadoresActualizar.Count() > 0)
                //    {
                //        var ids = contadoresActualizar.Select(x => x.Id).ToList();
                //        var detalle = _unitOfWork.ContadorBicLogDetalleRepository.ObtenerPorIdsContadorLog(ids);
                //        if (detalle.Count() > 0)
                //        {
                //            _unitOfWork.ContadorBicLogDetalleRepository.Delete(detalle.Select(x => x.Id), "ContadorBic_v5");
                //            _unitOfWork.Commit();
                //        }
                //        _unitOfWork.ContadorBicLogRepository.Update(contadoresActualizar);
                //        _unitOfWork.Commit();
                //    }
                //}
                //catch
                //{
                //}
                //try
                //{
                //    if (contadoresNuevos.Count() > 0)
                //    {
                //        _unitOfWork.ContadorBicLogRepository.Add(contadoresNuevos);
                //        _unitOfWork.Commit();
                //    }
                //}
                //catch
                //{
                //}
                /*Se omite nueva logica de cierre bic por ordenes de gerencia (2025-08-14)*/
                /*_OportunidadesBic = new List<Oportunidad>();
                 List<OportunidadContadorBicDTO> oportunidadContadorBics = _unitOfWork.ContadorBicRepository.ObtenerDatosParaBicPorPaisNuevaLogica(idPaisSede);
                 foreach (var oportunidadContadorBic in oportunidadContadorBics)
                 {
                     ProcesoConvertiBic(oportunidadContadorBic.IdOportunidad, "CerradoBIC-NL");
                     idsOportunidadesACerrar.Add(oportunidadContadorBic.IdOportunidad);

                 }
                 if (_OportunidadesBic != null && _OportunidadesBic.Count() > 0)
                 {
                     _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                     _unitOfWork.Commit();
                 }
                */
                return idsOportunidadesACerrar;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<int> EjecutarBIC2(int idPaisSede)
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> listaOportunidades = new();
                if (idPaisSede == 0)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasPeruBic2();
                }
                else if (idPaisSede == 56)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasChileBic2();
                }
                else if (idPaisSede == 52)
                {
                    listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesNoEjecutadasMexicoBic2();
                }
                var oportunidadesAgrupadas = listaOportunidades.GroupBy(o => new
                {
                    o.Id,
                    o.IdCodigoPais,
                    o.IdPersonal_Asignado,
                    o.IdFaseOportunidad
                }).Select(grp => new OportunidadAgrupadaDTO
                {
                    IdOportunidad = int.Parse((grp.Key.Id)),
                    ListaFechas = grp.OrderBy(c => c.FechaProgramada).Select(c => c.FechaProgramada).ToList(),
                    IdPais = grp.Key.IdCodigoPais,
                    IdPersonalAsignado = grp.Key.IdPersonal_Asignado,
                    IdFaseOportunidad = grp.Key.IdFaseOportunidad
                }).ToList();
                var listaConfiguracionBIC = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicAplicada();
                var configuracionBicPersonal = _unitOfWork.ContadorBicRepository.ObtenerConfiguracionBicPersonal();
                List<int> idsOportunidadesACerrar = new List<int>();
                List<ContadorBicLog> contadoresNuevos = new();
                List<ContadorBicLog> contadoresActualizar = new();
                try
                {
                    foreach (var configuracion in listaConfiguracionBIC)
                    {
                        int dias = configuracion.Dias;

                        var bloques = _unitOfWork.BloqueHorarioRepository.ObtenerPorIdConfiguracionBic(configuracion.Id);
                        foreach (var bloque in bloques)
                        {
                            bloque.Contador = 0;
                        }

                        foreach (var itemOportunidad in oportunidadesAgrupadas.ToList())
                        {
                            List<DateTime> fechas = itemOportunidad.ListaFechas;

                            var nombreTurnoUltimo = "";
                            DateTime fechaUltima = new DateTime(2019, 1, 1).Date;

                            ContadorBicLog? contadorBicLog = _unitOfWork.ContadorBicLogRepository.ObtenerUltimoRegistroPorIdOportunidad(itemOportunidad.IdOportunidad);
                            if (contadorBicLog != null)
                            {
                                contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                                contadorBicLog.FechaCalculo = DateTime.Now;
                                contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                                contadorBicLog.FechaModificacion = DateTime.Now;
                                contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                            }
                            else
                            {
                                contadorBicLog = new();
                                contadorBicLog.IdOportunidad = itemOportunidad.IdOportunidad;
                                contadorBicLog.IdFaseOportunidad = itemOportunidad.IdFaseOportunidad!.Value;
                                contadorBicLog.FechaCalculo = DateTime.Now;
                                contadorBicLog.Estado = true;
                                contadorBicLog.UsuarioCreacion = "ContadorBic_v5";
                                contadorBicLog.UsuarioModificacion = "ContadorBic_v5";
                                contadorBicLog.FechaCreacion = DateTime.Now;
                                contadorBicLog.FechaModificacion = DateTime.Now;
                                contadorBicLog.ContadorBicLogDetalles = new List<ContadorBicLogDetalle>();
                            }
                            foreach (var fecha in fechas)
                            {
                                TimeSpan horaFecha = fecha.TimeOfDay;
                                foreach (var bloque in bloques)
                                {
                                    if ((horaFecha >= bloque.HoraInicio) && (horaFecha <= bloque.HoraFin))
                                    {
                                        if ((bloque.Nombre == nombreTurnoUltimo && fecha.Date == fechaUltima.Date))
                                            break;
                                        else
                                        {
                                            nombreTurnoUltimo = bloque.Nombre;
                                            fechaUltima = fecha.Date;
                                            bloque.Contador++;

                                            var itemLogDetalle = contadorBicLog.ContadorBicLogDetalles.FirstOrDefault(s => s.FechaLogContacto.Date == fechaUltima);
                                            if (itemLogDetalle == null)
                                            {
                                                itemLogDetalle = new ContadorBicLogDetalle();
                                                itemLogDetalle.Estado = true;
                                                itemLogDetalle.UsuarioCreacion = "ContadorBic_v5";
                                                itemLogDetalle.UsuarioModificacion = "ContadorBic_v5";
                                                itemLogDetalle.FechaCreacion = DateTime.Now;
                                                itemLogDetalle.FechaModificacion = DateTime.Now;
                                                itemLogDetalle.FechaLogContacto = fechaUltima;
                                                itemLogDetalle.EstadoContactoManhana = false;
                                                itemLogDetalle.EstadoContactoTarde = false;
                                                if (bloque.Nombre == "Mañana")
                                                    itemLogDetalle.EstadoContactoManhana = true;
                                                else
                                                    itemLogDetalle.EstadoContactoTarde = true;
                                                contadorBicLog.ContadorBicLogDetalles.Add(itemLogDetalle);
                                            }
                                            else
                                            {
                                                itemLogDetalle.FechaLogContacto = fechaUltima;
                                                if (bloque.Nombre == "Mañana")
                                                    itemLogDetalle.EstadoContactoManhana = true;
                                                else
                                                    itemLogDetalle.EstadoContactoTarde = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            /* Inicio Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                            bool convertirABic = false;
                            foreach (var bloque in bloques)
                            {
                                if (bloque.Nombre == "Mañana")
                                    contadorBicLog.SinContactoManhana = bloque.Contador ?? 0;
                                else
                                    contadorBicLog.SinContactoTarde = bloque.Contador ?? 0;

                                //Casos especiales para el conteo de dias para BIC
                                if ((!itemOportunidad.IdPais.HasValue && bloque.Contador >= dias)
                                    || (
                                        itemOportunidad.IdPais.HasValue
                                        && (
                                            (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisMexico && bloque.Contador >= _DiasMexico)
                                            || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisChile && bloque.Contador >= _DiasChile)
                                            || (itemOportunidad.IdPais.Value == ValorEstatico.IdPaisColombia && bloque.Contador >= _DiasColombia)
                                            || (itemOportunidad.IdPais.Value != ValorEstatico.IdPaisMexico
                                                && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisChile
                                                && itemOportunidad.IdPais.Value != ValorEstatico.IdPaisColombia
                                                && bloque.Contador >= dias)
                                            )
                                        )
                                    )
                                {
                                    convertirABic = true;
                                }
                                if (convertirABic == false && configuracionBicPersonal.Count() > 0)
                                {
                                    foreach (var item in configuracionBicPersonal)
                                    {
                                        var listaAsesores = item.IdAsesores.Split(',');
                                        foreach (var item1 in listaAsesores)
                                        {
                                            if (item.IdPais != null)
                                            {
                                                if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                                                    && bloque.Contador >= item.DiasBic
                                                    && item.IdPais == itemOportunidad.IdPais)
                                                {
                                                    convertirABic = true;
                                                }
                                            }
                                            else
                                            {
                                                if (item1 == itemOportunidad.IdPersonalAsignado.ToString()
                                                    && bloque.Contador >= item.DiasBic)
                                                {
                                                    convertirABic = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                bloque.Contador = 0;
                            }
                            /* Fin Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                            if (convertirABic)
                            {
                                try
                                {
                                    //ProcesoConvertiBic2(itemOportunidad.IdOportunidad, "CerradoBIC2");//comentado temporalmente para validar por Marco Kilimajer
                                    idsOportunidadesACerrar.Add(itemOportunidad.IdOportunidad);
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (contadorBicLog.Id != 0)
                                    {
                                        contadoresActualizar.Add(contadorBicLog);
                                    }
                                    else
                                    {
                                        contadoresNuevos.Add(contadorBicLog);
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
                catch
                {
                }
                try
                {
                    if (_OportunidadesBic.Count() > 0)
                    {
                        _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                try
                {
                    if (contadoresActualizar.Count() > 0)
                    {
                        var ids = contadoresActualizar.Select(x => x.Id).ToList();
                        var detalle = _unitOfWork.ContadorBicLogDetalleRepository.ObtenerPorIdsContadorLog(ids);
                        if (detalle.Count() > 0)
                        {
                            _unitOfWork.ContadorBicLogDetalleRepository.Delete(detalle.Select(x => x.Id), "ContadorBic_v5");
                            _unitOfWork.Commit();
                        }
                        _unitOfWork.ContadorBicLogRepository.Update(contadoresActualizar);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                try
                {
                    if (contadoresNuevos.Count() > 0)
                    {
                        _unitOfWork.ContadorBicLogRepository.Add(contadoresNuevos);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                /*Se omite nueva logica de cierre bic por ordenes de gerencia (2025-08-14)*/
                /*_OportunidadesBic = new List<Oportunidad>();
                 List<OportunidadContadorBicDTO> oportunidadContadorBics = _unitOfWork.ContadorBicRepository.ObtenerDatosParaBicPorPaisNuevaLogica(idPaisSede);
                 foreach (var oportunidadContadorBic in oportunidadContadorBics)
                 {
                     ProcesoConvertiBic(oportunidadContadorBic.IdOportunidad, "CerradoBIC-NL");
                     idsOportunidadesACerrar.Add(oportunidadContadorBic.IdOportunidad);

                 }
                 if (_OportunidadesBic != null && _OportunidadesBic.Count() > 0)
                 {
                     _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                     _unitOfWork.Commit();
                 }
                */
                return idsOportunidadesACerrar;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 31/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza el proceso de ejecutar bics
        /// </summary>
        /// <returns> cantidad de registros </returns>
        public List<int> EjecutarBICManualmente()
        {
            try
            {
                var listaOportunidades = _unitOfWork.ContadorBicRepository.ObtenerOportunidadesACerrarManualmente();
                List<int> idsOportunidadesACerrar = new List<int>();
                try
                {

                    foreach (var idOportunidad in listaOportunidades)
                    {


                        /* Inicio Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                        bool convertirABic = true;

                        /* Fin Nueva lógica de validación de cantidad de no ejecutadas en turno mañana O turno tarde */
                        if (convertirABic)
                        {
                            try
                            {
                                ProcesoConvertiBic(idOportunidad.Id, "CerradoBIC");
                                idsOportunidadesACerrar.Add(idOportunidad.Id);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }

                    }

                }
                catch
                {
                }
                try
                {
                    if (_OportunidadesBic.Count() > 0)
                    {
                        _unitOfWork.OportunidadRepository.Update(_OportunidadesBic);
                        _unitOfWork.Commit();
                    }
                }
                catch
                {
                }
                return idsOportunidadesACerrar;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id Oportunidad </param>
        /// <param name="usuario"> Usuario bic </param>
        /// <returns> Vacio </returns>
        private void ProcesoConvertiBic(int idOportunidad, string usuario)
        {
            string flagError = "";
            try
            {
                Oportunidad oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad)!;
                if (!_unitOfWork.ActividadDetalleRepository.Exist(oportunidad.IdActividadDetalleUltima!.Value))
                    throw new Exception("La oportunidad no tiene actividad detalle");

                Oportunidad oportunidadTemp = new Oportunidad();
                oportunidadTemp.IdEstadoOportunidad = oportunidad.IdEstadoOportunidad;
                oportunidadTemp.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadTemp.IdFaseOportunidadIc = oportunidad.IdFaseOportunidadIc;
                oportunidadTemp.IdFaseOportunidadIp = oportunidad.IdFaseOportunidadIp;
                oportunidadTemp.IdFaseOportunidadPf = oportunidad.IdFaseOportunidadPf;
                oportunidadTemp.FechaEnvioFaseOportunidadPf = oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadTemp.FechaPagoFaseOportunidadIc = oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadTemp.FechaPagoFaseOportunidadPf = oportunidad.FechaPagoFaseOportunidadPf;
                oportunidadTemp.CodigoPagoIc = oportunidad.CodigoPagoIc;

                flagError = "ObtenerUltimoOportunidadLog";
                var ultimaOportunidadLog = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog(oportunidad.Id);
                var fechaFinLLamada = DateTime.Now;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _unitOfWork.OcurrenciaAlternoRepository.ObtenerOcurrenciaPorActividad(ValorEstatico.IdOcurrenciaCerradoReporteBic);
                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                    if (ocurrencia.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo)
                    {
                        oportunidad.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                    }
                    oportunidad.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                }
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado!.Value);
                var fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                // Actualizar Actividad Detalle

                var actividadActual = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(oportunidad.IdActividadDetalleUltima!.Value);
                actividadActual.FechaReal = DateTime.Now;
                actividadActual.DuracionReal = 13;
                actividadActual.IdCentralLlamada = 3;
                actividadActual.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                actividadActual.Comentario = "Cerrado Reporte BIC";
                actividadActual.IdOcurrenciaAlterno = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                //actividadNueva.IdOcurrenciaActividad = actividadDetalleUltima.IdOcurrenciaActividad;
                actividadActual.FechaModificacion = DateTime.Now;
                actividadActual.UsuarioModificacion = usuario;
                oportunidad.ActividadDetalles = new List<ActividadDetalle>()
                {
                    actividadActual
                };

                oportunidad.UltimoComentario = "Cerrado Reporte BIC";
                oportunidad.UltimaFechaProgramada = actividadActual.FechaProgramada == null ? DateTime.Now : actividadActual.FechaProgramada.Value;
                oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                //oportunidad.IdActividadDetalleUltima = actividadActual.Id;
                oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadEjecutada;

                if (oportunidad.IdFaseOportunidad != 0 && oportunidadTemp.IdFaseOportunidad != oportunidad.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    oportunidad.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(oportunidadTemp.IdFaseOportunidad, oportunidad.IdFaseOportunidad);
                }
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = usuario;

                //Guardar Log
                var oportunidadLogNueva = new OportunidadLog();
                oportunidadLogNueva.IdClasificacionPersona = ultimaOportunidadLog.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = ultimaOportunidadLog.IdPersonalAreaTrabajo;
                oportunidadLogNueva.IdCentroCosto = oportunidad.IdCentroCosto;
                oportunidadLogNueva.IdPersonalAsignado = oportunidad.IdPersonalAsignado;
                oportunidadLogNueva.IdTipoDato = oportunidad.IdTipoDato;
                oportunidadLogNueva.IdFaseOportunidadAnt = oportunidadTemp.IdFaseOportunidad;
                oportunidadLogNueva.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadLogNueva.IdOrigen = oportunidad.IdOrigen;
                oportunidadLogNueva.IdContacto = oportunidad.IdAlumno;
                oportunidadLogNueva.FechaFinLog = ultimaOportunidadLog.FechaLog;
                oportunidadLogNueva.IdCentroCostoAnt = ultimaOportunidadLog.IdCentroCosto;
                oportunidadLogNueva.IdAsesorAnt = ultimaOportunidadLog.IdPersonalAsignado;
                oportunidadLogNueva.FechaLog = fechareal;
                oportunidadLogNueva.IdActividadDetalle = actividadActual.Id;
                oportunidadLogNueva.IdOcurrenciaAlterno = actividadActual.IdOcurrenciaAlterno;
                oportunidadLogNueva.IdOcurrenciaActividadAlterno = null;
                oportunidadLogNueva.Comentario = "Cerrado Reporte BIC";
                oportunidadLogNueva.IdOportunidad = oportunidad.Id;
                oportunidadLogNueva.IdCategoriaOrigen = oportunidad.IdCategoriaOrigen;
                oportunidadLogNueva.IdSubCategoriaDato = oportunidad.IdSubCategoriaDato;
                oportunidadLogNueva.FechaRegistroCampania = oportunidad.FechaRegistroCampania;
                oportunidadLogNueva.IdFaseOportunidadIc = oportunidadTemp.IdFaseOportunidadIc;
                oportunidadLogNueva.IdFaseOportunidadIp = oportunidadTemp.IdFaseOportunidadIp;
                oportunidadLogNueva.IdFaseOportunidadPf = oportunidadTemp.IdFaseOportunidadPf;
                oportunidadLogNueva.FechaEnvioFaseOportunidadPf = oportunidadTemp.FechaEnvioFaseOportunidadPf;
                oportunidadLogNueva.FechaPagoFaseOportunidadIc = oportunidadTemp.FechaPagoFaseOportunidadIc;
                oportunidadLogNueva.FechaPagoFaseOportunidadPf = oportunidadTemp.FechaPagoFaseOportunidadPf;
                oportunidadLogNueva.FasesActivas = false;
                oportunidadLogNueva.CodigoPagoIc = oportunidad.CodigoPagoIc;
                oportunidadLogNueva.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = oportunidad.IdPersonalAreaTrabajo;
                oportunidadLogNueva.CambioFase = true;
                oportunidadLogNueva.FechaCambioFase = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioFaseAnt = ultimaOportunidadLog.FechaCambioFase;
                oportunidadLogNueva.CambioFaseAsesor = 1;
                oportunidadLogNueva.FechaCambioAsesor = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioAsesorAnt = ultimaOportunidadLog.FechaCambioAsesor;
                oportunidadLogNueva.CambioFaseIs = false;
                oportunidadLogNueva.CicloRn2 = ultimaOportunidadLog.CicloRn2;
                oportunidadLogNueva.FechaCreacion = DateTime.Now;
                oportunidadLogNueva.FechaModificacion = DateTime.Now;
                oportunidadLogNueva.UsuarioCreacion = usuario;
                oportunidadLogNueva.UsuarioModificacion = usuario;
                oportunidadLogNueva.Estado = true;

                oportunidad.OportunidadLogs = new List<OportunidadLog>()
                {
                    oportunidadLogNueva
                };
                try
                {
                    _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                }
                catch (Exception)
                {
                }
                _OportunidadesBic.Add(oportunidad);

                try
                {
                    //TODO: Pendiente de regularizacion
                    //Envio de sms al cerrar a bic
                    string res = "";
                    string URI = "/api/Oportunidad/EnviarIndividualSMSPorBIC/" + idOportunidad;
                    using (WebClient wc = new())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        res = wc.DownloadString(URI);
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //_unitOfWork.DetachAll();
                throw new Exception(ex.Message + '-' + flagError);
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id Oportunidad </param>
        /// <param name="usuario"> Usuario bic </param>
        /// <returns> Vacio </returns>
        private void ProcesoConvertiBic1(int idOportunidad, string usuario)
        {
            string flagError = "";
            try
            {
                Oportunidad oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad)!;
                if (!_unitOfWork.ActividadDetalleRepository.Exist(oportunidad.IdActividadDetalleUltima!.Value))
                    throw new Exception("La oportunidad no tiene actividad detalle");

                Oportunidad oportunidadTemp = new Oportunidad();
                oportunidadTemp.IdEstadoOportunidad = oportunidad.IdEstadoOportunidad;
                oportunidadTemp.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadTemp.IdFaseOportunidadIc = oportunidad.IdFaseOportunidadIc;
                oportunidadTemp.IdFaseOportunidadIp = oportunidad.IdFaseOportunidadIp;
                oportunidadTemp.IdFaseOportunidadPf = oportunidad.IdFaseOportunidadPf;
                oportunidadTemp.FechaEnvioFaseOportunidadPf = oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadTemp.FechaPagoFaseOportunidadIc = oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadTemp.FechaPagoFaseOportunidadPf = oportunidad.FechaPagoFaseOportunidadPf;
                oportunidadTemp.CodigoPagoIc = oportunidad.CodigoPagoIc;

                flagError = "ObtenerUltimoOportunidadLog";
                var ultimaOportunidadLog = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog(oportunidad.Id);
                var fechaFinLLamada = DateTime.Now;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _unitOfWork.OcurrenciaAlternoRepository.ObtenerOcurrenciaPorActividad(434);
                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                    if (ocurrencia.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo)
                    {
                        oportunidad.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                    }
                    oportunidad.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                }
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado!.Value);
                var fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                // Actualizar Actividad Detalle

                var actividadActual = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(oportunidad.IdActividadDetalleUltima!.Value);
                actividadActual.FechaReal = DateTime.Now;
                actividadActual.DuracionReal = 13;
                actividadActual.IdCentralLlamada = 3;
                actividadActual.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                actividadActual.Comentario = "Cerrado Reporte BIC1";
                actividadActual.IdOcurrenciaAlterno = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                //actividadNueva.IdOcurrenciaActividad = actividadDetalleUltima.IdOcurrenciaActividad;
                actividadActual.FechaModificacion = DateTime.Now;
                actividadActual.UsuarioModificacion = usuario;
                oportunidad.ActividadDetalles = new List<ActividadDetalle>()
                {
                    actividadActual
                };

                oportunidad.UltimoComentario = "Cerrado Reporte BIC1";
                oportunidad.UltimaFechaProgramada = actividadActual.FechaProgramada == null ? DateTime.Now : actividadActual.FechaProgramada.Value;
                oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                //oportunidad.IdActividadDetalleUltima = actividadActual.Id;
                oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadEjecutada;

                if (oportunidad.IdFaseOportunidad != 0 && oportunidadTemp.IdFaseOportunidad != oportunidad.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    oportunidad.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(oportunidadTemp.IdFaseOportunidad, oportunidad.IdFaseOportunidad);
                }
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = usuario;

                //Guardar Log
                var oportunidadLogNueva = new OportunidadLog();
                oportunidadLogNueva.IdClasificacionPersona = ultimaOportunidadLog.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = ultimaOportunidadLog.IdPersonalAreaTrabajo;
                oportunidadLogNueva.IdCentroCosto = oportunidad.IdCentroCosto;
                oportunidadLogNueva.IdPersonalAsignado = oportunidad.IdPersonalAsignado;
                oportunidadLogNueva.IdTipoDato = oportunidad.IdTipoDato;
                oportunidadLogNueva.IdFaseOportunidadAnt = oportunidadTemp.IdFaseOportunidad;
                oportunidadLogNueva.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadLogNueva.IdOrigen = oportunidad.IdOrigen;
                oportunidadLogNueva.IdContacto = oportunidad.IdAlumno;
                oportunidadLogNueva.FechaFinLog = ultimaOportunidadLog.FechaLog;
                oportunidadLogNueva.IdCentroCostoAnt = ultimaOportunidadLog.IdCentroCosto;
                oportunidadLogNueva.IdAsesorAnt = ultimaOportunidadLog.IdPersonalAsignado;
                oportunidadLogNueva.FechaLog = fechareal;
                oportunidadLogNueva.IdActividadDetalle = actividadActual.Id;
                oportunidadLogNueva.IdOcurrenciaAlterno = actividadActual.IdOcurrenciaAlterno;
                oportunidadLogNueva.IdOcurrenciaActividadAlterno = null;
                oportunidadLogNueva.Comentario = "Cerrado Reporte BIC1";
                oportunidadLogNueva.IdOportunidad = oportunidad.Id;
                oportunidadLogNueva.IdCategoriaOrigen = oportunidad.IdCategoriaOrigen;
                oportunidadLogNueva.IdSubCategoriaDato = oportunidad.IdSubCategoriaDato;
                oportunidadLogNueva.FechaRegistroCampania = oportunidad.FechaRegistroCampania;
                oportunidadLogNueva.IdFaseOportunidadIc = oportunidadTemp.IdFaseOportunidadIc;
                oportunidadLogNueva.IdFaseOportunidadIp = oportunidadTemp.IdFaseOportunidadIp;
                oportunidadLogNueva.IdFaseOportunidadPf = oportunidadTemp.IdFaseOportunidadPf;
                oportunidadLogNueva.FechaEnvioFaseOportunidadPf = oportunidadTemp.FechaEnvioFaseOportunidadPf;
                oportunidadLogNueva.FechaPagoFaseOportunidadIc = oportunidadTemp.FechaPagoFaseOportunidadIc;
                oportunidadLogNueva.FechaPagoFaseOportunidadPf = oportunidadTemp.FechaPagoFaseOportunidadPf;
                oportunidadLogNueva.FasesActivas = false;
                oportunidadLogNueva.CodigoPagoIc = oportunidad.CodigoPagoIc;
                oportunidadLogNueva.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = oportunidad.IdPersonalAreaTrabajo;
                oportunidadLogNueva.CambioFase = true;
                oportunidadLogNueva.FechaCambioFase = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioFaseAnt = ultimaOportunidadLog.FechaCambioFase;
                oportunidadLogNueva.CambioFaseAsesor = 1;
                oportunidadLogNueva.FechaCambioAsesor = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioAsesorAnt = ultimaOportunidadLog.FechaCambioAsesor;
                oportunidadLogNueva.CambioFaseIs = false;
                oportunidadLogNueva.CicloRn2 = ultimaOportunidadLog.CicloRn2;
                oportunidadLogNueva.FechaCreacion = DateTime.Now;
                oportunidadLogNueva.FechaModificacion = DateTime.Now;
                oportunidadLogNueva.UsuarioCreacion = usuario;
                oportunidadLogNueva.UsuarioModificacion = usuario;
                oportunidadLogNueva.Estado = true;

                oportunidad.OportunidadLogs = new List<OportunidadLog>()
                {
                    oportunidadLogNueva
                };
                try
                {
                    _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                }
                catch (Exception)
                {
                }
                _OportunidadesBic.Add(oportunidad);

                try
                {
                    //TODO: Pendiente de regularizacion
                    //Envio de sms al cerrar a bic
                    string res = "";
                    string URI = "/api/Oportunidad/EnviarIndividualSMSPorBIC/" + idOportunidad;
                    using (WebClient wc = new())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        res = wc.DownloadString(URI);
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //_unitOfWork.DetachAll();
                throw new Exception(ex.Message + '-' + flagError);
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Version: 1.0
        /// <summary>
        /// Actualiza información de Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id Oportunidad </param>
        /// <param name="usuario"> Usuario bic </param>
        /// <returns> Vacio </returns>
        private void ProcesoConvertiBic2(int idOportunidad, string usuario)
        {
            string flagError = "";
            try
            {
                Oportunidad oportunidad = _unitOfWork.OportunidadRepository.ObtenerPorId(idOportunidad)!;
                if (!_unitOfWork.ActividadDetalleRepository.Exist(oportunidad.IdActividadDetalleUltima!.Value))
                    throw new Exception("La oportunidad no tiene actividad detalle");

                Oportunidad oportunidadTemp = new Oportunidad();
                oportunidadTemp.IdEstadoOportunidad = oportunidad.IdEstadoOportunidad;
                oportunidadTemp.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadTemp.IdFaseOportunidadIc = oportunidad.IdFaseOportunidadIc;
                oportunidadTemp.IdFaseOportunidadIp = oportunidad.IdFaseOportunidadIp;
                oportunidadTemp.IdFaseOportunidadPf = oportunidad.IdFaseOportunidadPf;
                oportunidadTemp.FechaEnvioFaseOportunidadPf = oportunidad.FechaEnvioFaseOportunidadPf;
                oportunidadTemp.FechaPagoFaseOportunidadIc = oportunidad.FechaPagoFaseOportunidadIc;
                oportunidadTemp.FechaPagoFaseOportunidadPf = oportunidad.FechaPagoFaseOportunidadPf;
                oportunidadTemp.CodigoPagoIc = oportunidad.CodigoPagoIc;

                flagError = "ObtenerUltimoOportunidadLog";
                var ultimaOportunidadLog = _unitOfWork.OportunidadLogRepository.ObtenerUltimoOportunidadLog(oportunidad.Id);
                var fechaFinLLamada = DateTime.Now;

                flagError = "ObtenerOcurrenciaPorActividad";
                var ocurrencia = _unitOfWork.OcurrenciaAlternoRepository.ObtenerOcurrenciaPorActividad(435);
                if (ocurrencia != null)
                {
                    flagError = "ValidarEstadoOcurrencia";
                    if (_unitOfWork.OcurrenciaRepository.ValidarEstadoOcurrencia(ocurrencia.Id))
                    {
                        ocurrencia.IdEstadoOcurrencia = ValorEstatico.IdEstadoOcurrenciaNoEjecutado;
                    }
                    if (ocurrencia.IdFaseOportunidad != ValorEstatico.IdFaseOportunidadNulo)
                    {
                        oportunidad.IdFaseOportunidad = ocurrencia.IdFaseOportunidad;
                    }
                    oportunidad.IdEstadoOcurrenciaUltimo = ocurrencia.IdEstadoOcurrencia;

                }
                var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(oportunidad.IdPersonalAsignado!.Value);
                var fechareal = DateTime.Now;
                if (diferenciaHoraria != null)
                {
                    fechareal = DateTime.Now.AddHours(diferenciaHoraria.Valor.GetValueOrDefault());
                }

                // Actualizar Actividad Detalle

                var actividadActual = _unitOfWork.ActividadDetalleRepository.ObtenerPorId(oportunidad.IdActividadDetalleUltima!.Value);
                actividadActual.FechaReal = DateTime.Now;
                actividadActual.DuracionReal = 13;
                actividadActual.IdCentralLlamada = 3;
                actividadActual.IdEstadoActividadDetalle = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                actividadActual.Comentario = "Cerrado Reporte BIC2";
                actividadActual.IdOcurrenciaAlterno = ValorEstatico.IdOcurrenciaCerradoReporteBic;
                //actividadNueva.IdOcurrenciaActividad = actividadDetalleUltima.IdOcurrenciaActividad;
                actividadActual.FechaModificacion = DateTime.Now;
                actividadActual.UsuarioModificacion = usuario;
                oportunidad.ActividadDetalles = new List<ActividadDetalle>()
                {
                    actividadActual
                };

                oportunidad.UltimoComentario = "Cerrado Reporte BIC2";
                oportunidad.UltimaFechaProgramada = actividadActual.FechaProgramada == null ? DateTime.Now : actividadActual.FechaProgramada.Value;
                oportunidad.IdEstadoActividadDetalleUltimoEstado = ValorEstatico.IdEstadoActividadDetalleEjecutado;
                //oportunidad.IdActividadDetalleUltima = actividadActual.Id;
                oportunidad.IdEstadoOportunidad = ValorEstatico.IdEstadoOportunidadEjecutada;

                if (oportunidad.IdFaseOportunidad != 0 && oportunidadTemp.IdFaseOportunidad != oportunidad.IdFaseOportunidad)
                {
                    flagError = "GetFaseMaxima";
                    oportunidad.IdFaseOportunidadMaxima = _unitOfWork.FaseOportunidadRepository.ObternerFaseMaximaHistoria(oportunidadTemp.IdFaseOportunidad, oportunidad.IdFaseOportunidad);
                }
                oportunidad.FechaModificacion = DateTime.Now;
                oportunidad.UsuarioModificacion = usuario;

                //Guardar Log
                var oportunidadLogNueva = new OportunidadLog();
                oportunidadLogNueva.IdClasificacionPersona = ultimaOportunidadLog.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = ultimaOportunidadLog.IdPersonalAreaTrabajo;
                oportunidadLogNueva.IdCentroCosto = oportunidad.IdCentroCosto;
                oportunidadLogNueva.IdPersonalAsignado = oportunidad.IdPersonalAsignado;
                oportunidadLogNueva.IdTipoDato = oportunidad.IdTipoDato;
                oportunidadLogNueva.IdFaseOportunidadAnt = oportunidadTemp.IdFaseOportunidad;
                oportunidadLogNueva.IdFaseOportunidad = oportunidad.IdFaseOportunidad;
                oportunidadLogNueva.IdOrigen = oportunidad.IdOrigen;
                oportunidadLogNueva.IdContacto = oportunidad.IdAlumno;
                oportunidadLogNueva.FechaFinLog = ultimaOportunidadLog.FechaLog;
                oportunidadLogNueva.IdCentroCostoAnt = ultimaOportunidadLog.IdCentroCosto;
                oportunidadLogNueva.IdAsesorAnt = ultimaOportunidadLog.IdPersonalAsignado;
                oportunidadLogNueva.FechaLog = fechareal;
                oportunidadLogNueva.IdActividadDetalle = actividadActual.Id;
                oportunidadLogNueva.IdOcurrenciaAlterno = actividadActual.IdOcurrenciaAlterno;
                oportunidadLogNueva.IdOcurrenciaActividadAlterno = null;
                oportunidadLogNueva.Comentario = "Cerrado Reporte BIC2";
                oportunidadLogNueva.IdOportunidad = oportunidad.Id;
                oportunidadLogNueva.IdCategoriaOrigen = oportunidad.IdCategoriaOrigen;
                oportunidadLogNueva.IdSubCategoriaDato = oportunidad.IdSubCategoriaDato;
                oportunidadLogNueva.FechaRegistroCampania = oportunidad.FechaRegistroCampania;
                oportunidadLogNueva.IdFaseOportunidadIc = oportunidadTemp.IdFaseOportunidadIc;
                oportunidadLogNueva.IdFaseOportunidadIp = oportunidadTemp.IdFaseOportunidadIp;
                oportunidadLogNueva.IdFaseOportunidadPf = oportunidadTemp.IdFaseOportunidadPf;
                oportunidadLogNueva.FechaEnvioFaseOportunidadPf = oportunidadTemp.FechaEnvioFaseOportunidadPf;
                oportunidadLogNueva.FechaPagoFaseOportunidadIc = oportunidadTemp.FechaPagoFaseOportunidadIc;
                oportunidadLogNueva.FechaPagoFaseOportunidadPf = oportunidadTemp.FechaPagoFaseOportunidadPf;
                oportunidadLogNueva.FasesActivas = false;
                oportunidadLogNueva.CodigoPagoIc = oportunidad.CodigoPagoIc;
                oportunidadLogNueva.IdClasificacionPersona = oportunidad.IdClasificacionPersona;
                oportunidadLogNueva.IdPersonalAreaTrabajo = oportunidad.IdPersonalAreaTrabajo;
                oportunidadLogNueva.CambioFase = true;
                oportunidadLogNueva.FechaCambioFase = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioFaseAnt = ultimaOportunidadLog.FechaCambioFase;
                oportunidadLogNueva.CambioFaseAsesor = 1;
                oportunidadLogNueva.FechaCambioAsesor = oportunidadLogNueva.FechaLog;
                oportunidadLogNueva.FechaCambioAsesorAnt = ultimaOportunidadLog.FechaCambioAsesor;
                oportunidadLogNueva.CambioFaseIs = false;
                oportunidadLogNueva.CicloRn2 = ultimaOportunidadLog.CicloRn2;
                oportunidadLogNueva.FechaCreacion = DateTime.Now;
                oportunidadLogNueva.FechaModificacion = DateTime.Now;
                oportunidadLogNueva.UsuarioCreacion = usuario;
                oportunidadLogNueva.UsuarioModificacion = usuario;
                oportunidadLogNueva.Estado = true;

                oportunidad.OportunidadLogs = new List<OportunidadLog>()
                {
                    oportunidadLogNueva
                };
                try
                {
                    _unitOfWork.OportunidadRemarketingAgendaRepository.DesactivarRedireccionRemarketingAnterior(idOportunidad);
                }
                catch (Exception)
                {
                }
                _OportunidadesBic.Add(oportunidad);

                try
                {
                    //TODO: Pendiente de regularizacion
                    //Envio de sms al cerrar a bic
                    string res = "";
                    string URI = "/api/Oportunidad/EnviarIndividualSMSPorBIC/" + idOportunidad;
                    using (WebClient wc = new())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        res = wc.DownloadString(URI);
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                //_unitOfWork.DetachAll();
                throw new Exception(ex.Message + '-' + flagError);
            }
        }
    }
}
