using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MatriculaCabeceraDatosCertificadoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 29/11/2022
    /// <summary>
    /// Gestión general de T_MatriculaCabeceraDatosCertificado
    /// </summary>
    public class MatriculaCabeceraDatosCertificadoRepository : GenericRepository<TMatriculaCabeceraDatosCertificado>, IMatriculaCabeceraDatosCertificadoRepository
    {
        private Mapper _mapper;

        public MatriculaCabeceraDatosCertificadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMatriculaCabeceraDatosCertificado, MatriculaCabeceraDatosCertificado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMatriculaCabeceraDatosCertificado MapeoEntidad(MatriculaCabeceraDatosCertificado entidad)
        {
            try
            {
                //crea la entidad padre
                TMatriculaCabeceraDatosCertificado modelo = _mapper.Map<TMatriculaCabeceraDatosCertificado>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaCabeceraDatosCertificado Add(MatriculaCabeceraDatosCertificado entidad)
        {
            try
            {
                var MatriculaCabeceraDatosCertificado = MapeoEntidad(entidad);
                base.Insert(MatriculaCabeceraDatosCertificado);
                return MatriculaCabeceraDatosCertificado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMatriculaCabeceraDatosCertificado Update(MatriculaCabeceraDatosCertificado entidad)
        {
            try
            {
                var MatriculaCabeceraDatosCertificado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MatriculaCabeceraDatosCertificado.RowVersion = entidadExistente.RowVersion;

                base.Update(MatriculaCabeceraDatosCertificado);
                return MatriculaCabeceraDatosCertificado;
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


        public IEnumerable<TMatriculaCabeceraDatosCertificado> Add(IEnumerable<MatriculaCabeceraDatosCertificado> listadoEntidad)
        {
            try
            {
                List<TMatriculaCabeceraDatosCertificado> listado = new List<TMatriculaCabeceraDatosCertificado>();
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

        public IEnumerable<TMatriculaCabeceraDatosCertificado> Update(IEnumerable<MatriculaCabeceraDatosCertificado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMatriculaCabeceraDatosCertificado> listado = new List<TMatriculaCabeceraDatosCertificado>();
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
                MatriculaCabeceraDatosCertificado respuesta = new MatriculaCabeceraDatosCertificado();
                var query = @"SELECT 
                                Id, IdMatriculaCabecera, Duracion, FechaInicio, FechaFinal, NombreCurso, EstadoCambioDatos,
                                Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, IdMigracion 
                            FROM 
                                fin.T_MatriculaCabeceraDatosCertificado
                            WHERE Estado = 1 AND EstadoCambioDatos = 1 AND IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<MatriculaCabeceraDatosCertificado>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Repositorio: MatriculaCabeceraDatosCertificadoRepositorio
        /// Autor: Daniel Huaita
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna el registro del certificado actual
        /// </summary>
        /// <returns> MatriculaCabeceraDatosCertificadoDTO </returns>        
        public List<MatriculaCabeceraDatosCertificadoDTO> ObtenerDatosCertificadoPorMatricula(int IdMatriculaCabecera)
        {
            try
            {
                List<MatriculaCabeceraDatosCertificadoDTO> certificado = new List<MatriculaCabeceraDatosCertificadoDTO>();
                var query = string.Empty;
                query = "SELECT Id,IdMatriculaCabecera,Duracion,FechaInicio,FechaFinal,NombreCurso,EstadoCambioDatos, UsuarioCreacion AS Usuario,'' AS Mensaje FROM fin.T_MatriculaCabeceraDatosCertificado WHERE  Estado = 1 AND  IdMatriculaCabecera=@IdMatriculaCabecera AND  EstadoCambioDatos=0";
                var cargosDB = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<List<MatriculaCabeceraDatosCertificadoDTO>>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        } 
        public MatriculaCabeceraDatosCertificado ObtenerMatriculaCabceraDatosCertificado(int IdMatriculaCabecera)
        {
            try
            {
                MatriculaCabeceraDatosCertificado certificado = new MatriculaCabeceraDatosCertificado();
                var query = string.Empty;
                query = "SELECT Top 1 Id,IdMatriculaCabecera,Duracion,FechaInicio,FechaFinal,NombreCurso,EstadoCambioDatos, UsuarioCreacion AS Usuario,'' AS Mensaje FROM fin.T_MatriculaCabeceraDatosCertificado WHERE  Estado = 1 AND  IdMatriculaCabecera=@IdMatriculaCabecera AND  EstadoCambioDatos=1";
                var cargosDB = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    certificado = JsonConvert.DeserializeObject<MatriculaCabeceraDatosCertificado>(cargosDB);
                }
                return certificado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public DateTime TranformarCadenaEnFecha(string fecha)
        {
            try
            {
                DateTime FechaMod = new DateTime();
                string[] dates = fecha.Split(' ');
                string mes = "00";
                switch (dates[2].ToUpper())
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
                FechaMod = DateTime.Parse(dates[4] + "-" + mes + "-" + dates[0]);
                return FechaMod;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string TranformarFechaEnCadena(DateTime fecha)
        {
            try
            {
                string dia = fecha.ToString("dd");
                string mes = fecha.ToString("MM");
                string year = fecha.ToString("yyyy");
                string mesName = "";
                string FechaMod = "";
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
                FechaMod = dia + " de " + mesName + " del " + year;
                return FechaMod;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
