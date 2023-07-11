using CommonLayer;
using ManagerLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class BookManager:IBookManager
    {
        private readonly IBookRepository bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public BookModel AddBook(BookModel book)
        {
            return bookRepository.AddBook(book);
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            return bookRepository.GetAllBooks();
        }

        public string UpdateBook(BookModel book)
        {
            return bookRepository.UpdateBook(book);
        }

        public string DeleteLabel(string BookName)
        {
            return bookRepository.DeleteLabel(BookName);
        }
    }
}
