using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.LinkedIn;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.LinkedIn
{
    public class LinkedInApiService : ILinkedInApiService
    {
        private IUnitOfWork _unitOfWork;
        private string permisosHeader;
  
        private const string API_WOLKBOX = "agentbox.php";
        private WolkboxTokenLogDTO _wolkboxTokenLog = new();
        private StringDTO token;
        private bool _tokenValido = false;
        public LinkedInApiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            token = _unitOfWork.LinkedInApiRepository.ObtenerToken();

        }


        public async Task<bool> ObtenerDatos()
        {
            var form = false;
            var grupocampaña = false;
            var campañas = false;
            var leads = false;
            var EstadoEnvio = _unitOfWork.LinkedInApiRepository.ObtenerEstadoEnvio(1);
            if (EstadoEnvio.Valor == false && EstadoEnvio.Valor != null)
            {
                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                form = await ObtenerForms();
                grupocampaña = await ObtenerGrupoCampañas();

                if (grupocampaña == true)
                {
                    campañas = await ObtenerCampañas();
                }
                else
                {
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(3);
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                    return false;
                }
                if (campañas && form)
                {
                    leads = await ObtenerLeads();
                }
                else
                {
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(6);
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                    return false;
                }
                if (leads)
                {
                    var resultadoportunindad = GenerarOportunindadesLead();
                    if (resultadoportunindad != null)
                    {
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                        return true;

                    }
                    else
                    {
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(5);
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                        return false;
                    }
                }
                else
                {
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                    return false;
                }
            }
            else
            {
                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(1);
                return false;
            }



        }


        public async Task<bool> ObtenerForms()
        {
            
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(2);
            try
            {
                int sponsoredAccount = 512131517;
                int start = 0;
                int count = 100;
                permisosHeader = "Tipo1";
                bool registrado = false;
                bool hasMoreData = true;

                while (hasMoreData)
                {
                    var parametros = new List<string>
            {
                $"adForms?q=account&account=urn:li:sponsoredAccount:{sponsoredAccount}",
                $"totals=true",
                $"start={start}",
                $"count={count}"
            };

                    var (jsonString, statusCode) = await SolicitudHttpLinkedin(parametros);
                    if (statusCode != HttpStatusCode.OK)
                    {
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(2);
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(2);
                        return false;
                    }

                    JObject jsonResponse;
                    try
                    {
                        jsonResponse = JObject.Parse(jsonString);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON inválido: {ex.Message}");
                        return false;
                    }

                    var elements = jsonResponse["elements"] as JArray;
                    if (elements == null || elements.Count == 0)
                    {
                        return true;
                    }

                    foreach (var element in elements)
                    {
                        IntDTO? value;
                        JObject formElemet = (JObject)element["form"];
                        JArray questions = (JArray)formElemet["questions"];
                        FormularioFormDTO form = JsonConvert.DeserializeObject<FormularioFormDTO>(element.ToString());
                        FormLinkedinDTO entidad = new()
                        {
                            Id = form.id,
                            Form_name = form.form.name,
                            Form_description = form.form.description,
                            Form_headline = form.form.headline,
                            Question = questions.ToString(),
                            PaddingStart = 0
                        };

                        if (entidad != null)
                        {
                            value = _unitOfWork.LinkedInApiRepository.ObtenerPorIdForm(entidad.Id);
                            if (value.Valor == null)
                            {
                                _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoForm(entidad);
                                if (form?.form?.questions != null)
                                {
                                    foreach (var question in form.form.questions)
                                    {
                                        if (question.TypeSpecificQuestionDetails.ComLinkedinAdsTextQuestionDetails != null)
                                        {
                                            var questionForm = new QuestionFormDTO
                                            {
                                                IdLinkedInForm = entidad.Id,
                                                IdQuestionForm = question.questionId,
                                                Question = question.question,
                                                Respuesta = question.question
                                            };
                                            _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoQuestionForm(questionForm);
                                        }
                                        else if (question.TypeSpecificQuestionDetails.ComLinkedinAdsMultipleChoiceQuestionDetails?.Options != null)
                                        {
                                            var options = question.TypeSpecificQuestionDetails.ComLinkedinAdsMultipleChoiceQuestionDetails.Options;
                                            int optionIndex = 0;
                                            foreach (var option in options)
                                            {
                                                var optionText = option.ComLinkedinAdsTextOptionQuestion?.Text;
                                                if (!string.IsNullOrEmpty(optionText))
                                                {
                                                    var questionForm = new QuestionFormDTO
                                                    {
                                                        IdLinkedInForm = entidad.Id,
                                                        IdQuestionForm = question.questionId,
                                                        Question = question.question,
                                                        IdMultiSelect = optionIndex,
                                                        Respuesta = optionText
                                                    };

                                                    _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoQuestionForm(questionForm);
                                                    optionIndex++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }

                  
                    hasMoreData = elements.Count == count;
                    start += count;
                }
                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(2);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        public async Task<bool> ObtenerGrupoCampañas()
        {
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(3);
            try
            {
                int sponsoredAccount = 512131517;
                int start = 0;
                int count = 100;
                permisosHeader = "Tipo2";
                bool hasMoreData = true;
                int maxIterations = 100; // Evitar un bucle infinito
                int iteration = 0;

                while (hasMoreData && iteration < maxIterations)
                {
                    iteration++;

                    var parametros = new List<string>
                    {
                        $"adCampaignGroupsV2?q=search&search.status.values[0]=ACTIVE&search.status.values[1]=DRAFT&search.status.values[2]=PAUSED&search.account.values[0]=urn:li:sponsoredAccount:{sponsoredAccount}",
                        $"start={start}",
                        $"count={count}"
                    };

                    var (jsonString, statusCode) = await SolicitudHttpLinkedin(parametros);
                    if (statusCode != HttpStatusCode.OK)
                    {
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(3);
                        _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(3);
                        return false;
                    }

                    JObject jsonResponse;
                    try
                    {
                        jsonResponse = JObject.Parse(jsonString);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"JSON inválido: {ex.Message}");
                        return false;
                    }

                    var elements = jsonResponse["elements"] as JArray;
                    if (elements == null || elements.Count == 0)
                    {
                        return true;
                    }

                    foreach (var element in elements)
                    {
                        IntDTO? value;
                        GrupoCampanasLinkedInDTO groupCampaign = JsonConvert.DeserializeObject<GrupoCampanasLinkedInDTO>(element.ToString());
                        long timestamp = groupCampaign.ChangeAuditStamps.LastModified.Time;
                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        DateTime dateTime = dateTimeOffset.UtcDateTime;
                        GroupCampaignLinkedInDTO entidad = new()
                        {
                            Id = groupCampaign.Id,
                            name = groupCampaign.Name,
                            status = groupCampaign.Status,
                            lastModified = dateTime,
                        };

                        if (entidad != null)
                        {
                            value = _unitOfWork.LinkedInApiRepository.ObtenerPorIdGrupoCampaign(entidad.Id);
                            if (value?.Valor == null)
                            {
                                _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoGrupoCampaign(entidad);
                            }
                        }
                    }

      
                    hasMoreData = elements.Count == count;

                    start += count;
                }

                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(3);
                return true;  // Siempre retorna true si completa el proceso
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }



        public async Task<bool> ObtenerCampañas()
        {
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(4);
            try
            {
                int sponsoredAccount = 512131517;
                List<IntDTO> groupCampaignIds = _unitOfWork.LinkedInApiRepository.BusquedaDeGruposCampaign();
                int count = 100;
                permisosHeader = "Tipo1";

                foreach (var groupId in groupCampaignIds)
                {
                    bool hasMoreData = true;
                    int start = 0;

                    while (hasMoreData)
                    {
                        var parametros = new List<string>
                {

                    $"adCampaignsV2?q=search&search.status.values[0]=ACTIVE",
                        $"search.status.values[1]=DRAFT",
                        $"search.status.values[2]=PAUSED",
                        $"search.account.values[0]=urn:li:sponsoredAccount:{sponsoredAccount}",
                        $"search.campaignGroup.values[0]=urn:li:sponsoredCampaignGroup:{groupId.Valor}",
                        $"start={start}",
                        $"count={count}"
                };

                        var (jsonString, statusCode) = await SolicitudHttpLinkedin(parametros);
                        if (statusCode != HttpStatusCode.OK)
                        {
                            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(4);
                            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(4);
                            return false;
                        }

                        JObject jsonResponse;
                        try
                        {
                            jsonResponse = JObject.Parse(jsonString);
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"JSON inválido: {ex.Message}");
                            return false;
                        }

                        var elements = jsonResponse["elements"] as JArray;
                        if (elements == null || elements.Count == 0)
                        {
                            hasMoreData = false;
                            continue;
                        }

                        foreach (var element in elements)
                        {
                            CampanasLinkedInDTO Campaign = JsonConvert.DeserializeObject<CampanasLinkedInDTO>(element.ToString());
                            long timestamp = Campaign.ChangeAuditStamps.LastModified.Time;
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                            DateTime dateTime = dateTimeOffset.UtcDateTime;

                            CampaignLinkedInDTO entidad = new()
                            {
                                Id = Campaign.Id,
                                IdLinkedInGroupCampaign = groupId.Valor,
                                Nombre = Campaign.Name,
                                Status = Campaign.Status,
                                ObjectiveType = Campaign.ObjectiveType,
                                PacingStrategy = Campaign.PacingStrategy,
                                TypeCampaign = Campaign.Type,
                                UltimaModificacion = dateTime
                            };

                            var value = _unitOfWork.LinkedInApiRepository.ObtenerPorIdCampaign(entidad.Id);
                            if (value.Valor == null)
                            {
                                _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoCampaign(entidad);
                            }
                        }

                        // Actualiza `hasMoreData` y `start` para gestionar la paginación
                        hasMoreData = elements.Count == count;
                        start += count;
                    }
                }
                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(4);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }



        private async Task<(string resultado, HttpStatusCode statusCode)> SolicitudHttpLinkedin(List<string> parametros)
        {
            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                try
                {
                    var queryParams = string.Empty;
                    if (parametros != null && parametros.Count > 0)
                    {
                        queryParams = string.Join("&", parametros);
                    }

                    var urlBase = $"https://api.linkedin.com/v2/";
                    var urlApi = $"{urlBase}{queryParams}";

                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Valor);
                        client.DefaultRequestHeaders.Add("LinkedIn-Version", "202505");

                        if (permisosHeader == "Tipo2")
                        {
                            //client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
                        }

                        var response = await client.GetAsync(urlApi);
                        var jsonString = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            return (jsonString, response.StatusCode);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en la solicitud HTTP: {ex.Message}");
                    
                    throw;
                }

              
                intentos++;
                Console.WriteLine($"Reintentando... ({intentos}/{maxIntentos})");

               
                await Task.Delay(1000);
            }

            throw new Exception("No se pudo completar la solicitud después de varios intentos.");
        }

        public async Task<bool> ObtenerLeads()
        {
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(6);
            try
            {
                int sponsoredAccount = 512131517;
                List<IntDTO> idFormList = _unitOfWork.LinkedInApiRepository.BusquedaDeForms();
                int count = 100;
                permisosHeader = "Tipo1";

                foreach (var formId in idFormList)
                {
                    bool hasMoreData = true;
                    int start = 0;

                    while (hasMoreData)
                    {
                        var parametros = new List<string>
                {
                    $"adFormResponses?q=account&account=urn:li:sponsoredAccount:{sponsoredAccount}",
                    $"form=urn:li:adForm:{formId.Valor}",
                    $"start={start}",
                    $"count={count}"
                };

                        var (jsonString, statusCode) = await SolicitudHttpLinkedin(parametros);
                        if (statusCode != HttpStatusCode.OK)
                        {
                            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviadoError(6);
                            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(6);
                            return false;
                        }

                        JObject jsonResponse;
                        try
                        {
                            jsonResponse = JObject.Parse(jsonString);
                        }
                        catch (JsonException ex)
                        {
                            Console.WriteLine($"JSON inválido: {ex.Message}");
                            return false;
                        }

                        var elements = jsonResponse["elements"] as JArray;
                        if (elements == null || elements.Count == 0)
                        {
                            hasMoreData = false;
                            continue;
                        }

                        foreach (var element in elements)
                        {
                            LinkedInLeadDTO lead = JsonConvert.DeserializeObject<LinkedInLeadDTO>(element.ToString());
                            long timestamp = lead.FormResponse.SubmittedAt;
                            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                            DateTime dateTime = dateTimeOffset.UtcDateTime;
                            int campaignId = ExtractCampaignId(lead.Campaign);
                            string leadIdentifier = ExtractLeadId(lead.Id);

                            LinkedInLeadApiDTO entidad = new()
                            {
                                IdLinkedInLead = leadIdentifier,
                                IdLinkedInCampaign = campaignId,
                                IdLinkedInForm = formId.Valor,
                                LeadType = lead.LeadType,
                                Question = JsonConvert.SerializeObject(lead.FormResponse.Answers),
                                TestLead = lead.TestLead,
                                FechaLead = dateTime
                            };

                            var value = _unitOfWork.LinkedInApiRepository.ObtenerPorIdLead(entidad.IdLinkedInLead);
                            if (value.Valor == null)
                            {
                                _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoLeadLinkedIn(entidad);

                                foreach (var answer in lead.FormResponse.Answers)
                                {
                                    int questionId = ExtractQuestionId(answer.Question);
                                    string answerText = GetAnswerFromDetails(answer.AnswerDetails);

                                    QuestionLeadDTO questionLead = new()
                                    {
                                        IdLinkedInLead = entidad.IdLinkedInLead,
                                        IdLinkedInForm = formId.Valor,
                                        IdQuestionForm = questionId,
                                        Answer = answerText,
                                    };

                                    _unitOfWork.LinkedInApiRepository.InsertarObjetoSerializadoQuestionLead(questionLead);
                                }
                            }
                        }

                        hasMoreData = elements.Count == count;
                        start += count;
                    }
                }
                _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(6);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }


        private int ExtractQuestionId(string question)
        {
            var match = System.Text.RegularExpressions.Regex.Match(question, @".*,(\d+)\)$");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }

        private string GetAnswerFromDetails(AnswerDetailsDto answerDetails)
        {
            if (answerDetails.TextQuestionAnswer != null)
            {
                return answerDetails.TextQuestionAnswer.Answer;
            }
            if (answerDetails.MultipleChoiceAnswer != null && answerDetails.MultipleChoiceAnswer.Options.Count > 0)
            {
                var firstOption = answerDetails.MultipleChoiceAnswer.Options[0];
                if (firstOption.IndexDto != null)
                {
                    return firstOption.IndexDto.Index.ToString();
                }
            }
            return string.Empty;
        }

        private int ExtractCampaignId(string campaign)
        {
            var match = System.Text.RegularExpressions.Regex.Match(campaign, @"urn:li:sponsoredCampaign:(\d+)");
            return match.Success ? int.Parse(match.Groups[1].Value) : 0;
        }
        private string ExtractLeadId(string leadId)
        {
            var match = System.Text.RegularExpressions.Regex.Match(leadId, @"urn:li:adFormResponse:(.+)$");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }


        public object GenerarOportunindadesLead()
        {
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(5);
            List<StringDTO> IdLeads = _unitOfWork.LinkedInApiRepository.BuscarLeadsSinOportunindades();
            try
            {
                List<InformacionBaseOportunidad> lead = _unitOfWork.LinkedInApiRepository.BuscarDatoLead();
                List<InformacionBaseOportunidad> leadAceptados = _unitOfWork.LinkedInApiRepository.BuscarDatoLeadAprobados();
                List<InformacionBaseOportunidad> leadRevisar = _unitOfWork.LinkedInApiRepository.BuscarDatoLeadARevisar();
                var insertarRevisados = _unitOfWork.LinkedInApiRepository.InsertaOportunidadesparaRevisar();
                
                DateTime fechaHoraActual = DateTime.Now;
                IOportunidadService servicio = new OportunidadService(_unitOfWork);
                var usuario = "mkilimajer";
                if (leadAceptados != null)
                {
                    var resultado = servicio.ProcesarInformacionOportunidadesLinkedIn(leadAceptados, usuario);
                    int totalRegistrosLead = leadAceptados.Count;
                    ValidacionOportunidadCreadaLeadDTO valiOpo = new()
                    {
                        Fecha = fechaHoraActual,
                        Registros = totalRegistrosLead
                    };
                    if (lead.Count != 0)
                    {
                        _unitOfWork.LinkedInApiRepository.ValidarOportunindadesCreadas(valiOpo);

                        _unitOfWork.LinkedInApiRepository.RegistrarOportunidadesNoCreadads(fechaHoraActual);
                    }

                    
                    _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(5);
                    return resultado;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            _unitOfWork.LinkedInApiRepository.ActualizarEstadoEnviado(5);
            return true;
        }

        public IEnumerable<ReporteLeadsDTO> ObtenerReporteLeads()
        {
            return _unitOfWork.LinkedInApiRepository.ObtenerReporteLeads();
        }
        public IEnumerable<ReporteLeadsDTO> ObtenerReporteLeadsByFecha(FiltroLandingPagePortaLinkedInDTO filtro)
        {
        
            var leads = _unitOfWork.LinkedInApiRepository.ObtenerReporteLeadsByFecha(filtro);
            var alumnoService = new AlumnoService(_unitOfWork);
            foreach (var lead in leads)
            {
                if (!string.IsNullOrWhiteSpace(lead.Correo))
                    lead.Correo = alumnoService.EncriptarCorreoHash(lead.Correo);

                if (!string.IsNullOrWhiteSpace(lead.Celular))
                    lead.Celular = alumnoService.EncriptarNumeroHash(Regex.Replace(lead.Celular, @"[^\d]", ""));
            }
            return leads;
        }



        public IEnumerable<ReporteLeadsPendientesDTO> ObtenerReportePendientes()
        {
            return _unitOfWork.LinkedInApiRepository.ObtenerReportePendientes();
        }

        public bool Actualizar(LinkedInActualizarDTO dto, string usuario)
        {
            try
            {
                var existe = _unitOfWork.LinkedInApiRepository.ObtenerIdPorGuidLinkedInLead(dto.GuidLinkedInLead);
                var valorPais = _unitOfWork.LinkedInApiRepository.ObtenerQuestionLeadForm(dto.GuidLinkedInLead);
                if (valorPais!=null)
                {
                    if(valorPais.Pais != dto.Pais)
                    {
                        _unitOfWork.LinkedInApiRepository.ActualizarPaisQuestionLeadForm(dto, usuario);
                    }
                }
                if (existe.Valor == null)
                {
                    var respuesta = _unitOfWork.LinkedInApiRepository.CrearFormularioRegularizado(dto, usuario);
                    if (respuesta == true)
                        return true;
                    else
                        return false;
                }
                else
                {
                    var respuesta = _unitOfWork.LinkedInApiRepository.ActualizarFormularioRegularizado(dto, usuario);
                    if (respuesta == true)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool SubirOportunidadesPendientes(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario)) return false;

            try
            {
                var url = "https://localhost:44366/api/LinkedIn/SubirOportunidadesPendientes"
                          + "?usuario=" + Uri.EscapeDataString(usuario);

                using var http = new HttpClient();
                var resp = http.GetAsync(url).GetAwaiter().GetResult();
                return resp.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }



        public BoolDTO ValidarCreacionOportunidadLinkedinEstado()
        {
            {
                return _unitOfWork.LinkedInApiRepository.ValidarCreacionOportunidadLinkedinEstado();
            }
        }

        public BoolDTO ValidarEstadoParaControlLinkedin()
        {
            {
                return _unitOfWork.LinkedInApiRepository.ValidarObtencionLeadLinkedinEstado();
            }
        }
    }
}
