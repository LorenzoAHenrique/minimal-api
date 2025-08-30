using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Servicos;
using MinimalAPI.DTOs;
using MinimalAPI.Infraestrutura.Db;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculoServicoTest
{
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
    public void TestandoSalvarVeiculo()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

    var veiculo = new Veiculo();
    veiculo.Nome = "Teste";
    veiculo.Marca = "TesteMarca";
    veiculo.Ano = 2222;

    var veiculoServico = new VeiculoServico(context);

    // Act
    veiculoServico.Incluir(veiculo);
    var veiculoDoBanco = context.Veiculos.FirstOrDefault(v => v.Nome == "Teste");

    // Assert
    Assert.IsNotNull(veiculoDoBanco);
    Assert.AreEqual("Teste", veiculoDoBanco.Nome);
    }

    [TestMethod]
    public void TestandoBuscaPorId()
    {
        // Arrange
        var context = CriarContextoDeTeste();
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE Veiculos");

    var veiculo = new Veiculo();
    veiculo.Nome = "Teste";
    veiculo.Marca = "TesteMarca";
    veiculo.Ano = 2222;

    var veiculoServico = new VeiculoServico(context);

    // Act
    veiculoServico.Incluir(veiculo);
    var veiculoDoBanco = context.Veiculos.FirstOrDefault(v => v.Nome == "Teste");

    // Assert
    Assert.IsNotNull(veiculoDoBanco);
    Assert.AreEqual("Teste", veiculoDoBanco.Nome);
    }
}