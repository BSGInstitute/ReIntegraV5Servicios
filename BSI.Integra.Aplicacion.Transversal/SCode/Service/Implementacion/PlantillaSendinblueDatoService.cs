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
    /// Service: PlantillaSendinblueDatoService
    /// Autor: Adriana Chipana Ampuero.
    /// Fecha: 18/05/2022
    /// <summary>
    /// Gestión general de T_PlantillaSendinblueDato
    /// </summary>
    public class PlantillaSendinblueDatoService : IPlantillaSendinblueDatoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlantillaSendinblueDatoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConjuntoListum, PlantillaSendinblueDato>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }



        public bool AgregarPlantillaDatos(PlantillaSendinblueInsertarDTO datos, string usuario)
        {
            try
            {

                PlantillaSendinblue plantilla = new PlantillaSendinblue();
                plantilla.Id = 0;
                plantilla.Nombre = datos.Nombre;
                plantilla.IdPlantillaSendinblueBase = datos.IdPlantillaSendinblueBase;
                plantilla.HtmlContenido = datos.HtmlContenido;
                plantilla.HtmlEditado = datos.HtmlEditado;
                plantilla.Estado = true;
                plantilla.FechaCreacion = DateTime.Now;
                plantilla.FechaModificacion = DateTime.Now;
                plantilla.UsuarioCreacion = usuario;
                plantilla.UsuarioModificacion = usuario;

                var modelo = _unitOfWork.PlantillaSendinblueRepository.Add(plantilla);
                _unitOfWork.Commit();

                foreach (var item in datos.DatosEtiqueta)
                {
                    PlantillaSendinblueDato plantillaDato = new PlantillaSendinblueDato();
                    plantillaDato.Id = 0;
                    plantillaDato.Nombre = item.Nombre;
                    plantillaDato.Valor = item.Valor;
                    plantillaDato.Etiqueta = item.Etiqueta;
                    plantillaDato.IdPlantillaSendinblue = modelo.Id;
                    plantillaDato.IdPlantillaSendinblueTipoEtiqueta = item.IdPlantillaSendinblueTipoEtiqueta;
                    plantillaDato.Estado = true;
                    plantillaDato.FechaCreacion = DateTime.Now;
                    plantillaDato.FechaModificacion = DateTime.Now;
                    plantillaDato.UsuarioCreacion = usuario;
                    plantillaDato.UsuarioModificacion = usuario;

                    var resultado = _unitOfWork.PlantillaSendinblueDatoRepository.Add(plantillaDato);
                    _unitOfWork.Commit();

                }

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }
        public bool ActualizarPlantillaDatos(PlantillaSendinblueActualizarDTO datos, string usuario)
        {
            try
            {

                PlantillaSendinblue plantilla = new PlantillaSendinblue();
                var Objeto = _unitOfWork.PlantillaSendinblueRepository.FirstById(datos.Id);
                if (Objeto.Id > 0)
                {
                    plantilla.Id = Objeto.Id;
                    plantilla.Nombre = datos.Nombre;
                    plantilla.IdPlantillaSendinblueBase = datos.IdPlantillaSendinblueBase;
                    plantilla.HtmlContenido = datos.HtmlContenido;
                    plantilla.HtmlEditado = datos.HtmlEditado;
                    plantilla.Estado = true;
                    plantilla.FechaCreacion = Objeto.FechaCreacion;
                    plantilla.FechaModificacion = DateTime.Now;
                    plantilla.UsuarioCreacion = Objeto.UsuarioCreacion;
                    plantilla.UsuarioModificacion = usuario;

                    var modelo = _unitOfWork.PlantillaSendinblueRepository.Update(plantilla);
                    _unitOfWork.Commit();

                }
                else
                {
                    throw new Exception("No existe la plantilla");
                }

                foreach (var item in datos.DatosEtiqueta)
                {
                    PlantillaSendinblueDato plantillaDato = new PlantillaSendinblueDato();
                    var Objeto2 = _unitOfWork.PlantillaSendinblueDatoRepository.FirstById(item.Id);
                    if (Objeto2.Id > 0)
                    {
                        plantillaDato.Id = Objeto2.Id;
                        plantillaDato.Nombre = item.Nombre;
                        plantillaDato.Valor = item.Valor;
                        plantillaDato.Etiqueta = item.Etiqueta;
                        plantillaDato.IdPlantillaSendinblue = Objeto.Id;
                        plantillaDato.IdPlantillaSendinblueTipoEtiqueta = item.IdPlantillaSendinblueTipoEtiqueta;
                        plantillaDato.Estado = true;
                        plantillaDato.FechaCreacion = Objeto2.FechaCreacion;
                        plantillaDato.FechaModificacion = DateTime.Now;
                        plantillaDato.UsuarioCreacion = Objeto2.UsuarioCreacion;
                        plantillaDato.UsuarioModificacion = usuario;

                        var resultado = _unitOfWork.PlantillaSendinblueDatoRepository.Update(plantillaDato);
                        _unitOfWork.Commit();

                    }
                    else
                    {
                        throw new Exception("No existe la plantilla");
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw ex;
            }
        }


        public List<PlantillaSendinblueDatoDTO> ObtenerDatosPlantilllaPorId(int IdPlantillaSendinblue)
        {
            try
            {
                var modelo = _unitOfWork.PlantillaSendinblueDatoRepository.ObtenerDatosPlantilllaPorId(IdPlantillaSendinblue);
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
