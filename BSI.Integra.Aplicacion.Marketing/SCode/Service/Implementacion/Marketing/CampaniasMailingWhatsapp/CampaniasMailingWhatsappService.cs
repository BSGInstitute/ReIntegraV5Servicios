using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp;
using BSI.Integra.Repositorio.UnitOfWork;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp
{
    public class CampaniasMailingWhatsappService : ICampaniasMailingWhatsappService
    {
        private readonly IUnitOfWork unitOfWork;
        private ErrorGenerico error;

        public CampaniasMailingWhatsappService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            error = new ErrorGenerico();

        }

        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener Lista de campania mailing
        /// </summary>
        /// <returns>Objeto de clase Campania mailing</returns>
        public List<CampaniaMailingDTO> ObtenerListaCampaniaMailing()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerListaCampaniaMailing();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener campania general mailing
        /// </summary>
        /// <returns>Objeto de clase campania mailing</returns>
        public List<CampaniaMailingDTO> ObtenerCampaniaMailingGrid()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerListaCampaniaMailing();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de categoria origen
        /// </summary>
        /// <returns>una lista de categoria origen filtro</returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerListaCategoriaOrigen()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerListaCategoriaOrigen();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista de campanias mailing 
        /// </summary>
        /// <param name="Id">Identificador unico De categoria origen filtro</param>
        /// <returns>retorna una lista de categorias origen</returns>
        public List<CategoriaOrigenFiltroDTO> ObtenerListaCampaniaMailingDetalle(int Id)
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerListaCampaniaMailingDetalle(Id);
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los remitentes 
        /// </summary>
        /// <returns>una lista de remitentes</returns>
        public List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerListaRemitenteMailingAsesor();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener lista de probabilidades de probabiliad registros
        /// </summary>
        /// <returns>Lista de probabilidades de registro</returns>
        public List<ComboDTO> ObtenerTodoFiltro()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerTodoFiltro();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener las areas, subareas, programas generales
        /// </summary>
        /// <returns>Objeto que contiene areas, subareas y progrmaas generales</returns>
        public CombosAreasSubAreasMailchimpDTO ObteneAreasSubAreas()
        {
            try
            {
                CombosAreasSubAreasMailchimpDTO combo = new CombosAreasSubAreasMailchimpDTO();
                combo.Areas = unitOfWork.AreaCapacitacionRepository.ObtenerCombo().ToList();
                combo.SubAreas = unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro().ToList();
                combo.ProgramaGeneral = unitOfWork.PGeneralRepository.ObtenerProgramaSubAreaFiltro();
                return combo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener  las sub areas por id de area
        /// </summary>
        /// <param name="id">Identificador unico de area</param>
        /// <returns>una lista de subareas</returns>
        public List<SubAreaCapacitacionFiltroDTO> ObtenerSubAreas(int idAreaCapacitacion)
        {
            var res = unitOfWork.SubAreaCapacitacionRepository.ObtenerPorIdAreaCapacitacion(idAreaCapacitacion);
            return res;
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista de programas generales
        /// </summary>
        /// <param name="id">Identificador unico de programa general</param>
        /// <returns>lista de programa general sub area</returns>
        public List<PGeneralSubAreaCapacitacionFiltroDTO> ObtenerProgramaGeneral(int id)
        {
            var res = unitOfWork.PGeneralRepository.ObtenerProgramaGeneralPorSubAreaId(id);
            return res;
        }
        public List<ComboDTO> ObteneAreas()
        {
            return unitOfWork.AreaCapacitacionRepository.ObtenerCombo().ToList();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los centros de costo para el filtrado de data
        /// </summary>
        /// <returns>una lista de centros de costo</returns>
        public List<FiltroDTO> ObtenerParaFiltro()
        {
            return unitOfWork.campaniasMailingWhatsappRepository.ObtenerParaFiltro();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene El personal para el filtro
        /// </summary>
        /// <returns>una lista de personal</returns>
        public List<FiltroCombosDTO> ObtenerPersonalMarketingFiltro()
        {
            return unitOfWork.PeriodoRepository.ObtenerPersonalMarketingFiltro();
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina un a campania general
        /// </summary>
        /// <param name="IdCampaniaMailing">Identificador unico para campania general mailing</param>
        /// <param name="Usuario">usuario que realizo la peticion</param>
        /// <returns>una lista de centros de costo</returns>
        public ErrorGenerico Eliminar(int IdCampaniaMailing, string Usuario)
        {
            try
            {
                unitOfWork.CampaniaGeneralRepository.Delete(IdCampaniaMailing, Usuario);
                unitOfWork.Commit();
                error = new ErrorGenerico()
                {
                    Response = false
                };
            }
            catch (Exception e)
            {
                error = new ErrorGenerico()
                {
                    Response = false
                };
            }
            return error;
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña
        /// </summary>
        /// <returns>Objeto de clase CampaniaMailingDTO</returns>
        public CampaniaMailingDTO ObtenerDetalle(int Id)
        {
            try
            {
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Object EjecutarReplicado(int id, string usuario)
        {
            try
            {


                var whatsAppRemplazoEtiquetaService = new WhatsAppRemplazoEtiquetaService(unitOfWork);
                unitOfWork.CampaniaGeneralRepository.EjecutarReplicado(id, usuario);
                PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PreprocesamientoWhatsAppCampaniaGeneral = new PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO();
                var litaCampaniaGeneralDetalle = unitOfWork.CampaniaGeneralRepository.ObtenerCampaniaGeneralDetalle(id);

                foreach (var i in litaCampaniaGeneralDetalle)
                {
                    PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO PrioridadPreprocesamientoWhatsAppCampaniaGeneral = new PrioridadPreprocesamientoWhatsAppCampaniaGeneralDTO();
                    PrioridadPreprocesamientoWhatsAppCampaniaGeneral.IdCampaniaGeneralDetalle = i.Id;
                    PrioridadPreprocesamientoWhatsAppCampaniaGeneral.Usuario = usuario;
                    PrioridadPreprocesamientoWhatsAppCampaniaGeneral.ListaResponsableReal = unitOfWork.CampaniaGeneralRepository.ObtenerCantidadCampaniaGeneralDetalle(i.Id);
                    whatsAppRemplazoEtiquetaService.FinalizarPreProcesamientoWhatsApp(PrioridadPreprocesamientoWhatsAppCampaniaGeneral);
                }
                return true;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 13/01/2025
        /// Version 1.0
        /// <summary>
        /// Obtiene combo de SubAreas por idArea
        /// </summary>
        /// <param name="idArea"></param>
        /// <returns></returns>
        public IEnumerable<ComboDTO> ObtenerSubAreaPorIdDeAreaLista(List<int> idArea)
        {
            try
            {
                return unitOfWork.SubAreaCapacitacionRepository.ObtenerSubAreaPorIdDeAreaLista(idArea);
            }
            catch
            {
                throw;
            }
        }

    }
}
