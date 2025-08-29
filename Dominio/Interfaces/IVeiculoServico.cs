using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using minimal_api.Migrations;
using MinimalAPI.Dominio.Entidades;
using MinimalAPI.DTOs;

namespace MinimalAPI.Dominio.Interfaces;

public interface IVeiculoServico
{
    List<Veiculo> Todos(int? pagina, string? nome = null, string? marca = null);

    Veiculo? BuscaPorId(int id);

    void Incluir(Veiculo veiculo);

    void Atualizar(Veiculo veiculo);

    void Apagar(Veiculo veiculo);
}