using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: MatriculaCabeceraDatosCertificadoService
    /// Autor: Jonathan Caipo
    /// Fecha: 29/11/2022
    /// <summary>
    /// Gestión general de T_AsignacionOportunidad
    /// </summary>
    public class MatriculaCabeceraDatosCertificadoService : IMatriculaCabeceraDatosCertificadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MatriculaCabeceraDatosCertificadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public MatriculaCabeceraDatosCertificado Add(MatriculaCabeceraDatosCertificado entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraDatosCertificado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MatriculaCabeceraDatosCertificado Update(MatriculaCabeceraDatosCertificado entidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MatriculaCabeceraDatosCertificado>(modelo);
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
                _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabeceraDatosCertificado> Add(List<MatriculaCabeceraDatosCertificado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraDatosCertificado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MatriculaCabeceraDatosCertificado> Update(List<MatriculaCabeceraDatosCertificado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MatriculaCabeceraDatosCertificado>>(modelo);
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
                _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Transforma una cadena de fecha en DateTime
        /// </summary>
        /// <returns> DateTime </returns>        
        public DateTime TransformarCadenaEnFecha(string fecha)
        {
            try
            {
                DateTime fechaTotal = new DateTime();
                string[] date = fecha.Split(' ');
                string mes = "00";
                switch (date[2].ToUpper())
                {
                    case "ENERO":
                        mes = "01";
                        break;
                    case "FEBRERO":
                        mes = "02";
                        break;
                    case "MARZO":
                        mes = "03";
                        break;
                    case "ABRIL":
                        mes = "04";
                        break;
                    case "MAYO":
                        mes = "05";
                        break;
                    case "JUNIO":
                        mes = "06";
                        break;
                    case "JULIO":
                        mes = "07";
                        break;
                    case "AGOSTO":
                        mes = "08";
                        break;
                    case "SEPTIEMBRE":
                        mes = "09";
                        break;
                    case "SETIEMBRE":
                        mes = "09";
                        break;
                    case "OCTUBRE":
                        mes = "10";
                        break;
                    case "NOVIEMBRE":
                        mes = "11";
                        break;
                    case "DICIEMBRE":
                        mes = "12";
                        break;
                }
                fechaTotal = DateTime.Parse(date[4] + "-" + mes + "-" + date[0]);
                return fechaTotal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Transforma una fecha en cadena
        /// </summary>
        /// <returns> string </returns>        
        public string TransformarFechaEnCadena(DateTime fecha)
        {
            try
            {
                string dia = fecha.ToString("dd");
                string mes = fecha.ToString("MM");
                string year = fecha.ToString("yyyy");
                string mesName = "";
                string fechaTotal = "";
                switch (mes)
                {
                    case "01":
                        mesName = "Enero";
                        break;
                    case "02":
                        mesName = "Febrero";
                        break;
                    case "03":
                        mesName = "Marzo";
                        break;
                    case "04":
                        mesName = "Abril";
                        break;
                    case "05":
                        mesName = "Mayo";
                        break;
                    case "06":
                        mesName = "Junio";
                        break;
                    case "07":
                        mesName = "Julio";
                        break;
                    case "08":
                        mesName = "Agosto";
                        break;
                    case "09":
                        mesName = "Setiembre";
                        break;
                    case "10":
                        mesName = "Octubre";
                        break;
                    case "11":
                        mesName = "Noviembre";
                        break;
                    case "12":
                        mesName = "Diciembre";
                        break;
                }
                fechaTotal = dia + " de " + mesName + " del " + year;
                return fechaTotal;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Todo el combo de la tabla MatriculaCabeceraDatosCertificado por medio de idMatriculaCabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public MatriculaCabeceraDatosCertificado ObtenerTotal(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.MatriculaCabeceraDatosCertificadoRepository.ObtenerTotal(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
