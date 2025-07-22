﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.Interfaces.DA;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _conexionBase;
        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionBase = new SqlConnection(_configuration.GetConnectionString("BD"));
        }
        public SqlConnection ObtenerRepositorio()
        {
            return _conexionBase;
        }
    }
}
