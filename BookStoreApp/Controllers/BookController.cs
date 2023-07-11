using CommonLayer;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookManager bookManager;

        public BookController(IBookManager bookManager) 
        {
            this.bookManager = bookManager;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        [Route("AddBook")]
        public IActionResult AddBook(BookModel bookAddingModel)

        {
            try
            {
                BookModel bookAddData = this.bookManager.AddBook(bookAddingModel);

                if (bookAddingModel != null)
                {
                    return this.Ok(new { Success = true, message = " Book Added Successfully", result = bookAddingModel });
                }

                return this.BadRequest(new { success = true, message = "Book Already Exists" });

            }
            catch (Exception)
            {
                return this.NotFound(new { success = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllBooks")]
        public IActionResult GetAllBooks()
        {
            try
            {
                List<BookModel> bookmodellist = (List<BookModel>)this.bookManager.GetAllBooks();

                if (bookmodellist != null)
                {
                    return this.Ok(new { Success = true, message = "Books Fetched Successfully", result = bookmodellist });
                }
                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPatch]
        [Route("UpdateBook")]
        public IActionResult UpdateBook(BookModel bookModel)
        {
            try
            {
                

                string updateBook = this.bookManager.UpdateBook(bookModel);


                if (updateBook != null)
                {
                    return this.Ok(new { Success = true, message = "Updated Book successfully", result = updateBook });
                }

                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }

        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete]
        [Route("DeleteBook")]
        public IActionResult DeleteBook(string BookName)
        {
            try
            {
                string deleteBook = this.bookManager.DeleteLabel(BookName);


                if (deleteBook != null)
                {
                    return this.Ok(new { Success = true, message = "Deleted Book successfully", result = deleteBook });
                }


                return this.BadRequest(new { success = true, message = "Process Failed" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

    }
}
