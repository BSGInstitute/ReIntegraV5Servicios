using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: GoogleResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de reseñas de Google Places de BSG Institute.
    /// CRUD, consultas de administración vía SP, sincronización con Google Places API
    /// y Business Profile API (OAuth2) y notificación por correo.
    /// </summary>
    public class GoogleResenaService : IGoogleResenaService
    {
        private readonly IUnitOfWork     _unitOfWork;
        private readonly IConfiguration  _configuration;
        private readonly Mapper          _mapper;

        // ── Configuración de las APIs de Google ─────────────────────────────────
        private const string URL_BASE_GOOGLE_PLACES = "https://places.googleapis.com/v1/places";
        private const string CAMPO_MASCARA          = "displayName,rating,userRatingCount,reviews";
        private const string URL_TOKEN_GOOGLE       = "https://oauth2.googleapis.com/token";
        private const string URL_ACCOUNTS           = "https://mybusinessaccountmanagement.googleapis.com/v1/accounts";
        private const string URL_BUSINESS_INFO      = "https://mybusinessbusinessinformation.googleapis.com/v1";
        private const string URL_MYBUSINESS_V4      = "https://mybusiness.googleapis.com/v4";
        private const string USUARIO_SISTEMA        = "google.sincronizacion";
        private const string USUARIO_FALLBACK       = "SISTEMA";
        private const int PAGE_SIZE_REVIEWS         = 50;

        public GoogleResenaService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork    = unitOfWork;
            _configuration = configuration;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TGoogleResena, GoogleResena>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region CRUD

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña y persiste los cambios.
        /// </summary>
        /// <param name="entidad">Entidad GoogleResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        public GoogleResena Add(GoogleResena entidad)
        {
            var modelo = _unitOfWork.GoogleResenaRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GoogleResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una reseña y persiste los cambios.
        /// </summary>
        /// <param name="entidad">Entidad GoogleResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        public GoogleResena Update(GoogleResena entidad)
        {
            var modelo = _unitOfWork.GoogleResenaRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GoogleResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente una reseña por su Id.
        /// </summary>
        /// <param name="id">Identificador de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si la eliminación fue exitosa.</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.GoogleResenaRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listadoEntidad">Lista de entidades GoogleResena a insertar.</param>
        /// <returns>Lista de entidades insertadas.</returns>
        public List<GoogleResena> Add(List<GoogleResena> listadoEntidad)
        {
            var modelo = _unitOfWork.GoogleResenaRepository.Add(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<GoogleResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listadoEntidad">Lista de entidades GoogleResena a actualizar.</param>
        /// <returns>Lista de entidades actualizadas.</returns>
        public List<GoogleResena> Update(List<GoogleResena> listadoEntidad)
        {
            var modelo = _unitOfWork.GoogleResenaRepository.Update(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<GoogleResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente un listado de reseñas por sus Ids.
        /// </summary>
        /// <param name="listadoIds">Lista de Ids de reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si la eliminación fue exitosa.</returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            _unitOfWork.GoogleResenaRepository.Delete(listadoIds, usuario);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Consultas de Administración

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de reseñas ejecutando mkt.SP_GoogleResenaObtenerDatos (modo Grilla).
        /// </summary>
        /// <param name="filtro">Filtros: sedes, visibilidad, rating (1-5), rango de fechas, paginación.</param>
        /// <returns>GoogleResenaGrillaPaginadaDTO con datos y metadatos de paginación.</returns>
        public GoogleResenaGrillaPaginadaDTO ObtenerGrilla(GoogleResenaGrillaFiltroDTO filtro)
        {
            return _unitOfWork.GoogleResenaRepository.ObtenerGrilla(filtro);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las sedes configuradas con estadísticas agregadas ejecutando
        /// mkt.SP_GoogleResenaObtenerDatos (modo Sede).
        /// </summary>
        /// <returns>Lista de GoogleResenaSedeItemDTO.</returns>
        public List<GoogleResenaSedeItemDTO> ObtenerSedes()
        {
            return _unitOfWork.GoogleResenaRepository.ObtenerSedes();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las sedes de Google Places para el combo de filtros del frontend.
        /// Consulta directa a mkt.T_GooglePlacesConfiguracion vía EF Core.
        /// </summary>
        /// <returns>Lista de GoogleResenaSedeComboDTO.</returns>
        public List<GoogleResenaSedeComboDTO> ObtenerSedesCombo()
        {
            return _unitOfWork.GoogleResenaRepository.ObtenerSedesCombo();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca las reseñas indicadas como visibles (Mostrar=true).
        /// </summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        public bool MarcarResenaVisible(GoogleResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.GoogleResenaRepository.MarcarResenaVisible(
                dto.IdsGoogleResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca las reseñas indicadas como ocultas (Mostrar=false).
        /// </summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>true si se actualizaron correctamente.</returns>
        public bool MarcarResenaOculta(GoogleResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.GoogleResenaRepository.MarcarResenaOculta(
                dto.IdsGoogleResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Sincronización con Google API

        public async Task<string> SincronizarGoogleApi(string nombreUsuario)
        {
            var emailDestinatario  = ObtenerEmailUsuario(nombreUsuario);
            var inicio             = DateTime.UtcNow;
            var resultadosPorSede  = new List<GoogleResenaSedeResultadoDTO>();

            var credencial = _unitOfWork.GooglePlacesCredencialApiRepository
                .FirstBy(w => w.Estado == true);

            if (credencial == null)
                throw new Exception("No se encontró una credencial activa en mkt.T_GooglePlacesCredencialApi. Configure la API Key antes de sincronizar.");

            var apiKey = credencial.ApiKey;

            var sedesConfiguradas = _unitOfWork.GooglePlacesConfiguracionRepository
                .GetBy(w => w.Estado == true)
                .ToList();

            if (!sedesConfiguradas.Any())
                throw new Exception("No hay sedes configuradas activas en mkt.T_GooglePlacesConfiguracion.");

            using var clienteHttp = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };

            // Intentar OAuth2 para obtener TODAS las reseñas via Business Profile API
            string accessToken = null;
            Dictionary<string, string> locationNamesPorPlaceId = null;
            bool usarBusinessProfileApi = !string.IsNullOrEmpty(credencial.OAuth2ClientId)
                                       && !string.IsNullOrEmpty(credencial.OAuth2ClientSecret)
                                       && !string.IsNullOrEmpty(credencial.OAuth2RefreshToken);

            var diagnostico = new List<string>();

            if (usarBusinessProfileApi)
            {
                try
                {
                    accessToken = await ObtenerAccessTokenAsync(clienteHttp,
                        credencial.OAuth2ClientId, credencial.OAuth2ClientSecret, credencial.OAuth2RefreshToken);
                    diagnostico.Add("OAuth2: Access Token obtenido OK");

                    locationNamesPorPlaceId = await DescubrirLocationsPorPlaceIdAsync(clienteHttp, accessToken);
                    diagnostico.Add($"Business Profile: {locationNamesPorPlaceId.Count} location(s) descubiertas");

                    foreach (var kvp in locationNamesPorPlaceId)
                        diagnostico.Add($"  PlaceId={kvp.Key} → {kvp.Value}");
                }
                catch (Exception ex)
                {
                    diagnostico.Add($"OAuth2 ERROR: {ex.Message}");
                    usarBusinessProfileApi = false;
                    accessToken = null;
                    locationNamesPorPlaceId = null;
                }
            }
            else
            {
                diagnostico.Add("OAuth2: No configurado, usando solo Places API con API Key");
            }

            foreach (var sede in sedesConfiguradas)
            {
                var idConfiguracion = sede.Id;
                var placeId         = sede.IdentificadorCuenta;
                var nombreSede      = sede.NombreSede;

                var resultado = new GoogleResenaSedeResultadoDTO
                {
                    IdGooglePlacesConfiguracion = idConfiguracion,
                    IdentificadorCuenta = placeId,
                    NombreSede = nombreSede
                };

                try
                {
                    var resenasExistentes = _unitOfWork.GoogleResenaRepository
                        .ObtenerResenasPorSede(idConfiguracion)
                        .ToDictionary(r => r.IdentificadorResena ?? string.Empty, StringComparer.Ordinal);

                    // Combinar AMBAS fuentes para maximizar reseñas capturadas
                    var reviewsConsolidados = new Dictionary<string, dynamic>(StringComparer.OrdinalIgnoreCase);
                    decimal ratingFinal = 0m;
                    int totalOpinionesFinal = 0;

                    int resenasBusinessProfile = 0;
                    int resenasPlacesApi = 0;

                    // Fuente 1: Business Profile API (OAuth2) → reseñas con paginación
                    if (usarBusinessProfileApi && locationNamesPorPlaceId != null
                        && locationNamesPorPlaceId.TryGetValue(placeId, out string locationName))
                    {
                        try
                        {
                            var respBP = await ConsultarBusinessProfileReviewsAsync(clienteHttp, accessToken, locationName);
                            if (respBP != null)
                            {
                                ratingFinal = respBP.Rating;
                                totalOpinionesFinal = respBP.UserRatingCount;
                                foreach (var r in respBP.Reviews)
                                {
                                    string key = ExtraerClaveReview(r);
                                    if (!string.IsNullOrEmpty(key)) reviewsConsolidados[key] = r;
                                }
                                resenasBusinessProfile = respBP.Reviews.Count;
                                diagnostico.Add($"{nombreSede}: Business Profile → {respBP.Reviews.Count} reseñas (totalReviewCount={respBP.UserRatingCount})");
                            }
                        }
                        catch (Exception ex)
                        {
                            diagnostico.Add($"{nombreSede}: Business Profile ERROR → {ex.Message}");
                        }
                    }
                    else if (usarBusinessProfileApi)
                    {
                        diagnostico.Add($"{nombreSede}: PlaceId '{placeId}' no encontrado en Business Profile locations");
                    }

                    // Fuente 2: Places API (API Key) → reseñas adicionales por variación de idioma
                    try
                    {
                        var respPlaces = await ConsultarGooglePlacesApiAsync(clienteHttp, placeId, apiKey);
                        if (respPlaces != null)
                        {
                            if (ratingFinal == 0m) ratingFinal = respPlaces.Rating;
                            if (totalOpinionesFinal == 0) totalOpinionesFinal = respPlaces.UserRatingCount;
                            int antesPlaces = reviewsConsolidados.Count;
                            foreach (var r in respPlaces.Reviews)
                            {
                                string key = ExtraerClaveReview(r);
                                if (!string.IsNullOrEmpty(key) && !reviewsConsolidados.ContainsKey(key))
                                    reviewsConsolidados[key] = r;
                            }
                            resenasPlacesApi = reviewsConsolidados.Count - antesPlaces;
                            diagnostico.Add($"{nombreSede}: Places API → {respPlaces.Reviews.Count} reseñas ({resenasPlacesApi} nuevas únicas)");
                        }
                    }
                    catch (Exception ex)
                    {
                        diagnostico.Add($"{nombreSede}: Places API ERROR → {ex.Message}");
                    }

                    diagnostico.Add($"{nombreSede}: TOTAL consolidado → {reviewsConsolidados.Count} reseñas únicas (BP={resenasBusinessProfile}, Places={resenasPlacesApi})");

                    if (ratingFinal == 0m && !reviewsConsolidados.Any())
                    {
                        resultado.Exitoso      = false;
                        resultado.MensajeError = $"Sin respuesta de ninguna API para {nombreSede} ({placeId}).";
                        resultadosPorSede.Add(resultado);
                        continue;
                    }

                    resultado.TotalOpinionesGoogle = totalOpinionesFinal;
                    resultado.PromedioValoracion       = ratingFinal;

                    var porInsertar   = new List<GoogleResena>();
                    var porActualizar = new List<GoogleResena>();
                    int sinCambios    = 0;

                    foreach (var review in reviewsConsolidados.Values)
                    {
                        var dto = ConvertirReviewADto(review);
                        if (dto == null || string.IsNullOrEmpty(dto.IdentificadorResena)) continue;

                        if (resenasExistentes.TryGetValue(dto.IdentificadorResena, out GoogleResena existente))
                        {
                            if (TieneCambiosRespectoDatos(existente, dto))
                            {
                                AplicarCambiosDesdeApi(existente, dto);
                                porActualizar.Add(existente);
                            }
                            else { sinCambios++; }
                        }
                        else
                        {
                            var nueva = CrearResenaDesdeApi(dto, idConfiguracion);
                            porInsertar.Add(nueva);
                            resenasExistentes[dto.IdentificadorResena] = nueva;
                        }
                    }

                    if (porInsertar.Any())   _unitOfWork.GoogleResenaRepository.Add(porInsertar);
                    if (porActualizar.Any()) _unitOfWork.GoogleResenaRepository.Update(porActualizar);
                    if (porInsertar.Any() || porActualizar.Any()) _unitOfWork.Commit();

                    resultado.ResenasNuevas       = porInsertar.Count;
                    resultado.ResenasActualizadas = porActualizar.Count;
                    resultado.ResenasSinCambios   = sinCambios;
                    resultado.TotalDescargadas    = porInsertar.Count + porActualizar.Count + sinCambios;
                    resultado.Exitoso             = true;

                    Debug.WriteLine($"[Google] {nombreSede} COMPLETADO | nuevas={porInsertar.Count} " +
                                    $"act={porActualizar.Count} sin={sinCambios}");
                }
                catch (Exception ex)
                {
                    resultado.Exitoso      = false;
                    resultado.MensajeError = ex.Message;
                    Debug.WriteLine($"[Google] ERROR {nombreSede}: {ex.Message}");
                }

                resultadosPorSede.Add(resultado);
            }

            var resumen = ConstruirResumen(inicio, resultadosPorSede);
            EnviarCorreoResumen(resumen, resultadosPorSede, emailDestinatario);

            return JsonConvert.SerializeObject(
                new
                {
                    Resumen           = resumen,
                    ResultadosPorSede = resultadosPorSede,
                    Diagnostico       = diagnostico
                },
                Formatting.Indented);
        }

        #endregion

        #region OAuth2 – Token Exchange

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Intercambia un RefreshToken por un AccessToken mediante OAuth2.</summary>
        /// <param name="clienteHttp">Cliente HTTP reutilizable.</param>
        /// <param name="clientId">Client ID de la aplicación OAuth2.</param>
        /// <param name="clientSecret">Client Secret de la aplicación OAuth2.</param>
        /// <param name="refreshToken">Refresh Token almacenado en BD.</param>
        /// <returns>string</returns>
        private async Task<string> ObtenerAccessTokenAsync(
            HttpClient clienteHttp, string clientId, string clientSecret, string refreshToken)
        {
            var contenido = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "client_id",     clientId },
                { "client_secret", clientSecret },
                { "refresh_token", refreshToken },
                { "grant_type",    "refresh_token" }
            });

            var respuesta = await clienteHttp.PostAsync(URL_TOKEN_GOOGLE, contenido);
            var json      = await respuesta.Content.ReadAsStringAsync();

            if (!respuesta.IsSuccessStatusCode)
                throw new Exception($"Error al obtener Access Token de Google: {json}");

            dynamic tokenResp = JsonConvert.DeserializeObject<dynamic>(json);
            string accessToken = (string)tokenResp?.access_token;

            if (string.IsNullOrEmpty(accessToken))
                throw new Exception($"Google no devolvió access_token. Respuesta: {json}");

            return accessToken;
        }

        #endregion

        #region Google Business Profile API – Descubrir Locations

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Descubre las locations de Business Profile y las mapea por PlaceId.</summary>
        /// <param name="clienteHttp">Cliente HTTP reutilizable.</param>
        /// <param name="accessToken">Access Token OAuth2 vigente.</param>
        /// <returns>Dictionary de placeId a locationName</returns>
        private async Task<Dictionary<string, string>> DescubrirLocationsPorPlaceIdAsync(
            HttpClient clienteHttp, string accessToken)
        {
            var resultado = new Dictionary<string, string>(StringComparer.Ordinal);

            var cuentas = await ListarCuentasAsync(clienteHttp, accessToken);
            if (cuentas == null || !cuentas.Any())
            {
                Debug.WriteLine("[Google] No se encontraron cuentas de Business Profile.");
                return resultado;
            }

            Debug.WriteLine($"[Google] Cuentas encontradas: {string.Join(", ", cuentas)}");

            foreach (var accountName in cuentas)
            {
                try
                {
                    var url = $"{URL_BUSINESS_INFO}/{accountName}/locations?readMask=name,title,metadata";
                    string pageToken = null;

                    do
                    {
                        var urlPaginada = pageToken != null ? $"{url}&pageToken={pageToken}" : url;
                        var request = new HttpRequestMessage(HttpMethod.Get, urlPaginada);
                        request.Headers.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                        var resp = await clienteHttp.SendAsync(request);
                        var body = await resp.Content.ReadAsStringAsync();

                        if (!resp.IsSuccessStatusCode)
                        {
                            Debug.WriteLine($"[Google] Error listando locations de {accountName}: {body}");
                            break;
                        }

                        dynamic json = JsonConvert.DeserializeObject<dynamic>(body);

                        if (json?.locations != null)
                        {
                            foreach (var loc in json.locations)
                            {
                                string placeId      = (string)loc?.metadata?.placeId;
                                string locationName = (string)loc?.name;

                                if (!string.IsNullOrEmpty(placeId) && !string.IsNullOrEmpty(locationName))
                                {
                                    // Business Information API v1 devuelve name como "locations/123"
                                    // pero el v4 reviews endpoint necesita "accounts/xxx/locations/123"
                                    if (!locationName.StartsWith("accounts/"))
                                        locationName = $"{accountName}/{locationName}";

                                    resultado[placeId] = locationName;
                                    Debug.WriteLine($"[Google] Location descubierta: placeId={placeId} → {locationName} ({(string)loc?.title})");
                                }
                            }
                        }

                        pageToken = (string)json?.nextPageToken;
                    } while (!string.IsNullOrEmpty(pageToken));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Google] Error procesando locations de {accountName}: {ex.Message}");
                }
            }

            return resultado;
        }

        /// <summary>Lista las cuentas de Google Business Profile del usuario autenticado.</summary>
        /// <param name="clienteHttp">Cliente HTTP reutilizable.</param>
        /// <param name="accessToken">Access Token OAuth2 vigente.</param>
        /// <returns>List de string con los nombres de cuenta (accounts/xxx)</returns>
        private async Task<List<string>> ListarCuentasAsync(HttpClient clienteHttp, string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, URL_ACCOUNTS);
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

            var resp = await clienteHttp.SendAsync(request);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                throw new Exception($"Error al listar cuentas de Google Business Profile: {body}");

            dynamic json = JsonConvert.DeserializeObject<dynamic>(body);
            var cuentas = new List<string>();

            if (json?.accounts != null)
            {
                foreach (var account in json.accounts)
                {
                    string name = (string)account?.name;
                    if (!string.IsNullOrEmpty(name)) cuentas.Add(name);
                }
            }

            return cuentas;
        }

        #endregion

        #region Google Business Profile API – Reviews con paginación

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Consulta las reseñas de una location vía Google Business Profile API v4 con paginación.</summary>
        /// <param name="clienteHttp">Cliente HTTP reutilizable.</param>
        /// <param name="accessToken">Access Token OAuth2 vigente.</param>
        /// <param name="locationName">Nombre completo de la location (accounts/xxx/locations/yyy).</param>
        /// <returns>RespuestaGooglePlacesApi</returns>
        private async Task<RespuestaGooglePlacesApi> ConsultarBusinessProfileReviewsAsync(
            HttpClient clienteHttp, string accessToken, string locationName)
        {
            var respuesta = new RespuestaGooglePlacesApi();
            var reviewsUnicos = new Dictionary<string, dynamic>(StringComparer.Ordinal);
            string pageToken = null;
            int totalReviews = 0;
            int pagina = 0;

            do
            {
                pagina++;
                var url = $"{URL_MYBUSINESS_V4}/{locationName}/reviews?pageSize={PAGE_SIZE_REVIEWS}";
                if (!string.IsNullOrEmpty(pageToken))
                    url += $"&pageToken={pageToken}";

                Debug.WriteLine($"[Google] Reviews {locationName} - Página {pagina}, URL: {url}");

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var resp = await clienteHttp.SendAsync(request);
                var body = await resp.Content.ReadAsStringAsync();

                if (!resp.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"[Google] Error Business Profile reviews ({(int)resp.StatusCode}) {locationName}: {body}");
                    if (pagina == 1)
                        throw new Exception($"Business Profile API error ({(int)resp.StatusCode}) para {locationName}: {body}");
                    break;
                }

                dynamic json = JsonConvert.DeserializeObject<dynamic>(body);

                if (pagina == 1)
                {
                    respuesta.Rating          = (decimal?)json?.averageRating ?? 0m;
                    respuesta.UserRatingCount = (int?)json?.totalReviewCount ?? 0;
                    totalReviews = respuesta.UserRatingCount;
                    Debug.WriteLine($"[Google] {locationName}: totalReviewCount={totalReviews}, averageRating={respuesta.Rating}");
                }

                int enEstaPagina = 0;
                if (json?.reviews != null)
                {
                    foreach (var review in json.reviews)
                    {
                        string reviewName = (string)review?.name ?? (string)review?.reviewId;
                        if (!string.IsNullOrEmpty(reviewName) && !reviewsUnicos.ContainsKey(reviewName))
                        {
                            reviewsUnicos[reviewName] = review;
                            enEstaPagina++;
                        }
                    }
                }

                pageToken = (string)json?.nextPageToken;

                Debug.WriteLine($"[Google] {locationName} página {pagina}: +{enEstaPagina} nuevas, {reviewsUnicos.Count}/{totalReviews} total, nextPageToken={(!string.IsNullOrEmpty(pageToken) ? "sí" : "no")}");

            } while (!string.IsNullOrEmpty(pageToken));

            Debug.WriteLine($"[Google] {locationName} FINALIZADO: {reviewsUnicos.Count} reseñas descargadas de {totalReviews} reportadas por Google");

            respuesta.Reviews = reviewsUnicos.Values.ToList();
            return respuesta;
        }

        #endregion

        #region Consulta a Google Places API (Fallback con API Key)

        private static readonly string[] VARIACIONES_IDIOMA = { "es", "en", null };

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Consulta la Google Places API (New) con API Key como fallback de OAuth2.</summary>
        /// <param name="clienteHttp">Cliente HTTP reutilizable.</param>
        /// <param name="placeId">Identificador de la sede en Google Places.</param>
        /// <param name="apiKey">API Key de Google Places.</param>
        /// <returns>RespuestaGooglePlacesApi</returns>
        private async Task<RespuestaGooglePlacesApi> ConsultarGooglePlacesApiAsync(
            HttpClient clienteHttp, string placeId, string apiKey)
        {
            var respuestaConsolidada = new RespuestaGooglePlacesApi();
            var reviewsUnicos = new Dictionary<string, dynamic>(StringComparer.Ordinal);

            foreach (var idioma in VARIACIONES_IDIOMA)
            {
                try
                {
                    var url = string.IsNullOrEmpty(idioma)
                        ? $"{URL_BASE_GOOGLE_PLACES}/{placeId}"
                        : $"{URL_BASE_GOOGLE_PLACES}/{placeId}?languageCode={idioma}";

                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("X-Goog-Api-Key", apiKey);
                    request.Headers.Add("X-Goog-FieldMask", CAMPO_MASCARA);

                    var httpResp  = await clienteHttp.SendAsync(request);
                    var contenido = await httpResp.Content.ReadAsStringAsync();

                    if (!httpResp.IsSuccessStatusCode)
                    {
                        Debug.WriteLine($"[Google] Error API ({(int)httpResp.StatusCode}) idioma={idioma ?? "null"}: {contenido}");
                        continue;
                    }

                    dynamic json = JsonConvert.DeserializeObject<dynamic>(contenido);

                    if (respuestaConsolidada.Rating == 0m)
                    {
                        respuestaConsolidada.Rating          = (decimal?)json?.rating ?? 0m;
                        respuestaConsolidada.UserRatingCount = (int?)json?.userRatingCount ?? 0;
                    }

                    if (json?.reviews != null)
                    {
                        foreach (var review in json.reviews)
                        {
                            string reviewName = (string)review?.name;
                            if (!string.IsNullOrEmpty(reviewName) && !reviewsUnicos.ContainsKey(reviewName))
                                reviewsUnicos[reviewName] = review;
                        }
                    }

                    Debug.WriteLine($"[Google] {placeId} idioma={idioma ?? "null"} => {reviewsUnicos.Count} únicas acumuladas");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[Google] Excepción {placeId} idioma={idioma ?? "null"}: {ex.Message}");
                }
            }

            if (respuestaConsolidada.Rating == 0m && !reviewsUnicos.Any())
                return null;

            respuestaConsolidada.Reviews = reviewsUnicos.Values.ToList();
            return respuestaConsolidada;
        }

        #endregion

        #region Conversión de datos y comparación

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Convierte un item del JSON de la API al DTO interno.</summary>
        /// <param name="review">Objeto dinámico de la reseña.</param>
        /// <returns>GoogleResenaPlacesApiDTO</returns>
        private GoogleResenaPlacesApiDTO ConvertirReviewADto(dynamic review)
        {
            try
            {
                // Business Profile API format: reviewer.displayName, comment, starRating, createTime
                // Places API (New) format: authorAttribution.displayName, text.text, rating, publishTime
                bool esBusinessProfile = review?.reviewer != null;

                if (esBusinessProfile)
                {
                    string starRating = (string)review?.starRating;
                    int rating = ConvertirStarRatingANumero(starRating);

                    return new GoogleResenaPlacesApiDTO
                    {
                        IdentificadorResena        = (string)review?.name ?? (string)review?.reviewId,
                        NombreAutor                = (string)review?.reviewer?.displayName,
                        FotoAutor                  = NormalizarUrlFoto((string)review?.reviewer?.profilePhotoUrl),
                        UriAutor                   = null,
                        Valoracion                 = rating,
                        TextoResena                = (string)review?.comment,
                        IdiomaResena               = null,
                        FechaResena                = (DateTime?)review?.createTime ?? (DateTime?)review?.updateTime ?? DateTime.UtcNow,
                        DescripcionTiempoRelativo  = null,
                        UriGoogleMaps              = null
                    };
                }

                return new GoogleResenaPlacesApiDTO
                {
                    IdentificadorResena        = (string)review?.name,
                    NombreAutor                = (string)review?.authorAttribution?.displayName,
                    FotoAutor                  = NormalizarUrlFoto((string)review?.authorAttribution?.photoUri),
                    UriAutor                   = (string)review?.authorAttribution?.uri,
                    Valoracion                 = (int?)review?.rating ?? 0,
                    TextoResena                = (string)review?.text?.text,
                    IdiomaResena               = (string)review?.text?.languageCode,
                    FechaResena                = (DateTime?)review?.publishTime ?? DateTime.UtcNow,
                    DescripcionTiempoRelativo  = (string)review?.relativePublishTimeDescription,
                    UriGoogleMaps              = (string)review?.googleMapsUri
                };
            }
            catch { return null; }
        }

        /// <summary>Convierte el enum starRating de la Business Profile API a un entero 1-5.</summary>
        /// <param name="starRating">Valor del enum (ONE, TWO, THREE, FOUR, FIVE).</param>
        /// <returns>int</returns>
        private static int ConvertirStarRatingANumero(string starRating)
        {
            switch (starRating)
            {
                case "ONE":   return 1;
                case "TWO":   return 2;
                case "THREE": return 3;
                case "FOUR":  return 4;
                case "FIVE":  return 5;
                default:      return 0;
            }
        }

        /// <summary>Extrae la clave única de deduplicación de una reseña.</summary>
        /// <param name="review">Objeto dinámico de la reseña.</param>
        /// <returns>string</returns>
        private static string ExtraerClaveReview(dynamic review)
        {
            return (string)review?.name ?? (string)review?.reviewId;
        }

        /// <summary>Normaliza URLs de fotos de perfil (agrega https: si inicia con //).</summary>
        /// <param name="url">URL de la foto del autor.</param>
        /// <returns>string</returns>
        private static string NormalizarUrlFoto(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return null;
            url = url.Trim();
            if (url.StartsWith("//"))
                return "https:" + url;
            return url;
        }

        /// <summary>Compara si la reseña existente difiere de los datos de la API.</summary>
        /// <param name="existente">Reseña existente en BD.</param>
        /// <param name="dto">Datos obtenidos de la API.</param>
        /// <returns>bool</returns>
        private bool TieneCambiosRespectoDatos(GoogleResena existente, GoogleResenaPlacesApiDTO dto)
        {
            if (existente.Valoracion != dto.Valoracion) return true;
            if ((existente.TextoResena ?? "") != (dto.TextoResena ?? "")) return true;
            return false;
        }

        /// <summary>Aplica los cambios de la API sobre una reseña existente.</summary>
        /// <param name="existente">Reseña existente en BD.</param>
        /// <param name="dto">Datos obtenidos de la API.</param>
        private void AplicarCambiosDesdeApi(GoogleResena existente, GoogleResenaPlacesApiDTO dto)
        {
            existente.Valoracion                     = dto.Valoracion;
            existente.TextoResena                = dto.TextoResena ?? string.Empty;
            existente.NombreAutor                = dto.NombreAutor ?? existente.NombreAutor;
            existente.FotoAutor                  = dto.FotoAutor ?? existente.FotoAutor;
            existente.UriAutor                   = dto.UriAutor ?? existente.UriAutor;
            existente.IdiomaResena               = dto.IdiomaResena ?? existente.IdiomaResena;
            existente.DescripcionTiempoRelativo  = dto.DescripcionTiempoRelativo ?? existente.DescripcionTiempoRelativo;
            existente.UriGoogleMaps              = dto.UriGoogleMaps ?? existente.UriGoogleMaps;
            existente.UsuarioModificacion        = USUARIO_SISTEMA;
            existente.FechaModificacion          = DateTime.Now;
        }

        /// <summary>Crea una nueva entidad GoogleResena a partir de datos de la API.</summary>
        /// <param name="dto">Datos obtenidos de la API.</param>
        /// <param name="idGooglePlacesConfiguracion">Id de la sede a la que pertenece.</param>
        /// <returns>GoogleResena</returns>
        private GoogleResena CrearResenaDesdeApi(GoogleResenaPlacesApiDTO dto, int idGooglePlacesConfiguracion)
        {
            var ahora = DateTime.Now;
            return new GoogleResena
            {
                IdGooglePlacesConfiguracion = idGooglePlacesConfiguracion,
                IdentificadorResena        = dto.IdentificadorResena,
                NombreAutor                = dto.NombreAutor ?? string.Empty,
                FotoAutor                  = dto.FotoAutor,
                UriAutor                   = dto.UriAutor,
                Valoracion                 = dto.Valoracion,
                TextoResena                = dto.TextoResena ?? string.Empty,
                IdiomaResena               = dto.IdiomaResena,
                FechaResena                = dto.FechaResena,
                DescripcionTiempoRelativo  = dto.DescripcionTiempoRelativo,
                UriGoogleMaps              = dto.UriGoogleMaps,
                Mostrar                    = false,
                Estado                     = true,
                UsuarioCreacion            = USUARIO_SISTEMA,
                UsuarioModificacion        = USUARIO_SISTEMA,
                FechaCreacion              = ahora,
                FechaModificacion          = ahora
            };
        }

        #endregion

        #region Resumen y notificación

        /// <summary>Construye el DTO resumen global de la sincronización.</summary>
        /// <param name="inicio">Fecha/hora de inicio de la sincronización.</param>
        /// <param name="sedes">Resultados individuales por sede.</param>
        /// <returns>GoogleResenaSincronizacionResumenDTO</returns>
        private GoogleResenaSincronizacionResumenDTO ConstruirResumen(
            DateTime inicio, List<GoogleResenaSedeResultadoDTO> sedes)
            => new GoogleResenaSincronizacionResumenDTO
            {
                FechaSincronizacion      = inicio,
                TotalSedesProcesadas     = sedes.Count(r => r.Exitoso),
                TotalSedesConError       = sedes.Count(r => !r.Exitoso),
                TotalResenasProcesadas   = sedes.Sum(r => r.TotalDescargadas),
                TotalResenasNuevas       = sedes.Sum(r => r.ResenasNuevas),
                TotalResenasActualizadas = sedes.Sum(r => r.ResenasActualizadas),
                TotalResenasSinCambios   = sedes.Sum(r => r.ResenasSinCambios),
                DuracionSegundos         = (int)(DateTime.UtcNow - inicio).TotalSeconds
            };

        private const string CORREO_COPIA_OCULTA = "mmantilla@bsginstitute.com";
        private const string CORREO_REMITENTE = "soporte@bsginstitute.com";

        /// <summary>Obtiene el email del usuario logueado. Fallback al correo de copia oculta.</summary>
        /// <param name="nombreUsuario">UserName del usuario logueado.</param>
        /// <returns>string</returns>
        private string ObtenerEmailUsuario(string nombreUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreUsuario)) return CORREO_COPIA_OCULTA;
                var email = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(nombreUsuario);
                return string.IsNullOrWhiteSpace(email) ? CORREO_COPIA_OCULTA : email;
            }
            catch { return CORREO_COPIA_OCULTA; }
        }

        /// <summary>Envía un correo HTML con el resumen de la sincronización de Google Places.</summary>
        /// <param name="resumen">Resumen global de la sincronización.</param>
        /// <param name="sedes">Resultados individuales por sede.</param>
        /// <param name="emailDestinatario">Email del destinatario principal.</param>
        private void EnviarCorreoResumen(
            GoogleResenaSincronizacionResumenDTO resumen,
            List<GoogleResenaSedeResultadoDTO> sedes,
            string emailDestinatario)
        {
            try
            {
                var hayErrores    = sedes.Any(s => !s.Exitoso || !string.IsNullOrEmpty(s.MensajeError));
                var estadoGeneral = hayErrores ? "Con advertencias" : "Exitoso";
                var colorEstado   = hayErrores ? "#e67e22" : "#27ae60";
                var iconoEstado   = hayErrores ? "&#9888;" : "&#10004;";

                var filasHtml = new StringBuilder();
                foreach (var s in sedes)
                {
                    var colorFila   = s.Exitoso ? "#27ae60" : "#c0392b";
                    var textoEstado = s.Exitoso ? "OK" : "Error";
                    var bgFila      = s.Exitoso ? "#fff" : "#fef3cd";

                    filasHtml.Append($@"
                    <tr style='background:{bgFila}'>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee'>{s.NombreSede ?? s.IdentificadorCuenta}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center;color:{colorFila};font-weight:600'>{textoEstado}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center'>{s.ResenasNuevas}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center'>{s.ResenasActualizadas}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center'>{s.TotalDescargadas}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center;font-weight:600'>{s.PromedioValoracion:F1}</td>
                    </tr>");
                }

                var sedesConError  = sedes.Where(s => !string.IsNullOrEmpty(s.MensajeError)).ToList();
                var seccionErrores = "";
                if (sedesConError.Any())
                {
                    var listaErrores = new StringBuilder();
                    foreach (var s in sedesConError)
                        listaErrores.Append($"<li style='margin-bottom:4px'><b>{s.NombreSede ?? s.IdentificadorCuenta}:</b> {s.MensajeError}</li>");

                    seccionErrores = $@"
                    <div style='background:#fef3cd;padding:12px 16px;border-radius:6px;margin-top:16px;border-left:4px solid #e67e22'>
                      <p style='margin:0 0 6px;font-size:13px;font-weight:600;color:#856404'>Observaciones</p>
                      <ul style='margin:0;padding-left:18px;font-size:12px;color:#856404'>{listaErrores}</ul>
                    </div>";
                }

                var mailService = new TMK_MailService();
                var mailData = new TMKMailDataDTO
                {
                    Sender    = CORREO_REMITENTE,
                    Recipient = emailDestinatario,
                    Subject   = $"Sincronizacion Resenas Google Places - {estadoGeneral} - {resumen.TotalResenasNuevas} nueva(s) - {resumen.FechaSincronizacion:dd/MM/yyyy HH:mm}",
                    Cc        = "",
                    Bcc       = CORREO_COPIA_OCULTA,
                    AttachedFiles = null,
                    Message   = $@"
                        <div style='font-family:Segoe UI,Arial,sans-serif;max-width:640px;margin:0 auto;background:#ffffff'>

                          <!-- Header -->
                          <div style='background:#4285F4;padding:20px 24px;border-radius:8px 8px 0 0'>
                            <h1 style='margin:0;font-size:16px;color:#ffffff;font-weight:600'>Sincronizacion de Resenas Google Places</h1>
                            <p style='margin:4px 0 0;font-size:12px;color:rgba(255,255,255,0.7)'>{resumen.FechaSincronizacion:dd/MM/yyyy HH:mm} UTC</p>
                          </div>

                          <div style='padding:20px 24px;border:1px solid #e5e7eb;border-top:none;border-radius:0 0 8px 8px'>

                            <!-- Estado -->
                            <div style='display:flex;align-items:center;padding:12px 16px;background:#f8f9fa;border-radius:6px;margin-bottom:20px;border-left:4px solid {colorEstado}'>
                              <span style='font-size:18px;margin-right:8px'>{iconoEstado}</span>
                              <span style='font-size:14px;color:#333'>Estado: <b style='color:{colorEstado}'>{estadoGeneral}</b></span>
                            </div>

                            <!-- Metricas -->
                            <table style='width:100%;margin-bottom:20px' cellpadding='0' cellspacing='0'>
                              <tr>
                                <td style='width:25%;text-align:center;padding:12px 8px;background:#e8f0fe;border-radius:6px'>
                                  <div style='font-size:22px;font-weight:700;color:#4285F4'>{resumen.TotalResenasNuevas}</div>
                                  <div style='font-size:11px;color:#6b7280;margin-top:2px'>Nuevas</div>
                                </td>
                                <td style='width:8px'></td>
                                <td style='width:25%;text-align:center;padding:12px 8px;background:#e8f0fe;border-radius:6px'>
                                  <div style='font-size:22px;font-weight:700;color:#4285F4'>{resumen.TotalResenasActualizadas}</div>
                                  <div style='font-size:11px;color:#6b7280;margin-top:2px'>Actualizadas</div>
                                </td>
                                <td style='width:8px'></td>
                                <td style='width:25%;text-align:center;padding:12px 8px;background:#e8f0fe;border-radius:6px'>
                                  <div style='font-size:22px;font-weight:700;color:#4285F4'>{resumen.TotalResenasProcesadas}</div>
                                  <div style='font-size:11px;color:#6b7280;margin-top:2px'>Total procesadas</div>
                                </td>
                                <td style='width:8px'></td>
                                <td style='width:25%;text-align:center;padding:12px 8px;background:#e8f0fe;border-radius:6px'>
                                  <div style='font-size:22px;font-weight:700;color:#4285F4'>{resumen.DuracionSegundos}s</div>
                                  <div style='font-size:11px;color:#6b7280;margin-top:2px'>Duracion</div>
                                </td>
                              </tr>
                            </table>

                            <!-- Tabla detalle -->
                            <table style='width:100%;border-collapse:collapse;font-size:13px'>
                              <thead>
                                <tr style='background:#4285F4'>
                                  <th style='padding:10px 12px;text-align:left;color:#fff;font-weight:600;border-radius:6px 0 0 0'>Sede</th>
                                  <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Estado</th>
                                  <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Nuevas</th>
                                  <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Actualizadas</th>
                                  <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Total</th>
                                  <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600;border-radius:0 6px 0 0'>Rating</th>
                                </tr>
                              </thead>
                              <tbody>{filasHtml}</tbody>
                            </table>

                            {seccionErrores}
                          </div>

                          <!-- Footer -->
                          <div style='text-align:center;padding:16px 0'>
                            <p style='margin:0;font-size:11px;color:#9ca3af'>BSG Institute &middot; {DateTime.Now:yyyy} &middot; Integra V5</p>
                          </div>

                        </div>"
                };

                mailService.SetData(mailData);
                mailService.SendMessageTask();
            }
            catch { /* Errores de correo no interrumpen el proceso */ }
        }

        #endregion

        #region Clases internas

        /// <summary>Modelo interno para la respuesta consolidada de la Google Places API.</summary>
        private class RespuestaGooglePlacesApi
        {
            public decimal Rating          { get; set; }
            public int     UserRatingCount { get; set; }
            public List<dynamic> Reviews   { get; set; } = new();
        }

        #endregion
    }
}
