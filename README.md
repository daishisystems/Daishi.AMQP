<a href="http://insidethecpu.com/2015/05/22/microservices-with-c-and-rabbitmq/">![Image of insidethecpu](https://dl.dropboxusercontent.com/u/26042707/Daishi%20Systems%20Icon%20with%20Text%20%28really%20tiny%20with%20photo%29.png)</a>
# Microservices SDK for .NET applications
[![Join the chat at https://gitter.im/daishisystems/Daishi.AMQP](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/daishisystems/Daishi.AMQP?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/ly3h4f406u5332n3?svg=true)](https://ci.appveyor.com/project/daishisystems/daishi-amqp)
[![NuGet](https://img.shields.io/badge/nuget-v1.0.0-blue.svg)](https://www.nuget.org/packages/Daishi.AMQP)

As seen on <a href="https://visualstudiomagazine.com/articles/2015/09/30/microservices-csharp.aspx">visualstudiomagazine.com</a>.

*Microservices are groupings of lightweight services, interconnected, although independent of each other, without direct coupling or dependency. Microservices allow flexibility in terms of infrastructure; application traffic is routed to collections of services that may be distributed across CPU, disk, machine and network as opposed to a single monolithic platform designed to manage all traffic.*

Click <a href="http://insidethecpu.com/2015/05/22/microservices-with-c-and-rabbitmq/">here for an in-depth tutorial</a> on building Microservices using this framework.
<a href="http://insidethecpu.com/2015/05/22/microservices-with-c-and-rabbitmq/">![Image of Microservices as Gears](https://dl.dropboxusercontent.com/u/26042707/daishi.amqp.jpg)</a>
## Installation
```
PM> Install-Package Daishi.AMQP
```
## Sample Code
### Connect to RabbitMQ
```cs
var adapter = RabbitMQAdapter.Instance;
 
adapter.Init("hostName", 1234, "userName", "password", 50);
adapter.Connect();
```
### Send a Message
```cs
var message = "Hello, World!";
adapter.Publish(message, "queueName");
```
### Retrieve a Message
```cs
string output;
BasicDeliverEventArgs eventArgs;

adapter.TryGetNextMessage("queueName", out output, out eventArgs, 50);
```
### Continuously Poll for Messages
```cs
var consumer = new RabbitMQConsumerCatchAll("queueName", 10);
adapter.ConsumeAsync(consumer);
 
Console.ReadLine();
adapter.StopConsumingAsync(consumer);
```
## Contact the Developer
Please reach out and contact me for questions, suggestions, or to just talk tech in general.


<a href="http://insidethecpu.com/feed/">![RSS](https://dl.dropboxusercontent.com/u/26042707/rss.png)</a><a href="https://twitter.com/daishisystems">![Twitter](https://dl.dropboxusercontent.com/u/26042707/twitter.png)</a><a href="https://www.linkedin.com/in/daishisystems">![LinkedIn](https://dl.dropboxusercontent.com/u/26042707/linkedin.png)</a><a href="https://plus.google.com/102806071104797194504/posts">![Google+](https://dl.dropboxusercontent.com/u/26042707/g.png)</a><a href="https://www.youtube.com/user/daishisystems">![YouTube](https://dl.dropboxusercontent.com/u/26042707/youtube.png)</a>