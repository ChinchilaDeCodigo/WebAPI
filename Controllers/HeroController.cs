using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private IConfiguration _configuration;

        public HeroController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetHeroes")]
        public JsonResult getHeroes()
        {
            string query = "select * from dbo.hero";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DbTesteConn");
            SqlDataReader myReader;
            using(SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using(SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }
            
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddHero")]
        public JsonResult AddHero([FromForm] string newHero)
        {
            string query = "insert into dbo.hero values(@newHero)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DbTesteConn");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    sqlCommand.Parameters.AddWithValue("@newHero", newHero);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Adicionado com sucesso!");

        }

        [HttpDelete]
        [Route("DeleteHero")]
        public JsonResult DeleteHero(int id)
        {
            string query = "delete from dbo.hero where id=@id";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("DbTesteConn");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDatasource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult("Removido com sucesso!");

        }
    }
}
