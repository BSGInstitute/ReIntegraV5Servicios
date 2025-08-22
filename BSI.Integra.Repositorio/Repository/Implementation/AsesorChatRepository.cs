using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: Comercial/AsesorChat
    /// Autor: Jonathan Caipo
    /// Fecha: 25/11/2022
    /// <summary>
    /// Repositorio para consultas de com.T_AsesorChat
    /// </summary>
    public class AsesorChatRepository : GenericRepository<TAsesorChat>, IAsesorChatRepository
    {
        private Mapper _mapper;
        public AsesorChatRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsesorChat, AsesorChat>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAsesorChat MapeoEntidad(AsesorChat entidad)
        {
            try
            {
                //crea la entidad padre
                TAsesorChat modelo = _mapper.Map<TAsesorChat>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsesorChat Add(AsesorChat entidad)
        {
            try
            {
                var AsesorChat = MapeoEntidad(entidad);
                base.Insert(AsesorChat);
                return AsesorChat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsesorChat Update(AsesorChat entidad)
        {
            try
            {
                var AsesorChat = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsesorChat.RowVersion = entidadExistente.RowVersion;

                base.Update(AsesorChat);
                return AsesorChat;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TAsesorChat> Add(IEnumerable<AsesorChat> listadoEntidad)
        {
            try
            {
                List<TAsesorChat> listado = new List<TAsesorChat>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TAsesorChat> Update(IEnumerable<AsesorChat> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsesorChat> listado = new List<TAsesorChat>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jonathan Caipo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero)
        {
            try
            {
                string queryO = "SELECT IdPersonal FROM [com].[V_ObtenerChatAsignadosParaWhatsApp] WHERE IdCentroCosto = @IdCentroCosto";
                var queryPrograma = _dapperRepository.FirstOrDefault(queryO, new { IdCentroCosto = idCentroCosto });
                var oportunidad = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryPrograma);

                string queryA = "SELECT Id AS IdAlumno FROM [mkt].[T_Alumno] WHERE Celular = @Numero ORDER BY FechaCreacion DESC";
                var queryAlumno = _dapperRepository.FirstOrDefault(queryA, new { Numero = numero });
                var alumno = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryAlumno);

                if (alumno != null)
                {
                    oportunidad.IdAlumno = alumno.IdAlumno;
                }
                return oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de personas a ser notificadas
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public List<AddresseeDTO> ListaPersonaNotificacion()
        {
            try
            {
                List<AddresseeDTO> ListaAddressee = new List<AddresseeDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT  Email FROM mkt.T_ListaCorreoAlerta WHERE Estado = 1 AND IdTipoCorreoAlerta = 7", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    ListaAddressee = JsonConvert.DeserializeObject<List<AddresseeDTO>>(resultado);
                }
                return ListaAddressee;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ChatAsignadoNoAsignadoDTO> ObtenerTodoChatAsignados()
        {
            try
            {
                List<ChatAsignadoNoAsignadoDTO> listaChats = new List<ChatAsignadoNoAsignadoDTO>();

                var resultado = _dapperRepository.QueryDapper("SELECT* FROM com.V_ObtenerChatAsignadosNoAsignados ", new { });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaChats = JsonConvert.DeserializeObject<List<ChatAsignadoNoAsignadoDTO>>(resultado);
                }
                return listaChats;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de personas a ser notificadas
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <param name="numero"></param>
        /// <returns> PersonalAlumnoDTO </returns>
        public PersonalAlumnoDTO BuscarAlumnoPorWebHook(string Celular)
        {
            try
            {
                PersonalAlumnoDTO PersonalAlumno = new PersonalAlumnoDTO();

                var resultado = _dapperRepository.QuerySPFirstOrDefault("mkt.SP_ObtenerAlumnoAsesorPorCelular", new { Celular });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    PersonalAlumno = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(resultado);
                }
                return PersonalAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 17/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de los chats asignados y no asignados
        /// </summary>
        /// <returns>Lista de objetos de clase ChatAsignadoNoAsignadoDTO</returns>
        public ChatAsignadoNoAsignadoCompuestoDTO ObtenerChatAsignadosNoAsignados(FiltroCompuestroGrillaDTO paginador)
        {
            try
            {
                string Condicion = "";
                string Paginacion = "";
                string ChatAsignados = "";
                string nombreArea = "";
                string nombreSubArea = "";
                string nombrePGeneral = "";
                string nombrePais = "";
                string esAsignado = "";
                string nombrePersonal = "";
                //int Skip = 0;
                //int Take = 0;
                if (paginador.paginador.take != 0)
                {
                    Paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                }

                if (paginador.filter != null)
                {

                    foreach (var item in paginador.filter.Filters)
                    {
                        if (item.Value.Contains(""))
                        {
                            Condicion += " and " + item.Field + "=@" + item.Field;
                            ChatAsignados = item.Value;
                            switch (item.Field)
                            {
                                case "nombreArea":
                                    nombreArea = ChatAsignados;
                                    break;
                                case "nombreSubArea":
                                    nombreSubArea = ChatAsignados;
                                    break;
                                case "nombrePGeneral":
                                    nombrePGeneral = ChatAsignados;
                                    break;
                                case "nombrePais":
                                    nombrePais = ChatAsignados;
                                    break;
                                case "esAsignado":
                                    if (ChatAsignados == "true")
                                    {
                                        ChatAsignados = "1";
                                    }
                                    else if (ChatAsignados == "false")
                                    {
                                        ChatAsignados = "0";
                                    }
                                    esAsignado = ChatAsignados;
                                    break;
                                case "nombrePersonal":
                                    nombrePersonal = ChatAsignados;
                                    break;
                            }
                        }
                    }
                }
                ChatAsignadoNoAsignadoCompuestoDTO chatsAsignados = new ChatAsignadoNoAsignadoCompuestoDTO();
                var _query = "SELECT NombreArea, NombreSubArea, NombrePGeneral, NombrePais, IdAsesorChat, EsAsignado, NombrePersonal " +
                            " FROM com.V_ObtenerChatAsignadosNoAsignados Where NombreArea is not null" + Condicion + " Order by NombreArea " + Paginacion + "";
                var chatsAsignadosDB = _dapperRepository.QueryDapper(_query, new { ChatAsignados, nombreArea, nombreSubArea, nombrePGeneral, nombrePais, esAsignado, nombrePersonal, Skip = paginador.paginador.skip, Take = paginador.paginador.take });
                if (!string.IsNullOrEmpty(chatsAsignadosDB) && !chatsAsignadosDB.Contains("[]") && chatsAsignadosDB != null && chatsAsignadosDB != "null")
                {
                    chatsAsignados.Registros = JsonConvert.DeserializeObject<List<ChatAsignadoNoAsignadoDTO>>(chatsAsignadosDB);
                    string _queryCantidad = "SELECT count(*) FROM com.V_ObtenerChatAsignadosNoAsignados Where NombreArea is not null " + Condicion + "";
                    string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { ChatAsignados, nombreArea, nombreSubArea, nombrePGeneral, nombrePais, esAsignado, nombrePersonal });
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                    chatsAsignados.Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }
                else
                {
                    chatsAsignados.Registros = null;
                    chatsAsignados.Total = 0;
                }

                return chatsAsignados;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores(FiltroCompuestroGrillaDTO paginador)
        {
            try
            {
                string Condicion = "";
                string Paginacion = "";
                string ListaAsesores = "";
                string idPersonal = "";
                string nombreArea = "";
                string nombreSubArea = "";
                string nombrePGeneral = "";
                string nombrePais = "";
                string nombreAsesor = "";
                int Skip = paginador.paginador.skip;
                int Take = paginador.paginador.take;
                int Inicial = 0;
                int Final = 0;
                if (Skip >= Take)
                {
                    Inicial = Skip;
                    Final = Skip + Take;

                }
                else
                {
                    Inicial = Skip;
                    Final = Take;
                }
                //int Skip = 0;
                //int Take = 0;
                if (paginador.paginador.take != 0)
                {
                    Paginacion = " OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                }

                if (paginador.filter != null)
                {

                    foreach (var item in paginador.filter.Filters)
                    {
                        if (item.Value.Contains(""))
                        {
                            if (item.Field == "listaArea")
                            {
                                item.Field = "nombreArea";
                            }
                            else if (item.Field == "listaSubArea")
                            {
                                item.Field = "nombreSubArea";
                            }
                            else if (item.Field == "listaProgramaGeneral")
                            {
                                item.Field = "nombrePGeneral";
                            }
                            else if (item.Field == "listaPais")
                            {
                                item.Field = "nombrePais";
                            }
                            Condicion += " and " + item.Field + "=@" + item.Field;
                            ListaAsesores = item.Value;
                            switch (item.Field)
                            {
                                case "idPersonal":
                                    idPersonal = ListaAsesores;
                                    break;
                                case "nombreArea":
                                    nombreArea = ListaAsesores;
                                    break;
                                case "nombreSubArea":
                                    nombreSubArea = ListaAsesores;
                                    break;
                                case "nombrePGeneral":
                                    nombrePGeneral = ListaAsesores;
                                    break;
                                case "nombrePais":
                                    nombrePais = ListaAsesores;
                                    break;
                                case "nombreAsesor":
                                    nombreAsesor = ListaAsesores;
                                    break;
                            }
                        }
                    }
                }
                ChatListaAsesoresCompuestoDTO Asesores = new ChatListaAsesoresCompuestoDTO();
                List<ChatListaAsesoresDTO> ListaInicial = new List<ChatListaAsesoresDTO>();
                var _queryListado = "SELECT DISTINCT Id FROM mkt.V_ObtenerGrillaAsesorChat Where Id is not null " + Condicion + " ORDER BY Id DESC";
                var respuestaListado = _dapperRepository.QueryDapper(_queryListado, new { ListaAsesores, idPersonal, nombreArea, nombreSubArea, nombrePGeneral, nombrePais, nombreAsesor });
                var ListaResgistros = JsonConvert.DeserializeObject<List<ValorIntDTO>>(respuestaListado);
                List<int> ListaResgistrosNuevos = new List<int>();
                string listadoIds = "";
                if (ListaResgistros.Count > 0)
                {
                    if (Final > ListaResgistros.Count)
                    {
                        Final = ListaResgistros.Count;
                    }
                    for (int i = Inicial; i < Final; i++)
                    {
                        var aux = (ListaResgistros[i].Id).ToString();
                        if (i != Final - 1)
                        {
                            aux = aux + ",";
                        }
                        listadoIds = listadoIds + aux;
                    }
                    List<AsesorChatConsolidadoVisualizarDTO> listadoAsesorChatDetalleVisualizar = new List<AsesorChatConsolidadoVisualizarDTO>();
                    var _query = "SELECT Id,IdPersonal,NombreAsesor,IdArea,NombreArea,IdSubArea,NombreSubArea,IdPais,NombrePais,IdPGeneral,NombrePGeneral " +
                                " FROM mkt.V_ObtenerGrillaAsesorChat WHERE Id in (" + listadoIds + ")" + Condicion + "";
                    var respuesta = _dapperRepository.QueryDapper(_query, new { ListaAsesores, idPersonal, nombreArea, nombreSubArea, nombrePGeneral, nombrePais, nombreAsesor });
                    if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != null && respuesta != "null")
                    {
                        ListaInicial = JsonConvert.DeserializeObject<List<ChatListaAsesoresDTO>>(respuesta);
                    }
                    string _queryCantidad = "SELECT count(*) FROM mkt.V_ObtenerGrillaAsesorChat Where Id is not null " + Condicion + "";
                    string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { ListaAsesores, idPersonal, nombreArea, nombreSubArea, nombrePGeneral, nombrePais, nombreAsesor });
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                    if (ListaInicial.Count() > 0)
                    {
                        Asesores.Total = ListaResgistros.Count();
                        var listadoAsesorChatDetalleAgrupado = (
                           from p in ListaInicial
                           group p by new { p.Id, p.IdPersonal, p.NombreAsesor } into g
                           select new AsesorChatConsolidadoDTO()
                           {
                               Id = g.Key.Id,
                               NombreAsesor = g.Key.NombreAsesor,
                               IdPersonal = g.Key.IdPersonal,
                               ListaArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdArea, Nombre = x.NombreArea }).ToList(),
                               ListaProgramaGeneral = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPGeneral, Nombre = x.NombrePGeneral }).ToList(),
                               ListaPais = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPais, Nombre = x.NombrePais }).ToList(),
                               ListaSubArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdSubArea, Nombre = x.NombreSubArea }).ToList()
                           }
                    ).ToList();

                        foreach (var item in listadoAsesorChatDetalleAgrupado)
                        {
                            //area
                            string listadoArea = "<ul>";
                            foreach (var _area in item.ListaArea.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoArea += "<li>" + _area + "</li>";
                            }
                            listadoArea += "</ul>";

                            //subarea
                            var listadoSubArea = "<ul>";
                            foreach (var _SubArea in item.ListaSubArea.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoSubArea += "<li>" + _SubArea + "</li>";
                            }
                            listadoSubArea += "</ul>";

                            //pgeneral
                            var listadoPGeneral = "<ul>";
                            foreach (var _pGeneral in item.ListaProgramaGeneral.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoPGeneral += "<li>" + _pGeneral + "</li>";
                            }
                            listadoPGeneral += "</ul>";

                            //pais
                            var listadoPais = "<ul>";
                            foreach (var _pais in item.ListaPais.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoPais += "<li>" + _pais + "</li>";
                            }
                            listadoPais += "</ul>";

                            var asesorChatDetalleTemp = new AsesorChatConsolidadoVisualizarDTO()
                            {
                                Id = item.Id,
                                IdPersonal = item.IdPersonal,
                                NombreAsesor = item.NombreAsesor,
                                ListaArea = listadoArea,
                                ListaSubArea = listadoSubArea,
                                ListaProgramaGeneral = listadoPGeneral,
                                ListaPais = listadoPais
                            };
                            listadoAsesorChatDetalleVisualizar.Add(asesorChatDetalleTemp);
                        }
                    }
                    Asesores.Registros = listadoAsesorChatDetalleVisualizar;
                }
                return Asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores2()
        {

            try
            {
                ChatListaAsesoresCompuestoDTO Asesores = new ChatListaAsesoresCompuestoDTO();
                List<ChatListaAsesoresDTO> ListaInicial = new List<ChatListaAsesoresDTO>();
                var _queryListado = "SELECT DISTINCT Id FROM mkt.V_ObtenerGrillaAsesorChat Where Id is not null ";
                var respuestaListado = _dapperRepository.QueryDapper(_queryListado, new { });
                var ListaResgistros = JsonConvert.DeserializeObject<List<ValorIntDTO>>(respuestaListado);
                List<int> ListaResgistrosNuevos = new List<int>();
                string listadoIds = "";
                if (ListaResgistros.Count > 0)
                {

                    for (int i = 0; i < ListaResgistros.Count; i++)
                    {
                        var aux = (ListaResgistros[i].Id).ToString();
                        if (i != ListaResgistros.Count - 1)
                        {
                            aux = aux + ",";
                        }
                        listadoIds = listadoIds + aux;
                    }
                    List<AsesorChatConsolidadoVisualizarDTO> listadoAsesorChatDetalleVisualizar = new List<AsesorChatConsolidadoVisualizarDTO>();
                    var _query = "SELECT Id,IdPersonal,NombreAsesor,IdArea,NombreArea,IdSubArea,NombreSubArea,IdPais,NombrePais,IdPGeneral,NombrePGeneral " +
                                " FROM mkt.V_ObtenerGrillaAsesorChat WHERE Id in (" + listadoIds + ")" + " " + "ORDER BY fechaCreacion DESC";
                    var respuesta = _dapperRepository.QueryDapper(_query, new { });
                    if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]") && respuesta != null && respuesta != "null")
                    {
                        ListaInicial = JsonConvert.DeserializeObject<List<ChatListaAsesoresDTO>>(respuesta);
                    }
                    string _queryCantidad = "SELECT count(*) FROM mkt.V_ObtenerGrillaAsesorChat Where Id is not null ";
                    string queryCantidad = _dapperRepository.FirstOrDefault(_queryCantidad, new { });
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(queryCantidad);
                    if (ListaInicial.Count() > 0)
                    {
          
                        var listadoAsesorChatDetalleAgrupado = (
                           from p in ListaInicial
                           group p by new { p.Id, p.IdPersonal, p.NombreAsesor } into g
                           select new AsesorChatConsolidadoDTO()
                           {
                               Id = g.Key.Id,
                               NombreAsesor = g.Key.NombreAsesor,
                               IdPersonal = g.Key.IdPersonal,
                               ListaArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdArea, Nombre = x.NombreArea }).ToList(),
                               ListaProgramaGeneral = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPGeneral, Nombre = x.NombrePGeneral }).ToList(),
                               ListaPais = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdPais, Nombre = x.NombrePais }).ToList(),
                               ListaSubArea = g.Select(x => new AsesorChatDetalleDTO { Id = x.IdSubArea, Nombre = x.NombreSubArea }).ToList()
                           }
                    ).ToList();

                        Asesores.Total = ListaResgistros.Count();

                        foreach (var item in listadoAsesorChatDetalleAgrupado)
                        {
                            //area
                            string listadoArea = "<ul>";
                            foreach (var _area in item.ListaArea.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoArea += "<li>" + _area + "</li>";
                            }
                            listadoArea += "</ul>";

                            //subarea
                            var listadoSubArea = "<ul>";
                            foreach (var _SubArea in item.ListaSubArea.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoSubArea += "<li>" + _SubArea + "</li>";
                            }
                            listadoSubArea += "</ul>";

                            //pgeneral
                            var listadoPGeneral = "<ul>";
                            foreach (var _pGeneral in item.ListaProgramaGeneral.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoPGeneral += "<li>" + _pGeneral + "</li>";
                            }
                            listadoPGeneral += "</ul>";

                            //pais
                            var listadoPais = "<ul>";
                            foreach (var _pais in item.ListaPais.Select(x => x.Nombre).Distinct().ToList())
                            {
                                listadoPais += "<li>" + _pais + "</li>";
                            }
                            listadoPais += "</ul>";

                            var asesorChatDetalleTemp = new AsesorChatConsolidadoVisualizarDTO()
                            {
                                Id = item.Id,
                                IdPersonal = item.IdPersonal,
                                NombreAsesor = item.NombreAsesor,
                                ListaArea = listadoArea,
                                ListaSubArea = listadoSubArea,
                                ListaProgramaGeneral = listadoPGeneral,
                                ListaPais = listadoPais
                            };
                            listadoAsesorChatDetalleVisualizar.Add(asesorChatDetalleTemp);
                        }
                    }
                    Asesores.Registros = listadoAsesorChatDetalleVisualizar;
                }
                return Asesores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            }

    }
}