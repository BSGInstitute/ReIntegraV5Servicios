using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CertificadoGeneradoAutomaticoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 28/11/2022
    /// <summary>
    /// Gestión general de T_CertificadoGeneradoAutomatico
    /// </summary>
    public class CertificadoGeneradoAutomaticoRepository : GenericRepository<TCertificadoGeneradoAutomatico>, ICertificadoGeneradoAutomaticoRepository
    {
        private Mapper _mapper;

        public CertificadoGeneradoAutomaticoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoGeneradoAutomatico, CertificadoGeneradoAutomatico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCertificadoGeneradoAutomatico MapeoEntidad(CertificadoGeneradoAutomatico entidad)
        {
            try
            {
                //crea la entidad padre
                TCertificadoGeneradoAutomatico modelo = _mapper.Map<TCertificadoGeneradoAutomatico>(entidad);

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

        public TCertificadoGeneradoAutomatico Add(CertificadoGeneradoAutomatico entidad)
        {
            try
            {
                var CajaEgresoAprobado = MapeoEntidad(entidad);
                base.Insert(CajaEgresoAprobado);
                return CajaEgresoAprobado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCertificadoGeneradoAutomatico Update(CertificadoGeneradoAutomatico entidad)
        {
            try
            {
                var CertificadoGeneradoAutomatico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CertificadoGeneradoAutomatico.RowVersion = entidadExistente.RowVersion;

                base.Update(CertificadoGeneradoAutomatico);
                return CertificadoGeneradoAutomatico;
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


        public IEnumerable<TCertificadoGeneradoAutomatico> Add(IEnumerable<CertificadoGeneradoAutomatico> listadoEntidad)
        {
            try
            {
                List<TCertificadoGeneradoAutomatico> listado = new List<TCertificadoGeneradoAutomatico>();
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

        public IEnumerable<TCertificadoGeneradoAutomatico> Update(IEnumerable<CertificadoGeneradoAutomatico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCertificadoGeneradoAutomatico> listado = new List<TCertificadoGeneradoAutomatico>();
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
        /// Obtiene Correlativo Certificado
        /// </summary>
        /// <returns> int </returns>
        public int ObtenerCorrelativoCertificado()
        {
            ValorIntDTO valor = new ValorIntDTO();
            string query = "SELECT Id as Valor FROM pla.T_CertificadoGeneradoAutomatico ORDER BY Id DESC";
            string resultado = _dapperRepository.FirstOrDefault(query, null);
            if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
            {
                valor = JsonConvert.DeserializeObject<ValorIntDTO>(resultado)!;
                return valor.Valor.Value + 1;
            }
            else
            {
                throw new Exception("No se pudo Calcular Consecutivo");
            }
        }
        /// <summary>
        /// Actualiza a Nombre de ArchivoCertificado
        /// </summary>
        /// <param name="idMatriculaCabecera" "IdPEspecifico"></param>
        /// <returns></returns>
        public void ActualizarNombreArchivo(string NombreArchivo, int IdCertificado)
        {
            try
            {
                //List<PEspecificoMatriculaAlumnoAgendaDTO> pEspecificoMatriculaAlumnoAgendaDTOs = new List<PEspecificoMatriculaAlumnoAgendaDTO>();
                string _queryAlumnoFiltro = string.Empty;
                _queryAlumnoFiltro = "UPDATE pla.T_CertificadoGeneradoAutomatico SET NombreArchivo=@NombreArchivo WHERE id=@IdCertificado";
                var lista = _dapperRepository.QueryDapper(_queryAlumnoFiltro, new { NombreArchivo, IdCertificado });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
