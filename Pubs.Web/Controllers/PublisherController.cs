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
using Microsoft.Azure.Services.AppAuthentication;
using Pubs.Data.Contracts;
using Pubs.Data.DAL;
using Pubs.Data.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Pubs.Web.Controllers
{
    public class PublisherController : Controller
    {
        private IPubsData _pubsDataADO;
        private IPubsData _pubsDataEF;
        private SqlConnection _sqlConnection;
        private string _connectionString = ConfigurationManager.ConnectionStrings["pubs"].ToString();

        public PublisherController()
        {
            // Create a new SQL Server connection
            _sqlConnection = new SqlConnection(_connectionString);
            // Retrieve an access token for the application's Managed Identity and set it on the SQL connection
            _sqlConnection.AccessToken = new AzureServiceTokenProvider().GetAccessTokenAsync("https://database.windows.net/").Result;
            // Pass the SQL connection to the data repository
            _pubsDataADO = new PubsDAOADO(_sqlConnection); // ADO.Net
            _pubsDataEF = new PubsDAO(_sqlConnection); // Entity Framework
        }

        public PublisherController(IPubsData pubsDataADO, IPubsData pubsDataEF)
        {
            _pubsDataADO = pubsDataADO;
            _pubsDataEF = pubsDataEF;
        }

        // GET: Publisher
        public ActionResult Index()
        {
            // Retrieve a list of publishers using ADO.Net
            List<Publisher> model = _pubsDataADO.ListPublishers();
            ViewBag.Title = "Publishers using ADO.Net";
            return View(model);
        }

        public ActionResult ListPublishersUsingEF()
        {
            // Retrieve a list of publishers using Entity Framework
            List<Publisher> model = _pubsDataEF.ListPublishers();
            ViewBag.Title = "Publishers using Entity Framework";
            return View("Index", model);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // Cleanup the SQL connection
            if (_sqlConnection != null)
            {
                if (_sqlConnection.State != ConnectionState.Closed) _sqlConnection.Close();
                _sqlConnection.Dispose();
            }
        }
    }
}