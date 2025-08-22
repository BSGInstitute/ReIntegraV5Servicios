using AutoMapper;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: NuevoAlumnoCongeladoService
    /// Autor: Griselberto Huaman.
    /// Fecha: 17/12/2022
    /// <summary>
    /// Gestión general de T_NuevoAlumnoCongelado
    /// </summary>
    public class NuevoAlumnoCongeladoService : INuevoAlumnoCongeladoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public NuevoAlumnoCongeladoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNuevoAlumnoCongelado, NuevoAlumnoCongelado>(MemberList.None).ReverseMap();
                cfg.CreateMap<NuevoAlumnoCongelado, NuevoAlumnoCongeladoDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public NuevoAlumnoCongelado Add(NuevoAlumnoCongeladoDTO data)
        {
            try
            {
                var entidad = _mapper.Map<NuevoAlumnoCongelado>(data);
                entidad.Id = 0;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCongelamiento = DateTime.Now;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = true;
                var modelo = _unitOfWork.NuevoAlumnoCongeladoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<NuevoAlumnoCongelado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NuevoAlumnoCongelado Update(NuevoAlumnoCongeladoDTO data)
        {
            try
            {
                var rep = _unitOfWork.NuevoAlumnoCongeladoRepository;
                var anterior = _mapper.Map<NuevoAlumnoCongelado>(rep.FirstById(data.Id));
                var entidad = _mapper.Map<NuevoAlumnoCongelado>(data);
                entidad.FechaCreacion = anterior.FechaCreacion;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = anterior.UsuarioCreacion;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.Estado = anterior.Estado;
                entidad.FechaCongelamiento = anterior.FechaCongelamiento;
                var modelo = _unitOfWork.NuevoAlumnoCongeladoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<NuevoAlumnoCongelado>(modelo);
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
                _unitOfWork.NuevoAlumnoCongeladoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NuevoAlumnoCongelado> Add(List<NuevoAlumnoCongelado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.NuevoAlumnoCongeladoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<NuevoAlumnoCongelado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NuevoAlumnoCongelado> Update(List<NuevoAlumnoCongelado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.NuevoAlumnoCongeladoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<NuevoAlumnoCongelado>>(modelo);
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
                _unitOfWork.NuevoAlumnoCongeladoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman.
        /// Fecha: 17/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NuevoAlumnoCongelado
        /// </summary>
        /// <returns> List<NuevoAlumnoCongeladoDTO> </returns>
        public IEnumerable<NuevoAlumnoCongeladoDTO> ObtenerListaNuevoAlumnoCongelado()
        {
            try
            {
                return _unitOfWork.NuevoAlumnoCongeladoRepository.ObtenerListaNuevoAlumnoCongelado();
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }


        public IEnumerable<NuevoAlumnoCongeladoExcelDTO> MostrarDatosExcel(IFormFile ArchivoExcel)
        {
            try
            {
                var ListaReporteFlujo = new List<NuevoAlumnoCongeladoExcelDTO>();
                int index = 0;
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    NewLine = Environment.NewLine,
                    Delimiter = ";",
                    MissingFieldFound = null,
                    BadDataFound = null,
                };
                using (var reader = new StreamReader(ArchivoExcel.OpenReadStream()))
                using (var cvs = new CsvReader(reader, config))
                {
                    
                    //cvs.Configuration.Delimiter = ";";
                    //cvs.Configuration.MissingFieldFound = null;
                    //cvs.Configuration.BadDataFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;

                        NuevoAlumnoCongeladoExcelDTO flujoData = new NuevoAlumnoCongeladoExcelDTO();
                        flujoData.CodigoMatricula = cvs.GetField<string>("Codigo Matricula");
                        flujoData.NroCuota = cvs.GetField<int>("Nro Cuota");
                        flujoData.NroSubCuota = cvs.GetField<int>("Nro Subcuota");
                        flujoData.FechaVencimiento = cvs.GetField<string>("Fecha Vencimiento Actual") == "" ? DateTime.Today : cvs.GetField<DateTime>("Fecha Vencimiento Actual");
                        flujoData.Cuota = cvs.GetField<decimal>("Total Cuota Dolar");
                        flujoData.Saldo = cvs.GetField<decimal>("Saldo Pendiente");
                        flujoData.Mora = cvs.GetField<decimal>("Mora");
                        flujoData.MontoPagado = cvs.GetField<decimal>("Real Pago Dolar");
                        flujoData.FechaPago = cvs.GetField<string>("Fecha Pago") == "" ? DateTime.Today : cvs.GetField<DateTime>("Fecha Pago");
                        if(flujoData.CodigoMatricula!=null && flujoData.NroCuota != 0 && flujoData.NroSubCuota != 0
                            && flujoData.Cuota != 0)ListaReporteFlujo.Add(flujoData);
                        else throw new Exception("Uno/varios valores o Campos son incorrectos!");
                    }
                }
                var Nregistros = index;
                return ListaReporteFlujo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los datos del CSV , cargados en la grilla.
        /// </summary>
        /// <returns> boolean </returns>
        /// <param name="json">Grupo de parametros </param>
        public bool InsertarExcelAlumnoCongelado(FiltroNuevoAlumnoCongeladoExcelDTO json)
        {
            try
            {
                return _unitOfWork.NuevoAlumnoCongeladoRepository.InsertarExcelNuevoAlumnoCongelado(json.ListaAlumnoCongelado, json.FechaCongelamiento, json.IdPeriodo, json.Usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}

