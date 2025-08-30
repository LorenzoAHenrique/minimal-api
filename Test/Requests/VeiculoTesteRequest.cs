
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Helpers;

namespace Test.Requests
{
	[TestClass]
	public class VeiculoTesteRequest
	{
		[TestMethod]
		public async Task DeveListarVeiculosComoAnonimo()
		{
			var response = await Setup.client.GetAsync("/veiculos");
			var json = await response.Content.ReadAsStringAsync();
			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, $"Body: {json}");
		}
	}
}
