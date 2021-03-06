using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using POS.DataAccess.Repository.IRepository;
using POS.Models.Models;
using POS.Models.Models.Authentication;
using POS.ViewModels;


namespace POS.Controllers
{
    public class ClientController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ClientController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }






        [Authorize(Roles = UserRoles.SYSADMIN)]
        [HttpGet]
        [Route("~/client/exists")]
        public  IActionResult exists()
        {

            Client client = _unitOfWork.Client.GetFirstOrDefault();
            if(client == null)
            {
                return Json(new { success = false });
            }
            else
            {

                User user = _unitOfWork.Client.GetFirstAdmin(client.code);
                ClientVM clientVM = _mapper.Map<ClientVM>(client);
                if(user!= null) {

                    clientVM.admin_firstname = user.first_name;
                    clientVM.admin_lastname = user.last_name;
                    clientVM.admin_mobile = user.phone;
                    clientVM.admin_email = user.email;
                    clientVM.admin_id = user.user_id;
                    
                }

                return Json(new { success = true, client = clientVM });
            }

        }



        [Authorize(Roles = UserRoles.SYSADMIN )]
        [HttpPost]
        [Route("~/client/add")]
        public async Task<IActionResult> Upsert(ClientVM clientVM)
        {
            try {
                if (ModelState.IsValid)
                {


                    Client client = _mapper.Map<Client>(clientVM);

                    if (client.id == 0)
                    {
                        client.code = _unitOfWork.Client.GetClientCode();
                    }

                    string webRootPath = _hostEnvironment.WebRootPath;
                    string absImagePath;
                    var files = HttpContext.Request.Form.Files;
                    if (files.Count > 0)
                    {
                        var logo = files.FirstOrDefault(s => s.Name == "logo_file");//gets the file

                        string fileName = client.code;//sets the file and folder name as the id given
                        string path = @"images\client\" + client.code + @"\logo\";
                        var uploads = Path.Combine(webRootPath, path);//creates the directory

                        if (!Directory.Exists(uploads))//if the directory doesnt alrready exists then it creates the directory
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        var extenstion = Path.GetExtension(logo.FileName);// gets the file extension

                        if (client.logo != null)// will only be accessed during update and if there is a photo
                        {
                            //this is an edit and we need to remove old image
                            var imagePath = Path.Combine(webRootPath, client.logo.TrimStart('\\'));

                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }


                        }
                        using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + extenstion), FileMode.Create))
                        {
                            logo.CopyTo(filesStreams);
                        }

                        absImagePath = "/images/client/" + fileName + "/logo/";
                        client.logo = absImagePath + fileName + extenstion;


                    }
                    else
                    {
                       
                        if (client.id != 0)
                        {
                         

                        }
                    }



                    if (client.id == 0)
                    {

                        _unitOfWork.Client.Add(client);

                        using (var Hclient = new HttpClient())
                        {
                            Hclient.BaseAddress = new Uri(SD.SERVER);
                            Hclient.DefaultRequestHeaders.Accept.Clear();
                            Hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string token = await HttpContext.GetTokenAsync("access_token");
                            Hclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            Registration clientO = new Registration()
                            {

                                user_type = "ADMIN",
                                first_name = clientVM.admin_firstname,
                                last_name = clientVM.admin_lastname,
                                phone = clientVM.admin_mobile,
                                email = clientVM.email,
                                password = clientVM.password,
                                status = true,
                                client_code = client.code
                            };
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(clientO);
                            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                            var response = Hclient.PostAsync("/Pos/Registration/", content).Result;
                            // var response = client.GetAsync("test/second").Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                //  Client modelObject = await JsonSerializer.DeserializeAsync<Client>(await response.Content.ReadAsStreamAsync());
                                var x = await JsonSerializer.DeserializeAsync<ResponseN>(await response.Content.ReadAsStreamAsync());
                                if (!x.success)
                                {
                                    return Json(new { success = false, message = x.message });
                                }
                            }

                        }

                    }
                    else
                    {
                        try { _unitOfWork.Client.Update(client); }
                        catch (Exception e) { return Json(new { success = false, message = e.Message }); }



                        using (var Hclient = new HttpClient())
                        {
                            Hclient.BaseAddress = new Uri(SD.SERVER);
                            Hclient.DefaultRequestHeaders.Accept.Clear();
                            Hclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string token = await HttpContext.GetTokenAsync("access_token");
                            Hclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            Registration clientO = new Registration()
                            {

                                user_type = "ADMIN",
                                first_name = clientVM.admin_firstname,
                                last_name = clientVM.admin_lastname,
                                phone = clientVM.admin_mobile,
                                email = clientVM.email,
                                password = clientVM.password,
                                client_code = client.code,
                                user_id = clientVM.admin_id
                            };
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(clientO);
                            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                            var response = Hclient.PostAsync("/Pos/Admin/Update/", content).Result;
                            // var response = client.GetAsync("test/second").Result;
                            if (response.IsSuccessStatusCode)
                            {
                                string responseString = response.Content.ReadAsStringAsync().Result;
                                //  Client modelObject = await JsonSerializer.DeserializeAsync<Client>(await response.Content.ReadAsStreamAsync());
                                var x = await JsonSerializer.DeserializeAsync<ResponseN>(await response.Content.ReadAsStreamAsync());
                                if (!x.success)
                                {
                                    return Json(new { success = false, message = x.message });
                                }
                            }

                        }


                    }






                    _unitOfWork.Save();
                    return Json(new { success = true, message = client });
                }
                else
                {
                    return Json(new { success = false, message = "Add failed!!" });
                }

            }
            catch(Exception e)
            {
                return Json(new { success = false, message = e.Message });
            }
           

        }


        public class ResponseN
        {
            public bool success { get; set; }
            public dynamic message { get; set; }
        }

    }
}
