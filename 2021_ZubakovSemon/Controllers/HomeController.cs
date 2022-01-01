using _2021_ZubakovSemon.Models;
using FastReport.Web;
using MathLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace _2021_ZubakovSemon.Controllers
{
    public class HomeController : Controller
    {
        readonly ILiteDbContext _myContext;

        readonly IGeneratePdf _generatePdf;
        readonly IHostEnvironment _hostingEnvironment;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHostEnvironment hostingEnvironment, ILiteDbContext myContext)
        {
            _logger = logger;
            _myContext = myContext;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult InputNew()
        {
            StenkaMathLibNew _stenka = new StenkaMathLibNew();

            #region --- Задать исходные данные по умолчанию

            _stenka.H0 = 3;
            _stenka.TempMat = 700;
            _stenka.T0Gasa = 40;
            _stenka.VoGas = 0.5;
            _stenka.asred = 1.33;
            _stenka.Cm = 1.7;
            _stenka.Gm = 1.49;
            _stenka.av = 2450;
            _stenka.D = 2.1;

            DataInputNewModel _DataInput = new DataInputNewModel();

            _DataInput.H0 = _stenka.H0;
            _DataInput.TempMat = _stenka.TempMat;
            _DataInput.T0Gasa = _stenka.T0Gasa;
            _DataInput.VoGas = _stenka.VoGas;
            _DataInput.asred = _stenka.asred;
            _DataInput.Cm = _stenka.Cm;
            _DataInput.Gm = _stenka.Gm;
            _DataInput.av = _stenka.av;
            _DataInput.D = _stenka.D;
            _DataInput.SloyM = _stenka.SloyM;


            #endregion --- Задать исходные данные по умолчанию


            return View(_DataInput);
        }

        [HttpPost]
        public IActionResult DataInputNew(DataInputNewModel DataInput)
        {
            HttpContext.Session.Set<DataInputNewModel>("IntputNew", DataInput);

            if (DataInput.ActionSave=="true")
            {
                return SaveData(DataInput);
            }

            DataOutputNewModel Raschet = new DataOutputNewModel(DataInput);
            Raschet.Raschet();

            HttpContext.Session.Set<DataOutputNewModel>("OutputNew", Raschet);


            ViewBag.SuccessSave = false;

            return View("OutputNew", Raschet);
        }

        public IActionResult GraphicNew()
        {
            if (HttpContext.Session.Keys.Contains("OutputNew"))
            {
                DataOutputNewModel Raschet = HttpContext.Session.Get<DataOutputNewModel>("OutputNew");

                GraphicNewModel model = new GraphicNewModel(Raschet.StenkaData);
                return View(model);
            }
            return View();
        }

        [HttpGet]
        public IActionResult InputFromStore(int id)
        {
            var data = _myContext.GetRaschet(id);
            data.ActionSave = "false";
            return View("InputNew", data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public ActionResult SaveData(DataInputNewModel model)
        {
            try
            {
                DataInputNewModel Data = HttpContext.Session.Get<DataInputNewModel>("InputNew");
                
                if(Data==null)
                {
                    Data = model;
                    _myContext.SaveRaschet(new DataInputNewModel[] { Data });
                    HttpContext.Session.Set<DataInputNewModel>("InputNew", Data);
                    Data.IsSaved = true;
                }
                else if (!Data.IsSaved)
                {
                    _myContext.SaveRaschet(new DataInputNewModel[] { Data });
                    Data.IsSaved = true;
                }
                return View("InputNew", Data);
            }
            catch (Exception er)
            {
                return BadRequest(); 
            }
        }
            
        [HttpGet]
        public ActionResult Archive(string email)
        {
            try
            {
                ArvicheOutputs data = new ArvicheOutputs();
                data.DataSet = _myContext.GetRaschet(email);
                return View(data);
            }
            catch (Exception er)
            {
                return BadRequest();
            }
        }

    }
}
