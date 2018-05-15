using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using My_Contact_Details;
using My_Contact_Details.Controllers;
using My_Contact_Details.Models;

namespace My_Contact_Details.Tests.Controllers
{

    [TestClass]
    public class AccountControllerTest
    {

        [TestMethod]
        public void ChangePassword_Get_ReturnsView()
        {
            // 排列
            AccountController controller = GetAccountController();

            // 作用
            ActionResult result = controller.ChangePassword();

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(10, ((ViewResult)result).ViewData["PasswordLength"]);
        }

        [TestMethod]
        public void ChangePassword_Post_ReturnsRedirectOnSuccess()
        {
            // 排列
            AccountController controller = GetAccountController();
            ChangePasswordModel model = new ChangePasswordModel()
            {
                OldPassword = "goodOldPassword",
                NewPassword = "goodNewPassword",
                ConfirmPassword = "goodNewPassword"
            };

            // 作用
            ActionResult result = controller.ChangePassword(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("ChangePasswordSuccess", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void ChangePassword_Post_ReturnsViewIfChangePasswordFails()
        {
            // 排列
            AccountController controller = GetAccountController();
            ChangePasswordModel model = new ChangePasswordModel()
            {
                OldPassword = "goodOldPassword",
                NewPassword = "badNewPassword",
                ConfirmPassword = "badNewPassword"
            };

            // 作用
            ActionResult result = controller.ChangePassword(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual("目前密碼不正確或是新密碼無效。", controller.ModelState[""].Errors[0].ErrorMessage);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        [TestMethod]
        public void ChangePassword_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // 排列
            AccountController controller = GetAccountController();
            ChangePasswordModel model = new ChangePasswordModel()
            {
                OldPassword = "goodOldPassword",
                NewPassword = "goodNewPassword",
                ConfirmPassword = "goodNewPassword"
            };
            controller.ModelState.AddModelError("", "虛擬錯誤訊息。");

            // 作用
            ActionResult result = controller.ChangePassword(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        [TestMethod]
        public void ChangePasswordSuccess_ReturnsView()
        {
            // 排列
            AccountController controller = GetAccountController();

            // 作用
            ActionResult result = controller.ChangePasswordSuccess();

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void LogOff_LogsOutAndRedirects()
        {
            // 排列
            AccountController controller = GetAccountController();

            // 作用
            ActionResult result = controller.LogOff();

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignOut_WasCalled);
        }

        [TestMethod]
        public void LogOn_Get_ReturnsView()
        {
            // 排列
            AccountController controller = GetAccountController();

            // 作用
            ActionResult result = controller.LogOn();

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void LogOn_Post_ReturnsRedirectOnSuccess_WithoutReturnUrl()
        {
            // 排列
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                RememberMe = false
            };

            // 作用
            ActionResult result = controller.LogOn(model, null);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
            Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        }

        [TestMethod]
        public void LogOn_Post_ReturnsRedirectOnSuccess_WithReturnUrl()
        {
            // 排列
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                RememberMe = false
            };

            // 作用
            ActionResult result = controller.LogOn(model, "/someUrl");

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            RedirectResult redirectResult = (RedirectResult)result;
            Assert.AreEqual("/someUrl", redirectResult.Url);
            Assert.IsTrue(((MockFormsAuthenticationService)controller.FormsService).SignIn_WasCalled);
        }

        [TestMethod]
        public void LogOn_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // 排列
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                UserName = "someUser",
                Password = "goodPassword",
                RememberMe = false
            };
            controller.ModelState.AddModelError("", "虛擬錯誤訊息。");

            // 作用
            ActionResult result = controller.LogOn(model, null);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
        }

        [TestMethod]
        public void LogOn_Post_ReturnsViewIfValidateUserFails()
        {
            // 排列
            AccountController controller = GetAccountController();
            LogOnModel model = new LogOnModel()
            {
                UserName = "someUser",
                Password = "badPassword",
                RememberMe = false
            };

            // 作用
            ActionResult result = controller.LogOn(model, null);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual("所提供的使用者名稱或密碼不正確。", controller.ModelState[""].Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Register_Get_ReturnsView()
        {
            // 排列
            AccountController controller = GetAccountController();

            // 作用
            ActionResult result = controller.Register();

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(10, ((ViewResult)result).ViewData["PasswordLength"]);
        }

        [TestMethod]
        public void Register_Post_ReturnsRedirectOnSuccess()
        {
            // 排列
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                UserName = "someUser",
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // 作用
            ActionResult result = controller.Register(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult redirectResult = (RedirectToRouteResult)result;
            Assert.AreEqual("Home", redirectResult.RouteValues["controller"]);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void Register_Post_ReturnsViewIfRegistrationFails()
        {
            // 排列
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                UserName = "duplicateUser",
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };

            // 作用
            ActionResult result = controller.Register(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual("使用者名稱已經存在。請輸入不同的使用者名稱。", controller.ModelState[""].Errors[0].ErrorMessage);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        [TestMethod]
        public void Register_Post_ReturnsViewIfModelStateIsInvalid()
        {
            // 排列
            AccountController controller = GetAccountController();
            RegisterModel model = new RegisterModel()
            {
                UserName = "someUser",
                Email = "goodEmail",
                Password = "goodPassword",
                ConfirmPassword = "goodPassword"
            };
            controller.ModelState.AddModelError("", "虛擬錯誤訊息。");

            // 作用
            ActionResult result = controller.Register(model);

            // 判斷提示
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual(model, viewResult.ViewData.Model);
            Assert.AreEqual(10, viewResult.ViewData["PasswordLength"]);
        }

        private static AccountController GetAccountController()
        {
            AccountController controller = new AccountController()
            {
                FormsService = new MockFormsAuthenticationService(),
                MembershipService = new MockMembershipService()
            };
            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }

        private class MockFormsAuthenticationService : IFormsAuthenticationService
        {
            public bool SignIn_WasCalled;
            public bool SignOut_WasCalled;

            public void SignIn(string userName, bool createPersistentCookie)
            {
                // 確認引數是不是我們所需要的
                Assert.AreEqual("someUser", userName);
                Assert.IsFalse(createPersistentCookie);

                SignIn_WasCalled = true;
            }

            public void SignOut()
            {
                SignOut_WasCalled = true;
            }
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someUser"), null /* roles */);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }
        }

        private class MockMembershipService : IMembershipService
        {
            public int MinPasswordLength
            {
                get { return 10; }
            }

            public bool ValidateUser(string userName, string password)
            {
                return (userName == "someUser" && password == "goodPassword");
            }

            public MembershipCreateStatus CreateUser(string userName, string password, string email)
            {
                if (userName == "duplicateUser")
                {
                    return MembershipCreateStatus.DuplicateUserName;
                }

                // 確認值是不是我們所需要的
                Assert.AreEqual("goodPassword", password);
                Assert.AreEqual("goodEmail", email);

                return MembershipCreateStatus.Success;
            }

            public bool ChangePassword(string userName, string oldPassword, string newPassword)
            {
                return (userName == "someUser" && oldPassword == "goodOldPassword" && newPassword == "goodNewPassword");
            }
        }

    }
}
