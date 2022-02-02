//===============================================================================
// Microsoft FastTrack for Azure
// Managed Identity for .Net Framework 4.6.2 Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.Practices.EnterpriseLibrary.WindowsAzure.TransientFaultHandling.SqlAzure;
using Microsoft.Practices.TransientFaultHandling;
using Pubs.Data.Contracts;
using Pubs.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pubs.Data.DAL
{
    /// <summary>
    /// ADO.Net implementation of the IPubsData contract
    /// </summary>
    public class PubsDAOADO : IPubsData
    {
        private SqlConnection _cnnSql;

        public PubsDAOADO(SqlConnection cnnSql)
        {
            _cnnSql = cnnSql;
        }

        #region IPubsData

        /// <summary>
        /// Create a new author
        /// </summary>
        /// <param name="author"><see cref="Pubs.Data.Models.Author"/></param>
        public void CreateAuthor(Author author)
        {
            const string sql = "dbo.SP_CreateAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@authorID", author.AuthorID));
            parameters.Add(new SqlParameter("@lastName", author.LastName));
            parameters.Add(new SqlParameter("@firstName", author.FirstName));
            parameters.Add(new SqlParameter("@phoneNumber", author.PhoneNumber));
            parameters.Add(new SqlParameter("@address", author.Address));
            parameters.Add(new SqlParameter("@city", author.City));
            parameters.Add(new SqlParameter("@state", author.State));
            parameters.Add(new SqlParameter("@postalCode", author.PostalCode));
            parameters.Add(new SqlParameter("@hasContract", author.HasContract));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
                sqlHelper.DatabaseName = "some database";
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public void UpdateAuthor(Author author)
        {
            const string sql = "dbo.SP_UpdateAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@authorID", author.AuthorID));
            parameters.Add(new SqlParameter("@lastName", author.LastName));
            parameters.Add(new SqlParameter("@firstName", author.FirstName));
            parameters.Add(new SqlParameter("@phoneNumber", author.PhoneNumber));
            parameters.Add(new SqlParameter("@address", author.Address));
            parameters.Add(new SqlParameter("@city", author.City));
            parameters.Add(new SqlParameter("@state", author.State));
            parameters.Add(new SqlParameter("@postalCode", author.PostalCode));
            parameters.Add(new SqlParameter("@hasContract", author.HasContract));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public void DeleteAuthor(string authorID)
        {
            const string sql = "dbo.SP_DeleteAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@authorID", authorID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public Author GetAuthor(string authorID)
        {
            return GetAuthor(authorID, true);
        }

        public List<Author> ListAuthors()
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthors";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Author author in authors)
            {
                author.Titles = ListTitlesByAuthor(author.AuthorID);
            }

            return authors;
        }

        public List<Author> ListAuthorsByLastName(string lastName)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsByLastName";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@lastName", string.Format("{0}%", lastName)));

            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return authors;
        }

        public List<Author> ListAuthorsByID(string authorID)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsByID";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@authorID", string.Format("{0}%", authorID)));

            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return authors;
        }

        public void CreatePublisher(Publisher publisher)
        {
            const string sql = "dbo.SP_CreatePublisher";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@publisherID", publisher.PublisherID));
            parameters.Add(new SqlParameter("@name", publisher.Name));
            parameters.Add(new SqlParameter("@city", publisher.City));
            parameters.Add(new SqlParameter("@state", publisher.State));
            parameters.Add(new SqlParameter("@country", publisher.Country));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public void UpdatePublisher(Publisher publisher)
        {
            const string sql = "dbo.SP_UpdatePublisher";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@publisherID", publisher.PublisherID));
            parameters.Add(new SqlParameter("@name", publisher.Name));
            parameters.Add(new SqlParameter("@city", publisher.City));
            parameters.Add(new SqlParameter("@state", publisher.State));
            parameters.Add(new SqlParameter("@country", publisher.Country));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public void UpdatePublishers(List<Publisher> publishers)
        {
            DataTable dtPublishers = new DataTable();

            dtPublishers.Columns.Add("PublisherID", typeof(string));
            dtPublishers.Columns.Add("Name", typeof(string));
            dtPublishers.Columns.Add("City", typeof(string));
            dtPublishers.Columns.Add("State", typeof(string));
            dtPublishers.Columns.Add("Country", typeof(string));

            foreach(Publisher p in publishers)
            {
                DataRow row = dtPublishers.NewRow();
                row["PublisherID"] = p.PublisherID;
                row["Name"] = p.Name;
                row["City"] = p.City;
                if (string.IsNullOrEmpty(p.State))
                {
                    row["State"] = DBNull.Value;
                }
                else
                {
                    row["State"] = p.State;
                }
                row["Country"] = p.Country;
                dtPublishers.Rows.Add(row);
            }

            const string sql = "dbo.SP_UpdatePublishers";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@Publishers", SqlDbType.Structured));
            parameters[0].Value = dtPublishers;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

        }

        public void DeletePublisher(string publisherID)
        {
            const string sql = "dbo.SP_DeletePublisher";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@publisherID", publisherID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public Publisher GetPublisher(string publisherID)
        {
            return GetPublisher(publisherID, true);
        }

        public List<Publisher> ListPublishers()
        {
            List<Publisher> publishers = new List<Publisher>();
            const string sql = "dbo.SP_ListPublishers";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            try
            {
                // Transient fault handling using Enterprise Library
                RetryPolicy retryPolicy = new RetryPolicy<SqlAzureTransientErrorDetectionStrategy>(3);

                retryPolicy.ExecuteAction(() =>
                                  {
                                      sqlHelper = new SqlHelper(_cnnSql);
                                      SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                                      while (reader.Read())
                                      {
                                          Publisher publisher = MapReaderToPublisher(reader);
                                          if (publisher != null) publishers.Add(publisher);
                                      }
                                  });
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach(Publisher publisher in publishers)
            {
                publisher.Titles = ListTitlesByPublisher(publisher.PublisherID);
            }

            return publishers;
        }

        public void CreateTitle(Title title)
        {
            const string sql = "dbo.SP_CreateTitle";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", title.TitleID));
            parameters.Add(new SqlParameter("@bookTitle", title.BookTitle));
            parameters.Add(new SqlParameter("@type", title.Type));
            parameters.Add(new SqlParameter("@publisherID", title.Publisher.PublisherID));
            parameters.Add(new SqlParameter("@price", title.Price));
            parameters.Add(new SqlParameter("@advance", title.Advance));
            parameters.Add(new SqlParameter("@royalty", title.Royalty));
            parameters.Add(new SqlParameter("@yearToDateSales", title.YearToDateSales));
            parameters.Add(new SqlParameter("@notes", title.Notes));
            parameters.Add(new SqlParameter("@publishDate", title.PublishDate));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Author author in title.Authors)
            {
                CreateTitleAuthor(title.TitleID, author.AuthorID);
            }
        }

        public void UpdateTitle(Title title)
        {
            const string sql = "dbo.SP_UpdateTitle";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", title.TitleID));
            parameters.Add(new SqlParameter("@bookTitle", title.BookTitle));
            parameters.Add(new SqlParameter("@type", title.Type));
            parameters.Add(new SqlParameter("@publisherID", title.Publisher.PublisherID));
            parameters.Add(new SqlParameter("@price", title.Price));
            parameters.Add(new SqlParameter("@advance", title.Advance));
            parameters.Add(new SqlParameter("@royalty", title.Royalty));
            parameters.Add(new SqlParameter("@yearToDateSales", title.YearToDateSales));
            parameters.Add(new SqlParameter("@notes", title.Notes));
            parameters.Add(new SqlParameter("@publishDate", title.PublishDate));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public void DeleteTitle(string titleID)
        {
            Title title = GetTitle(titleID);
            if (title != null)
            {
                foreach (Author author in title.Authors)
                {
                    DeleteTitleAuthor(titleID, author.AuthorID);
                }
            }

            const string sql = "dbo.SP_DeleteTitle";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", titleID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        public Title GetTitle(string titleID)
        {
            Title title = null;
            const string sql = "dbo.SP_GetTitle";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", titleID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    title = MapReaderToTitle(reader);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            if (title != null)
            {
                title.Publisher = GetPublisher(title.Publisher.PublisherID, false);
                title.Authors = ListAuthorsByTitle(title.TitleID);
            }

            return title;
        }

        public List<Title> ListTitles()
        {
            List<Title> titles = new List<Title>();
            const string sql = "dbo.SP_ListTitles";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Title title = MapReaderToTitle(reader);
                    if (title != null) titles.Add(title);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Title title in titles)
            {
                title.Publisher = GetPublisher(title.Publisher.PublisherID, false);
                title.Authors = ListAuthorsByTitle(title.TitleID);
            }

            return titles;
        }

        #endregion

        #region Methods

        public Author GetAuthor(string authorID, bool loadTitles)
        {
            Author author = null;
            const string sqlGetAuthor = "dbo.SP_GetAuthor";
            const string sqlGetAuthorWithTitles = "dbo.SP_GetAuthorAndTitles";
            string command = string.Empty;
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            if (loadTitles)
            {
                command = sqlGetAuthorWithTitles;
            }
            else
            {
                command = sqlGetAuthor;
            }

            parameters.Add(new SqlParameter("@authorID", authorID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(command, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    author = MapReaderToAuthor(reader);
                }
                reader.NextResult();
                author.Titles = new List<Title>();
                while (reader.Read())
                {
                    Title title = MapReaderToTitle(reader);
                    if (title != null) author.Titles.Add(title);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            //if (author != null && loadTitles)
            //{
            //    author.Titles = ListTitlesByAuthor(authorID);
            //}

            return author;
        }

        private Publisher GetPublisher(string publisherID, bool loadTitles)
        {
            Publisher publisher = null;
            const string sql = "dbo.SP_GetPublisher";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@publisherID", publisherID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    publisher = MapReaderToPublisher(reader);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            if (publisher != null && loadTitles)
            {
                publisher.Titles = ListTitlesByPublisher(publisherID);
            }

            return publisher;
        }

        private List<Author> ListAuthorsByTitle(string titleID)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsByTitle";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", titleID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return authors;
        }

        private List<Title> ListTitlesByAuthor(string authorID)
        {
            List<Title> titles = new List<Title>();
            const string sql = "dbo.SP_ListTitlesByAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@authorID", authorID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Title title = MapReaderToTitle(reader);
                    if (title != null) titles.Add(title);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return titles;
        }

        private List<Title> ListTitlesByPublisher(string publisherID)
        {
            List<Title> titles = new List<Title>();
            const string sql = "dbo.SP_ListTitlesByPublisher";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@publisherID", publisherID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Title title = MapReaderToTitle(reader);
                    if (title != null) titles.Add(title);
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return titles;
        }

        private void CreateTitleAuthor(string titleID, string authorID)
        {
            const string sql = "dbo.SP_CreateTitleAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", titleID));
            parameters.Add(new SqlParameter("@authorID", authorID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        private void DeleteTitleAuthor(string titleID, string authorID)
        {
            const string sql = "dbo.SP_DeleteTitleAuthor";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;

            parameters.Add(new SqlParameter("@titleID", titleID));
            parameters.Add(new SqlParameter("@authorID", authorID));
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                sqlHelper.Execute(sql, CommandType.StoredProcedure, ref parameters);
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }
        }

        private Author MapReaderToAuthor(SqlDataReader reader)
        {
            Author author = null;

            if (reader != null)
            {
                author = new Author();
                author.AuthorID = reader["au_id"].ToString();
                author.LastName = reader["au_lname"].ToString();
                author.FirstName = reader["au_fname"].ToString();
                author.PhoneNumber = reader["phone"].ToString();
                author.Address = reader["address"].ToString();
                author.City = reader["city"].ToString();
                author.State = reader["state"].ToString();
                author.PostalCode = reader["zip"].ToString();
                author.HasContract = Convert.ToBoolean(reader["contract"]);
            }

            return author;
        }

        private Publisher MapReaderToPublisher(SqlDataReader reader)
        {
            Publisher publisher = null;

            if (reader != null)
            {
                publisher = new Publisher();
                publisher.PublisherID = reader["pub_id"].ToString();
                publisher.Name = reader["pub_name"].ToString();
                publisher.City = reader["city"].ToString();
                publisher.State = reader["state"].ToString();
                publisher.Country = reader["country"].ToString();
            }

            return publisher;
        }

        private Title MapReaderToTitle(SqlDataReader reader)
        {
            Title title = null;

            if (reader != null)
            {
                title = new Title();
                title.TitleID = reader["title_id"].ToString();
                title.BookTitle = reader["title"].ToString();
                title.Type = reader["type"].ToString();
                title.Publisher = new Publisher();
                title.Publisher.PublisherID = reader["pub_id"].ToString();
                if (reader["price"] != System.DBNull.Value) title.Price = Convert.ToDecimal(reader["price"]);
                if (reader["advance"] != System.DBNull.Value) title.Advance = Convert.ToDecimal(reader["advance"]);
                if (reader["royalty"] != System.DBNull.Value) title.Royalty = Convert.ToInt32(reader["royalty"]);
                if (reader["ytd_sales"] != System.DBNull.Value) title.YearToDateSales = Convert.ToInt32(reader["ytd_sales"]);
                if (reader["notes"] != System.DBNull.Value) title.Notes = reader["notes"].ToString();
                title.PublishDate = Convert.ToDateTime(reader["pubdate"]);
            }

            return title;
        }

        #endregion


        public List<Author> ListAuthors(int startRow, int numberOfRows, string sortBy, string sortDirection, out int numberOfAuthors)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsPaged";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@startRow", startRow));
            parameters.Add(new SqlParameter("@numberOfRows", numberOfRows));
            parameters.Add(new SqlParameter("@sortBy", sortBy));
            parameters.Add(new SqlParameter("@sortDirection", sortDirection.ToUpper()));

            numberOfAuthors = 0;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        numberOfAuthors = (int)reader["NumberOfAuthors"];
                    }
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Author author in authors)
            {
                author.Titles = ListTitlesByAuthor(author.AuthorID);
            }

            return authors;
        }

        public List<Author> ListAuthorsByLastName(string lastName, int startRow, int numberOfRows, out int numberOfAuthors)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsByLastNamePaged";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@lastName", string.Format("{0}%", lastName)));
            parameters.Add(new SqlParameter("@startRow", startRow));
            parameters.Add(new SqlParameter("@numberOfRows", numberOfRows));

            numberOfAuthors = 0;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        numberOfAuthors = (int)reader["NumberOfAuthors"];
                    }
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return authors;
        }

        public List<Author> ListAuthorsByID(string authorID, int startRow, int numberOfRows, out int numberOfAuthors)
        {
            List<Author> authors = new List<Author>();
            const string sql = "dbo.SP_ListAuthorsByIDPaged";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@authorID", string.Format("{0}%", authorID)));
            parameters.Add(new SqlParameter("@startRow", startRow));
            parameters.Add(new SqlParameter("@numberOfRows", numberOfRows));

            numberOfAuthors = 0;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Author author = MapReaderToAuthor(reader);
                    if (author != null) authors.Add(author);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        numberOfAuthors = (int)reader["NumberOfAuthors"];
                    }
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            return authors;
        }

        public List<Publisher> ListPublishers(int startRow, int numberOfRows, out int numberOfPublishers)
        {
            List<Publisher> publishers = new List<Publisher>();
            const string sql = "dbo.SP_ListPublishersPaged";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@startRow", startRow));
            parameters.Add(new SqlParameter("@numberOfRows", numberOfRows));

            numberOfPublishers = 0;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Publisher publisher = MapReaderToPublisher(reader);
                    if (publisher != null) publishers.Add(publisher);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        numberOfPublishers = (int)reader["NumberOfPublishers"];
                    }
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Publisher publisher in publishers)
            {
                publisher.Titles = ListTitlesByPublisher(publisher.PublisherID);
            }

            return publishers;
        }

        public List<Title> ListTitles(int startRow, int numberOfRows, out int numberOfTitles)
        {
            List<Title> titles = new List<Title>();
            const string sql = "dbo.SP_ListTitlesPaged";
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = null;
            parameters.Add(new SqlParameter("@startRow", startRow));
            parameters.Add(new SqlParameter("@numberOfRows", numberOfRows));

            numberOfTitles = 0;
            try
            {
                sqlHelper = new SqlHelper(_cnnSql);
                SqlDataReader reader = sqlHelper.ExecuteDataReader(sql, CommandType.StoredProcedure, ref parameters);
                while (reader.Read())
                {
                    Title title = MapReaderToTitle(reader);
                    if (title != null) titles.Add(title);
                }
                if (reader.NextResult())
                {
                    while (reader.Read())
                    {
                        numberOfTitles = (int)reader["NumberOfTitles"];
                    }
                }
            }
            catch (Exception)
            {
                // Implement your logging, etc. here
                throw;
            }
            finally
            {
                if (sqlHelper != null) sqlHelper.Close();
            }

            foreach (Title title in titles)
            {
                title.Publisher = GetPublisher(title.Publisher.PublisherID, false);
                title.Authors = ListAuthorsByTitle(title.TitleID);
            }

            return titles;
        }
    }
}
