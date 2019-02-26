﻿using hjudgeWebHost.Data.Identity;
using hjudgeWebHost.Models;
using hjudgeWebHost.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace hjudgeWebHost.Middlewares
{
    public static class PrivilegeAuthentication
    {
        [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
        public class RequireSignedInAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!(context.HttpContext.RequestServices.GetService(typeof(UserManager<UserInfo>)) is UserManager<UserInfo> userManager) ||
                    !(context.HttpContext.RequestServices.GetService(typeof(SignInManager<UserInfo>)) is SignInManager<UserInfo> signInManager))
                    throw new NullReferenceException("UserManager<UserInfo> or SignInManager<UserInfo> is null");

                var userInfo = await userManager.GetUserAsync(context.HttpContext.User);

                if (!signInManager.IsSignedIn(context.HttpContext.User) || userInfo == null)
                {
                    context.Result = new JsonResult(new ResultModel
                    {
                        ErrorCode = ErrorDescription.NotSignedIn
                    });
                    return;
                }

                await next();
            }
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
        public class RequireAdminAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!(context.HttpContext.RequestServices.GetService(typeof(UserManager<UserInfo>)) is UserManager<UserInfo> userManager) ||
                    !(context.HttpContext.RequestServices.GetService(typeof(SignInManager<UserInfo>)) is SignInManager<UserInfo> signInManager))
                    throw new NullReferenceException("UserManager<UserInfo> or SignInManager<UserInfo> is null");

                var userInfo = await userManager.GetUserAsync(context.HttpContext.User);

                if (!PrivilegeHelper.IsAdmin(userInfo?.Privilege ?? 0))
                {
                    context.Result = new JsonResult(new ResultModel
                    {
                        ErrorCode = ErrorDescription.NoEnoughPrivilege
                    });
                    return;
                }

                await next();
            }
        }

        [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
        public class RequireTeacherAttribute : Attribute, IAsyncActionFilter
        {
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!(context.HttpContext.RequestServices.GetService(typeof(UserManager<UserInfo>)) is UserManager<UserInfo> userManager) ||
                    !(context.HttpContext.RequestServices.GetService(typeof(SignInManager<UserInfo>)) is SignInManager<UserInfo> signInManager))
                    throw new NullReferenceException("UserManager<UserInfo> or SignInManager<UserInfo> is null");

                var userInfo = await userManager.GetUserAsync(context.HttpContext.User);

                if (!PrivilegeHelper.IsTeacher(userInfo?.Privilege ?? 0))
                {
                    context.Result = new JsonResult(new ResultModel
                    {
                        ErrorCode = ErrorDescription.NoEnoughPrivilege
                    });
                    return;
                }

                await next();
            }
        }
    }
}