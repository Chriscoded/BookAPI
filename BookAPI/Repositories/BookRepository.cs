using BookAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext context;

        public BookRepository(BookContext context)
        {
            this.context = context;
        }

        public async Task<Book> Create(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();

            return book;
        }

        public async Task<IEnumerable<Book>> Get()
        {
          return await context.Books.ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await context.Books.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var BookToDelete = await context.Books.FindAsync(id);
            context.Books.Remove(BookToDelete);
            await context.SaveChangesAsync();
        }
    }
}
