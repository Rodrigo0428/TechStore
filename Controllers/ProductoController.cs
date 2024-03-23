    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TechStore.Models;

    namespace TechStore.Controllers
    {
        public class ProductoController : Controller
        {
            private readonly ILogger<ProductoController> _logger;

            public ProductoController(ILogger<ProductoController> logger)
            {
                _logger = logger;
            }

            public IActionResult Index()
            {
                return View();
            }   

            [HttpPost]
        public IActionResult AddProduct([Bind("Name, Description, Price")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                // Calcular el IGV del producto
                decimal igv = producto.Price * 0.18m;
                decimal totalPrice = producto.Price + igv;

                // Agregar el IGV a ViewData para mostrarlo en la vista
                ViewData["IGV"] = igv;
                ViewData["TotalPrice"] = totalPrice;

                // Puedes guardar el producto en la base de datos aquí si es necesario

                ViewData["Message"] = "Se ha agregado el producto correctamente."; // Agregar un mensaje de éxito

                return View("Index", producto); // Devuelve la vista "Index" con el modelo producto y los valores del IGV
            }

            // Si el modelo no es válido, vuelve a la misma vista para corregir los errores
            return View("Index", producto);
        }
        }
    }