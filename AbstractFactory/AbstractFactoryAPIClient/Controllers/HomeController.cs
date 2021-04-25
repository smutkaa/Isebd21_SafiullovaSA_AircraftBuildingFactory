using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryAPIClient.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AbstractFactoryAPIClient.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        public IActionResult Index()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<OrderViewModel>>($"api/main/getorders?clientId={Program.Client.Id}"));
        }
        [HttpGet]

        public IActionResult Mail()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessages?clientId={Program.Client.Id}"));
        }
        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Client);
        }
        [HttpPost]
        public void Privacy(string login, string password, string name)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(name))
            {
                
                Program.Client.ClientName = name;
                Program.Client.Login = login;
                Program.Client.Password = password;
                APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                {
                    Id = Program.Client.Id,
                    ClientName = name,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ??
                HttpContext.TraceIdentifier
            });
        }
        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Client = APIClient.GetRequest<ClientViewModel>($"api/client/login?login={login}&password={password}");
                if (Program.Client == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password)
            && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/client/register", new
                ClientBindingModel
                {
                    ClientName = fio,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Aircraft = APIClient.GetRequest<List<AircraftViewModel>>("api/main/getaircraftlist");
            return View();
        }
        [HttpPost]
        public void Create(int aircraft, int count, decimal sum)
        {
            if (count == 0 || sum == 0)
            {
                return;
            }
            APIClient.PostRequest("api/main/createorder", new CreateOrderBindingModel
            {
                ClientId = (int)Program.Client.Id,
                AircraftId = aircraft,
                Count = count,
                Sum = sum
            });
            Response.Redirect("Index");
        }
        [HttpPost]
        public decimal Calc(decimal count, int aircraft)
        {
            AircraftViewModel airc =
            APIClient.GetRequest<AircraftViewModel>($"api/main/getaircraft?aircraftId={aircraft}");
            return count * airc.Price;
        }

    }
}
