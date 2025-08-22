using Microsoft.AspNet.SignalR.Client;

namespace BSI.Integra.Aplicacion.Transversal.Socket
{
    public class AgendaSocket
    {
        private static Object thisLock = new Object();
        //private IHubProxy myHubProxy = null;
        private static AgendaSocket instancia = null;
        private string url = null;

        public static AgendaSocket getInstance()
        {
            lock (thisLock)
            {
                if (instancia == null)
                {
                    instancia = new AgendaSocket
                    {
                        //url = System.Configuration.ConfigurationManager.AppSettings["UrlServidorSocket"]
                        url = $"https://integrav4-signalr.bsginstitute.com"
                    };
                }
            }
            return instancia;
        }

        public string ValidarOportunidad(int idAsignacionAutomaticaTemp)
        {
            string respuesta = "OK";
            try
            {
                //enviar al servicio
                var querystringData = new Dictionary<string, string>
                {
                    { "idUsuario", "2" },
                    { "usuarioNombre", "Integra" },
                    { "rooms", "" }
                };
                var hubConnection = new HubConnection(url, querystringData);

                IHubProxy myHubProxy = hubConnection.CreateHubProxy("IntegraHub");
                hubConnection.Start().Wait();

                myHubProxy.Invoke("ValidarAsignacionAutomatica", idAsignacionAutomaticaTemp).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception("Ha ocurrido un error al abrir la coneccion: " + task.Exception.GetBaseException());
                    }

                }).Wait();
                hubConnection.Stop();
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }

        /// Autor: _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene conexión para ejecutar nueva actividad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <param name="idAsesor"> Id de Asesor </param>
        /// <returns> string </returns>
        public string NuevaActividadParaEjecutar(int idOportunidad, int idAsesor)
        {
            string respuesta = "OK";
            try
            {
                //enviar al servicio
                var querystringData = new Dictionary<string, string>();
                querystringData.Add("idUsuario", "2");
                querystringData.Add("usuarioNombre", "Integra");
                querystringData.Add("rooms", "");
                var hubConnection = new HubConnection(url, querystringData);

                IHubProxy myHubProxy = hubConnection.CreateHubProxy("IntegraHub");
                hubConnection.Start().Wait();

                myHubProxy.Invoke("NuevaActividadParaEjecutar", idOportunidad, idAsesor).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception("Ha ocurrido un error al abrir la coneccion: " + task.Exception.GetBaseException());
                    }

                }).Wait();
                hubConnection.Stop();
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }
        public string SolicitudOperacionesRealizadaCancelada(int idOportunidad, int idAsesor, int Realizado)
        {
            string respuesta = "OK";
            try
            {
                //enviar al servicio
                var querystringData = new Dictionary<string, string>();
                querystringData.Add("idUsuario", "2");
                querystringData.Add("usuarioNombre", "Integra");
                querystringData.Add("rooms", "");
                var hubConnection = new HubConnection(url, querystringData);

                IHubProxy myHubProxy = hubConnection.CreateHubProxy("IntegraHub");
                hubConnection.Start().Wait();

                myHubProxy.Invoke("SolicitudOperacionesRealizadaCancelada", idOportunidad, idAsesor, Realizado).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception("Ha ocurrido un error al abrir la coneccion: " + task.Exception.GetBaseException());
                    }

                }).Wait();
                hubConnection.Stop();
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }

        public string ToRoomActividadEjecutada(string idAsesor, string idActividadEjecutada)
        {
            string respuesta = "OK";

            try
            {
                //enviar al servicio
                var querystringData = new Dictionary<string, string>();
                querystringData.Add("usuarioId", "2");
                querystringData.Add("usuarioNombre", "Integra");
                querystringData.Add("rooms", "");
                var hubConnection = new HubConnection(url, querystringData);

                IHubProxy myHubProxy = hubConnection.CreateHubProxy("MyHub");
                hubConnection.Start().Wait();

                myHubProxy.Invoke("toRoomActividadEjecutada", idAsesor, idActividadEjecutada).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        throw new Exception("Ha ocurrido un error al abrir la coneccion: " + task.Exception.GetBaseException());
                    }

                }).Wait();
                hubConnection.Stop();
            }
            catch (Exception ex)
            {
                respuesta = ex.ToString();
            }
            return respuesta;
        }

    }
}
