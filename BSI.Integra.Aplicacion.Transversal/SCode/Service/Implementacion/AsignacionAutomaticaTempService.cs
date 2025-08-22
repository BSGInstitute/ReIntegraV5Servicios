using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Tools;
using BSI.Integra.Aplicacion.Transversal.Validador;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AsignacionAutomaticaTempService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaTemp
    /// </summary>
    public class AsignacionAutomaticaTempService : IAsignacionAutomaticaTempService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        private Parser _parser;
        public AsignacionAutomatica datos = new AsignacionAutomatica();
        private AsignacionAutomaticaTemp _asignacionAutomaticaTemp;

        public AsignacionAutomaticaTempService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomaticaTemp, AsignacionAutomaticaTemp>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCiudad, Ciudad>(MemberList.None).ReverseMap();
                cfg.CreateMap<CiudadDTO, Ciudad>(MemberList.None).ReverseMap();

            });

            _mapper = new Mapper(config);
            _parser = new Parser();
        }

        #region Metodos Base
        public AsignacionAutomaticaTemp Add(AsignacionAutomaticaTemp entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaTempRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaTemp>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AsignacionAutomaticaTemp Update(AsignacionAutomaticaTemp entidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaTempRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<AsignacionAutomaticaTemp>(modelo);
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
                _unitOfWork.AsignacionAutomaticaTempRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaTemp> Add(List<AsignacionAutomaticaTemp> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaTempRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaTemp>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AsignacionAutomaticaTemp> Update(List<AsignacionAutomaticaTemp> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.AsignacionAutomaticaTempRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<AsignacionAutomaticaTemp>>(modelo);
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
                _unitOfWork.AsignacionAutomaticaTempRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez
        /// Fecha: 17/03/2021
        /// Version: 1.0
        /// <summary>
        /// Mapea Oportunidades a Asignacion Automatica Temp 
        /// </summary>
        /// <param name="nuevo"> Lista de oportunidades del portal web </param>  
        public void MapearAsignacionAutomaticaTemp(ref AsignacionAutomaticaTemp asignacionAutomaticaTemp, AsignacionAutomaticaTempDTO nuevo)
        {
            asignacionAutomaticaTemp.Id = 0;
            asignacionAutomaticaTemp.Nombres = nuevo.Nombres;
            asignacionAutomaticaTemp.Apellidos = nuevo.Apellidos;
            asignacionAutomaticaTemp.IdAreaFormacion = nuevo.IdAreaFormacion;
            asignacionAutomaticaTemp.IdAreaTrabajo = nuevo.IdAreaTrabajo;
            asignacionAutomaticaTemp.IdCargo = nuevo.IdCargo;

            if (nuevo.CentroCosto == null || nuevo.CentroCosto.Equals(""))
            {
                bool esAdwords = EsAdwords(nuevo.IdCategoriaOrigen.GetValueOrDefault());
                if (esAdwords)
                {
                    if (nuevo.Campania != null)
                    {
                        var centroCostoAdws = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPorCampania(nuevo.Campania);
                        asignacionAutomaticaTemp.CentroCosto = centroCostoAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoAdws.IdCentroCosto;
                    }
                    else if (nuevo.IdConjuntoAnuncio != null)
                    {
                        var nombreCampania = _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerNombreCampaniaPorIdFaseOportunidad(nuevo.IdFaseOportunidadPortal);
                        var centroCostoPorCampaniaAdws = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPorNombreIdConjuntoAnuncio((int)nombreCampania.IdConjuntoAnuncio, nombreCampania.NombreCampania);
                        asignacionAutomaticaTemp.CentroCosto = centroCostoPorCampaniaAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoPorCampaniaAdws.IdCentroCosto;
                    }
                    else
                    {
                        asignacionAutomaticaTemp.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                    }
                }
                else
                {
                    asignacionAutomaticaTemp.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                }
            }
            else
            {
                asignacionAutomaticaTemp.CentroCosto = nuevo.CentroCosto;
            }

            if (nuevo.IdCentroCosto == null || nuevo.IdCentroCosto.Value == 0)
            {
                asignacionAutomaticaTemp.IdCentroCosto = 15907;
            }
            else
            {
                asignacionAutomaticaTemp.IdCentroCosto = nuevo.IdCentroCosto;
            }

            asignacionAutomaticaTemp.Correo = nuevo.Correo;
            asignacionAutomaticaTemp.IdFaseOportunidad = nuevo.IdFaseOportunidad;
            asignacionAutomaticaTemp.Fijo = nuevo.Fijo;
            asignacionAutomaticaTemp.IdIndustria = nuevo.IdIndustria;
            asignacionAutomaticaTemp.Movil = nuevo.Movil;
            asignacionAutomaticaTemp.NombrePrograma = nuevo.NombrePrograma;
            asignacionAutomaticaTemp.Origen = nuevo.IdOrigen;
            asignacionAutomaticaTemp.IdPais = nuevo.Pais == null ? 0 : Convert.ToInt32(nuevo.Pais);
            asignacionAutomaticaTemp.Procesado = false;
            asignacionAutomaticaTemp.IdCiudad = nuevo.Ciudad == null ? 0 : Convert.ToInt32(nuevo.Ciudad);
            asignacionAutomaticaTemp.IdTipoDato = nuevo.IdTipoDato;
            asignacionAutomaticaTemp.IdConjuntoAnuncio = nuevo.IdConjuntoAnuncio;

            if (nuevo.FechaRegistroCampania != null)
            {
                try
                {
                    asignacionAutomaticaTemp.FechaRegistroCampania = Convert.ToDateTime(nuevo.FechaRegistroCampania);//???
                }
                catch (Exception)
                {
                    asignacionAutomaticaTemp.FechaRegistroCampania = DateTime.Now;
                }
            }
            else
            {
                asignacionAutomaticaTemp.FechaRegistroCampania = DateTime.Now;
            }
            if (Guid.TryParse(nuevo.IdFaseOportunidadPortal, out Guid idFaseOportunidadPortal))
            {
                asignacionAutomaticaTemp.IdFaseOportunidadPortal = idFaseOportunidadPortal;
            }
            asignacionAutomaticaTemp.IdTipoInteraccion = nuevo.IdTipoInteraccion;
            asignacionAutomaticaTemp.IdCategoriaDato = nuevo.IdCategoriaOrigen;
            asignacionAutomaticaTemp.IdInteraccionFormulario = nuevo.IdInteraccionFormulario;
            asignacionAutomaticaTemp.UrlOrigen = nuevo.UrlOrigen;
            asignacionAutomaticaTemp.IdTiempoCapacitacion = nuevo.IdTiempoCapacitacion;
            asignacionAutomaticaTemp.IdPagina = nuevo.IdPagina;
            asignacionAutomaticaTemp.FechaCreacion = DateTime.Now;
            asignacionAutomaticaTemp.FechaModificacion = DateTime.Now;
            asignacionAutomaticaTemp.UsuarioCreacion = "System";
            asignacionAutomaticaTemp.UsuarioModificacion = "System";
            asignacionAutomaticaTemp.Estado = true;
        }



        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Valida si es oportunidad de Categoria Adwords
        /// </summary>
        /// <param name="idCategoriaOrigen"> Id Categoria Origen </param>        
        /// <returns></returns>    
        public bool EsAdwords(int idCategoriaOrigen)
        {
            var listaCategoriasAdwords = _unitOfWork.CategoriaOrigenRepository.ObtenerCategoriaOrigenAdwords();
            bool flag = false;
            foreach (var item in listaCategoriasAdwords)
            {
                if (item.Id == idCategoriaOrigen)
                {
                    flag = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return flag;
        }
        // <summary>
        /// Marca como procesado el dato entrante
        /// </summary>
        /// <param name="procesados">Array de cadena con los elementos procesados</param>
        /// <param name="idPagina">Id de la pagina de donde proviene el dato</param>
        public void MarcarComoProcesados(string[] procesados, int idPagina)
        {
            foreach (string procesado in procesados)
            {
                try
                {
                    string URI = "https://integrav4-syncv3.bsginstitute.com/portal/MarcarComoProcesado?idFaseOportunidadPortal=" + procesado;

                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(URI);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha: 23/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza validaciones al BO para generar el registro en asignacion automatica
        /// </summary>
        /// <param name="idAsignacionAutomaticaTemp">Id de la asignacion automatica temporal (PK de la tabla mkt.T_AsignacionAutomatica_Temp)</param>
        /// <param name="listaOrigenes">Lista de origenes a analizar (PK de la tabla mkt.T_Origen)</param>
        /// <param name="listaPaises">Lista de paises a analizar (PK de la tabla conf.T_Pais)</param>
        public void ValidarRegistroFormularioAsignacionAutomaticaTemp(int idAsignacionAutomaticaTemp, Dictionary<int, string> listaPaises, Dictionary<string, OrigenesCategoriaOrigenDTO> listaOrigenes)
        {
            var _repAsignacionAutomaticaTemp = _unitOfWork.AsignacionAutomaticaTempRepository;
            var registro = _repAsignacionAutomaticaTemp.FirstById(idAsignacionAutomaticaTemp);
            if (registro != null)
            {
                string preNombres = System.Text.RegularExpressions.Regex.Replace(registro.Nombres, @"\s+", " ");
                var nombres = _parser.ParserCaracteres(preNombres).Split(new char[] { ' ' }).ToList()
                .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();
                //Nombres
                if (nombres.Count == 1)
                {
                    datos.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    datos.Nombre2 = string.Empty;
                }
                else if (nombres.Count == 2)
                {
                    datos.Nombre1 = nombres.FirstOrDefault().Length >= 100 ? nombres.FirstOrDefault().Substring(0, 100) : nombres.FirstOrDefault();
                    datos.Nombre2 = nombres[1].Length >= 100 ? nombres[1].Substring(0, 100) : nombres[1];
                }
                else if (nombres.Count > 2)
                {
                    datos.Nombre1 = string.Join(" ", nombres.ToArray()).Length >= 100 ? String.Join(" ", nombres.ToArray()).Substring(0, 100) : String.Join(" ", nombres.ToArray());
                    datos.Nombre2 = string.Empty;
                }

                //Apellidos
                registro.Apellidos = registro.Apellidos == null ? "" : registro.Apellidos;
                string preApellidos = System.Text.RegularExpressions.Regex.Replace(registro.Apellidos, @"\s+", " ");
                var apellidos = _parser.ParserCaracteres(preApellidos).Split(new char[] { ' ' }).ToList()
                    .Where(s => s.Trim().Length > 0).Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower()).ToList();

                if (apellidos.Count == 1)
                {
                    datos.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    datos.ApellidoMaterno = string.Empty;
                }
                else if (apellidos.Count == 2)
                {
                    datos.ApellidoPaterno = apellidos.FirstOrDefault().Length >= 100 ? apellidos.FirstOrDefault().Substring(0, 100) : apellidos.FirstOrDefault();
                    datos.ApellidoMaterno = apellidos[1].Length >= 100 ? apellidos[1].Substring(0, 100) : apellidos[1];
                }
                else if (apellidos.Count > 2)
                {
                    datos.ApellidoPaterno = String.Join(" ", apellidos.ToArray()).Length >= 100 ? String.Join(" ", apellidos.ToArray()).Substring(0, 100) : String.Join(" ", apellidos.ToArray());
                    datos.ApellidoMaterno = string.Empty;
                }
                else
                {
                    datos.ApellidoPaterno = string.Empty;
                    datos.ApellidoMaterno = string.Empty;
                }
                //Celular
                datos.Telefono = MapeadorReplace.MapTelefonoCelular(registro.Fijo ?? string.Empty);
                datos.Celular = MapeadorReplace.MapTelefonoCelular(registro.Movil);

                string celularTemporal = string.Empty;

                if (datos.Celular.Length == 0)
                    datos.Celular = "1234";

                if (datos.Celular.Length == 0)
                    throw new Exception("Celular no valido");
                //eliminar ceros de adelante del numero si esque los hubiera
                int i = 0;
                for (; i < datos.Celular.Length && datos.Celular[i].Equals('0'); ++i) ;

                try
                {
                    datos.Celular = datos.Celular.Substring(i).Length > 0 ? datos.Celular.Substring(i) : string.Concat("1", new String('0', datos.Celular.Length - 1));
                }
                catch (Exception e)
                {
                    datos.Celular = datos.Celular.Substring(i);
                }

                if (datos.Celular.Length == 12 && registro.IdPais == 52 && datos.Celular.StartsWith("52"))
                    datos.Celular = string.Concat("00", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 52 && !datos.Celular.StartsWith("52"))
                    datos.Celular = string.Concat("0052", datos.Celular);
                else if (datos.Celular.Length == 11 && registro.IdPais == 54 && !datos.Celular.StartsWith("54"))//argentina
                    datos.Celular = string.Concat("0054", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 56 && !datos.Celular.StartsWith("56"))//chile
                    datos.Celular = string.Concat("0056", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 506 && !datos.Celular.StartsWith("506"))//costa rica
                    datos.Celular = string.Concat("00506", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 53 && !datos.Celular.StartsWith("00"))//cuba
                    datos.Celular = string.Concat("00", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 593 && !datos.Celular.StartsWith("593"))//ecuador
                    datos.Celular = string.Concat("00593", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 503 && !datos.Celular.StartsWith("503"))//el salvador
                    datos.Celular = string.Concat("00503", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 502 && !datos.Celular.StartsWith("502"))//guatemala
                    datos.Celular = string.Concat("00502", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 504 && !datos.Celular.StartsWith("504"))//honduras
                    datos.Celular = string.Concat("00504", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 505 && !datos.Celular.StartsWith("505"))//nicaragua
                    datos.Celular = string.Concat("00505", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 507 && !datos.Celular.StartsWith("507"))//panama
                    datos.Celular = string.Concat("00507", datos.Celular);
                else if (datos.Celular.Length == 9 && registro.IdPais == 595 && !datos.Celular.StartsWith("595"))//paraguay
                    datos.Celular = string.Concat("00595", datos.Celular);
                else if (datos.Celular.Length == 7 && registro.IdPais == 1809 && !datos.Celular.StartsWith("1809"))//rep. dominicana
                    datos.Celular = string.Concat("001809", datos.Celular);
                else if (datos.Celular.Length == 8 && registro.IdPais == 598 && !datos.Celular.StartsWith("598"))//uruguay
                    datos.Celular = string.Concat("00598", datos.Celular);
                else if (datos.Celular.Length == 10 && registro.IdPais == 58 && !datos.Celular.StartsWith("58"))//venezuela
                    datos.Celular = string.Concat("58", datos.Celular);

                // asegurar que el dato tenga pais
                if (registro.IdPais == null)
                {
                    if (datos.Celular.Length == 12 && datos.Celular.StartsWith("57")) registro.IdPais = datos.IdPais = 57;
                    else if (datos.Celular.Length == 11 && datos.Celular.StartsWith("591")) registro.IdPais = datos.IdPais = 591;
                    else if (datos.Celular.Length == 11 && datos.Celular.StartsWith("51")) registro.IdPais = datos.IdPais = 51;
                }

                // eliminar el codigo del pais del celular
                if (datos.Celular.Length == 12 && registro.IdPais.Value == 57) datos.Celular = datos.Celular.Substring(2);
                else if (datos.Celular.Length == 11 && registro.IdPais.Value == 591) datos.Celular = datos.Celular.Substring(3);
                else if (datos.Celular.Length == 11 && registro.IdPais.Value == 51) datos.Celular = datos.Celular.Substring(2);
                if (registro.Correo==null)
                {
                    registro.Correo = "";
                }

                datos.Email = registro.Correo.Trim();
                datos.IdCentroCosto = registro.IdCentroCosto;

                //asegurarse de que tenga un centro de costo por defecto en caso no se haya podido identificar el centro de costo o el usuario no lo haya registrado
                if (datos.IdCentroCosto == null || datos.IdCentroCosto == 0) datos.IdCentroCosto = 15907;  // CC: "REGISTRO CENTRO DE COSTO 2020 I LIMA"
                else datos.IdCentroCosto = registro.IdCentroCosto;
                datos.NombrePrograma = registro.NombrePrograma;
                datos.IdTipoDato = registro.IdTipoDato;

                //Origen
                StringBuilder origen = new StringBuilder();

                registro.IdCategoriaDato = registro.IdCategoriaDato == null ? 18 : registro.IdCategoriaDato;
                registro.IdCategoriaDato = registro.IdCategoriaDato == 0 ? 18 : registro.IdCategoriaDato;

                var _repCategoriaOrigen = _unitOfWork.CategoriaOrigenRepository;
                var categoriaDato = _repCategoriaOrigen.ObtenerCategoriaOrigenSubCategoriaDato(registro.IdCategoriaDato ?? 0, registro.IdTipoInteraccion.GetValueOrDefault());
                datos.IdSubCategoriaDato = (categoriaDato != null && categoriaDato.IdSubCategoriaDato != 0) ? categoriaDato.IdSubCategoriaDato : 0;
                datos.IdCategoriaDato = registro.IdCategoriaDato;
                datos.IdTipoInteraccion = registro.IdTipoInteraccion;
                datos.IdInteraccionFormulario = registro.IdInteraccionFormulario;
                datos.UrlOrigen = registro.UrlOrigen;


                origen.Append("LAN").Append(listaPaises[registro.IdPais ?? default(int)]).Append(categoriaDato.CodigoOrigen.ToUpper());
                var origenNombre = origen.ToString().ToUpper();
                if (categoriaDato.IdTipoCategoriaOrigen == 16)
                {
                    if (categoriaDato.NombreCategoriaOrigen.Contains("Offline"))
                        datos.IdOrigen = 132;
                    else
                        datos.IdOrigen = 114;
                }
                else
                {
                    if (!listaOrigenes.ContainsKey(origenNombre))
                    {
                        datos.IdOrigen = 0;
                    }
                    else
                    {
                        datos.IdOrigen = listaOrigenes[origenNombre].Id;
                    }
                }
                datos.OrigenCampania = registro.Origen;
                datos.IdTipoDato = registro.IdTipoDato;
                datos.IdFaseOportunidad = registro.IdFaseOportunidad;
                datos.Email = registro.Correo.Trim();

                datos.NombrePrograma = registro.NombrePrograma;
                datos.IdCargo = registro.IdCargo;
                datos.IdIndustria = registro.IdIndustria;
                datos.IdAreaFormacion = registro.IdAreaFormacion;
                datos.IdAreaTrabajo = registro.IdAreaTrabajo;
                datos.IdPais = registro.IdPais;
                datos.IdCiudad = registro.IdCiudad;
                datos.IdConjuntoAnuncio = registro.IdConjuntoAnuncio;
                datos.IdAnuncioFacebook = registro.IdAnuncioFacebook;
                datos.FechaRegistroCampania = registro.FechaRegistroCampania;
                datos.IdFaseOportunidadPortal = registro.IdFaseOportunidadPortal;

                datos.IdTiempoCapacitacion = registro.IdTiempoCapacitacion;
                datos.IdPagina = registro.IdPagina;

                datos.Estado = true;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.UsuarioCreacion = "SYSTEM";
                datos.UsuarioModificacion = "SYSTEM";
            }
        }

        /// Autor: Margiory ramirez
        /// Fecha: 23/11/2022
        /// <summary>
        /// Valida la lista de asignacionautomatica erroneos
        /// </summary>
        /// <param name="contexto">Objeto de clase integraDBContext</param>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaErrorBO</returns>
        public List<AsignacionAutomaticaError> Validar(AsignacionAutomatica asignacionAutomatica)
        {
            var listaErrores = new List<AsignacionAutomaticaError>();
            //REGLAS DE VALIDACION PARA REGLAS ERRONEAS 
            //-> Validamos el email es correcto
            int idContacto = 0;
            if (asignacionAutomatica.IdPais == 0)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = asignacionAutomatica.Id,
                    Campo = "Ciudad",
                    Descripcion = "Asigne una ciudad",
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
                return listaErrores;
            }
            string campo = "email";
            try
            {
                Validador.Validador.ValidarEmail(asignacionAutomatica.Email);

            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Longitud del Celular & Telefono
            campo = "celular";
            try
            {
                //validad numero de celular por ciudad
                Validador.Validador.ValidarLongitudCelular(asignacionAutomatica.IdPais, asignacionAutomatica.Celular, _unitOfWork);//validar por pais
            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Ciudad
            campo = "ciudad";
            try
            {
                if (asignacionAutomatica.IdCentroCosto == null || asignacionAutomatica.IdCentroCosto.Value == 0)
                {
                    asignacionAutomatica.IdCentroCosto = ValorEstatico.IdCentroCostoRegistro2020ILima;
                }
                if (asignacionAutomatica.IdCentroCosto == null || asignacionAutomatica.IdCentroCosto.Value == 0)
                {
                    campo = "CentroCosto";
                    throw new ValidatorException("No se encontro centro de costo");
                }
            }
            catch (ValidatorException e)
            {
                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = datos.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //Validamos Si el existe el Origen
            campo = "origen";
            try
            {
                if (asignacionAutomatica.IdOrigen.Equals(0))
                {
                    throw new ValidatorException("No se encontro Origen");
                }
            }
            catch (ValidatorException e)
            {

                listaErrores.Add(new AsignacionAutomaticaError
                {
                    IdAsignacionAutomatica = asignacionAutomatica.Id,
                    Campo = campo,
                    Descripcion = e.Message,
                    IdAsignacionAutomaticaTipoError = 1,
                    IdContacto = idContacto
                });
            }
            //REGLAS DE VALIDACION PARA DATOS REPETIDOS->Solo las validamos si no hubo errores tipo erroneo
            if (listaErrores.Count() > 0)
            {
                return listaErrores;
            }
            //Actualizar IdContacto
            var alumnoValidaEmail = _unitOfWork.AlumnoRepository.ObtenerPorEmail(asignacionAutomatica.Email.ToUpper(), null) ?? _unitOfWork.AlumnoRepository.ObtenerPorEmail(null, datos.Email.ToUpper());

            //Alumno = Alumno == null ? _repAlumno.ObtenerPor

            if (alumnoValidaEmail != null && alumnoValidaEmail.Id != 0)
            {
                asignacionAutomatica.IdAlumno = alumnoValidaEmail.Id;
                asignacionAutomatica.Nombre1 = string.IsNullOrEmpty(asignacionAutomatica.Nombre1) ? alumnoValidaEmail.Nombre1 : asignacionAutomatica.Nombre1;
                asignacionAutomatica.Nombre2 = string.IsNullOrEmpty(asignacionAutomatica.Nombre2) ? alumnoValidaEmail.Nombre2 : asignacionAutomatica.Nombre2;
                asignacionAutomatica.ApellidoPaterno = string.IsNullOrEmpty(asignacionAutomatica.ApellidoPaterno) ? alumnoValidaEmail.ApellidoPaterno : asignacionAutomatica.ApellidoPaterno;
                asignacionAutomatica.ApellidoMaterno = string.IsNullOrEmpty(asignacionAutomatica.ApellidoMaterno) ? alumnoValidaEmail.ApellidoMaterno : asignacionAutomatica.ApellidoMaterno;
                asignacionAutomatica.Telefono = string.IsNullOrEmpty(asignacionAutomatica.Telefono) ? alumnoValidaEmail.Telefono : asignacionAutomatica.Telefono;
                asignacionAutomatica.Celular = string.IsNullOrEmpty(asignacionAutomatica.Celular) ? alumnoValidaEmail.Celular : asignacionAutomatica.Celular;
                asignacionAutomatica.Email = string.IsNullOrEmpty(datos.Email) ? alumnoValidaEmail.Email1 : asignacionAutomatica.Email;
                asignacionAutomatica.IdPais = asignacionAutomatica.IdPais == 0 ? alumnoValidaEmail.IdCodigoPais ?? 0 : asignacionAutomatica.IdPais;
                asignacionAutomatica.IdCiudad = asignacionAutomatica.IdCiudad == 0 ? alumnoValidaEmail.IdCodigoRegionCiudad ?? 0 : asignacionAutomatica.IdCiudad;
                asignacionAutomatica.IdAreaFormacion = asignacionAutomatica.IdAreaFormacion.Equals(0) || asignacionAutomatica.IdAreaFormacion == null ? alumnoValidaEmail.IdAformacion : asignacionAutomatica.IdAreaFormacion;
                asignacionAutomatica.IdAreaTrabajo = asignacionAutomatica.IdAreaTrabajo.Equals(0) || asignacionAutomatica.IdAreaTrabajo == null ? alumnoValidaEmail.IdAtrabajo : asignacionAutomatica.IdAreaTrabajo;
                asignacionAutomatica.IdIndustria = asignacionAutomatica.IdIndustria.Equals(0) || asignacionAutomatica.IdIndustria == null ? alumnoValidaEmail.IdIndustria : asignacionAutomatica.IdIndustria;
                asignacionAutomatica.IdCargo = asignacionAutomatica.IdCargo.Equals(0) || asignacionAutomatica.IdCargo == null ? alumnoValidaEmail.IdCargo : asignacionAutomatica.IdCargo;

                //Valido la Funcion Calculo Individual
                IPersonaService personaService = new PersonaService(_unitOfWork);

                int? idClasificacionPersona = personaService.InsertarPersona(alumnoValidaEmail.Id, TipoPersona.Alumno, "PortalWeb");
                if (idClasificacionPersona == null && idClasificacionPersona != 0)
                {
                    throw new Exception("No se creo el persona clasificacion");
                }
            }
            else//CREO EL ALUMNO PARA QUE A CREAR LA OPORTUNIDAD YA TENGA ID
            {
                IAlumnoService alumnoService = new AlumnoService(_unitOfWork);
                alumnoService.Alumno = new Alumno();
                alumnoService.Alumno.Nombre1 = asignacionAutomatica.Nombre1;
                alumnoService.Alumno.Nombre2 = asignacionAutomatica.Nombre2;
                alumnoService.Alumno.ApellidoPaterno = asignacionAutomatica.ApellidoPaterno;
                alumnoService.Alumno.ApellidoMaterno = asignacionAutomatica.ApellidoMaterno;
                alumnoService.Alumno.Telefono = asignacionAutomatica.Telefono;
                alumnoService.Alumno.Celular = asignacionAutomatica.Celular;
                alumnoService.Alumno.Email1 = asignacionAutomatica.Email;
                alumnoService.Alumno.IdCodigoPais = asignacionAutomatica.IdPais;
                alumnoService.Alumno.IdCodigoRegionCiudad = asignacionAutomatica.IdCiudad;
                alumnoService.Alumno.IdCiudad = asignacionAutomatica.IdCiudad;
                alumnoService.Alumno.IdAformacion = asignacionAutomatica.IdAreaFormacion;
                alumnoService.Alumno.IdAtrabajo = asignacionAutomatica.IdAreaTrabajo;
                alumnoService.Alumno.IdIndustria = asignacionAutomatica.IdIndustria;
                alumnoService.Alumno.IdCargo = asignacionAutomatica.IdCargo;
                alumnoService.Alumno.IdEmpresa = null;
                alumnoService.Alumno.Estado = true;
                alumnoService.Alumno.UsuarioCreacion = "SYSTEM";
                alumnoService.Alumno.UsuarioModificacion = "SYSTEM";
                alumnoService.Alumno.FechaModificacion = DateTime.Now;
                alumnoService.Alumno.FechaCreacion = DateTime.Now;

                alumnoService.ValidarEstadoContactoWhatsAppTemporal();

                var empresaAlumno = alumnoService.Alumno.IdEmpresa;
                alumnoService.Alumno.IdEmpresa = (empresaAlumno == 0 || empresaAlumno == -1) ? null : empresaAlumno;
                var respuestaAlumno = _unitOfWork.AlumnoRepository.Add(alumnoService.Alumno);
                _unitOfWork.Commit();
                alumnoService.Alumno.Id = respuestaAlumno.Id;
                IPersonaService personaService = new PersonaService(_unitOfWork);
                //Valido la Funcion Calculo Individual
                int? idCreacionCorrecta = personaService.InsertarPersona(alumnoService.Alumno.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Alumno, "PortalWeb");
                //Si boto error en al funcion 
                if (idCreacionCorrecta == null && idCreacionCorrecta != 0)
                {
                    var nombreTablaV3 = "talumnos";
                    var nombreTablaV4 = "mkt.T_Alumno";
                    var resultado = _unitOfWork.AlumnoRepository.EliminarFisicaAlumno(nombreTablaV3, nombreTablaV4, alumnoService.Alumno.Id, null, 0);
                    if (resultado == true)
                    {
                        throw new Exception("Se elimino el alumno");
                    }
                    else
                    {
                        throw new Exception("No se elimino alumno");
                    }
                    //throw new Exception("ocurrio un error NO se pudo Insertar el docente");
                }
                asignacionAutomatica.IdAlumno = alumnoService.Alumno.Id;
            }
            return listaErrores;
        }
        public AsignacionAutomaticaTemp ObtenerPorId(int idAsignacionAutomaticaTemp)
        {
            try
            {
                return _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerPorId(idAsignacionAutomaticaTemp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public AsignacionAutomaticaTemp ProcesarAsignacionAutomaticaLeadgen(LeadgenInformacionDTO leadgenInformacionDTO)
        {
            string formato = "MM/dd/yyyy HH:mm:ss";

            AsignacionAutomaticaTemp lead = new AsignacionAutomaticaTemp();

            var _areaFormacionRepositorio = _unitOfWork.AreaFormacionRepository;
            var _areaTrabajoRepositorio = _unitOfWork.AreaTrabajoRepository;
            var _industriaRepositorio = _unitOfWork.IndustriaRepository;
            var _cargoRepositorio = _unitOfWork.CargoRepository;
            var _alumnoRepositorio = _unitOfWork.AlumnoRepository;
            var _ciudadRepositorio = _unitOfWork.CiudadRepository;
            var _centroCostoRepositorio = _unitOfWork.CentroCostoRepository;
            var _tiempoCapacitacionRepositorio = _unitOfWork.TiempoCapacitacionRepository;
            var _pespecificoRepositorio = _unitOfWork.PEspecificoRepository;
            var _pgeneralRepositorio = _unitOfWork.PGeneralRepository;
            var _categoriaOrigenRepositorio = _unitOfWork.CategoriaOrigenRepository;
            var _tipoInteraccionRepositorio = _unitOfWork.TipoInteraccionRepository;

            try
            {
                int pais =0, region=0;
                string movil = ObtenerNumeroTelefonico(leadgenInformacionDTO.Telefono);
                movil = QuitarCerosIzquierda(movil);

                string idCampania = leadgenInformacionDTO.AdsetId;
                string ciudadStringTemp = QuitarCaracteres(leadgenInformacionDTO.Ciudad.ToLower());
                var listaPaises = new List<int>(new int[] { 51, 57, 591, 52 });
                List<string> nombresApellidosSeparados = ProcesarNombre(leadgenInformacionDTO.NombreCompleto);

                List<TCiudad> listaCiudades = new List<TCiudad>();

                int firma = 0;

                /*Comparacion Mexico*/
                var ciudadMexico = _ciudadRepositorio.FirstBy(x => x.Nombre.ToLower().StartsWith(string.Concat(ciudadStringTemp)) && x.IdPais == 52/*Mexico*/);

                if (ciudadMexico != null && !string.IsNullOrEmpty(movil))
                {
                    if (movil.StartsWith("5201") && movil.Length == 14)
                        movil = string.Concat("52", movil.Substring(4));
                    else if (movil.StartsWith("1") && movil.Length == 11)
                        movil = string.Concat("52", movil.Substring(1));
                    else if (movil.Length == 10)
                        movil = string.Concat("52", movil);

                    if (movil.StartsWith("52"))
                        listaCiudades.Add(ciudadMexico);
                }

                if (!listaCiudades.Any())
                {
                    if (ciudadStringTemp == "santiago de chile")
                    {
                        ciudadStringTemp = "santiago";
                    }
                    listaCiudades = _ciudadRepositorio.GetBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp) && a.IdPais != 52/*Mexico*/).ToList();
                    listaPaises.Remove(52);
                }

                foreach (TCiudad item in listaCiudades)
                {
                    if (item.IdPais == 51) firma += 51;
                    else if (item.IdPais == 57) firma += 57;
                    else if (item.IdPais == 591) firma += 591;
                    else if (item.IdPais == 52) firma += 52;
                    else if (item.IdPais == 56) firma += 56;
                }

                //CiudadBO ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Equals(ciudadStringTemp));
                TCiudad ciudadTemp = null;
                if (firma == 51)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                else if (firma == 57)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                else if (firma == 591)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                else if (firma == 52)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 52);
                else if (firma == 56)
                    ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 56);
                else if (firma == 108) // detectados colombia y peru
                {
                    if (movil.Length == 12) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                    else if (movil.Length == 11) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                }
                else if (firma == 648) // detectados colombia y bolivia
                {
                    if (movil.Length == 12) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 57);
                    else if (movil.Length == 11) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                }
                else if (firma == 642) // detectados peru y bolivia
                {
                    if (movil.Length == 11 && movil.StartsWith("591")) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 591);
                    else if (movil.Length == 11 && movil.StartsWith("51")) ciudadTemp = listaCiudades.FirstOrDefault(x => x.IdPais == 51);
                }

                if (ciudadTemp == null) // probablemente pusieron el nombre de la ciudad exacta y en nuestra db solo hacemos match con departamentos y por ello confunde un dato de colombia como mexico
                {
                    if (movil.Length == 11 && movil.StartsWith("591")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 2061).FirstOrDefault(); // la paz por defecto
                    else if (movil.Length == 11 && movil.StartsWith("51")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 14).FirstOrDefault();  // lima por defecto
                    else if (movil.Length == 12 && movil.StartsWith("57")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 1956).FirstOrDefault();  // bogota por defecto
                    //else if (movil.Length == 12 && movil.StartsWith("52")) ciudadTemp = _ciudadRepositorio.GetBy(x => x.Id == 25).FirstOrDefault();  // bogota por defecto
                }

                if (ciudadTemp == null)
                {
                    ciudadStringTemp = ciudadStringTemp.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'ó').Replace('ú', 'u');
                    ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp) && listaPaises.Contains(a.IdPais));
                    if (ciudadTemp == null) ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Nombre.ToLower().Contains(ciudadStringTemp));
                }


                if (ciudadTemp != null)
                {
                    pais = ciudadTemp.IdPais;
                    region = ciudadTemp.Id;

                    if (movil.Equals("")) movil = new string('0', ciudadTemp.LongCelular);
                    else
                    {
                        if (pais != 51)
                        {
                            //movil = "00" + movil;
                            if (pais == 57)
                            {
                                if (movil.Length > ciudadTemp.LongCelular)
                                {
                                    int resto = (movil.Length - ciudadTemp.LongCelular);
                                    movil = movil.Substring(resto, ciudadTemp.LongCelular);// "57 3178160602"
                                }
                            }
                            else if (pais == 591)
                            {
                                if (movil.Length > ciudadTemp.LongCelular)
                                {
                                    int resto = (movil.Length - ciudadTemp.LongCelular);
                                    movil = movil.Substring(resto, ciudadTemp.LongCelular);// "591 31781606"
                                }
                            }
                            else
                            {
                                movil = "00" + movil;
                            }

                        }
                        else
                        {
                            if (movil.StartsWith("51"))
                            {
                                var regex = new Regex(Regex.Escape("51"));
                                movil = regex.Replace(movil, "", 1);
                            }
                        }
                    }

                }


                if (ciudadStringTemp != null)
                {

                    //validacion solo para Mexico

                    var ciudadFacebook = ciudadStringTemp;

                    var detalle = _unitOfWork.FacebookFormularioLeadgenRepository.ObtenerDatosCiudadMexico(ciudadStringTemp);

                    if (detalle == null)
                    {

                        ciudadFacebook = ciudadStringTemp;
                        if (ciudadStringTemp.IndexOf("mexi", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            pais = 52;
                            if (pais == 52)
                            {
                                ciudadTemp = _ciudadRepositorio.FirstBy(x => x.Id == 25);
                                region = ciudadTemp.Id;
                            }
                        }
                    }

                    else if (detalle != null)
                    {
                        pais = 52;

                        var ciudadesTemp = _ciudadRepositorio.GetAll().ToList();

                        
                        if (ciudadesTemp.Any())
                        {
                            pais = detalle.IdPais;
                            region = detalle.Id;
                        }

                    }

                }


                else
                {
                    ciudadTemp = _ciudadRepositorio.FirstBy(a => a.Id == 2370);
                    pais = ciudadTemp.IdPais;
                    region = ciudadTemp.Id;
                     var ciudadFacebook = ciudadStringTemp;

                }
            


                string areaFormacion = QuitarCaracteres(leadgenInformacionDTO.AreaFormacion);
                string industria = QuitarCaracteres(leadgenInformacionDTO.Industria);
                string areaTrabajo = QuitarCaracteres(leadgenInformacionDTO.AreaTrabajo);
                string cargo = QuitarCaracteres(leadgenInformacionDTO.Cargo);
                string cc = ObtenerCentroCosto(leadgenInformacionDTO.AdsetName);
                string tiempoCapacitacion = QuitarCaracteres(leadgenInformacionDTO.InicioCapacitacion);
                string programaGeneral = QuitarCaracteres(leadgenInformacionDTO.CondicionalPregunta1);
                string modalidad = QuitarCaracteres(leadgenInformacionDTO.CondicionalPregunta2);

                if (leadgenInformacionDTO.FormularioRemarketing)
                {

                    TAlumno alumno = _alumnoRepositorio.FirstBy(x => x.Email1 == leadgenInformacionDTO.Email);
                    if (alumno != null)
                    {
                        lead.IdAreaFormacion = alumno.IdAformacion == null ? null : alumno.IdAformacion;
                        lead.IdAreaTrabajo = alumno.IdAtrabajo == null ? null : alumno.IdAtrabajo;
                        lead.IdIndustria = alumno.IdIndustria == null ? null : alumno.IdIndustria;
                        lead.IdCargo = alumno.IdCargo == null ? null : alumno.IdCargo;
                    }
                    else
                    {
                        lead.IdAreaFormacion = null;
                        lead.IdAreaTrabajo = null;
                        lead.IdIndustria = null;
                        lead.IdCargo = null;
                    }
                }
                else
                {
                    var objAreaFormacion = _areaFormacionRepositorio.FirstBy(x => x.Nombre == areaFormacion, s => new { s.Id });
                    if (objAreaFormacion != null) lead.IdAreaFormacion = objAreaFormacion.Id;
                    else lead.IdAreaFormacion = 0;
                    try
                    {
                        lead.IdAreaTrabajo = _areaTrabajoRepositorio.FirstBy(x => x.Nombre == areaTrabajo).Id;
                    }
                    catch
                    {
                        lead.IdAreaTrabajo = 29;//29 es el Id De 'Otros'
                    }
                    lead.IdIndustria = industria == "x" ? lead.IdIndustria : _industriaRepositorio.FirstBy(x => x.Nombre == industria).Id;
                    lead.IdCargo = _cargoRepositorio.FirstBy(x => x.Nombre == cargo).Id;
                }

                var objCentroCosto = _centroCostoRepositorio.FirstBy(x => x.Nombre.Contains(cc), s => new { s.Id });
                if (objCentroCosto != null) lead.IdCentroCosto = objCentroCosto.Id;

                PgeneralIdPaginaDTO pgeneralIdPaginaDTO = _pgeneralRepositorio.ObtenerIdPagina(lead.IdCentroCosto ?? 0);
                if (pgeneralIdPaginaDTO != null) lead.IdPagina = pgeneralIdPaginaDTO.IdPagina;

                var objTiempoCapacitacion = _tiempoCapacitacionRepositorio.FirstBy(a => a.Nombre.ToUpper().Equals(tiempoCapacitacion.ToUpper()), s => new { s.Id });
                if (objTiempoCapacitacion != null) lead.IdTiempoCapacitacion = objTiempoCapacitacion.Id;
                else lead.IdTiempoCapacitacion = 0;

                if (programaGeneral != "x" && modalidad != "x")
                {
                    PespecificoCentroCostoDTO pespecificoCentroCostoDTO = _pespecificoRepositorio.ObtenerCentroCostoPresencial(programaGeneral, modalidad);
                    if (pespecificoCentroCostoDTO == null) pespecificoCentroCostoDTO = _pespecificoRepositorio.ObtenerCentroCostoOnline(programaGeneral);
                    lead.IdCentroCosto = pespecificoCentroCostoDTO.IdCentroCosto;
                }

                lead.Correo = leadgenInformacionDTO.Email.Trim();
                lead.IdFaseOportunidad = 2;
                lead.Fijo = "";
                lead.Movil = movil;
                lead.NombrePrograma = cc;

                try
                {
                    var centroCostoFinal = _centroCostoRepositorio.ObtenerCentrosCostoPorNombre(lead.NombrePrograma);

                    if (centroCostoFinal != null)
                    {
                        if (lead.IdCentroCosto != null && lead.IdCentroCosto != centroCostoFinal.IdCentroCosto)
                        {
                            lead.IdCentroCosto = centroCostoFinal.IdCentroCosto;
                        }
                    }
                }
                catch (Exception e)
                {
                }


                lead.Nombres = nombresApellidosSeparados[0];
                lead.Apellidos = nombresApellidosSeparados[1];


                if (leadgenInformacionDTO.AdsetName.Contains("-REMKT-"))
                {
                    lead.Origen = "Facebook Remarketing Formulario Facebook";
                    lead.IdCategoriaDato = ValorEstatico.IdFacebookRemarketingFormulario;
                }
                else if (leadgenInformacionDTO.AdsetName.Contains("-CP-"))
                {

                    lead.Origen = "Fomulario Facebook 5 Campos";
                    lead.IdCategoriaDato = ValorEstatico.IdFacebookFormulario5Campos;
                }
                else if (leadgenInformacionDTO.AdsetName.Contains("-MKT-VIS-"))
                {

                    lead.Origen = "Facebook Remarketing VIS Formulario Facebook";
                    lead.IdCategoriaDato = ValorEstatico.IdFacebookFormularioVisitante;
                }
                else if (leadgenInformacionDTO.FormularioRemarketing)
                {
                    lead.Origen = "Facebook Remarketing Formulario Facebook";
                    lead.IdCategoriaDato = ValorEstatico.IdFacebookRemarketingFormulario;
                }

            
                lead.IdTipoInteraccion = _tipoInteraccionRepositorio.FirstBy(x => x.Nombre == "Paso - 1").Id;
                lead.IdPais = pais;
                lead.IdCiudad = region;
                lead.IdTipoDato = ValorEstatico.IdTipoDatoLanzamiento;
                //lead.FechaRegistroCampania = DateTime.Parse(leadgenInformacionDTO.created_time);
                lead.FechaRegistroCampania = DateTime.ParseExact(leadgenInformacionDTO.created_time, formato, CultureInfo.InvariantCulture);
                return lead;

            }


            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    


        public string ObtenerNumeroTelefonico(string numero)
        {
            StringBuilder nuevoNumero = new StringBuilder();
            for (var i = 0; i < numero.Length; i++)
            {
                if (char.IsNumber(numero[i]))
                {
                    nuevoNumero.Append(numero[i].ToString());
                }
            }
            return nuevoNumero.ToString();
        }

        public string QuitarCerosIzquierda(string numero)
        {
            int i = 0;
            for (; i < numero.Length; i++)
                if (numero[i] != '0') break;

            return numero.Substring(i);
        }
        public string QuitarCaracteres(string cadena)
        {
            if (cadena != null)
            {
                cadena = cadena.Replace("_", " ").Trim();
            }
            return cadena;
        }
        public List<string> ProcesarNombre(string fullName)
        {
            List<string> nombresApellidos = new List<string>();
            fullName = fullName.ToLower();
            fullName = fullName.Replace('á', 'a').Replace('é', 'e').Replace('í', 'i').Replace('ó', 'o')
                        .Replace('ú', 'u').Replace('ñ', 'n');
            char delimiter = ' ';
            string[] substrings = fullName.Split(delimiter);
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            for (int i = 0; i < substrings.Length; i++)
            {
                if (!string.IsNullOrEmpty(substrings[i]))
                {
                    nombresApellidos.Add(myTI.ToTitleCase(substrings[i]));
                }
            }
            List<string> nombresApellidosSeparados = new List<string>();
            return SepararNombresApellidos(nombresApellidos);

        }
        public List<string> SepararNombresApellidos(List<string> lista)
        {
            List<string> NombresApellidos = new List<string>();
            switch (lista.Count)
            {
                case 1:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[0]);
                    break;
                case 2:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[1]);
                    break;
                case 3:
                    NombresApellidos.Add(lista[0]);
                    NombresApellidos.Add(lista[1] + " " + lista[2]);
                    break;
                case 4:
                    NombresApellidos.Add(lista[0] + " " + lista[1]);
                    NombresApellidos.Add(lista[2] + " " + lista[3]);
                    break;
                default:
                    string nombres = "";
                    for (int i = 0; i < lista.Count - 2; i++)
                    {
                        if (i == 0)
                            nombres = lista[i];
                        else
                            nombres = nombres + " " + lista[i];
                    }
                    NombresApellidos.Add(nombres);
                    NombresApellidos.Add(lista[lista.Count - 2] + " " + lista[lista.Count - 1]);
                    break;
            }
            return NombresApellidos;
        }

        public string ObtenerCentroCosto(string cadena)
        {
            char delimiter = '-';
            string[] substrings = cadena.Split(delimiter);
            return QuitarEspacios(substrings[0]);
        }
        public string QuitarEspacios(string cadena)
        {
            string tmp = cadena;
            tmp = cadena.Trim();
            return tmp;
        }


        /// <summary>
        /// Procesa el registro del formulario nuevo del portal web
        /// </summary>
        /// <param name="idRegistroPortalWeb">Id del registro del portal web</param>
        /// <param name="idPagina">Id de la pagina de donde proviene el dato</param>
        public AsignacionAutomaticaTemp ProcesarRegistroFormularioNuevoPortalWeb(string idRegistroPortalWeb, int idPagina)
        {
            //Traemos el registro de la pagina web y lo guardamos de manera temporal
           // AsignacionAutomaticaTemp datos = new AsignacionAutomaticaTemp();
            AsignacionAutomaticaTemModeloDTO registro = _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerNuevosRegistroById(idRegistroPortalWeb, idPagina);
            if (registro == null)
            {
                throw new Exception("Registro no se encontro o ya fue procesado");
            }
            else
            {
               var datos = MapearAsignacionAutomaticaTemporal(registro);

                datos.Nombres = datos.Nombres;
                datos.Apellidos = datos.Apellidos;
                datos.Correo = datos.Correo;
                datos.Fijo = datos.Fijo;
                datos.Movil = datos.Movil;
                datos.IdPais = datos.IdPais;
                datos.IdCiudad = datos.IdCiudad;
                datos.IdCargo = datos.IdCargo;
                datos.IdAreaTrabajo = datos.IdAreaTrabajo;

                datos.IdIndustria = datos.IdIndustria;
                datos.NombrePrograma = datos.NombrePrograma;
                datos.IdCentroCosto = datos.IdCentroCosto;
                datos.CentroCosto = datos.CentroCosto;
                datos.IdTipoDato = datos.IdTipoDato;
                datos.IdFaseOportunidad = datos.IdFaseOportunidad;
                datos.Procesado = datos.Procesado;
                datos.IdConjuntoAnuncio = datos.IdConjuntoAnuncio;
                datos.IdFaseOportunidadPortal = datos.IdFaseOportunidadPortal;
                datos.FechaRegistroCampania = datos.FechaRegistroCampania;
                datos.IdTiempoCapacitacion = datos.IdTiempoCapacitacion;
                datos.IdCategoriaDato = datos.IdCategoriaDato;
                datos.IdTipoInteraccion = datos.IdTipoInteraccion;
                datos.IdInteraccionFormulario = datos.IdInteraccionFormulario;
                datos.UrlOrigen = datos.UrlOrigen;
                datos.IdPagina = datos.IdPagina;
                datos.IdAnuncioFacebook = datos.IdAnuncioFacebook;
                datos.IdFacebookFormularioLeadgen = datos.IdFacebookFormularioLeadgen;
                datos.AptoProcesamiento = datos.AptoProcesamiento;
                datos.FechaCreacion = DateTime.Now;
                datos.FechaModificacion = DateTime.Now;
                datos.Estado = true;
                datos.UsuarioCreacion = "Signal";
                datos.UsuarioModificacion = "Signal";
                return datos;
     
    }
        }

        /// Autor:Margiory Ramirez
        /// Fecha: 08/03/2021
        /// Version: 1.0
        /// <summary>
        /// Mapea Oportunidades a Asignacion Automatica Temp 
        /// </summary>
        /// <param name="nuevo"> Lista de oportunidades del portal web </param>  
        public AsignacionAutomaticaTemp MapearAsignacionAutomaticaTemporal(AsignacionAutomaticaTemModeloDTO nuevo)
        {
            var datos = new AsignacionAutomaticaTemp();
            datos.Id = 0;
            datos.Nombres = nuevo.Nombres;
            datos.Apellidos = nuevo.Apellidos;
            datos.IdAreaFormacion = nuevo.IdAreaFormacion;
            datos.IdAreaTrabajo = nuevo.IdAreaTrabajo;
            datos.IdCargo = nuevo.IdCargo;

            if (nuevo.CentroCosto == null || nuevo.CentroCosto.Equals(""))
            {
                bool esAdwords = EsAdwords((int)nuevo.IdCategoriaOrigen);

                if (esAdwords)
                {
                    if (nuevo.Campania != null)
                    {
                        var centroCostoAdws = _unitOfWork.CategoriaOrigenRepository.ObtenerCentroCostoPorCampania(nuevo.Campania);
                        datos.CentroCosto = centroCostoAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoAdws.IdCentroCosto;
                    }
                    else if (nuevo.IdConjuntoAnuncio != null)
                    {
                        var nombreCampania = _unitOfWork.AsignacionAutomaticaTempRepository.ObtenerNombreCampaniaPorIdFaseOportunidad(nuevo.IdFaseOportunidadPortal);
                        var centroCostoPorCampaniaAdws = _unitOfWork.CentroCostoRepository.ObtenerCentroCostoPorNombreIdConjuntoAnuncio((int)nombreCampania.IdConjuntoAnuncio, nombreCampania.NombreCampania);
                        datos.CentroCosto = centroCostoPorCampaniaAdws.CentroCosto;
                        nuevo.IdCentroCosto = centroCostoPorCampaniaAdws.IdCentroCosto;
                    }
                    else
                    {
                        datos.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                    }
                }
                else
                {
                    datos.CentroCosto = "REGISTRO CENTRO DE COSTO 2020 I LIMA";
                }
            }
            else
            {
                datos.CentroCosto = nuevo.CentroCosto;
            }

            if (nuevo.IdCentroCosto == null || nuevo.IdCentroCosto.Value == 0)
            {
                datos.IdCentroCosto = 15907;
            }
            else
            {
                datos.IdCentroCosto = nuevo.IdCentroCosto;
            }

            datos.Correo = nuevo.Correo;
            datos.IdFaseOportunidad = nuevo.IdFaseOportunidad;
            datos.Fijo = nuevo.Fijo;
            datos.IdIndustria = nuevo.IdIndustria;
            datos.Movil = nuevo.Movil;
            datos.NombrePrograma = nuevo.NombrePrograma;
            datos.Origen = nuevo.IdOrigen;
            datos.IdPais = nuevo.Pais == null ? 0 : Convert.ToInt32(nuevo.Pais);
            datos.Procesado = false;
            datos.IdCiudad = nuevo.Ciudad == null ? 0 : Convert.ToInt32(nuevo.Ciudad);
            datos.IdTipoDato = nuevo.IdTipoDato;
            datos.IdConjuntoAnuncio = nuevo.IdConjuntoAnuncio;

            if (nuevo.FechaRegistroCampania != null)
            {
                try
                {
                    datos.FechaRegistroCampania = Convert.ToDateTime(nuevo.FechaRegistroCampania);//???
                }
                catch (Exception)
                {
                    datos.FechaRegistroCampania = DateTime.Now;
                }
            }
            else
            {
                datos.FechaRegistroCampania = DateTime.Now;
            }
            if (Guid.TryParse(nuevo.IdFaseOportunidadPortal, out Guid idFaseOportunidadPortal))
            {
                datos.IdFaseOportunidadPortal = idFaseOportunidadPortal;
            }
            datos.IdTipoInteraccion = nuevo.IdTipoInteraccion;
            datos.IdCategoriaDato = nuevo.IdCategoriaOrigen;
            datos.IdInteraccionFormulario = nuevo.IdInteraccionFormulario;
            datos.UrlOrigen = nuevo.UrlOrigen;
            datos.IdTiempoCapacitacion = nuevo.IdTiempoCapacitacion;
            datos.IdPagina = nuevo.IdPagina;
            datos.FechaCreacion = DateTime.Now;
            datos.FechaModificacion = DateTime.Now;
            datos.UsuarioCreacion = "System";
            datos.UsuarioModificacion = "System";
            datos.Estado = true;
            return datos;
        }





    }

}


