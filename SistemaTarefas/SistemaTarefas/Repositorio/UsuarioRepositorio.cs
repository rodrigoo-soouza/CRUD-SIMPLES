using Microsoft.EntityFrameworkCore;
using SistemaTarefas.Data;
using SistemaTarefas.Models;
using SistemaTarefas.Repositorio.Interfaces;

namespace SistemaTarefas.Repositorio;

public class UsuarioRepositorio : IUsusarioRepositorio
{
    private readonly SistemaTarefasDBContext _dbContext;

    public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContex)
    {
        _dbContext = sistemaTarefasDBContex;
    }

    public async Task<UsuarioModel> BuscarPorId(int id)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UsuarioModel>> BuscarTodosUsuarios()
    {
        return await _dbContext.Usuarios.ToListAsync();
    }

    public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);
        await _dbContext.SaveChangesAsync();

        return usuario;
    }

    public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, int id)
    {
        UsuarioModel usuarioPorID = await BuscarPorId(id);

        if (usuarioPorID == null)
        {
            throw new Exception($"Usuário para o {id} não foi encontrado no banco de dados.");
        }

        usuarioPorID.Nome = usuario.Nome;
        usuarioPorID.Email = usuario.Email;

        _dbContext.Usuarios.Update(usuarioPorID);
        await _dbContext.SaveChangesAsync();

        return usuarioPorID;
    }

    public async Task<bool> Apagar(int id)
    {
        UsuarioModel usuarioPorID = await BuscarPorId(id);

        if (usuarioPorID == null)
        {
            throw new Exception($"Usuário para o {id} não foi encontrado no banco de dados.");
        }
    
        _dbContext.Usuarios.Remove(usuarioPorID);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    
        
}
