using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: EmpresaController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión de Empresa
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class EmpresaController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public EmpresaController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a insertar</param>
        /// <returns>Retorna 200 y objeto ingresado o 400 y mensaje de error </returns>
        [HttpPost("Insertar")]
        public IActionResult Insertar([FromBody] EmpresaDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var empresaService = new EmpresaService(unitOfWork);
                var empresa = new Empresa();

                empresa.Nombre = Objeto.Nombre;
                empresa.Ruc = Objeto.Ruc;
                empresa.IdTipoIdentificador = Objeto.IdTipoIdentificador;
                empresa.Direccion = Objeto.Direccion;
                empresa.Telefono = Objeto.Telefono;
                empresa.PaginaWeb = Objeto.PaginaWeb;
                empresa.Email = Objeto.Email;
                empresa.Trabajadores = Objeto.Trabajadores;
                empresa.NivelFacturacion = Objeto.NivelFacturacion;
                empresa.IdPais = Objeto.IdPais;
                empresa.IdRegion = Objeto.IdRegion;
                empresa.IdCiudad = Objeto.IdCiudad;
                empresa.IdIndustria = Objeto.IdIndustria;
                empresa.IdTipoEmpresa = Objeto.IdTipoEmpresa;
                empresa.IdTamanio = Objeto.IdTamanio;
                empresa.Ciiu = Objeto.Ciiu;
                if(empresa.IdPais == 52)
                {
                    empresa.IdMunicipioMexico = Objeto.IdMunicipioMexico;
                    empresa.IdAsentamientoMexico = Objeto.IdAsentamientoMexico;
                    empresa.IdCiudadMexico = Objeto.IdCiudadMexico;
                    empresa.CodigoPostal = Objeto.CodigoPostal;
                }
                empresa.IdCodigoCiiuIndustria = Objeto.IdCodigoCiiuIndustria;
                empresa.Estado = true;
                empresa.FechaCreacion = DateTime.Now;
                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioCreacion = Objeto.usuario;
                empresa.UsuarioModificacion = Objeto.usuario;
                var respuesta = empresaService.Add(empresa);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a insertar</param>
        /// <returns>Retorna 200 y listado de objetos ingresados o 400 y mensaje de error</returns>
        [HttpPost("InsertarLista")]
        public IActionResult InsertarLista([FromBody] List<Empresa> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                var respuesta = servicio.Add(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla
        /// </summary>
        /// <param name="entidad">Entidad a modificar</param>
        /// <returns>Retorna 200 y objeto actualizado o 400 y mensaje de error</returns>
        [HttpPut("Actualizar")]
        public IActionResult Actualizar([FromBody] EmpresaDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var empresaService = new EmpresaService(unitOfWork);
                var empresa = empresaService.ObtenerPorId(Objeto.Id);

                empresa.Nombre = Objeto.Nombre;
                empresa.Ruc = Objeto.Ruc;
                empresa.IdTipoIdentificador = Objeto.IdTipoIdentificador;
                empresa.Direccion = Objeto.Direccion;
                empresa.Telefono = Objeto.Telefono;
                empresa.PaginaWeb = Objeto.PaginaWeb;
                empresa.Email = Objeto.Email;
                empresa.Trabajadores = Objeto.Trabajadores;
                empresa.NivelFacturacion = Objeto.NivelFacturacion;
                empresa.IdPais = Objeto.IdPais;
                empresa.IdRegion = Objeto.IdRegion;
                empresa.IdCiudad = Objeto.IdCiudad;
                empresa.IdIndustria = Objeto.IdIndustria;
                empresa.IdTipoEmpresa = Objeto.IdTipoEmpresa;
                empresa.IdTamanio = Objeto.IdTamanio;
                empresa.Ciiu = Objeto.Ciiu;
                if (empresa.IdPais == 52)
                {
                    empresa.IdMunicipioMexico = Objeto.IdMunicipioMexico;
                    empresa.IdAsentamientoMexico = Objeto.IdAsentamientoMexico;
                    empresa.IdCiudadMexico = Objeto.IdCiudadMexico;
                    empresa.CodigoPostal = Objeto.CodigoPostal;
                }
                empresa.IdCodigoCiiuIndustria = Objeto.IdCodigoCiiuIndustria;
                empresa.FechaModificacion = DateTime.Now;
                empresa.UsuarioModificacion = Objeto.usuario;
                var respuesta = empresaService.Update(empresa);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: PUT
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion basica a la tabla de una lista
        /// </summary>
        /// <param name="listado">Lista de entidades a actualizar</param>
        /// <returns>Retorna 200 y listado de objetos actualizados o 400 y mensaje de error</returns>
        [HttpPut("ActualizarLista")]
        public IActionResult ActualizarLista([FromBody] List<Empresa> listado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                var respuesta = servicio.Update(listado);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("Eliminar/{id}/{usuario}")]
        public IActionResult Eliminar(int id, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                var respuesta = servicio.Delete(id, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: DELETE
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion logica basica a la tabla de una lista
        /// </summary>
        /// <param name="listadoIds">Lista de Id de la entidad a eliminar</param>
        /// <param name="usuario">Nombre del usuario que realiza la eliminacion</param>
        /// <returns>Retorna 200 y bandera de eliminacion realizada o 400 y mensaje de error</returns>
        [HttpDelete("EliminarListado/{usuario}")]
        public IActionResult EliminarListado(List<int> listadoIds, string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                var respuesta = servicio.Delete(listadoIds, usuario);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Empresa
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerEmpresa")]
        public IActionResult ObtenerEmpresa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerEmpresa());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Juan D. Huanaco Quispe.
        /// Fecha: 26/04/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos de una empresa por Id
        /// </summary>
        /// <returns> Retorna 200 y objeto o mensaje de error </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult<EmpresaDTO> ObtenerPorId(int id)
        {
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerEmpresaDtoPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Flavio R.
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Empresa
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos o 400 y mensaje de error </returns>
        [HttpGet("ObtenerEmpresas")]
        public IActionResult ObtenerEmpresas()
        {
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerEmpresas());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/03/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Empresa
        /// </summary>
        [HttpPost("ObtenerEmpresaFiltro")]
        public IActionResult ObtenerEmpresaFiltro([FromBody] FiltroKendoGridDTO gridState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerEmpresaFiltro(gridState));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// Tipo Función: GET
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_Empresa para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("[Action]")]
        public IActionResult ObtenerCombo()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerCombo());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TipoIdentificador para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboTipoIdentificador")]
        public IActionResult ObtenerComboTipoIdentificador()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerComboTipoIdentificador());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_TamanioEmpresa para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboTamanioEmpresa")]
        public IActionResult ObtenerComboTamanioEmpresa()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerComboTamanioEmpresa());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Griselberto Huaman
        /// Fecha: 10/06/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros guardados en T_CodigoCiiuIndustria para combo.
        /// </summary>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpGet("ObtenerComboCodigoCiiuIndustria")]
        public IActionResult ObtenerComboCodigoCiiuIndustria()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerComboCodigoCiiuIndustria());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de Empresas relacionadas al Nombre Parcial.
        /// </summary>
        /// <param name="Filtros">Filtros que contienen el Nombre Parcial</param>
        /// <returns> Retorna 200 y lista de objetos para combo o 400 y mensaje de error </returns>
        [HttpPost("[action]")]
        public IActionResult ObtenerAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var servicio = new EmpresaService(unitOfWork);
                return Ok(servicio.ObtenerAutocomplete(Filtros["valor"].ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 11/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una sola empresa por medio del id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerEmpresaPorId(int id)
        {
            try
            {
                EmpresaService servicioEmpresa = new EmpresaService(unitOfWork);
                return Ok(servicioEmpresa.ObtenerEmpresaPorId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: GIlmer Quispe
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las empresas ordenado por FechaCreacion de forma Descendiente
        /// </summary> 
        /// <returns> List<EmpresaDTO>> </EmpresaDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult Obtener()
        {
            try
            {
                var empresaService = new EmpresaService(unitOfWork);
                var data = empresaService.ObtenerEmpresa2();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        /// Tipo Función: GET
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_CodigoCiiuIndustria con el estado=1.
        /// </summary> 
        /// <returns> CodigoCiiuIndustria </returns>
        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerNombreCodigoCIIUPorId(int id)
        {

            try
            {
                var codigoCiiuIndustriaService = new CodigoCiiuIndustriaService(unitOfWork);
                var codigoCIIU = codigoCiiuIndustriaService.ObtenerPorId(id);
                return Ok(codigoCIIU);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerNombreCodigoCIIUPorFiltro([FromBody] Dictionary<string, string> filtros)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            if (filtros.Count <= 0 && filtros != null)
            {
                return Ok();
            }
            try
            {
                var codigoCiiuIndustriaService = new CodigoCiiuIndustriaService(unitOfWork);
                string filtro = filtros["valor"].ToString();
                var codigoCIIU = codigoCiiuIndustriaService.ObtenerPorNombre(filtro);

                return Ok(codigoCIIU);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
