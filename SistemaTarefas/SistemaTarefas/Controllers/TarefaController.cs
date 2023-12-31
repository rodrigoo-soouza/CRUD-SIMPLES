using Microsoft.AspNetCore.Mvc;
using SistemaTarefas.Models;
using SistemaTarefas.Repositorio.Interfaces;

namespace SistemaTarefas.Controllers;

[Route("api/[controller]")]
[ApiController]

public class TarefaController : Controller
{
    private readonly ITarefasRepositorio _tarefaRepositorio;
    
    public TarefaController(ITarefasRepositorio tarefaRepositorio)
    {
        _tarefaRepositorio = tarefaRepositorio;
    }

    [HttpGet]
    public async Task<ActionResult<List<TarefaModel>>> ListarTodas()
    {
        List<TarefaModel> tarefas = await _tarefaRepositorio.BuscarTodasTarefas();

        return Ok(tarefas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TarefaModel>> BuscarPorId(int id)
    {
        TarefaModel tarefa = await _tarefaRepositorio.BuscarPorId(id);

        return Ok(tarefa);
    }

    [HttpPost]
    public async Task<ActionResult<TarefaModel>> Cadastrar([FromBody] TarefaModel tarefaModel)
    {
        TarefaModel tarefa = await _tarefaRepositorio.Adicionar(tarefaModel);

        return Ok(tarefa);
    }
}
