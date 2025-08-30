using MinimalAPI.Dominio.Entidades;
using MinimalAPI.Dominio.Interfaces;
using MinimalAPI.DTOs;

namespace Test.Mocks;

public class VeiculoServicoMock : IVeiculoServico
{
    
    private static List<Veiculo> veiculos = new List<Veiculo>(){
        new Veiculo{
            Id = 1,
            Nome = "Carro Teste",
            Marca = "Marca Teste",
            Ano = 2020,
        },
        new Veiculo{
            Id = 2,
            Nome = "Carro Teste 2",
            Marca = "Marca Teste 2",
            Ano = 2022,
            
        }
    };
    public void Apagar(Veiculo veiculo)
    {
    veiculos.Remove(veiculo);
    }

    public void Atualizar(Veiculo veiculo)
    {
        throw new NotImplementedException();
    }

    public Veiculo? BuscaPorId(int id)
    {
        return veiculos.Find(v => v.Id == id);
    }

    public void Incluir(Veiculo veiculo)
    {
    veiculo.Id = veiculos.Count() + 1;
    veiculos.Add(veiculo);
    }

    public List<Veiculo> Todos(int? pagina, string? nome = null, string? marca = null)
    {
        return veiculos;
    }
}