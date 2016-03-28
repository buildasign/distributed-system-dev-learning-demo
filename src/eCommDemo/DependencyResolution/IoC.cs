// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System;
using eCommApi.Configuration;
using MassTransit;
using StructureMap;

namespace eCommDemo.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    //                    h.Username("guest");
                    //                    h.Password("guest");
                });
            });
            var container = new Container(x =>
            {
                x.For<IBusControl>().Use(bus);
                x.For<IEnterpriseBus>().Use<EnterpriseBus>();
                x.AddRegistry<DatabaseRegistry>();
            });

            return container;
        }
    }

    public interface IEnterpriseBus
    {
        void Publish<T>(T message) where T : class;
    }

    public class EnterpriseBus : IEnterpriseBus
    {
        private readonly IBusControl _busControl;

        public EnterpriseBus(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public void Publish<T>(T message) where T : class
        {
            _busControl.Publish<T>(message);
        }
    }
}