using Dapper;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="employeeId"></param>
        /// <param name="http"></param>
        /// <returns>True: nếu trùng</returns>
        /// <returns>False: nếu không trùng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public bool CheckDuplicateEmployeeCode(string employeeCode, Guid employeeId, HttpType http)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "";
                DynamicParameters dynamicParameters = new DynamicParameters();
                if (http == HttpType.POST)
                {
                    sqlCommand = "Proc_CheckEmployeeCodeExist";
                    dynamicParameters.Add("EmployeeCode", employeeCode);
                }
                else
                {
                    //Check trùng mã khách hàng
                    sqlCommand = "Proc_CheckEmployeeCodeExistPut";
                    dynamicParameters.Add("EmployeeCode", employeeCode);
                    dynamicParameters.Add("Id", employeeId);
                }
                var Exists = dbConnection.QueryFirstOrDefault<bool>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return Exists;
            }
        }

        /// <summary>
        /// Lấy tất cả tên phòng ban nhân viên 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>dữ liệu phòng ban từ bảng Department</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<EmployeeDepartment> GetDepartmentName()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetDepartments";

                var departments = dbConnection.Query<EmployeeDepartment>(sqlCommand, commandType: CommandType.StoredProcedure);
                return departments;
            }
        }

        /// <summary>
        /// Lấy tên phòng ban nhân viên dựa trên id 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>tên phòng ban nhân viên dựa trên id</returns>
        /// CreatedBy: NABANG (18/05/21)
        public EmployeeDepartment GetDepartmentById(Guid departmentId)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetDepartmentById";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("departmentId", departmentId);
                var department = dbConnection.QueryFirstOrDefault<EmployeeDepartment>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return department;
            }
        }
        /// <summary>
        /// Trả về mã nhân viên lớn nhất trong database
        /// </summary>
        /// <returns>EmployeeCode</returns>
        /// CreatedBy: NABANG (18/05/21)
        public string GetMaxCode()
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetMaxEmployeeCode";

                var response = dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
                return response;
            }
        }

        /// <summary>
        /// Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns>List dữ liệu phù hợp với kết quả search</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetSearchResult(string searchResult)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_SearchEmployee";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("SearchField", searchResult);

                var response = dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return response;
            }
        }
        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetEmployees(int pageSize, int pageIndex, string filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetEmployeeFilter";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@pageIndex", pageIndex);
                dynamicParameters.Add("@pageSize", pageSize);
                dynamicParameters.Add("@filter", filter);

                var employees = dbConnection.Query<Employee>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return employees;
            }
        }
        /// <summary>
        /// Lấy số lượng nhân viên sau khi filter
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>số lượng nhân viên</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int GetTotalEmployees(string filter)
        {
            using (dbConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "Proc_GetTotalEmployees";
                DynamicParameters dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@filter", filter);

                var totalRecord = dbConnection.QueryFirstOrDefault<int>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
                return totalRecord;
            }
        }
    }
}
