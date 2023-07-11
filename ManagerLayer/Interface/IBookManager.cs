using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interface
{
    public interface IBookManager
    {
        public BookModel AddBook(BookModel book);

        public IEnumerable<BookModel> GetAllBooks();

        public string UpdateBook(BookModel book);

        public string DeleteLabel(string BookName);
    }
}
