using Adminstration.Parent.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using IdentityModel.Client;
using System;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using IdentityServer4;
using IdentityServer4.Models;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Adminstration.Parent.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger)
        {

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            }
            return View();
        }

        [Authorize(Roles = "Parent")]
        public async Task<IActionResult> Privacy()
        {
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            Console.WriteLine(idToken);
            Console.WriteLine(accessToken);
            //Console.WriteLine(JsonConvert.SerializeObject(HttpContext.User));
            //HttpContext.SignOutAsync("Cookies");
            return View();
            //var client = new HttpClient();
            //var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44350");
            //if (disco.IsError)
            //{
            //    Console.WriteLine(disco.Error);
            //}
            //var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,
            //    ClientId = "Administration-Parent-Client",
            //    ClientSecret = "49C1A7E1-0C794A89A3D6A37998FB86B0",
            //    GrantType = "hybrid",

            //    Scope = "parent.write openid profile"
            //});

            //if (tokenResponse.IsError)
            //{
            //    Console.WriteLine(tokenResponse.Error);
            //}

            //Console.WriteLine(tokenResponse.Json);
            //Console.WriteLine("\n\n");

            //// call api
            //var apiClient = new HttpClient();
            //apiClient.SetBearerToken(tokenResponse.AccessToken);

            ////var response = await apiClient.GetAsync("https://localhost:6001/identity");
            ////if (!response.IsSuccessStatusCode)
            ////{
            ////    Console.WriteLine(response.StatusCode);
            ////}
            ////else
            ////{
            //    //var content = await response.Content.ReadAsStringAsync();
            //    //Console.WriteLine(tokenResponse.Json);
            ////}
            //var responseInfo = await client.GetUserInfoAsync(new UserInfoRequest
            //{
            //    Address = disco.UserInfoEndpoint,
            //    Token = tokenResponse.AccessToken,

            //});
            //if (responseInfo.IsError)
            //{
            //    //await HttpContext.SignOutAsync("oidc");
            //}
            //Console.WriteLine(responseInfo.Raw);

            //string result = JsonSerializer.Serialize(HttpContext.User.Claims.FirstOrDefault().Properties);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}