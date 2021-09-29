using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyyBlog.Areas.Admin.Controllers
{
    public class Tags : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
