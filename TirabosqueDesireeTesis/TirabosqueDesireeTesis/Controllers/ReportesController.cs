using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TirabosqueDesireeTesis.Clases;
using TirabosqueDesireeTesis.Models;

namespace TirabosqueDesireeTesis.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportesController : Controller
    {
        private CarniceriaContext db = new CarniceriaContext();


        public ActionResult ReporteListaPedidos()
        {
            var vm = new VMPedidoXEstado
            {
                IdEstado = 0,
                ListaPedidos = MisPedidosHelpers.GetListaPedidos(),
            };



            ViewBag.IdEstado = new SelectList(MisPedidosHelpers.GetListaEstados(), "IdEstado", "Descripcion");


            return View(vm);
        }

        [HttpPost]
        public ActionResult ReporteListaPedidos(VMPedidoXEstado vm, FormCollection form)
        {
            var idEstado = Convert.ToInt32(form["IdEstado"]);

            var vmodel = new VMPedidoXEstado
            {
                IdEstado = idEstado,
                ListaPedidos = MisPedidosHelpers.GetListaPedidosXEstados(idEstado),
            };

            ViewBag.IdEstado = new SelectList(MisPedidosHelpers.GetListaEstados(), "IdEstado", "Descripcion");

            return View(vmodel);
        }

        public ActionResult DescargarReporteLista(VMPedidoXEstado vm, FormCollection form)
        {
            var idEstado = Int32.Parse(form["IdEstado"]);



            try
            {
                var pedidos = MisPedidosHelpers.GetListaPedidosXEstados(idEstado);
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/ReporteListaPedidos.rpt");
                rptH.Load();

                rptH.SetDataSource(pedidos);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


            }
            catch (Exception ex)
            {

                throw;
            }




        }


        public ActionResult ReporteGraficoParcial()
        {


            return View();
        }
        public ActionResult DescargarGraficoParcial()
        {

            try
            {
                var pedidos = MisPedidosHelpers.GetListaPedidosDespachadosXCliente();
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/GraficoParcial2.rpt");

                rptH.Load();

                rptH.SetDataSource(pedidos);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult Porcentajes()
        {


            return View();
        }
        public ActionResult DescargarGraficoPorcentajes()
        {
            try
            {
                var pedidos = MisPedidosHelpers.GetListaPedidosYPorcentajes();
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/GraficoPorcentajes.rpt");
                rptH.Load();

                rptH.SetDataSource(pedidos);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public ActionResult ReporteClienteYPedidos()
        {

            VMClientePedidoReporte vm = new VMClientePedidoReporte();
            vm.IdUsuario = 0;
            vm.ListaUsuarios = MisPedidosHelpers.GetListaUsuarios();


            return View(vm);
        }
        [HttpPost]
        public ActionResult ReporteClienteYPedidos(FormCollection form, VMClientePedidoReporte vm)
        {

            var idUsuario = Int32.Parse(form["IdUsuario"]);

            var vmodel = new VMClientePedidoReporte
            {
                IdUsuario = idUsuario,
                ListaClientePedidos = MisPedidosHelpers.GetClientesXPedidos(idUsuario),
            };



            return View(vmodel);
        }
        public ActionResult DescargarReporteXCliente(VMPedidoXEstado vm, FormCollection form)
        {


            var idUsuario = Int32.Parse(form["IdUsuario"]);



            try
            {
                var pedidos = MisPedidosHelpers.GetClientesXPedidos(idUsuario);
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/ReporteClientesXPedidos.rpt");
                rptH.Load();

                rptH.SetDataSource(pedidos);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public ActionResult ReporteGraficoTotal()
        {
            return View();
        }
        public ActionResult DescargarGraficoTotal2()
        {
            try
            {
                var pedidos = MisPedidosHelpers.GetListaClientesYTotales();
                var rptH = new ReportClass();
                rptH.FileName = Server.MapPath("/Reportes/ReporteGraficoTotal.rpt");

                rptH.Load();

                rptH.SetDataSource(pedidos);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                Stream stream = rptH.ExportToStream(ExportFormatType.PortableDocFormat);
                rptH.Dispose();
                rptH.Close();
                return new FileStreamResult(stream, "application/pdf");


            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
