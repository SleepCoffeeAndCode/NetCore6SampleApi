using BusinessLayer.DTO;
using BusinessLayer.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer;

public class PersonService : IPersonService
{
    private readonly string? _connStr;

    public PersonService(IConfiguration config)
    {
        _connStr = config.GetSection("ConnectionStrings:SandBoxDB").Value;
    }
    public void CreatePersonTable()
    {
        using (IDbConnection db = new SqlConnection(_connStr))
        {
            #region sql
            string sql = @"
                    IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PERSON3]') AND type in (N'U'))
                    DROP TABLE [dbo].[PERSON3]


                    CREATE TABLE [dbo].[PERSON3](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [FIRST_NAME] [nvarchar](50) NULL,
	                    [LAST_NAME] [nvarchar](50) NULL,
	                    [EMAIL] [nvarchar](512) NULL,
	                    [GENDER] [nvarchar](16) NULL,
	                    [CREATED_DATE] [datetime] NULL,
                     CONSTRAINT [PK_PERSON3_table] PRIMARY KEY CLUSTERED 
                    (
	                    [ID] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
                    ) ON [PRIMARY]
            ";
            #endregion
            db.Execute(sql);
        }
    }

    public IEnumerable<PersonDTO> GetAllPersons()
    {
        using (IDbConnection db = new SqlConnection(_connStr))
        {
            #region sql
            string sql = @"
                    SELECT 
                   [ID]
                  ,[FIRST_NAME]
                  ,[LAST_NAME]
                  ,[EMAIL]
                  ,[GENDER]
	              ,[CREATED_DATE]
                FROM [Sandbox].[dbo].[PERSON3];
            ";
            #endregion
            IEnumerable<PersonDTO> result = db.Query<PersonDTO>(sql);
            return result;
        }
    }

    public IEnumerable<PersonDTO> GetPersonById(int id)
    {
        using (IDbConnection db = new SqlConnection(_connStr))
        {
            #region sql
            string sql = @"
                    SELECT 
                   [ID]
                  ,[FIRST_NAME]
                  ,[LAST_NAME]
                  ,[EMAIL]
                  ,[GENDER]
	              ,[CREATED_DATE]
              FROM [Sandbox].[dbo].[PERSON3] P
            WHERE
	            P.ID = @ID;
            ";
            #endregion
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("ID", id, DbType.Int64);
            IEnumerable<PersonDTO>? result = db.Query<PersonDTO>(sql, dynamicParameters);

            return result;
        }
    }

    public IEnumerable<PersonDTO> GetPersonByFirstName(string fname)
    {
        throw new NotImplementedException();
    }

    public int AddPerson(PersonDTO person)
    {
        using (IDbConnection db = new SqlConnection(_connStr))
        {
            #region sql
            string sql = @"
                    INSERT INTO dbo.PERSON3 
                        (
                            FIRST_NAME
                            ,LAST_NAME
                            ,EMAIL
                            ,GENDER
                            ,CREATED_DATE
                        ) VALUES (
                            @FIRST_NAME
                            ,@LAST_NAME
                            ,@EMAIL
                            ,@GENDER
                            ,@CREATED_DATE
                        )
            ";
            #endregion
            return db.Execute(sql, person);
        }

    }

    public void UpdatePerson(PersonDTO person)
    {
        using (IDbConnection db = new SqlConnection(_connStr))
        {
            #region sql
            string sql = @"
                  UPDATE [dbo].[PERSON3]
	                    SET
                          [FIRST_NAME] = @FIRST_NAME
                          ,[LAST_NAME] = @LAST_NAME
                          ,[EMAIL] = @EMAIL
                          ,[GENDER] = @GENDER
                          ,[CREATED_DATE] = @CREATED_DATE
                    WHERE 
	                    [ID] = @ID;  
            ";
            #endregion
            var response = db.Execute(sql, person);
            var result = response;
        }
    }
}