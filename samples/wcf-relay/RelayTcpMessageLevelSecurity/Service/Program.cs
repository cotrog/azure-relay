//  
//  Copyright � Microsoft Corporation, All Rights Reserved
// 
//  Licensed under the Apache License, Version 2.0 (the "License"); 
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
// 
//  http://www.apache.org/licenses/LICENSE-2.0 
// 
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
//  OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION
//  ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A
//  PARTICULAR PURPOSE, MERCHANTABILITY OR NON-INFRINGEMENT.
// 
//  See the Apache License, Version 2.0 for the specific language
//  governing permissions and limitations under the License. 

namespace RelaySamples
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Threading.Tasks;
    using Microsoft.ServiceBus;

    class Program : ITcpListenerSample
    {
        public async Task Run(string listenAddress, string listenToken)
        {
            var host = new ServiceHost(typeof (EchoService), new Uri(listenAddress));
            host.Description.Behaviors.Remove<ServiceDebugBehavior>();
            foreach (var ep in host.Description.Endpoints)
            {
                ep.Behaviors.Add(new TransportClientEndpointBehavior(
                    TokenProvider.CreateSharedAccessSignatureTokenProvider(listenToken)));
            }

            host.Open();

            Console.WriteLine("Service address: " + listenAddress);
            Console.WriteLine("Press [Enter] to exit");
            Console.ReadLine();

            host.Close();
        }
    }
}