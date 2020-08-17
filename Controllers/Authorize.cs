//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc.Filters;
//using IAuthorizationFilter = Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace ManagementApp.Controllers {
//    public class Authorize: AuthorizeAttribute, IAuthorizationFilter {
//        public void OnAuthorization(AuthorizationContext filterContext) {
//            if (filterContext.ActionDescriptor.IsDefined(typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute), true)
//                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute), true))
//                {
//                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
//                return;
//                }

//            // Check for authorization
//            if (HttpContext.Session.GetString("username") == null)
//                {
//                filterContext.Result = new HttpUnauthorizedResult();
//                }
//            }
//        }
