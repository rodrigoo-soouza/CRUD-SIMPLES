using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Data;
using SistemaTarefas.Models;
using SistemaTarefas.Repositorio.Interfaces;

namespace SistemaTarefas.Repositorio;

public class TarefaRepositorio : ITarefasRepositorio
{

    private readonly SistemaTarefasDBContext _dbContext;

    public TarefaRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
    {
        _dbContext = sistemaTarefasDBContext;   
    }

    public async Task<TarefaModel> BuscarPorId(int id)
    {
        return await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TarefaModel>> BuscarTodasTarefas()
    {
        return await _dbContext.Tarefas.ToListAsync();
    }

    public async Task<TarefaModel> Adicionar(TarefaModel tarefa)
    {
        await _dbContext.Tarefas.AddAsync(tarefa);
        await _dbContext.SaveChangesAsync();

        return tarefa;
    }

    public async Task<TarefaModel> Atualizar(TarefaModel tarefa, int id)
    {
        TarefaModel tarefaPorId = await BuscarPorId(id);

        if (tarefaPorId == null)
        {
            throw new Exception($"Tarefa para o ID {id} não foi encontrada no banco de dados.");
        }

        tarefaPorId.Nome = tarefa.Nome;
        tarefaPorId.Descricao = tarefa.Descricao;
        tarefaPorId.Status = tarefa.Status;
        tarefaPorId.UsuarioId = tarefa.UsuarioId;

        _dbContext.Tarefas.Update(tarefaPorId);
        await _dbContext.SaveChangesAsync();

        return tarefaPorId;
    }

    public async Task<bool> Apagar(int id)
    {
        TarefaModel tarefaPorId = await BuscarPorId(id);

        if (tarefaPorId == null)
        {
            throw new Exception($"Tarefa para o ID: {id} não foi encontrada no banco de dados.");
        }
    
        _dbContext.Tarefas.Remove(tarefaPorId);
        await _dbContext.SaveChangesAsync();

        return true;
    }
        
}
