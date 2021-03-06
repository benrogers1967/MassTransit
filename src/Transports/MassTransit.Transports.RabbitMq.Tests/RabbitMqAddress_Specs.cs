// Copyright 2007-2011 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.Transports.RabbitMq.Tests
{
	using System;
	using Magnum.TestFramework;
	using NUnit.Framework;

	[Scenario]
	public class GivenAVHostAddress
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheHost()
		{
			_addr.ConnectionFactory.VirtualHost.ShouldEqual("thehost");
		}

		[Then]
		public void TheQueue()
		{
			_addr.Name.ShouldEqual("queue");
		}

		[Then]
		public void Rebuilt()
		{
//            _addr.RebuiltUri.ShouldEqual(new Uri("rabbitmq://guest:guest@some_server:5432/thehost/queue"));
		}
	}

	[Scenario]
	public class GivenAnAddressWithUnderscore
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheQueue()
		{
			_addr.Name.ShouldEqual("the_queue");
		}

		[Then]
		public void Rebuilt()
		{
			//          _addr.RebuiltUri.ShouldEqual(new Uri("rabbitmq://guest:guest@some_server:5432/thehost/the_queue"));
		}
	}

	[Scenario]
	public class GivenAnAddressWithPeriod
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/the.queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheQueue()
		{
			_addr.Name.ShouldEqual("the.queue");
		}

		[Then]
		public void Rebuilt()
		{
			//    _addr.RebuiltUri.ShouldEqual(new Uri("rabbitmq://guest:guest@some_server:5432/thehost/the.queue"));
		}
	}

	[Scenario]
	public class GivenAnAddressWithColon
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/the:queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheQueue()
		{
			_addr.Name.ShouldEqual("the:queue");
		}

		[Then]
		public void Rebuilt()
		{
			//    _addr.RebuiltUri.ShouldEqual(new Uri("rabbitmq://guest:guest@some_server:5432/thehost/the:queue"));
		}
	}


	[Scenario]
	public class GivenAnAddressWithSlash
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/the/queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
		}

		[Then]
		[ExpectedException(typeof (RabbitMqAddressException))]
		public void TheQueue()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}
	}

	[Scenario]
	public class GivenANonVHostAddress
	{
		public Uri Uri = new Uri("rabbitmq://some_server/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheHost()
		{
			_addr.ConnectionFactory.VirtualHost.ShouldEqual("/");
		}

		[Then]
		public void TheQueue()
		{
			_addr.Name.ShouldEqual("the_queue");
		}
	}

	[Scenario]
	public class GivenAPortedAddress
	{
		public Uri Uri = new Uri("rabbitmq://some_server:12/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheHost()
		{
			_addr.ConnectionFactory.VirtualHost.ShouldEqual("/");
		}

		[Then]
		public void ThePort()
		{
			_addr.ConnectionFactory.Port.ShouldEqual(12);
		}

		[Then]
		public void Rebuilt()
		{
			//    _addr.RebuiltUri.ShouldEqual(new Uri(@"rabbitmq://guest:guest@some_server:12/the_queue"));
		}
	}

	[Scenario]
	public class GivenANonPortedAddress
	{
		public Uri Uri = new Uri("rabbitmq://some_server/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheHost()
		{
			_addr.ConnectionFactory.VirtualHost.ShouldEqual("/");
		}

		[Then]
		public void ThePort()
		{
			_addr.ConnectionFactory.Port.ShouldEqual(5432);
		}
	}


	[Scenario]
	public class GivenAEmptyUserNameUrl
	{
		public Uri Uri = new Uri("rabbitmq://some_server/thehost/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheUsername()
		{
			_addr.ConnectionFactory.UserName.ShouldEqual("guest");
		}

		[Then]
		public void ThePassword()
		{
			_addr.ConnectionFactory.Password.ShouldEqual("guest");
		}
	}


	[Scenario]
	public class GivenAUserNameUrl
	{
		public Uri Uri = new Uri("rabbitmq://dru:mt@some_server/thehost/the_queue");
		RabbitMqEndpointAddress _addr;

		[When]
		public void WhenParsed()
		{
			_addr = RabbitMqEndpointAddress.Parse(Uri);
		}

		[Then]
		public void TheUsername()
		{
			_addr.ConnectionFactory.UserName.ShouldEqual("dru");
		}

		[Then]
		public void ThePassword()
		{
			_addr.ConnectionFactory.Password.ShouldEqual("mt");
		}

		[Then]
		public void Rebuilt()
		{
			//_addr.RebuiltUri.ShouldEqual(new Uri("rabbitmq://dru:mt@some_server:5432/thehost/the_queue"));
		}
	}
}