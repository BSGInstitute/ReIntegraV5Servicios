using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_InfGenDTO
    {
        public static SentinelSdtInfGenDTO builderEntityDTO(SentinelServicio.SDT_IC_InfGen entity)
        {
            SentinelSdtInfGenDTO rpta = new SentinelSdtInfGenDTO();
            string pattern = "^([0][1-9]|[1-9]|[12][0-9]|3[01])(\\/|-)([0][1-9]|[1-9]|[1][0-2])\\2(\\d{4})";
            rpta.Dni = entity.DNI;
            //rpta.FechaNacimiento = DateTime.Parse("dd/MM/yyyy");
            if (entity.fecnac != "" && entity.fecnac != "//-/" && entity.fecnac != "//")
            {
                try
                {
                    rpta.FechaNacimiento = obtenerFecha(entity.fecnac, pattern);
                }
                catch (Exception e)
                {
                    throw new Exception("Error en SentinelSdtInfGen - FechaNacimiento " + e.Message);

                }

                //rpta.FechaNacimiento = DateTime.ParseExact(entity.fecnac, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-PE"));
            }

            else
                rpta.FechaNacimiento = null;
            rpta.Sexo = entity.sexo;
            rpta.Digito = entity.digito;
            rpta.DigitoAnterior = entity.digitoAnterior;
            rpta.Ruc = entity.RUC;
            rpta.RazonSocial = entity.RazonSocial;
            rpta.NombreComercial = entity.NomComercial;
            if (entity.FechaBaja != "" && entity.FechaBaja != "//-/" && entity.FechaBaja != "//" && entity.FechaBaja != "/-/")
            {
                string patron = "^([0][1-9]|[1-9]|[1][0-2])(\\/|-)([0][1-9]|[1-9]|[12][0-9]|3[01])\\2(\\d{4})";
                try
                {
                    rpta.FechaBaja = obtenerFechaInversa(entity.FechaBaja, patron);
                }
                catch (Exception e)
                {
                    throw new Exception("Error en SentinelSdtInfGen - FechaBaja " + e.Message);

                }


                //rpta.FechaBaja = DateTime.ParseExact(entity.FechaBaja, "MM/dd/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-PE"));
            }
            else
            {
                rpta.FechaBaja = null;
            }
            //rpta.FechaBaja = entity.FechaBaja == "" ? "" : Convert.ToDateTime(entity.FechaBaja);
            rpta.TipoContribuyente = entity.TipoContrib;
            rpta.CodigoTipoContribuyente = entity.CodTipoContrib;
            rpta.EstadoContribuyente = entity.EstContrib;
            rpta.CodigoEstadoContribuyente = entity.CodEstContrib;
            rpta.CondicionContribuyente = entity.CondContrib;
            rpta.CodigoCondicionContribuyente = entity.CodCondContrib;
            rpta.ActividadEconomica = entity.ActEconomica;
            rpta.Ciiu = entity.CIIU;
            rpta.ActividadEconomica2 = entity.ActEconomica2;
            rpta.Ciiu2 = entity.CIIU2;
            rpta.ActividadEconomica3 = entity.ActEconomica3;
            rpta.Ciiu3 = entity.CIIU3;
            if (entity.FIniActividad != "")
            {
                try
                {
                    rpta.FechaActividad = obtenerFecha(entity.FIniActividad, pattern);
                }
                catch (Exception e)
                {
                    throw new Exception("Error en SentinelSdtInfGen - FechaActividad " + e.Message);
                }
                //rpta.FechaActividad = DateTime.ParseExact(entity.FIniActividad, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-PE"));
            }
            else
            {
                rpta.FechaActividad = null;
            }

            rpta.Direccion = entity.Direccion;
            rpta.Referencia = entity.Referencia;
            rpta.Departamento = entity.Departamento;
            rpta.Provincia = entity.Provincia;
            rpta.Distrito = entity.Distrito;
            rpta.Ubigeo = entity.Ubigeo;
            if (entity.FConstitucion != "" && entity.FConstitucion != "-" && entity.FConstitucion != " ")
            {
                try
                {
                    rpta.FechaConstitucion = obtenerFecha(entity.FConstitucion, pattern);
                }
                catch (Exception e)
                {
                    throw new Exception("Error en SentinelSdtInfGen - FechaConstitucion " + e.Message);
                }

                //rpta.FechaConstitucion = DateTime.ParseExact(entity.FConstitucion, "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("es-PE")); 
            }
            else
            {
                rpta.FechaConstitucion = null;
            }
            //rpta.FechaConstitucion = Convert.ToDateTime(entity.FConstitucion);
            rpta.ActvidadComercioExterior = entity.ActvComercioExterior;
            rpta.CodigoActividadComerExt = entity.CodActvComerExt;
            rpta.CodigoDependencia = entity.CodDependencia;
            rpta.Dependencia = entity.Dependencia;
            rpta.Folio = entity.Folio;
            rpta.Asiento = entity.Asiento;
            rpta.Tomo = entity.Tomo;
            rpta.PartidaReg = entity.PartidaReg;
            rpta.Patron = entity.CPatron;

            return rpta;
        }

        public static IList<SentinelSdtInfGenDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_InfGen> listaInput)
        {
            var listOutput = new List<SentinelSdtInfGenDTO>();
            foreach (SentinelServicio.SDT_IC_InfGen entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
        private static DateTime obtenerFecha(string fechaString, string patron)
        {
            int dia = 0;
            int mes = 0;
            int año = 0;

            foreach (Match match in Regex.Matches(fechaString, patron, RegexOptions.IgnoreCase))
            {
                dia = int.Parse(match.Groups[1].Value);
                mes = int.Parse(match.Groups[3].Value);
                año = int.Parse(match.Groups[4].Value);
            }
            if (mes > 12 && dia < 13)
            {
                int aux = dia;
                dia = mes;
                mes = aux;
            }
            DateTime fecha = new DateTime(año, mes, dia);
            return fecha;
        }
        private static DateTime obtenerFechaInversa(string fechaString, string patron)
        {
            int dia = 0;
            int mes = 0;
            int año = 0;

            foreach (Match match in Regex.Matches(fechaString, patron, RegexOptions.IgnoreCase))
            {
                dia = int.Parse(match.Groups[3].Value);
                mes = int.Parse(match.Groups[1].Value);
                año = int.Parse(match.Groups[4].Value);
            }
            if (mes > 12 && dia < 13)
            {
                int aux = dia;
                dia = mes;
                mes = aux;
            }

            DateTime fecha = new DateTime(año, mes, dia);
            return fecha;
        }
    }
}
