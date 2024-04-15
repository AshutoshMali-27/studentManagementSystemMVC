using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using studentManagementSystemMVC.Models;
using System.Text;
using static System.Net.WebRequestMethods;

namespace studentManagementSystemMVC.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7020/api/StudentAPI/";
       // 

        private HttpClient client=new HttpClient();

        [HttpGet]
        public IActionResult Index()
        {
            List<ClsStudent> students = new List<ClsStudent>();
            url = url + "GetAllStudent";
            HttpResponseMessage response=client.GetAsync(url).Result;
            if(response.IsSuccessStatusCode)
            {

                string result=response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<ClsStudent>>(result);
                if(data != null)
                {
                    students = data;
                    
                }
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }

        public IActionResult Create(ClsStudent std)
        {
            url = url + "SetAllStudent";
            string data=JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(data,Encoding.UTF8,"application/json");
            HttpResponseMessage response=client.PostAsync(url, content).Result;
            if(response.IsSuccessStatusCode )
            {
                TempData["Insert_message"] = "Student Added";
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ClsStudent std = new ClsStudent();
          //  List<ClsStudent> std = new List<ClsStudent>();
            url = url + "getAllStudentsByID/";
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
        
            var data = JsonConvert.DeserializeObject<ClsStudent>(result);
          
            if (data != null)
            {
                std = data;

            }

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(ClsStudent std)
        {
            url = url + "updateStudentsByID/";
            string Data=JsonConvert.SerializeObject(std);
            StringContent content = new StringContent(Data, Encoding.UTF8, "application/json");
            HttpResponseMessage response=client.PutAsync(url+std.Id,content).Result;
            if (response.IsSuccessStatusCode )
            {
                TempData["updated_message"] = "Student updated...";
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            ClsStudent std = new ClsStudent();
            //  List<ClsStudent> std = new List<ClsStudent>();
            url = url + "getAllStudentsByID/";
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            var data = JsonConvert.DeserializeObject<ClsStudent>(result);

            if (data != null)
            {
                std = data;

            }

            return View(std);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            ClsStudent std = new ClsStudent();
            //  List<ClsStudent> std = new List<ClsStudent>();
            url = url + "getAllStudentsByID/";
            HttpResponseMessage response = client.GetAsync(url + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;

            var data = JsonConvert.DeserializeObject<ClsStudent>(result);

            if (data != null)
            {
                std = data;

            }

            return View(std);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            url = url + "DeleteStudentsByID/";


            HttpResponseMessage response = client.DeleteAsync(url + +id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Delete_message"] = "Student deleted...";
                return RedirectToAction("Index");
            }

            return View(id);
        }


    }
}
