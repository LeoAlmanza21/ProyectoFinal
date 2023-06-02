using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Web;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WebApplication1.Controllers
{


    [ApiController]
    [Route("[Controller]")]
    public class productController : Controller
    {
        private readonly NorthwindContext contexto;



        public productController(NorthwindContext context)

        {
            contexto = context;
        }

        [HttpGet]
        [Route("GetActivos")]
        public IEnumerable<Product> GetActivos()
        {
            /*Product p = new Product();
            p.ProductName = "X";
            p.UnitPrice = 20;
            Product p1 = new Product()
            {
                ProductName = "X",
                UnitPrice = 20
            };*/

            IEnumerable<Product> lista =
                 from prod in contexto.Products
                 where prod.Discontinued == false
                 select new Product()
                 {
                     ProductName = prod.ProductName,
                     UnitPrice = prod.UnitPrice
                 };
            return lista;
        }





        /////////////////////////////////////////////////////PRIMERA CONSULTA/////////////////////////////////

        [HttpGet]
        [Route("GetSalesPeriod/{startDate}/{finishDate}")]
        public IEnumerable<SalesPeriod> GetSalesPeriod(String startDate, String finishDate)
        {
            DateTime inicioFecha = DateTime.Parse(startDate);
            DateTime finFecha = DateTime.Parse(finishDate);
            //return contexto.Orders.Where
            //    (
            //        orders => orders.OrderDate.Value.Month >= startDate.Month &&
            //                  orders.OrderDate.Value.Month <= finishDate.Month
            //    )
            IEnumerable<SalesPeriod> list =
                from orders in contexto.Orders
                join orderDetails in contexto.Orderdetails on orders.OrderId equals orderDetails.OrderId
                join products in contexto.Products on orderDetails.ProductId equals products.ProductId
                where orders.OrderDate.Value.Month >= inicioFecha.Month &&
                      orders.OrderDate.Value.Month <= finFecha.Month
                //orderby orderDetails.UnitPrice * orderDetails.Quantity descending
                group new { products, orderDetails } by new { orderDetails.OrderId } into p
                orderby p.Sum(p => p.orderDetails.UnitPrice * p.orderDetails.Quantity) descending

                select new SalesPeriod()
                {
                    productName = p.Select(p => p.products.ProductName).First(),
                    sale = p.Sum(p => p.orderDetails.UnitPrice * p.orderDetails.Quantity)
                };
            //select new SalesPeriod() { 
            //    productName = products.ProductName,
            //    sale = orderDetails.UnitPrice * orderDetails.Quantity                    

            //};

            return list.Take(10);
        }




        //////////////////////////////////////////////SEGUNDA CONSULTA////////////////////////////////////////
        //////////////////////////OBTENER TODAS LAS VENTAS DE UN PRODUCTO EN UN PERIODO DE TIEMPO
       

        //[HttpGet("{name}/{startDate}/{finishDate}")]
        [HttpGet]
        //[Route("GetSalesFromProduct/{name}")]
        [Route("GetSalesFromProduct/{name}/{startDate}/{finishDate}")]
        //[Route("GetSalesFromProduct/{name}")]
        //[Route("GetSalesFromProduct")]
        //mivariable = Request.QueryString["name"].ToString();

        public IEnumerable<Object> GetSalesFromProduct( String name, String startDate, String finishDate)
        {
            DateTime inicioFecha = DateTime.Parse(startDate);
            DateTime finFecha = DateTime.Parse(finishDate);

            IEnumerable<Object> list =
             contexto.Products

                .Where(p => p.ProductName == name)
                .Join(
                    contexto.Orderdetails,
                    p => p.ProductId,
                    md => md.ProductId,
                    (p, md) => new
                    {
                        Producto = p.ProductId,
                        Movimiento = md.OrderId,
                        Nombre = p.ProductName,
                        Cantidad = md.Quantity
                    }
                )
                .Join(
                    contexto.Orders,
                    md => md.Movimiento,
                    m => m.OrderId,
                    (md, m) => new
                    {
                        Product = md.Producto,
                        Name = md.Nombre,
                        Quantity = md.Cantidad,
                        OrderD = m.OrderDate,

                    }
                )
                .Where(m => m.OrderD >= inicioFecha /*DateTime.Parse("1997-01-01 00:00:00")*/
                    && m.OrderD <= finFecha /*DateTime.Parse("1997-12-31 00:00:00")*/
                );

            return list;


        }




        ////////////////////////////////////////////////TERCERA CONSULTA/////////////////////////////////
        /////////////////////////////////////////OBTENER LAS VENTAS POR TRIMESTRE 

        [HttpGet]
        [Route("GetSalesByTri/{year}/{tri}")]
        public IEnumerable<Object> GetSalesByTri(int year, int tri)
        {
            int limSup = tri * 3;
            int limInf = limSup - 2;

            IEnumerable<Object> list =

             contexto.Products
            .Join(
                    contexto.Orderdetails,
                    p => p.ProductId,
                    md => md.ProductId,
                    (p, md) => new
                    {
                        Producto = p.ProductId,
                        Name = p.ProductName,
                        Movimiento = md.OrderId,
                        Cantidad = md.Quantity,
                    }
            )
            .Join(
                    contexto.Orders,
                    md => md.Movimiento,
                    m => m.OrderId,
                    (md, m) => new
                    {
                        Poducto = md.Producto,
                        Namee = md.Name,
                        Anio = m.OrderDate.Value.Year,
                        Mes = m.OrderDate.Value.Month,
                        Cantidad = md.Cantidad,
                        customer = m.Customer
                    }
                )
                .Where(m => m.Anio == 1997 && m.Mes <= limSup && m.Mes >= limInf
                )
                .GroupBy(e => new { e.Namee })
                .Select(e => new
                {
                    
                    customer = e.Select(p => p.customer.CustomerId),
                    company = e.Select(p => p.customer.CompanyName),
                    producto = e.Key.Namee,
                    cantidad = e.Sum(l => l.Cantidad),
                    
                    

                })
                .OrderBy(e => e.cantidad)
                .Take(10)
                .AsEnumerable();
            ;

            return list;
        }







        [HttpGet]
        [Route("GetActivos2")]
        public IEnumerable<Object> GetActivos2()
        {
            IEnumerable<Object> lista =
                 //from Products prod
                 from prod in contexto.Products
                     //join Categories cat on prod.CategoryId=cat.CategoryId
                 join cat in contexto.Categories on prod.CategoryId equals cat.CategoryId
                 where prod.Discontinued == false
                 select new
                 {
                     Name = prod.ProductName,
                     Price = prod.UnitPrice,
                     Category = cat.CategoryName
                     //Categoria=prod.Category.CategoryName
                 };

            return lista;


        }


        [HttpGet]
        [Route("GetActivos2Metodos")]
        public IEnumerable<Object> GetActivos2Metodos()
        {
            //"Programacion Web".Replace("r","R").Replace(" ","-").Replace("r","R")
            IEnumerable<Object> lista =
                 contexto.Products
                 .Where(producto => producto.Discontinued == false)
                 .Join(contexto.Categories,
                    producto => producto.CategoryId,
                    categoria => categoria.CategoryId,
                    (p, c) => new {
                        Nombre = p.ProductName,
                        Precio = p.UnitPrice,
                        Categoria = c.CategoryName,
                        Unidades = p.UnitsInStock
                    })
                 .Where(x => x.Unidades > 0);


            return lista;
        }




    }
}
