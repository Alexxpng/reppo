
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductoController : Controller
    {
        private List<ProductoModel> ObtenerProductos()
        {
            if (Session["productos"] == null)
            {
                List<ProductoModel> productos = new List<ProductoModel>();

                productos.Add(new ProductoModel
                {
                    Id = 1,
                    Nombre = "Laptop",
                    Descripcion = "Laptop para trabajo y estudio",
                    Precio = 2500.00m,
                    Stock = 10
                });

                productos.Add(new ProductoModel
                {
                    Id = 2,
                    Nombre = "Mouse",
                    Descripcion = "Mouse optico USB",
                    Precio = 45.50m,
                    Stock = 30
                });

                productos.Add(new ProductoModel
                {
                    Id = 3,
                    Nombre = "Teclado",
                    Descripcion = "Teclado USB en espanol",
                    Precio = 80.00m,
                    Stock = 15
                });

                Session["productos"] = productos;
            }

            return (List<ProductoModel>)Session["productos"];
        }

        public ActionResult Index()
        {
            return View(ObtenerProductos());
        }

        public ActionResult Details(int id)
        {
            ProductoModel producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoModel model)
        {
            if (ModelState.IsValid)
            {
                List<ProductoModel> productos = ObtenerProductos();

                model.Id = productos.Count == 0
                    ? 1
                    : productos.Max(p => p.Id) + 1;

                productos.Add(model);
                Session["productos"] = productos;

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            ProductoModel producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoModel model)
        {
            if (ModelState.IsValid)
            {
                List<ProductoModel> productos = ObtenerProductos();

                ProductoModel producto = productos
                    .FirstOrDefault(p => p.Id == model.Id);

                if (producto == null)
                    return HttpNotFound();

                producto.Nombre = model.Nombre;
                producto.Descripcion = model.Descripcion;
                producto.Precio = model.Precio;
                producto.Stock = model.Stock;

                Session["productos"] = productos;

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            ProductoModel producto = ObtenerProductos()
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return HttpNotFound();

            return View(producto);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<ProductoModel> productos = ObtenerProductos();

            ProductoModel producto = productos
                .FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return HttpNotFound();

            productos.Remove(producto);
            Session["productos"] = productos;

            return RedirectToAction("Index");
        }
    }
}
