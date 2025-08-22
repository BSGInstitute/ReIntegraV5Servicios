namespace BSI.Integra.Aplicacion.Transversal.Validador
{
    public sealed class ConfiguracionReglas
    {
        private static volatile ConfiguracionReglas instance;
        private static object syncRoot = new Object();
        private Dictionary<string, Dictionary<string, string>> configuracionReglas;

        private ConfiguracionReglas()
        {
            //var container = new UnityContainer();
            //container.RegisterType<ITCRM_ReglaValidacionService, TCRM_ReglaValidacionServiceImpl>();
            //container.RegisterType<ITCRM_ReglaValidacionRepository, TCRM_ReglaValidacionRepository>();
            //container.RegisterType<ITCRM_ReglaValidacionParametrosService, TCRM_ReglaValidacionParametrosServiceImpl>();
            //container.RegisterType<ITCRM_ReglaValidacionParametrosRepository, TCRM_ReglaValidacionParametrosRepository>();
            //container.RegisterType<IValidator<TCRM_ReglaValidacionDTO>, Validadores.TCRM_ReglaValidacionDTOValidator>
            //        (new ContainerControlledLifetimeManager());
            //container.RegisterType<IValidator<TCRM_ReglaValidacionParametrosDTO>, Validadores.TCRM_ReglaValidacionParametrosDTOValidator>
            //            (new ContainerControlledLifetimeManager());

            //var reglaValidacionService = container.Resolve<ITCRM_ReglaValidacionService>();
            //var reglaValidacionParametrosService = container.Resolve<ITCRM_ReglaValidacionParametrosService>();

            //Leemos la configuracion

            //var listaReglas = reglaValidacionService.GetAll();
            //var diccionarioReglas = new Dictionary<string, Dictionary<string, string>>();
            //foreach (var regla in listaReglas)
            //{
            //    var listaParametros = reglaValidacionParametrosService.GetAllByReglaValidacionId(new Guid(regla.Id));
            //    var diccionarioParam = new Dictionary<string, string>();
            //    foreach (var parametro in listaParametros)
            //    {
            //        diccionarioParam.Add(parametro.llave, parametro.valor);
            //    }
            //    diccionarioReglas.Add(regla.nombre, diccionarioParam);
            //}
            //this.configuracionReglas = diccionarioReglas;
        }

        public static ConfiguracionReglas Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new ConfiguracionReglas();
                        }
                    }
                }

                return instance;
            }
        }


        public static string getConfiguracion(string regla, string parametro)
        {
            var diccionario = Instance.configuracionReglas;
            if (diccionario.ContainsKey(regla))
            {
                if (diccionario[regla].ContainsKey(parametro))
                {
                    return diccionario[regla][parametro];
                }
                else return null;
            }
            else return null;
        }

        public static void setConfiguracion(string regla, string parametro, string valor)
        {
            var diccionario = Instance.configuracionReglas;
            if (diccionario.ContainsKey(regla))
            {
                if (diccionario[regla].ContainsKey(parametro))
                {
                    diccionario[regla][parametro] = valor;
                }
            }
        }
    }
}
