using MinimalAPI.Dominio.Entidades;

namespace Test.Domain.Entidades;

[TestClass]
public class VeiculosTest
{
    [TestMethod]
    public void TestarGetSetPropriedades()
    {
        // Arrange
        var veiculo = new Veiculo();

        //Act
        veiculo.Id = 1;
        veiculo.Nome = "Testador";
        veiculo.Marca = "teste";
        veiculo.Ano = 2000;

        //Assert
        Assert.AreEqual(1, veiculo.Id);
        Assert.AreEqual("Testador", veiculo.Nome);
        Assert.AreEqual("teste", veiculo.Marca);
        Assert.AreEqual(2000, veiculo.Ano);

    }
}