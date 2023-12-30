using Microsoft.AspNetCore.Mvc;
using SistemaTarefas.Models;
using SistemaTarefas.Repositorio.Interfaces;

namespace SistemaTarefas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    
    private readonly IUsusarioRepositorio _usuarioRepositorio;

    public UsuarioController(IUsusarioRepositorio ususarioRepositorio)
    {
        _usuarioRepositorio = ususarioRepositorio;       
    }

    [HttpGet]
    public async Task<ActionResult <List<UsuarioModel>>> BuscarTodosUsuarios()
    {
        List<UsuarioModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
        
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult <UsuarioModel>> BuscarPorId(int id)
    {
        UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
        
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody] UsuarioModel usuarioModel)
    {
        UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);

        return Ok(usuarioModel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel, int id) 
    {
        usuarioModel.Id = id;
        UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);

        return Ok(usuario);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<UsuarioModel>> Apagar(int id)
    {
        bool apagado = await _usuarioRepositorio.Apagar(id);

        return Ok(apagado);
    }
    
}
