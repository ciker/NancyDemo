﻿using Nancy;
using Nancy.Diagnostics;
using Nancy.Authentication.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Nancy.Conventions;

namespace NancyDemo
{
    public class CustomBootstrapper:DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));//Basic认证添加
            pipelines.OnError += Error;
        }

        private dynamic Error(NancyContext context, Exception ex) 
        {
            //记录异常 ex
            return ex.Message;
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
            //添加文件夹
            conventions.StaticContentsConventions.AddDirectory("file","uploads");
            //添加文件
            conventions.StaticContentsConventions.AddFile("index.html", "1.html");
        }
    }
}
