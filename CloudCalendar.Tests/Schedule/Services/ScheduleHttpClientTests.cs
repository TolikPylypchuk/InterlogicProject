﻿using System.Net.Http;

using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using CloudCalendar.Schedule.Services.Options;

namespace CloudCalendar.Schedule.Services
{
	[TestClass]
	public class ScheduleHttpClientTests
	{
		Mock<IOptionsSnapshot<ScheduleHttpClientOptions>> mockOptions;

		[TestInitialize]
		public void Initialize()
		{
			var options = new ScheduleHttpClientOptions
			{
				Schema = "http",
				Host = "localhost",
				Port = 8080,
				PathFormat = "/classes/year/{0}/semester/{1}"
			};

			this.mockOptions =
				new Mock<IOptionsSnapshot<ScheduleHttpClientOptions>>();
			this.mockOptions.Setup(o => o.Value)
				.Returns(options);
		}

		[TestMethod]
		public void GetScheduleAsyncShouldGetTheSchedule()
		{
			var client = new ScheduleHttpClient(
				new HttpClient(), this.mockOptions.Object);

			var schedule = client.GetScheduleAsync(2016, 2).Result;

			Assert.IsNotNull(schedule);
		}
	}
}
