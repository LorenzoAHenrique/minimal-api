using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.ModelViews;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.DTOs;
using MinimalAPI.Infraestrutura.Db;
using Test.Helpers;

namespace Test.Requests;

[TestClass]
public class AdministradorTesteRequest
{
    [ClassInitialize]
    public static void ClassInit(TestContext testContext)
    {
        Setup.ClassInit(testContext);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        Setup.ClassCleanup();
    }


    [TestMethod]
    public async Task TestarGetSetPropriedades()
    {
        // Arrange
        var loginDTO = new LoginDTO
        {
            Email = "adm@teste.com",
            Senha = "123456"
        };

        var content = new StringContent(JsonSerializer.Serialize(loginDTO), Encoding.UTF8, "Application/json");

        // Act
        var response = await Setup.client.PostAsync("/administradores/login", content);

        // Assert
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadAsStringAsync();
        var admLogado = JsonSerializer.Deserialize<AdministradorLogado>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.IsNotNull(admLogado?.Email ?? "");
        Assert.IsNotNull(admLogado?.Perfil ?? "");
        Assert.IsNotNull(admLogado?.Token ?? "");

        Console.WriteLine(admLogado?.Token);
    }

    [TestMethod]
    public void TesteLoginComSenhaErrada()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Administradores");

        var adm = new Administrador();
        adm.Email = "login@teste.com";
        adm.Senha = "senha123";
        adm.Perfil = "Adm";

        var administradorServico = new AdministradorServico(context);
        administradorServico.Incluir(adm);

        // Act
        var loginDTO = new LoginDTO
        {
            Email = "login@teste.com",
            Senha = "senhaErrada"
        };
        var resultado = administradorServico.Login(loginDTO);

        // Assert
        Assert.IsNull(resultado);
    }


    private DbContexto CriarContextoDeTeste()
    {
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var path = Path.GetFullPath(Path.Combine(assemblyPath ?? "", "..", "..", ".."));

        var builder = new ConfigurationBuilder()
            .SetBasePath(path ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        return new DbContexto(configuration);
    }
    [TestMethod]
public async Task DeveCadastrarAdministradorComSucesso()
{
    var adminDTO = new
    {
        Email = $"admin{Guid.NewGuid()}@teste.com",
        Senha = "123456",
        Perfil = "Adm"
    };
    var content = new StringContent(JsonSerializer.Serialize(adminDTO), Encoding.UTF8, "application/json");
    var response = await Setup.client.PostAsync("/administradores/", content);
    var responseBody = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Status: {response.StatusCode}, Body: {responseBody}");
    Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
}
}