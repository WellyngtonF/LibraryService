using Api.Data;
using Api.Entities.Interfaces;
using Api.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            return await _context.Author.FindAsync(id);
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            await _context.Author.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAuthor(Author author)
        {
            _context.Author.Update(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}