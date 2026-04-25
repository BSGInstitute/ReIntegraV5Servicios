using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Reflection;

namespace BSI.Integra.Aplicacion.Transversal.Helper
{
    public interface IValorEstatico
    {
        void CargarConfiguracion();
    }
    public class ValorEstatico : IValorEstatico
    {
        private readonly IUnitOfWork _unitOfWork;
        public static bool CargaInicial { get; private set; } = false;
        public ValorEstatico(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //if (!CargaInicial)
            //{
            //    CargarConfiguracion();
            //}
        }
        public void CargarConfiguracion()
        {
            var fijo = _unitOfWork.ConfiguracionFijaRepository.ObtenerTodosLosRegistros(); ;
            LlenarAtributos(fijo);
        }
        private void LlenarAtributos(List<ValorEstaticoDTO> valores)
        {
            CargaInicial = true;
            foreach (var valor in valores)
            {
                var propiedad = this.GetType().GetProperty(valor.NombreAtributo);

                if (propiedad != null)
                {
                    switch (valor.TipoDato)
                    {
                        case "int":
                            propiedad.SetValue(null, Convert.ToInt32(valor.Valor));
                            break;
                        case "string":
                            propiedad.SetValue(null, valor.Valor);
                            break;
                        case "bool":
                            propiedad.SetValue(null, Convert.ToBoolean(valor.Valor));
                            break;
                    }
                }

            }
        }
        public static int IdPlantillaAccesoTemporalWhatsApp { get; private set; }
        #region HardCodeoAtributos
        ///<value>GP</value>
        public static string AreaTrabajoGestionPersonas { get; private set; }
        ///<value>OP</value>
        public static string AreaTrabajoOperaciones { get; private set; }
        ///<value>VE</value>
        public static string AreaTrabajoVentas { get; private set; }
        ///<value>2152</value>
        public static int IdActividadCabeceraLlamadaCierre { get; private set; }
        ///<value>14</value>
        public static int IdActividadCabeceraLlamadaConfirEnvioDoc { get; private set; }
        ///<value>27</value>
        public static int IdActividadCabeceraLlamadaConfirInteresProgPrelan { get; private set; }
        ///<value>11</value>
        public static int IdActividadCabeceraLlamadaConfirInteresProHis { get; private set; }
        ///<value>5</value>
        public static int IdActividadCabeceraLlamadaConfirmacionPago { get; private set; }
        ///<value>2154</value>
        public static int IdActividadCabeceraLlamadaConfirmacionRegistroPW { get; private set; }
        ///<value>2149</value>
        public static int IdActividadCabeceraLlamadaConfirmacionRevisionInfo { get; private set; }
        ///<value>12</value>
        public static int IdActividadCabeceraLlamadaConfirSeguimientoRN { get; private set; }
        ///<value>2140</value>
        public static int IdActividadCabeceraLlamadaContactoInicial { get; private set; }
        ///<value>2619</value>
        public static int IdActividadCabeceraLlamadaContactoPredictiva { get; private set; }
        ///<value>48</value>
        public static int IdActividadCabeceraLlamadaSeguimiento { get; private set; }
        ///<value>17</value>
        public static int IdActividadCabeceraLlamadaSeguimientoRN2 { get; private set; }
        ///<value>18</value>
        public static int IdActividadCabeceraPrimerContactoClienteProbMedia { get; private set; }
        ///<value>5</value>
        public static int IdAreaTrabajoGestionPersonas { get; private set; }
        ///<value>1</value>
        public static int IdAutenticacionFacebookLeadsReportes { get; private set; }
        ///<value>31</value>
        public static int IdCategoriaObjetoFiltroActividadCabecera { get; private set; }
        ///<value>21</value>
        public static int IdCategoriaObjetoFiltroActividadesLlamada { get; private set; }
        ///<value>1</value>
        public static int IdCategoriaObjetoFiltroArea { get; private set; }
        ///<value>14</value>
        public static int IdCategoriaObjetoFiltroAreaFormacion { get; private set; }
        ///<value>15</value>
        public static int IdCategoriaObjetoFiltroAreaTrabajo { get; private set; }
        ///<value>27</value>
        public static int IdCategoriaObjetoFiltroCampaniaMailing { get; private set; }
        ///<value>12</value>
        public static int IdCategoriaObjetoFiltroCargo { get; private set; }
        ///<value>16</value>
        public static int IdCategoriaObjetoFiltroCategoriaOrigen { get; private set; }
        ///<value>11</value>
        public static int IdCategoriaObjetoFiltroCiudad { get; private set; }
        ///<value>28</value>
        public static int IdCategoriaObjetoFiltroConjuntoLista { get; private set; }
        ///<value>33</value>
        public static int IdCategoriaObjetoFiltroDocumentoAlumno { get; private set; }
        ///<value>37</value>
        public static int IdCategoriaObjetoFiltroEstadoAcademico { get; private set; }
        ///<value>39</value>
        public static int IdCategoriaObjetoFiltroEstadoLlamada { get; private set; }
        ///<value>34</value>
        public static int IdCategoriaObjetoFiltroEstadoMatricula { get; private set; }
        ///<value>38</value>
        public static int IdCategoriaObjetoFiltroEstadoPago { get; private set; }
        ///<value>46</value>
        public static int IdCategoriaObjetoFiltroFaseOportunidadActual { get; private set; }
        ///<value>29</value>
        public static int IdCategoriaObjetoFiltroFiltroSegmento { get; private set; }
        ///<value>13</value>
        public static int IdCategoriaObjetoFiltroIndustria { get; private set; }
        ///<value>35</value>
        public static int IdCategoriaObjetoFiltroModalidadCurso { get; private set; }
        ///<value>32</value>
        public static int IdCategoriaObjetoFiltroOcurrencia { get; private set; }
        ///<value>6</value>
        public static int IdCategoriaObjetoFiltroOpoInicialFaseActual { get; private set; }
        ///<value>7</value>
        public static int IdCategoriaObjetoFiltroOpoInicialFaseMaxima { get; private set; }
        ///<value>10</value>
        public static int IdCategoriaObjetoFiltroPais { get; private set; }
        ///<value>30</value>
        public static int IdCategoriaObjetoFiltroPGeneralPrincipalExcluir { get; private set; }
        ///<value>45</value>
        public static int IdCategoriaObjetoFiltroPorcentajeAvance { get; private set; }
        ///<value>23</value>
        public static int IdCategoriaObjetoFiltroProbabilidadOportunidad { get; private set; }
        ///<value>26</value>
        public static int IdCategoriaObjetoFiltroProbabilidadVentaCruzada { get; private set; }
        ///<value>4</value>
        public static int IdCategoriaObjetoFiltroProgramaEspecifico { get; private set; }
        ///<value>3</value>
        public static int IdCategoriaObjetoFiltroProgramaGeneral { get; private set; }
        ///<value>36</value>
        public static int IdCategoriaObjetoFiltroSesion { get; private set; }
        ///<value>40</value>
        public static int IdCategoriaObjetoFiltroSesionWebinar { get; private set; }
        ///<value>2</value>
        public static int IdCategoriaObjetoFiltroSubArea { get; private set; }
        ///<value>43</value>
        public static int IdCategoriaObjetoFiltroSubEstadoMatricula { get; private set; }
        ///<value>44</value>
        public static int IdCategoriaObjetoFiltroTarifario { get; private set; }
        ///<value>22</value>
        public static int IdCategoriaObjetoFiltroTipoCategoriaOrigen { get; private set; }
        ///<value>18</value>
        public static int IdCategoriaObjetoFiltroTipoFormulario { get; private set; }
        ///<value>17</value>
        public static int IdCategoriaObjetoFiltroTipoInteraccionFormulario { get; private set; }
        ///<value>41</value>
        public static int IdCategoriaObjetoFiltroTrabajoAlumno { get; private set; }
        ///<value>42</value>
        public static int IdCategoriaObjetoFiltroTrabajoAlumnoFinal { get; private set; }
        ///<value>8</value>
        public static int IdCategoriaObjetoFiltroUltimaOpoFaseActual { get; private set; }
        ///<value>9</value>
        public static int IdCategoriaObjetoFiltroUltimaOpoFaseMaxima { get; private set; }
        ///<value>24</value>
        public static int IdCategoriaObjetoFiltroVCArea { get; private set; }
        ///<value>47</value>
        public static int IdCategoriaObjetoFiltroVCPGeneral { get; private set; }
        ///<value>25</value>
        public static int IdCategoriaObjetoFiltroVCSubArea { get; private set; }
        ///<value>349</value>
        public static int IdCategoriaOrigenFacebookComentarios { get; private set; }
        ///<value>531</value>
        public static int IdCategoriaOrigenFacebookCorreo { get; private set; }
        ///<value>529</value>
        public static int IdCategoriaOrigenFacebookInbox { get; private set; }
        ///<value>360</value>
        public static int IdCategoriaOrigenFacebookPreLanC2FormularioPropio { get; private set; }
        ///<value>165</value>
        public static int IdCentroCostoPersonal { get; private set; }
        ///<value>190</value>
        public static int IdCentroCostoPersonalLima { get; private set; }
        ///<value>18839</value>
        public static int IdCentroCostoPruebaMailing { get; private set; }
        ///<value>15907</value>
        public static int IdCentroCostoRegistro2020ILima { get; private set; }
        ///<value>4</value>
        public static int IdCiudadArequipa { get; private set; }
        ///<value>1956</value>
        public static int IdCiudadBogota { get; private set; }
        ///<value>2370</value>
        public static int IdCiudadInternacional { get; private set; }
        ///<value>2061</value>
        public static int IdCiudadLaPaz { get; private set; }
        ///<value>14</value>
        public static int IdCiudadLima { get; private set; }
        ///<value>2066</value>
        public static int IdCiudadSantaCruz { get; private set; }
        ///<value>15</value>
        public static int IdCronogramaTipoModificacionCuota { get; private set; }
        ///<value>2</value>
        public static int IdEstadoActividadDetalleEjecutado { get; private set; }
        ///<value>1</value>
        public static int IdEstadoActividadDetalleNoEjecutado { get; private set; }
        ///<value>2</value>
        public static int IdEstadoContactoWhatsAppInvalido { get; private set; }
        ///<value>3</value>
        public static int IdEstadoContactoWhatsAppSinValidar { get; private set; }
        ///<value>1</value>
        public static int IdEstadoContactoWhatsAppValido { get; private set; }
        ///<value>5</value>
        public static int IdEstadoEnvioArchivadoCorrecto { get; private set; }
        ///<value>6</value>
        public static int IdEstadoEnvioArchivadoIncorrecto { get; private set; }
        ///<value>4</value>
        public static int IdEstadoEnvioEnProceso { get; private set; }
        ///<value>4</value>
        public static int IdEstadoMatriculaBeca { get; private set; }
        ///<value>1</value>
        public static int IdEstadoMatriculaRegular { get; private set; }
        ///<value>6</value>
        public static int IdEstadoMatriculaReincorporado { get; private set; }
        ///<value>7</value>
        public static int IdEstadoOcurrenciaAsignacionManual { get; private set; }
        ///<value>1</value>
        public static int IdEstadoOcurrenciaEjecutado { get; private set; }
        ///<value>2</value>
        public static int IdEstadoOcurrenciaNoEjecutado { get; private set; }
        ///<value>1</value>
        public static int IdEstadoOportunidadEjecutada { get; private set; }
        ///<value>2</value>
        public static int IdEstadoOportunidadNoProgramada { get; private set; }
        ///<value>6</value>
        public static int IdEstadoOportunidadProgramada { get; private set; }
        ///<value>7</value>
        public static int IdEstadoOportunidadReasignacionVentaCruzada { get; private set; }
        ///<value>8</value>
        public static int IdEstadoOportunidadSegMejProg { get; private set; }
        ///<value>2</value>
        public static int IdEstadoPagoMatriculaMatriculado { get; private set; }
        ///<value>1</value>
        public static int IdEstadoPagoMatriculaPorMatricular { get; private set; }
        ///<value>4</value>
        public static int IdEstadoSeguimientoPreProcesoListaWhatsAppSinDatos { get; private set; }
        ///<value>38</value>
        public static int IdFacebookFormulario3Campos { get; private set; }
        ///<value>329</value>
        public static int IdFacebookFormulario5Campos { get; private set; }
        ///<value>331</value>
        public static int IdFacebookMultipleFormulario { get; private set; }
        ///<value>352</value>
        public  static int IdFacebookFormularioVisitante { get; private set; }
        ///<value>352</value>
        public static int IdFacebookRemarketingFormulario { get; private set; }
        ///<value>4</value>
        public static int IdFaseOportunidadBIC { get; private set; }
        ///<value>2</value>
        public static int IdFaseOportunidadBNC { get; private set; }
        public static int IdFaseOportunidadBNC1 { get; private set; }
        ///<value>15</value>
        public static int IdFaseOportunidadBNCRN2 { get; private set; }
        ///<value>25</value>
        public static int IdFaseOportunidadD { get; private set; }
        ///<value>11</value>
        public static int IdFaseOportunidadE { get; private set; }
        ///<value>12</value>
        public static int IdFaseOportunidadIC { get; private set; }
        ///<value>8</value>
        public static int IdFaseOportunidadIP { get; private set; }
        ///<value>5</value>
        public static int IdFaseOportunidadIS { get; private set; }
        ///<value>13</value>
        public static int IdFaseOportunidadIT { get; private set; }
        ///<value>3</value>
        public static int IdFaseOportunidadNI { get; private set; }
        ///<value>31</value>
        public static int IdFaseOportunidadNulo { get; private set; }
        ///<value>32</value>
        public static int IdFaseOportunidadOD { get; private set; }
        ///<value>33</value>
        public static int IdFaseOportunidadOM { get; private set; }
        ///<value>22</value>
        public static int IdFaseOportunidadPF { get; private set; }
        ///<value>7</value>
        public static int IdFaseOportunidadRN { get; private set; }
        ///<value>10</value>
        public static int IdFaseOportunidadRN2 { get; private set; }
        public static int IdFaseOportunidadRN2A { get; private set; }
        ///<value>6</value>
        public static int IdFaseOportunidadRN3 { get; private set; }
        ///<value>26</value>
        public static int IdFaseOportunidadRN4 { get; private set; }
        ///<value>2</value>
        public static int IdFormaPagoEnEfectivo { get; private set; }
        ///<value>9</value>
        public static int IdFurAprobadoNoEjecutado { get; private set; }
        ///<value>3</value>
        public static int IdFurAprobadoPorJefeArea { get; private set; }
        ///<value>5</value>
        public static int IdFurAprobadoPorJefeFinanzas { get; private set; }
        ///<value>2</value>
        public static int IdFurEstadoPorAprobar { get; private set; }
        ///<value>4</value>
        public static int IdFurObservadoPorJefeArea { get; private set; }
        ///<value>6</value>
        public static int IdFurObservadoPorJefeFinanzas { get; private set; }
        ///<value>1</value>
        public static int IdFurProyectado { get; private set; }
        ///<value>7</value>
        public static int IdFurTodosPorEliminar { get; private set; }
        ///<value>7</value>
        public static int IdGoogleAdsAPIKeyAplicacion { get; private set; }
        ///<value>4</value>
        public static int IdGoogleAdsDeveloperToken { get; private set; }
        ///<value>5</value>
        public static int IdGoogleAdsOAuth2IdCliente { get; private set; }
        ///<value>6</value>
        public static int IdGoogleAdsOAuth2SecretoCliente { get; private set; }
        ///<value>9</value>
        public static int IdGoogleAdsReporteAccessToken { get; private set; }
        ///<value>8</value>
        public static int IdGoogleAdsReporteRefreshToken { get; private set; }
        ///<value>22</value>
        public static int IdListaCursoAreaEtiquetaTi1 { get; private set; }
        ///<value>281</value>
        public static int IdMailingBasesPropiasChat { get; private set; }
        ///<value>439</value>
        public static int IdMailingBasesPropiasChatOffline { get; private set; }
        ///<value>214</value>
        public static int IdMailingBasesPropiasFormularioAccesoPrueba { get; private set; }
        ///<value>155</value>
        public static int IdMailingBasesPropiasFormularioCarrera { get; private set; }
        ///<value>228</value>
        public static int IdMailingBasesPropiasFormularioContactenos { get; private set; }
        ///<value>146</value>
        public static int IdMailingBasesPropiasFormularioPago { get; private set; }
        ///<value>223</value>
        public static int IdMailingBasesPropiasFormularioPrograma { get; private set; }
        ///<value>18</value>
        public static int IdMailingBasesPropiasFormularioPropio { get; private set; }
        ///<value>226</value>
        public static int IdMailingBasesPropiasFormularioRegistrarse { get; private set; }
        ///<value>282</value>
        public static int IdMailingBasesPropiasInteligenteChat { get; private set; }
        ///<value>438</value>
        public static int IdMailingBasesPropiasInteligenteChatOffline { get; private set; }
        ///<value>250</value>
        public static int IdMailingBasesPropiasInteligenteFormularioCarrera { get; private set; }
        ///<value>252</value>
        public static int IdMailingBasesPropiasInteligenteFormularioPago { get; private set; }
        ///<value>253</value>
        public static int IdMailingBasesPropiasInteligenteFormularioPrograma { get; private set; }
        ///<value>249</value>
        public static int IdMailingBasesPropiasInteligenteFormularioPropio { get; private set; }
        ///<value>255</value>
        public static int IdMailingBasesPropiasIntFormularioAccesoPrueba { get; private set; }
        ///<value>254</value>
        public static int IdMailingBasesPropiasIntFormularioContactenos { get; private set; }
        ///<value>251</value>
        public static int IdMailingBasesPropiasIntFormularioRegistrarse { get; private set; }
        ///<value>600</value>
        public static int IdMailingGeneralDefecto { get; private set; }
        ///<value>8</value>
        public static int IdMisFurPorEliminar { get; private set; }
        ///<value>1</value>
        public static int IdModalidadCursoOnlineAsincronica { get; private set; }
        ///<value>2</value>
        public static int IdModalidadCursoOnlineSincronica { get; private set; }
        ///<value>0</value>
        public static int IdModalidadCursoPresencial { get; private set; }
        ///<value>57</value>
        public static int IdModuloCrearOportunidadesWhatsApp { get; private set; }
        ///<value>332</value>
        public static int IdModuloSistemaWhatsAppMailing { get; private set; }
        ///<value>19</value>
        public static int IdMonedaDolares { get; private set; }
        ///<value>232</value>
        public static int IdOcurrenciaCerradoOD { get; private set; }
        ///<value>233</value>
        public static int IdOcurrenciaCerradoOM { get; private set; }
        ///<value>34</value>
        public static int IdOcurrenciaCerradoReporteBic { get; private set; }
        ///<value>260</value>
        public static int IdOcurrenciaCierreRN8 { get; private set; }
        ///<value>382</value>
        public static int IdOcurrenciaConfirmaPagoIs { get; private set; }
        ///<value>383</value>
        public static int IdOcurrenciaIsSinLlamada { get; private set; }
        ///<value>259</value>
        public static int IdOcurrenciaReprogramacionAutomaticaRN2 { get; private set; }
        ///<value>114</value>
        public static int IdOrigenChat { get; private set; }
        ///<value>132</value>
        public static int IdOrigenChatOffline { get; private set; }
        ///<value>131</value>
        public static int IdOrigenCorreoElectronico { get; private set; }
        ///<value>591</value>
        public static int IdPaisBolivia { get; private set; }
        ///<value>57</value>
        public static int IdPaisColombia { get; private set; }
        ///<value>52</value>
        public static int IdPaisMexico { get; private set; }
        ///<value>51</value>
        public static int IdPaisPeru { get; private set; }
        ///<value>56</value>
        public static int IdPaisChile { get; private set; }
        ///<value>3</value>
        public static int IdPersonalAreaTrabajoOperaciones { get; private set; }
        ///<value>7</value>
        public static int IdPersonalAreaTrabajoPlanificacion { get; private set; }
        ///<value>8</value>
        public static int IdPersonalAreaTrabajoVentas { get; private set; }
        ///<value>3695</value>
        public static int IdPersonalAsesorAsignacionHistorico { get; private set; }
        ///<value>125</value>
        public static int IdPersonalAsignacionAutomatica { get; private set; }
        ///<value>4723</value>
        public static int IdPersonalCorreoPorDefecto { get; private set; }
        ///<value>4589</value>
        public static int IdPersonalWhatsappNuevasOportunidades { get; private set; }
        ///<value>1386</value>
        public static int IdPlantillaAccesoTemporalMailing { get; private set; }
        ///<value>1399</value>
        public static int IdPlantillaAccesoTemporalPersonalMailing { get; private set; }
        ///<value>1364</value>
        public static int IdPlantillaAlumnoProyectoCalificado { get; private set; }
        ///<value>2</value>
        public static int IdPlantillaBaseEmail { get; private set; }
        ///<value>15</value>
        public static int IdPlantillaBaseMensajeTexto { get; private set; }
        ///<value>8</value>
        public static int IdPlantillaBaseWhatsAppFacebook { get; private set; }
        ///<value>7</value>
        public static int IdPlantillaBaseWhatsAppPropio { get; private set; }
        ///<value>1109</value>
        public static int IdPlantillaBienvenidaAlumnoAOnline { get; private set; }
        ///<value>1108</value>
        public static int IdPlantillaBienvenidaAlumnoPresencialOnline { get; private set; }
        ///<value>1363</value>
        public static int IdPlantillaDocenteProyectoPendienteCalificar { get; private set; }
        ///<value>1084</value>
        public static int IdPlantillaEnvioCorreoInformacion { get; private set; }
        ///<value>874</value>
        public static int IdPlantillaInformacionCarrera { get; private set; }
        ///<value>130</value>
        public static int IdPlantillaInformacionCursoAutomatico { get; private set; }
        ///<value>827</value>
        public static int IdPlantillaInformacionCursoVentas { get; private set; }
        ///<value>617</value>
        public static int IdPlantillaMaestroTemplateV2 { get; private set; }
        ///<value>1</value>
        public static int IdProbabilidadRegistroSinProbabilidad { get; private set; }
        ///<value>164</value>
        public static int IdProductoBonoProductividad { get; private set; }
        ///<value>163</value>
        public static int IdProductoComisionAdministracion { get; private set; }
        ///<value>166</value>
        public static int IdProductoComisionProduccion { get; private set; }
        ///<value>165</value>
        public static int IdProductoComisionVentas { get; private set; }
        ///<value>172</value>
        public static int IdProductoCTSAdministracion { get; private set; }
        ///<value>173</value>
        public static int IdProductoCtsProduccion { get; private set; }
        ///<value>174</value>
        public static int IdProductoCtsVentas { get; private set; }
        ///<value>224</value>
        public static int IdProductoEsSalud { get; private set; }
        ///<value>274</value>
        public static int IdProductoGratificacionAdministracion { get; private set; }
        ///<value>275</value>
        public static int IdProductoGratificacionProduccion { get; private set; }
        ///<value>276</value>
        public static int IdProductoGratificacionVentas { get; private set; }
        ///<value>426</value>
        public static int IdProductoPlanillaAdministracion { get; private set; }
        ///<value>427</value>
        public static int IdProductoPlanillaProduccion { get; private set; }
        ///<value>428</value>
        public static int IdProductoPlanillaVentas { get; private set; }
        ///<value>703</value>
        public static int IdProgramaGeneralDSIG { get; private set; }
        ///<value>598</value>
        public static int IdProgramaGeneralFFISO27001 { get; private set; }
        ///<value>7633</value>
        public static int IdProgramaGeneralFFISO37001 { get; private set; }
        ///<value>810</value>
        public static int IdProgramaGeneralFFISO45001 { get; private set; }
        ///<value>686</value>
        public static int IdProgramaGeneralFFISO9001 { get; private set; }
        ///<value>396</value>
        public static int IdProveedorPersonal { get; private set; }
        ///<value>481</value>
        public static int IdProveedorSeguroSocialDeSalud { get; private set; }
        ///<value>1456</value>
        public static int IdRecordatorioSms01Maniana { get; private set; }
        ///<value>1457</value>
        public static int IdRecordatorioSms01Tarde { get; private set; }
        ///<value>1458</value>
        public static int IdRecordatorioSms02 { get; private set; }
        ///<value>1459</value>
        public static int IdRecordatorioSms03 { get; private set; }
        ///<value>1460</value>
        public static int IdRecordatorioSms04 { get; private set; }
        ///<value>2</value>
        public static int IdRolAsistenteAdministracionFinanzas { get; private set; }
        ///<value>1</value>
        public static int IdRolCoordinadoraFinanzas { get; private set; }
        ///<value>19</value>
        public static int IdRolJefaturaFinanzas { get; private set; }
        ///<value>22</value>
        public static int IdRolJefePlanificacion { get; private set; }
        ///<value>1</value>
        public static int IdSedeTrabajoArequipa { get; private set; }
        ///<value>3</value>
        public static int IdSedeTrabajoBogota { get; private set; }
        ///<value>2</value>
        public static int IdSedeTrabajoLima { get; private set; }
        ///<value>5</value>
        public static int IdSedeTrabajoSantaCruz { get; private set; }
        ///<value>16</value>
        public static int IdTipoCategoriaOrigenChat { get; private set; }
        ///<value>38</value>
        public static int IdTipoCategoriaOrigenChatB { get; private set; }
        ///<value>7</value>
        public static int IdTipoDatoHistorico { get; private set; }
        ///<value>8</value>
        public static int IdTipoDatoLanzamiento { get; private set; }
        ///<value>14</value>
        public static int IdTipoInteraccionCorreoElectronicoRecibido { get; private set; }
        ///<value>15</value>
        public static int IdTipoInteraccionFormularioEnviadoCompleto { get; private set; }
        ///<value>4</value>
        public static int IdTipoInteraccionGeneralFormulario { get; private set; }
        ///<value>11</value>
        public static int IdTipoInteraccionPaso1 { get; private set; }
        ///<value>4</value>
        public static int IdTipoPedidoACredito { get; private set; }
        ///<value>1</value>
        public static int IdTipoPedidoGastoInmediato { get; private set; }
        ///<value>1</value>
        public static int IdTipoPersonaAlumno { get; private set; }
        ///<value>2</value>
        public static int IdTipoPersonaPersonal { get; private set; }
        ///<value>4</value>
        public static int IdTipoRemuneracionBono { get; private set; }
        ///<value>3</value>
        public static int IdTipoRemuneracionCTS { get; private set; }
        ///<value>2</value>
        public static int IdTipoRemuneracionGratificacion { get; private set; }
        ///<value>1</value>
        public static int IdTipoRemuneracionSueldo { get; private set; }
        ///<value>2</value>
        public static int IdTokenMailChimpAPIMarketing { get; private set; }
        ///<value>3</value>
        public static int IdUsernameMailChimpAPIMarketing { get; private set; }
        ///<value>4</value>
        public static int IdWhatsAppEstadoValidacionFallido { get; private set; }
        ///<value>1376</value>
        public static int IdWhatsAppMultipleSubCategoriaDato { get; private set; }
        ///<value>Senior</value>
        public static string ParametroPrueba { get; private set; }
        ///<value>48</value>
        public static int IdCategoriaObjetoFiltroUOArea { get; private set; }
        ///<value>48</value>
        public static int IdCategoriaObjetoFiltroUOSubArea { get; private set; }
        ///<value>49</value>
        public static int IdCategoriaObjetoFiltroUOPGeneral { get; private set; }
        ///<value>50</value>
        public static int IdCategoriaObjetoFiltroMPIArea { get; private set; }
        ///<value>51</value>
        public static int IdCategoriaObjetoFiltroMPISubArea { get; private set; }
        ///<value>52</value>
        public static int IdCategoriaObjetoFiltroMPIPGeneral { get; private set; }
        ///<value>53</value>
        public static int IdCategoriaObjetoFiltroProbabilidadValor { get; private set; }
        ///<value>54</value>
        public static int IdCategoriaObjetoFiltroProbabilidadArea { get; private set; }
        ///<value>55</value>
        public static int IdCategoriaObjetoFiltroProbabilidadSubArea { get; private set; }
        ///<value>56</value>
        public static int IdCategoriaObjetoFiltroProbabilidadPGeneral { get; private set; }
        ///<value>57</value>
        public static int IdCategoriaObjetoFiltroNivelEmbudoEsquema1 { get; private set; }
        ///<value>58</value>
        public static int IdCategoriaObjetoFiltroNivelEmbudoEsquema2 { get; private set; }
        ///<value>59</value>
        #endregion  HardCodeoAtributos

        public static Dictionary<string, string> GetProperties()
        {
            var listado = new Dictionary<string, string>();
            Type type = typeof(ValorEstatico);
            foreach (var p in type.GetFields(BindingFlags.Static | BindingFlags.NonPublic))
            {
                listado.Add(p.Name, p.GetValue(null).ToString());
            }
            return listado;
        }
    }
}
