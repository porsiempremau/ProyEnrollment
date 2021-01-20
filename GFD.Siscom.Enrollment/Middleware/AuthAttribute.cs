using GFD.Siscom.Enrollment.Utilities.Auth;
using GFD.Siscom.Enrollment.Utilities.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GFD.Siscom.Enrollment.Middleware
{
    public class AuthAttribute : ActionFilterAttribute
    {
        private string Roles;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool Is_Ajax = !string.IsNullOrEmpty(filterContext.HttpContext.Request.ContentType);

            if (filterContext.HttpContext.Session != null)
            {
                if (Auth.Login == null)
                    if (!Is_Ajax)
                    {
                        filterContext.Result = new RedirectResult("/login/Ups!!, No ha iniciado sesión, favor de ingresar sus credenciales");                        
                    }
                    else
                    {
                        var result = new ObjectResult(new { status = 401, message = "Unauthorized" });
                        result.StatusCode = 401;

                        filterContext.Result = result;
                    }

                else
                {
                    if (!string.IsNullOrEmpty(this.Roles) && !Auth.Login.HasRoles(this.Roles))
                    {
                        if (!Is_Ajax)
                        {
                            filterContext.Result = new RedirectToActionResult("Index", "Error", new ErrorViewModel() { Message = "No tiene permisos Para acceder a esta ruta", StatusCode = 403, TitleError = "Prohibido" });
                        }
                        else
                        {
                            var result = new ObjectResult(new { status = 403, message = "Forbidden" });
                            result.StatusCode = 403;

                            filterContext.Result = result;
                        }

                    }
                }
            }
        }

        public AuthAttribute(string roles)
        {
            this.Roles = roles;
        }
        public AuthAttribute()
        {
            this.Roles = null;
        }
    }

    internal class RoleAttribute : ActionFilterAttribute
    {
        private string Roles;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool Is_Ajax = !string.IsNullOrEmpty(filterContext.HttpContext.Request.ContentType);

            if (!string.IsNullOrEmpty(this.Roles) && !Auth.Login.HasRoles(this.Roles))
            {
                if (!Is_Ajax)
                {
                    filterContext.Result = new RedirectToActionResult("Index", "Error", new ErrorViewModel() { Message = "No tiene permisos Para acceder a esta ruta", StatusCode = 403, TitleError = "Prohibido" });
                }
                else
                {
                    var result = new ObjectResult(new { status = 403, message = "Forbidden" });
                    result.StatusCode = 403;

                    filterContext.Result = result;
                }
            }
        }

        public RoleAttribute(string roles)
        {
            this.Roles = roles;
        }
    }

    internal class AnonymousAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Auth.Login != null)
            {
                filterContext.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}
