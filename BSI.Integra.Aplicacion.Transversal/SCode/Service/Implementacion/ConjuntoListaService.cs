using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using PdfSharp.Pdf.Filters;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConjuntoListaService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_ConjuntoLista
    /// </summary>
    public class ConjuntoListaService : IConjuntoListaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConjuntoListaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConjuntoListum, ConjuntoLista>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }


        public bool Insertar(ConjuntoListaDetalleCompletoListoDTO ConjuntoListaDetalleCompleto, string Usuario)
        {
            try
            {
                //using (TransactionScope scope = new TransactionScope())
                //{
                    var conjuntoLista = new ConjuntoLista()
                    {
                        Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre,
                        Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion,
                        IdCategoriaObjetoFiltro = ConjuntoListaDetalleCompleto.ConjuntoLista.IdCategoriaObjetoFiltro,
                        IdFiltroSegmento = ConjuntoListaDetalleCompleto.ConjuntoLista.IdFiltroSegmento,
                        NroListasRepeticionContacto = ConjuntoListaDetalleCompleto.ConjuntoLista.NroListasRepeticionContacto,
                        ConsiderarYaEnviados = ConjuntoListaDetalleCompleto.ConjuntoLista.ConsiderarYaEnviados,
                        UsuarioCreacion = Usuario,
                        UsuarioModificacion = Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    conjuntoLista.ListaConjuntoListaDetalle = new List<ConjuntoListaDetalle>(); // Inicializar la propiedad ListaConjuntoListaDetalle

                    //foreach (var item in ConjuntoListaDetalleCompleto.ConjuntoListaDetalle)
                    //{
                        var conjuntoListaDetalle = new ConjuntoListaDetalle()
                        {
                            Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre,
                            Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion,
                            Prioridad = 1,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };

                        //// Llenamos hijos
                        //var conjuntoListaDetalleValor = new List<ConjuntoListaDetalleValor>();

                        //foreach (var area in item.ListaArea)
                        //{
                        //    var _new = new ConjuntoListaDetalleValor
                        //    {
                        //        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                        //        Valor = area.Valor,
                        //        Estado = true,
                        //        UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        FechaCreacion = DateTime.Now,
                        //        FechaModificacion = DateTime.Now
                        //    };
                        //    conjuntoListaDetalleValor.Add(_new);
                        //}
                        //foreach (var subArea in item.ListaSubArea)
                        //{
                        //    var _new = new ConjuntoListaDetalleValor
                        //    {
                        //        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                        //        Valor = subArea.Valor,
                        //        Estado = true,
                        //        UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        FechaCreacion = DateTime.Now,
                        //        FechaModificacion = DateTime.Now
                        //    };//Donde es el error? donde te vota el error
                        //    conjuntoListaDetalleValor.Add(_new);
                        //}
                        //foreach (var pGeneral in item.ListaProgramaGeneral)
                        //{
                        //    var _new = new ConjuntoListaDetalleValor
                        //    {
                        //        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                        //        Valor = pGeneral.Valor,
                        //        Estado = true,
                        //        UsuarioCreacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        UsuarioModificacion = ConjuntoListaDetalleCompleto.NombreUsuario,
                        //        FechaCreacion = DateTime.Now,
                        //        FechaModificacion = DateTime.Now
                        //    };
                        //    conjuntoListaDetalleValor.Add(_new);
                        //}
                        //// Inicializar ListaConjuntoListaDetalleValor//desde donde estas probND
                        //conjuntoListaDetalle.ListaConjuntoListaDetalleValor = conjuntoListaDetalleValor;
                        conjuntoLista.ListaConjuntoListaDetalle.Add(conjuntoListaDetalle);
                    //}
                    _unitOfWork.ConjuntoListaRepository.Add(conjuntoLista);
                    _unitOfWork.Commit();
                   // scope.Complete();
                //}

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        public bool Actualizar(ConjuntoListaDetalleCompletoListoDTO ConjuntoListaDetalleCompleto, string Usuario)
        {
            try
            {
                var _repConjuntoLista = _unitOfWork.ConjuntoListaRepository;
                var _repConjuntoListaDetalle = _unitOfWork.ConjuntoListaDetalleRepository;
                using (TransactionScope scope = new TransactionScope())
                {

                    if (!_repConjuntoLista.Exist(ConjuntoListaDetalleCompleto.ConjuntoLista.Id))
                    {
                        return false;
                    }
                    else {

                        var datos = _repConjuntoLista.FirstById(ConjuntoListaDetalleCompleto.ConjuntoLista.Id);

                    var conjuntoLista = new ConjuntoLista();
                    conjuntoLista.Id = ConjuntoListaDetalleCompleto.ConjuntoLista.Id;
                    conjuntoLista.Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre;
                    conjuntoLista.Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion;
                    conjuntoLista.IdCategoriaObjetoFiltro = ConjuntoListaDetalleCompleto.ConjuntoLista.IdCategoriaObjetoFiltro;
                    conjuntoLista.IdFiltroSegmento = ConjuntoListaDetalleCompleto.ConjuntoLista.IdFiltroSegmento;
                    conjuntoLista.NroListasRepeticionContacto = ConjuntoListaDetalleCompleto.ConjuntoLista.NroListasRepeticionContacto;
                    conjuntoLista.ConsiderarYaEnviados = ConjuntoListaDetalleCompleto.ConjuntoLista.ConsiderarYaEnviados;
                    conjuntoLista.UsuarioModificacion = Usuario;
                    conjuntoLista.UsuarioCreacion = datos.UsuarioCreacion;
                    conjuntoLista.FechaModificacion = DateTime.Now;
                    conjuntoLista.FechaCreacion = datos.FechaCreacion;
                        conjuntoLista.Estado = true;
                    conjuntoLista.RowVersion = datos.RowVersion;

                    var listaIds = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == ConjuntoListaDetalleCompleto.ConjuntoLista.Id).ToList();

                        conjuntoLista.ListaConjuntoListaDetalle = new List<ConjuntoListaDetalle>();

                        foreach (var id in listaIds)
                    {
                        var conjuntoListaDetalle = new ConjuntoListaDetalle()
                        {
                            Id = id.Id,
                            IdConjuntoLista = ConjuntoListaDetalleCompleto.ConjuntoLista.Id,
                            Nombre = ConjuntoListaDetalleCompleto.ConjuntoLista.Nombre,
                            Descripcion = ConjuntoListaDetalleCompleto.ConjuntoLista.Descripcion,
                            Prioridad = 1,
                            UsuarioCreacion = id.UsuarioCreacion,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = id.FechaCreacion,
                            FechaModificacion = DateTime.Now,
                            Estado = true,
                            RowVersion=id.RowVersion
                        };
                        conjuntoLista.ListaConjuntoListaDetalle.Add(conjuntoListaDetalle);
                    }
                    
                    _repConjuntoLista.Update(conjuntoLista);
                    _unitOfWork.Commit();
                    scope.Complete();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int id, string nombre)
        {
            try
            {
                var _repConjuntoLista = _unitOfWork.ConjuntoListaRepository;
                var _repConjuntoListaDetalle = _unitOfWork.ConjuntoListaDetalleRepository;
                var _repConjuntoListaDetalleValor = _unitOfWork.ConjuntoListaDetalleValorRepository;
                var _repConjuntoListaResultado = _unitOfWork.ConjuntoListaResultadoRepository;

                if (_repConjuntoLista.Exist(id))
                {
                    //Eliminamos hijos - detalle
                    var conjuntoListaDetalle = _repConjuntoListaDetalle.GetBy(x => x.IdConjuntoLista == id).ToList();
                    foreach (var item in conjuntoListaDetalle)
                    {
                        var conjuntoListaResultador = _repConjuntoListaResultado.GetBy(x => x.IdConjuntoListaDetalle == item.Id).ToList();
                        foreach (var valorResultado in conjuntoListaResultador)
                        {
                            var conjuntoListaDetalleValor = _repConjuntoListaDetalleValor.GetBy(x => x.IdConjuntoListaDetalle == item.Id).ToList();
                            foreach (var valor in conjuntoListaDetalleValor)
                            {
                                _repConjuntoListaDetalleValor.Delete(valor.Id, nombre);
                            }

                            _repConjuntoListaResultado.Delete(valorResultado.Id, nombre);
                        }
                        _repConjuntoListaDetalle.Delete(item.Id, nombre);
                    }
                    _repConjuntoLista.Delete(id, nombre);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }


        public List<ConjuntoListaGrillaDTO> Obtener()
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoListaRepository.Obtener();
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }

        public ConjuntoListaDTO Obtener(int id)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoListaRepository.Obtener(id);
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }

        public List<ComboDTO> ObtenerCombo()
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoListaRepository.ObtenerCombo();
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }


        public ConjuntoListaDetalleCompletoListoDTO ObtenerDetalle(int IdConjuntoLista)
        {
            try
            {
                var listaConjuntoLista = _unitOfWork.ConjuntoListaRepository.Obtener(IdConjuntoLista);
                //var listaDetalle = _unitOfWork.ConjuntoListaDetalleRepository.Obtener(IdConjuntoLista);

                //foreach (var item in listaDetalle)
                //{
                //    var conjuntoListaDetalleValor = _unitOfWork.ConjuntoListaDetalleValorRepository.ObtenerConjuntoListaDetalleValor(item.IdConjuntoLista);
                //    item.ListaArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                //    item.ListaSubArea = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                //    item.ListaProgramaGeneral = conjuntoListaDetalleValor.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                //}

                var conjuntoListaDetalleCompleto = new ConjuntoListaDetalleCompletoListoDTO()
                {
                    ConjuntoLista = listaConjuntoLista,
                    //ConjuntoListaDetalle = listaDetalle
                };

                return conjuntoListaDetalleCompleto;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }

        public List<ConjuntoListaCompuestoDTO> ObtenerConjuntoFiltro(int id)
        {
            try
            {
                if (!_unitOfWork.ConjuntoListaRepository.Exist(id))
                {
                    throw new Exception("Conjunto lista no existente");
                }
                var conjuntoLista = _unitOfWork.ConjuntoListaRepository.FirstById(id);

                var listadoDatosFiltrados = this.ObtenerResultados(id, conjuntoLista.IdFiltroSegmento);

                return listadoDatosFiltrados;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ConjuntoListaCompuestoDTO> ObtenerResultados(int id, int idFiltroSegmento)
        {
            try
            {
                if (!_unitOfWork.FiltroSegmentoRepository.Exist(idFiltroSegmento))
                {
                    throw new Exception("No existe filtro segmento");
                }
                var filtroSegemento = _unitOfWork.FiltroSegmentoRepository.FirstById( idFiltroSegmento);
                return  _unitOfWork.ConjuntoListaRepository.ObtenerResultado(id, filtroSegemento.IdFiltroSegmentoTipoContacto.Value);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public bool Duplicar(int id, string usuario)
        {
            try
            {
                var conjuntoListaDetalle = this.ObtenerDetalle(id);
                //conjuntoListaDetalle.NombreUsuario = usuario;
                conjuntoListaDetalle.ConjuntoLista.Nombre = string.Concat(conjuntoListaDetalle.ConjuntoLista.Nombre, " - COPIA");
                conjuntoListaDetalle.ConjuntoLista.Descripcion = string.Concat(conjuntoListaDetalle.ConjuntoLista.Descripcion, " - COPIA");

                var resultado = this.Insertar(conjuntoListaDetalle, usuario);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


        }

        public bool SubirLista(ConjuntoListaSubirDTO json, string usuario )
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoListaRepository.SubirLista(json, usuario);
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }




        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario)
        {
            try
            {
                var modelo = _unitOfWork.ConjuntoListaRepository.GenerarUrlFormulariosLink(datos, usuario);
                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }




    }
}
