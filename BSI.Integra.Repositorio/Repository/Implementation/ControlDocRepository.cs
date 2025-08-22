using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ControlDocRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ControlDoc
    /// </summary>
    public class ControlDocRepository : GenericRepository<TControlDoc>, IControlDocRepository
    {
        private Mapper _mapper;

        public ControlDocRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TControlDoc, ControlDoc>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TControlDoc MapeoEntidad(ControlDoc entidad)
        {
            try
            {
                //crea la entidad padre
                TControlDoc modelo = _mapper.Map<TControlDoc>(entidad);

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

        public TControlDoc Add(ControlDoc entidad)
        {
            try
            {
                var ControlDoc = MapeoEntidad(entidad);
                base.Insert(ControlDoc);
                return ControlDoc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TControlDoc Update(ControlDoc entidad)
        {
            try
            {
                var ControlDoc = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ControlDoc.RowVersion = entidadExistente.RowVersion;

                base.Update(ControlDoc);
                return ControlDoc;
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


        public IEnumerable<TControlDoc> Add(IEnumerable<ControlDoc> listadoEntidad)
        {
            try
            {
                List<TControlDoc> listado = new List<TControlDoc>();
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

        public IEnumerable<TControlDoc> Update(IEnumerable<ControlDoc> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TControlDoc> listado = new List<TControlDoc>();
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

        /// <summary>
        /// Obtiene los documentos para un alumno por matricula
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<DocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabecera(string CodigoMatricula)
        {
            try
            {
                List<DocumentoMatriculaDTO> matriculas = new List<DocumentoMatriculaDTO>();
                var _query = "SELECT CodigoMatricula, IdCriterioDocs, NombreDocumento, Estado,EstadoEntero FROM fin.V_ObtenerDatosDocumentosPorMatriculaCabecera WHERE CodigoMatricula = @CodigoMatricula AND Estado=1";
                var matriculasBD = _dapperRepository.QueryDapper(_query, new { CodigoMatricula });
                if (!matriculasBD.Contains("[]") && !string.IsNullOrEmpty(matriculasBD))
                {
                    matriculas = JsonConvert.DeserializeObject<List<DocumentoMatriculaDTO>>(matriculasBD);
                }
                return matriculas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los documentos para un alumno por Matricula cabecera
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<ControlDocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabeceraControl(int idMatriculaCabecera)
        {
            try
            {
                List<ControlDocumentoMatriculaDTO> matriculas = new List<ControlDocumentoMatriculaDTO>();
                var _query = "SELECT IdControlDoc, IdMatriculaCabecera, CodigoMatricula, IdCriterioDoc, NombreDocumento, EstadoDocumento, Recepcionado FROM fin.V_ObtenerDatosDocumentosPorMatricula WHERE IdMatriculaCabecera = @idMatriculaCabecera AND EstadoCriterioDocumento = 1 AND EstadoMatriculaCabecera = 1";
                var matriculasBD = _dapperRepository.QueryDapper(_query, new { idMatriculaCabecera });
                if (!matriculasBD.Contains("[]") && !string.IsNullOrEmpty(matriculasBD))
                {
                    matriculas = JsonConvert.DeserializeObject<List<ControlDocumentoMatriculaDTO>>(matriculasBD);
                }
                return matriculas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



    }
}
