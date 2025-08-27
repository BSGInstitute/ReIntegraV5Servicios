using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: OportunidadInformacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de Informacion de Oportunidades
    /// </summary>
    public class HoraReprogramacionAutomaticaService : IHoraReprogramacionAutomaticaService
    {
        private IUnitOfWork _unitOfWork;

        //public int IdOportunidad = 0;
        //public int IdOcurrencia = 0;
        //public string CodigoFase = "";
        //public int IdActividadCabecera = 0;
        //public int IdTipoDato = 0;
        public int IdPersonal = 0;
        private DateTime _fechaActualDiferencia = DateTime.Now;
        //public int IdCategoriaOrigen = 0;
        public List<List<TimeSpan?>> PersonalHorario = new List<List<TimeSpan?>>();

        public HoraReprogramacionAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener la Fecha y Hora de la Actividad Reprogramacion Automatica
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> void </returns>
        public string ObtenerFechaHoraActividadReprogramacionAutomatica(int idActividadCabecera, int idCategoriaOrigen, int idPersonal, string codigoFase, int idOcurrencia, List<List<TimeSpan?>> horario)
        {
            var listRpta = GetFechaHoraReprogramacion(idActividadCabecera, idCategoriaOrigen, idPersonal, codigoFase, idOcurrencia, horario);

            var Listarpta = listRpta.Year + "/" + listRpta.Month + "/" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;

            return Listarpta;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Fecha y Hora de Reprogramacion
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> void </returns>
        private DateTime GetFechaHoraReprogramacion(int idActividadCabecera, int idCategoriaOrigen, int idPersonal, string codigoFase, int idOcurrencia, List<List<TimeSpan?>> horario)
        {
            var servicioReprogramacionCabecera = new ReprogramacionCabeceraService(_unitOfWork);
            var reprogramacion = servicioReprogramacionCabecera.ObtenerCantidadIntervaloYReprogramacionPorDiaPermitida(idActividadCabecera, idCategoriaOrigen);
            var reprogramacionesAsesor = servicioReprogramacionCabecera.ObtenerCantidadReprogramacionDelDiaPorAsesor(idActividadCabecera, idCategoriaOrigen, idPersonal);

            var diferenciaHoraria = _unitOfWork.PersonalRepository.ObtenerDiferenciaHoraria(idPersonal);
            _fechaActualDiferencia = DateTime.Now;
            if (diferenciaHoraria != null)
            {
                _fechaActualDiferencia = DateTime.Now.AddHours(diferenciaHoraria.Valor!.Value);
            }
            if (reprogramacion == null)
            {
                reprogramacion = new();
                reprogramacion.MaxReproPorDia = 200;
                if (codigoFase == "BNC" || codigoFase == "IT")
                    reprogramacion.IntervaloSigProgramacionMin = 240;
                if (codigoFase == "IP" || codigoFase == "PF" || codigoFase == "IC")
                    reprogramacion.IntervaloSigProgramacionMin = 150;
            }

            //En el caso de IP siempre seran 4 horas
            if (codigoFase == "IP")
            {
                reprogramacion.IntervaloSigProgramacionMin = 240;
            }
            //fin - En el caso de IP siempre seran 4 horas

            if (reprogramacionesAsesor == null)
            {
                if (reprogramacion == null)
                    throw new Exception("No existe Intervalo para reprogramacion para esta Actividad y Categoria de dato");

                if (idOcurrencia == 234) // SI ES 222. Dato para eliminar
                {
                    reprogramacion.IntervaloSigProgramacionMin = 1440;//24 HORAS
                }
                //nuevo calculo si pasa la media noche
                var flujoNormal = 1;
                if (_fechaActualDiferencia.Date == _fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                {
                    //si con la suma de  tiempo aun sigue siendo el mismo dia//caso por defecto
                    flujoNormal = 1;
                }
                else if (_fechaActualDiferencia.Date < _fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                {
                    flujoNormal = 0;
                }
                //fin nuevo calculo si pasa la media noche
                DateTime fecha = CalcularProgramacionAutomaticaByAsesor(_fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin), flujoNormal, reprogramacion.IntervaloSigProgramacionMin, horario);

                fecha = CalcularHorario(fecha, idPersonal, horario);

                return fecha;
            }
            else
            {
                if (reprogramacion == null)
                {
                    throw new Exception("No existe Intervalo para reprogramacion para esta Actividad y Categoria de dato");
                }
                else
                {
                    if (reprogramacionesAsesor.ReproDia < reprogramacion.MaxReproPorDia)
                    {
                        //valido 222. Dato para eliminar
                        if (idOcurrencia == 234 || idOcurrencia == 222 || idOcurrencia == 238 || idOcurrencia == 371 || idOcurrencia == 413) // SI ES 222. Dato para eliminar // 238:Número no existe o está suspendido // 371:No solicitó información // 413:Número equivocado
                        {
                            reprogramacion.IntervaloSigProgramacionMin = 1440;//24 HORAS
                        }

                        //nuevo calculo si pasa la media noche
                        var flujoNormal = 1;
                        if (_fechaActualDiferencia.Date == _fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                        {
                            //si con la suma de  tiempo aun sigue siendo el mismo dia//caso por defecto
                            flujoNormal = 1;
                        }
                        else if (_fechaActualDiferencia.Date < _fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin).Date)
                        {
                            flujoNormal = 0;
                        }
                        //fin nuevo calculo si pasa la media noche


                        var fecha = CalcularProgramacionAutomaticaByAsesor(_fechaActualDiferencia.AddMinutes(reprogramacion.IntervaloSigProgramacionMin), flujoNormal, reprogramacion.IntervaloSigProgramacionMin, horario);

                        //aqui valido si la hora esta bien sino le trae la mas cercana
                        fecha = CalcularHorario(fecha, idPersonal, horario);
                        //fin la validacion
                        return fecha;
                    }
                    else
                    {
                        throw new Exception("Ya no se pueden re-programar mas actividades");
                    }
                }
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Calcula la Programacion Automatica tomando en cuenta los tiempos libres
        /// </summary>
        /// <param name="idCentroCosto">Id de Centro Costo</param>
        /// <returns> void </returns>
        public DateTime CalcularProgramacionAutomaticaByAsesor(DateTime horaParaProgramar, int flujoNormal, int IntervaloSigProgramacionMin, List<List<TimeSpan?>> horario)
        {
            var libreEntrada = _unitOfWork.TiempoLibreRepository.ObtenerTiempoLibreTipoUno();
            var libreEntradaAlmuerzo = _unitOfWork.TiempoLibreRepository.ObtenerTiempoLibreTipoDos();

            foreach (var item in horario)
            {
                if (item[0] != null)
                {
                    if (libreEntrada != null)
                    {
                        TimeSpan tiempo = new TimeSpan(0, libreEntrada.TiempoMin, 0);
                        item[0] = item[0].Value.Add(tiempo);
                    }
                }
                if (item[2] != null)
                {
                    if (libreEntradaAlmuerzo != null)
                    {
                        TimeSpan tiempo2 = new TimeSpan(0, libreEntradaAlmuerzo.TiempoMin, 0);
                        item[2] = item[2].Value.Add(tiempo2);
                    }
                }
            }

            DateTime resp;
            if (flujoNormal == 0)//caso Pase de la media noche
            {
                resp = CalcularProgramacionAutomaticaMediaNoche(horario, horaParaProgramar, IntervaloSigProgramacionMin);
            }
            else//sigue flujo normal
            {
                resp = CalcularProgramacionAutomatica(horario, horaParaProgramar);
            }
            return resp;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor pasada la media noche
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="horaParaReprogramar"></param>
        /// <returns> DateTime </returns>
        public DateTime CalcularProgramacionAutomaticaMediaNoche(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar, int IntervaloSigProgramacionMin)
        {
            if ((horario[(int)horaParaReprogramar.DayOfWeek][0] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][1] >= horaParaReprogramar.TimeOfDay) ||
                (horario[(int)horaParaReprogramar.DayOfWeek][2] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][3] >= horaParaReprogramar.TimeOfDay))
                return horaParaReprogramar;

            TimeSpan? restantedia = horario[(int)horaParaReprogramar.Date.AddDays(-1).DayOfWeek][3] != null ? horario[(int)horaParaReprogramar.AddDays(-1).DayOfWeek][3] - _fechaActualDiferencia.TimeOfDay : horario[(int)horaParaReprogramar.AddDays(-1).DayOfWeek][1] - _fechaActualDiferencia.TimeOfDay;
            if (restantedia < new TimeSpan(0, 0, 0))
            {
                restantedia = new TimeSpan(0, 0, 0);
            }
            TimeSpan resto = new TimeSpan(0, IntervaloSigProgramacionMin, 0) - restantedia.Value;

            int cont = (int)horaParaReprogramar.DayOfWeek;

            while (true)
            {
                if (horario[cont][0].HasValue && horario[cont][1].HasValue)
                {
                    if (horario[cont][0] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][0].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }

                    if (horario[cont][1].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][1].Value;
                }

                if (horario[cont][2].HasValue && horario[cont][3].HasValue)
                {
                    if (horario[cont][2] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][2].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }
                    if (horario[cont][3].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][3].Value;

                }

                cont = (cont + 1) % horario.Count;
                horaParaReprogramar = horaParaReprogramar.AddDays(1);
                horaParaReprogramar = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);
            }
            return horaParaReprogramar;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="horaParaReprogramar"></param>
        /// <returns> DateTime </returns>
        public DateTime CalcularProgramacionAutomatica(List<List<TimeSpan?>> horario, DateTime horaParaReprogramar)
        {
            if ((horario[(int)horaParaReprogramar.DayOfWeek][0] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][1] >= horaParaReprogramar.TimeOfDay) ||
                (horario[(int)horaParaReprogramar.DayOfWeek][2] <= horaParaReprogramar.TimeOfDay &&
                 horario[(int)horaParaReprogramar.DayOfWeek][3] >= horaParaReprogramar.TimeOfDay))
            {
                return horaParaReprogramar;
            }

            //si cae en su hora de almuerzo
            if (horario[(int)horaParaReprogramar.DayOfWeek][1] <= horaParaReprogramar.TimeOfDay &&
                horario[(int)horaParaReprogramar.DayOfWeek][2] >= horaParaReprogramar.TimeOfDay)
            {
                return new DateTime(horaParaReprogramar.Year,horaParaReprogramar.Month,horaParaReprogramar.Day, horario[(int)horaParaReprogramar.DayOfWeek][2].Value.Hours , horario[(int)horaParaReprogramar.DayOfWeek][2].Value.Minutes, horario[(int)horaParaReprogramar.DayOfWeek][2].Value.Seconds);
            }


            TimeSpan resto = new TimeSpan(0, 0, 0);

            int cont = (int)horaParaReprogramar.DayOfWeek;

            while (true)
            {
                if (horario[cont][0].HasValue && horario[cont][1].HasValue)
                {
                    if (horario[cont][0] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][0].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }

                    if (horario[cont][1].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][1].Value;
                }

                if (horario[cont][2].HasValue && horario[cont][3].HasValue)
                {
                    if (horario[cont][2] > horaParaReprogramar.TimeOfDay)
                    {
                        TimeSpan nuevaHora = horario[cont][2].Value + resto;
                        horaParaReprogramar = CalcularProgramacionAutomatica(horario, new DateTime(horaParaReprogramar.Year,
                                                                                                   horaParaReprogramar.Month,
                                                                                                   horaParaReprogramar.Day,
                                                                                                   nuevaHora.Hours,
                                                                                                   nuevaHora.Minutes,
                                                                                                   nuevaHora.Seconds));
                        break;
                    }
                    if (horario[cont][3].Value >= horaParaReprogramar.TimeOfDay)
                        break;
                    else
                        resto = horaParaReprogramar.TimeOfDay - horario[cont][3].Value;

                }

                cont = (cont + 1) % horario.Count;
                horaParaReprogramar = horaParaReprogramar.AddDays(1);
                horaParaReprogramar = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);

            }
            return horaParaReprogramar;
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha disponible para la Reprogramacion Automatica validando las horas bloqueadas para el Asesor durante el dia
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns> DateTime </returns>
        public DateTime CalcularHorario(DateTime fecha, int idPersonal, List<List<TimeSpan?>> horario)
        {
            var servicioHoraBloqueada = new HoraBloqueadaService(_unitOfWork);

            int intervalo = 2;
            var horas_bloqueadas = servicioHoraBloqueada.ObtenerHorasBloquedasReprogramacionPorAsesor(idPersonal, fecha);

            TimeSpan tiempo_pregun = new TimeSpan(fecha.Hour, fecha.Minute, fecha.Second);

            if (horario[(int)fecha.DayOfWeek][0] <= tiempo_pregun && horario[(int)fecha.DayOfWeek][1] >= tiempo_pregun)
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][0];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][1])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio.Value.Hours && w.Hora.Minute == hora_inicio.Value.Minutes).FirstOrDefault();

                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            return hora_final;
                        }
                    }
                }
            }
            else
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][2];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][3])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.TimeOfDay == hora_inicio).FirstOrDefault();
                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            return hora_final;
                        }
                    }
                }
            }
            return fecha;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha y hora de la reprogramacion automatica - operaciones
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <returns> string </returns>
        public string ObtenerFechaHoraReprogramacionAutomaticaOperaciones(int idOportunidad)
        {
            var listRpta = ObtenerFechaReprogramacionOperaciones(idOportunidad);

            if (listRpta.Day == 1 && listRpta.Month == 5 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(1);
            }
            if (listRpta.Day == 28 && listRpta.Month == 7 && listRpta.Year == 2018)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 25 && listRpta.Month == 12)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 1 && listRpta.Month == 1)
            {
                listRpta = listRpta.AddDays(2);
            }
            if (listRpta.Day == 1 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(4);
            }
            if (listRpta.Day == 2 && listRpta.Month == 4 && listRpta.Year == 2021)
            {
                listRpta = listRpta.AddDays(3);
            }
            var Listarpta = listRpta.Year + "/" + listRpta.Month + "/" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;

            return Listarpta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha disponible para la Reprogramacion Automatica validando las horas bloqueadas para el Coordinador Operaciones
        /// durante el dia
        /// </summary> 
        /// <param name="fecha"></param>
        /// <returns></returns>
        public DateTime CalcularHorarioOperaciones(DateTime fecha)
        {
            bool validadiaconespacio = true;
            int intervalo = 3;

            var horario = PersonalHorario;

            var horas_bloqueadas = _unitOfWork.HoraBloqueadaRepository.ObtenerHorasBloquedasReprogramacionPorAsesor(IdPersonal, fecha);

            TimeSpan tiempo_pregun = new TimeSpan(fecha.Hour, fecha.Minute, fecha.Second);

            int countControlarBucle = 0;
            while (horario[(int)fecha.DayOfWeek][0] == null && countControlarBucle < 8)
            {
                fecha = fecha.AddDays(1);
                countControlarBucle++;
                horas_bloqueadas = _unitOfWork.HoraBloqueadaRepository.ObtenerHorasBloquedasReprogramacionPorAsesor(IdPersonal, fecha);
            }

            if (horario[(int)fecha.DayOfWeek][0] <= tiempo_pregun && horario[(int)fecha.DayOfWeek][1] >= tiempo_pregun)
            {
                TimeSpan? hora_inicio = horario[(int)fecha.DayOfWeek][0];

                while (hora_inicio < horario[(int)fecha.DayOfWeek][1])
                {
                    TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                    hora_inicio = hora_inicio.Value.Add(tiempo);

                    if (hora_inicio > fecha.TimeOfDay)
                    {
                        var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio.Value.Hours && w.Hora.Minute == hora_inicio.Value.Minutes).FirstOrDefault();

                        if (hora_bloq == null)
                        {
                            DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio.Value.Hours, hora_inicio.Value.Minutes, hora_inicio.Value.Seconds);
                            validadiaconespacio = false;
                            return hora_final;
                        }
                    }
                }
            }

            TimeSpan? hora_inicio2 = horario[(int)fecha.DayOfWeek][2];

            while (hora_inicio2 < horario[(int)fecha.DayOfWeek][3])
            {
                TimeSpan tiempo = new TimeSpan(0, intervalo, 0);
                hora_inicio2 = hora_inicio2.Value.Add(tiempo);

                if (hora_inicio2 > fecha.TimeOfDay)
                {
                    var hora_bloq = horas_bloqueadas.Where(w => w.Fecha.Date == fecha.Date && w.Hora.Hour == hora_inicio2.Value.Hours && w.Hora.Minute == hora_inicio2.Value.Minutes).FirstOrDefault();

                    if (hora_bloq == null)
                    {
                        DateTime hora_final = new DateTime(fecha.Year, fecha.Month, fecha.Day, hora_inicio2.Value.Hours, hora_inicio2.Value.Minutes, hora_inicio2.Value.Seconds);
                        validadiaconespacio = false;
                        return hora_final;
                    }
                }
            }
            //añadido para vovler a llamr al dia sgte
            if (validadiaconespacio == true)
            {
                return CalcularHorarioOperaciones(fecha.AddDays(1));
            }
            return fecha;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha de reprogramacion operaciones
        /// </summary>
        /// <param name="idOportunidad"> Id de la oportunidad </param>
        /// <returns> DateTime </returns>
        private DateTime ObtenerFechaReprogramacionOperaciones(int idOportunidad)
        {
            int dias = 0;
            var temp = _unitOfWork.TiempoLibreRepository.ObtenerDiasReprogramacionAutomaticaOperaciones(idOportunidad);
            dias = temp.Valor.Value;
            DateTime fecha = CalcularProgramacionAutomaticaByAsesorOperaciones(dias);
            fecha = CalcularHorarioOperaciones(fecha);
            return fecha;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Valida el Horario del Asesor vs su timepo Libre del Dia
        /// </summary>
        /// <param name="dias"></param> 
        /// <returns> DateTime </returns>
        public DateTime CalcularProgramacionAutomaticaByAsesorOperaciones(int dias)
        {
            DateTime resp = CalcularProgramacionAutomaticaOperaciones(PersonalHorario, dias);
            return resp;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la fecha disponible para la reprogramacion segun el horario del Asesor pasada la media noche
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="dias"></param>
        /// <returns> DateTime </returns>
        public DateTime CalcularProgramacionAutomaticaOperaciones(List<List<TimeSpan?>> horario, int dias)
        {
            var estado = true;
            DateTime horaParaReprogramarfinal = new DateTime();
            while (estado)
            {
                if (horario[(int)DateTime.Now.AddDays(dias).DayOfWeek][0] != null)
                {

                    DateTime horaParaReprogramar = DateTime.Now.AddDays(dias);
                    horaParaReprogramarfinal = new DateTime(horaParaReprogramar.Year,
                                                   horaParaReprogramar.Month,
                                                   horaParaReprogramar.Day, 0, 0, 0);
                    estado = false;
                }
                else
                {
                    dias = dias + 1;
                }
            }
            return horaParaReprogramarfinal.Add(horario[(int)DateTime.Now.AddDays(dias).DayOfWeek][0].Value);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera la fecha y hora de la reprogramación manual para Operaciones
        /// </summary>
        /// <param name="fecha"> fecha de reprogramación </param> 
        /// <returns> string: nueva fecha </returns>
        public string ObtenerFechaHoraReprogramacionManualOperaciones(DateTime fecha)
        {
            if (fecha.Hour == 19 && fecha.Minute == 0 && fecha.Second == 0)
            {
                return fecha.Year + "-" + fecha.Month + "-" + fecha.Day + " " + fecha.Hour + ":" + fecha.Minute + ":" + fecha.Second;
            }
            var listRpta = ObtenerFechaReprogramacionManualOperaciones(fecha);
            var Listarpta = listRpta.Year + "-" + listRpta.Month + "-" + listRpta.Day + " " + listRpta.Hour + ":" + listRpta.Minute + ":" + listRpta.Second;
            return Listarpta;
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/01/2023
        /// Version: 1.0
        /// <summary>
        /// Genera la fecha y hora de la reprogramación manual para Operaciones
        /// </summary>
        /// <param name="fecha"> fecha de reprogramación </param> 
        /// <returns> DateTime: nueva fecha </returns>
        public DateTime ObtenerFechaReprogramacionManualOperaciones(DateTime fecha)
        {
            DateTime fechafinal = CalcularHorarioOperaciones(fecha);
            return fechafinal;
        }
        public bool ReprogramarAlumnoClasesOnline(int idAlumno)
        {
            try
            {
                _unitOfWork.HoraBloqueadaRepository.ReprogramarAlumnoClasesOnline(idAlumno);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
