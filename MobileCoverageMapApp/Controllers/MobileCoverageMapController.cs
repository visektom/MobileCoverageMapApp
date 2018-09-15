using MobileCoverageMapApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace MobileCoverageMapApp.Controllers
{
    public class MobileCoverageMapController : Controller
    {
        // GET: MobileCoverageMap
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Index3()
        {
            return View();
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult Index5()
        {
            return View();
        }

        public JsonResult GetData(string telcoOperator)
        {
            return Json(ImportFromCSV(telcoOperator), JsonRequestBehavior.AllowGet);
        }

        public void DownloadData()
        {
            string remoteUri = "http://data.ctu.cz/dataset/pokryti-dalnic-mobilnim-signalem/resource/4d5c2a16-a700-4336-b3fa-48701aef7add#";
            using (HttpClient client = new HttpClient())
            {
                bool parseSuccess = Uri.TryCreate(remoteUri, UriKind.Absolute, out Uri result);
                client.BaseAddress = result;
            }
        }

        public void ImportFromExcel()
        {
            MobileCoverageMapAppViewModel model = new MobileCoverageMapAppViewModel();
        }

        public MobileCoverageMapAppViewModel ImportFromCSV(string telcoOperator)
        {
            string filePath = @"c:\Users\tomas\Documents\Visual Studio 2017\Projects\MobileCoverageMapApp\MobileCoverageMapApp\App_Data\pokryti-dalnic-mobilnim-signalem-d0.csv";
            MobileCoverageMapAppViewModel model = new MobileCoverageMapAppViewModel();
            model.Measurements = new List<Measurement>();

            using (var reader = new StreamReader(filePath))
            {
                string firstLine = reader.ReadLine();
                string[] headers = firstLine.Split(';');

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');

                    bool timeParseSuccess = DateTime.TryParse(values[0], out DateTime time);
                    bool latParseSuccess = float.TryParse(values[1], out float lat);
                    bool lngParseSuccess = float.TryParse(values[2], out float lng);
                    bool RSRPParseSuccess;
                    float RSRP;

                    switch (telcoOperator)
                    {
                        case "O2":
                            RSRPParseSuccess = float.TryParse(values[5], out RSRP);

                            break;
                        case "T-Mobile":
                            RSRPParseSuccess = float.TryParse(values[3], out RSRP);
                            break;
                        case "Vodafone":
                            RSRPParseSuccess = float.TryParse(values[7], out RSRP);
                            break;
                        default:
                            RSRP = default(float);
                            break;
                    }

                    model.Measurements.Add(new Measurement {
                        Time = time,
                        Lat = lat,
                        Lng = lng,
                        RSRP = RSRP,
                        ContentString = lat.ToString() + ", " + lng.ToString() + "<br \\>" + "<b>RSRP: </b>" + RSRP.ToString()
                    });
                }
            }

            return model;
        }
    }
}