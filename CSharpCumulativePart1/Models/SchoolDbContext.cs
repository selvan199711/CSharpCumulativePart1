using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace CSharpCumulativePart2.Models
{
    public sealed partial class SchoolDbContext
    {
        private readonly string _connStr;
        public SchoolDbContext(IConfiguration config)
        {
            _connStr = config.GetConnectionString("SchoolDb")
                       ?? throw new InvalidOperationException("missing connection string 'SchoolDb'.");
        }

        private MySqlConnection CreateConnection() => new MySqlConnection(_connStr);

        // --- your actual table/column names ---
        private const string TBL = "Teachers";
        private const string COL_ID = "Id";
        private const string COL_FNAME = "FirstName";
        private const string COL_LNAME = "LastName";
        private const string COL_EMP = "EmployeeNumber";
        private const string COL_HIREDATE = "HireDate";
        private const string COL_SALARY = "Salary";
        // --------------------------------------

        public async Task<Teacher?> FindTeacherAsync(int id)
        {
            var sql = $@"
                SELECT
                    `{COL_ID}`       AS teacherid,
                    `{COL_FNAME}`    AS teacherfname,
                    `{COL_LNAME}`    AS teacherlname,
                    `{COL_EMP}`      AS employeenumber,
                    `{COL_HIREDATE}` AS hiredate,
                    `{COL_SALARY}`   AS salary
                FROM `{TBL}`
                WHERE `{COL_ID}` = @id;";

            using var conn = CreateConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var r = await cmd.ExecuteReaderAsync();
            if (!await r.ReadAsync()) return null;

            int idIx = r.GetOrdinal("teacherid");
            int fnIx = r.GetOrdinal("teacherfname");
            int lnIx = r.GetOrdinal("teacherlname");
            int empIx = r.GetOrdinal("employeenumber");
            int hdIx = r.GetOrdinal("hiredate");
            int salIx = r.GetOrdinal("salary");

            return new Teacher
            {
                TeacherId = r.GetInt32(idIx),
                FirstName = r.GetString(fnIx),
                LastName = r.GetString(lnIx),
                EmployeeNumber = r.GetString(empIx),
                HireDate = r.GetDateTime(hdIx),
                Salary = r.GetDecimal(salIx)
            };
        }

        public async Task<int> UpdateTeacherAsync(Teacher t)
        {
            var sql = $@"
                UPDATE `{TBL}`
                SET
                    `{COL_FNAME}`    = @fn,
                    `{COL_LNAME}`    = @ln,
                    `{COL_EMP}`      = @emp,
                    `{COL_HIREDATE}` = @hd,
                    `{COL_SALARY}`   = @sal
                WHERE `{COL_ID}` = @id;";

            using var conn = CreateConnection();
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fn", t.FirstName);
            cmd.Parameters.AddWithValue("@ln", t.LastName);
            cmd.Parameters.AddWithValue("@emp", t.EmployeeNumber);
            cmd.Parameters.AddWithValue("@hd", t.HireDate);
            cmd.Parameters.AddWithValue("@sal", t.Salary);
            cmd.Parameters.AddWithValue("@id", t.TeacherId);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Course>> ListCoursesAsync()
        {
            const string sql = @"
             SELECT
            `Id`         AS courseid,
            `Title`      AS title,
            `Credits`    AS credits,
            `TeacherId`  AS teacherid
             FROM `Courses`;";

            var list = new List<Course>();

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                list.Add(new Course
                {
                    CourseId = r.GetInt32(r.GetOrdinal("courseid")),
                    Title = r.GetString(r.GetOrdinal("title")),
                    Credits = r.GetInt32(r.GetOrdinal("credits")),
                    TeacherId = r.IsDBNull(r.GetOrdinal("teacherid")) ? (int?)null : r.GetInt32(r.GetOrdinal("teacherid"))
                });
            }
            return list;
        }

        public async Task<Course?> FindCourseAsync(int id)
        {
            const string sql = @"
             SELECT
            `Id`         AS courseid,
            `Title`      AS title,
            `Credits`    AS credits,
            `TeacherId`  AS teacherid
             FROM `Courses`
             WHERE `Id`=@id;";

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var r = await cmd.ExecuteReaderAsync();
            if (!await r.ReadAsync()) return null;

            return new Course
            {
                CourseId = r.GetInt32(r.GetOrdinal("courseid")),
                Title = r.GetString(r.GetOrdinal("title")),
                Credits = r.GetInt32(r.GetOrdinal("credits")),
                TeacherId = r.IsDBNull(r.GetOrdinal("teacherid")) ? (int?)null : r.GetInt32(r.GetOrdinal("teacherid"))
            };
        }

        public async Task<List<Student>> ListStudentsAsync()
        {
            const string sql = @"
             SELECT
            `Id`             AS studentid,
            `FullName`       AS fullname,
            `EnrollmentDate` AS enroll,
            `Program`        AS program
             FROM `Students`;";

            var list = new List<Student>();

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            using var r = await cmd.ExecuteReaderAsync();
            while (await r.ReadAsync())
            {
                list.Add(new Student
                {
                    StudentId = r.GetInt32(r.GetOrdinal("studentid")),
                    FullName = r.GetString(r.GetOrdinal("fullname")),
                    EnrollmentDate = r.GetDateTime(r.GetOrdinal("enroll")),
                    Program = r.GetString(r.GetOrdinal("program"))
                });
            }
            return list;
        }

        public async Task<Student?> FindStudentAsync(int id)
        {
            const string sql = @"
             SELECT
            `Id`             AS studentid,
            `FullName`       AS fullname,
            `EnrollmentDate` AS enroll,
            `Program`        AS program
             FROM `Students`
             WHERE `Id`=@id;";

            using var conn = new MySqlConnection(_connStr);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var r = await cmd.ExecuteReaderAsync();
            if (!await r.ReadAsync()) return null;

            return new Student
            {
                StudentId = r.GetInt32(r.GetOrdinal("studentid")),
                FullName = r.GetString(r.GetOrdinal("fullname")),
                EnrollmentDate = r.GetDateTime(r.GetOrdinal("enroll")),
                Program = r.GetString(r.GetOrdinal("program"))
            };
        }
    }
}
