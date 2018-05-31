using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestBooksMVC.Models;

namespace TestBooksMVC.Controllers
{
    public class BooksController : Controller
    {
        string connectionString = @"Data Source = THIKA-PC; Initial Catalog = Books; Integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttblBooks = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM LISTBOOKS",sqlcon);
                sqlda.Fill(dttblBooks);
            }
                return View(dttblBooks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new BooksModel());
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(BooksModel bookmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO ListBooks VALUES(@Title,@Author,@DatePublished,@Pages,@Type)";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@Title", bookmodel.Title);
                sqlcmd.Parameters.AddWithValue("@Author", bookmodel.Author);
                sqlcmd.Parameters.AddWithValue("@DatePublished", bookmodel.DatePublished);
                sqlcmd.Parameters.AddWithValue("@Pages", bookmodel.Pages);
                sqlcmd.Parameters.AddWithValue("@Type", bookmodel.Type);
                sqlcmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            BooksModel bookmodel = new BooksModel();
            DataTable dttblBook = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM ListBooks Where BooksId = @BooksId";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.SelectCommand.Parameters.AddWithValue("@BooksId", id);
                sqlda.Fill(dttblBook);
            }
            if (dttblBook.Rows.Count == 1)
            {
                bookmodel.BooksId = Convert.ToInt32(dttblBook.Rows[0][0].ToString());
                bookmodel.Title = (dttblBook.Rows[0][1].ToString());
                bookmodel.Author = (dttblBook.Rows[0][2].ToString());
                bookmodel.DatePublished = Convert.ToDateTime(dttblBook.Rows[0][3].ToString());
                bookmodel.Pages = Convert.ToInt32(dttblBook.Rows[0][4].ToString());
                bookmodel.Type = (dttblBook.Rows[0][5].ToString());

                return View(bookmodel);
            }
            else
            return RedirectToAction("Index");

        }

        // POST: Books/Edit/5
        [HttpPost]
        public ActionResult Edit(BooksModel bookmodel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE ListBooks SET Title = @Title , Author = @Author , DatePublised = @DatePublised , Pages = @Pages , TypeBooks = @Type Where BooksId = @BooksId ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@BooksId", bookmodel.BooksId);
                sqlcmd.Parameters.AddWithValue("@Title", bookmodel.Title);
                sqlcmd.Parameters.AddWithValue("@Author", bookmodel.Author);
                sqlcmd.Parameters.AddWithValue("@DatePublised", bookmodel.DatePublished);
                sqlcmd.Parameters.AddWithValue("@Pages", bookmodel.Pages);
                sqlcmd.Parameters.AddWithValue("@Type", bookmodel.Type);
                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "DELETE FROM ListBooks Where BooksId = @BooksId ";
                SqlCommand sqlcmd = new SqlCommand(query, sqlcon);
                sqlcmd.Parameters.AddWithValue("@BooksId", id);
                sqlcmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}
