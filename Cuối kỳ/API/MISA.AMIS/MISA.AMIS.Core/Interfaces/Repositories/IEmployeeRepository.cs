using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {

        /// <summary>
        /// Trả về giá trị của departmentName (tên phòng ban) 
        /// </summary>
        /// <returns>Tên phòng ban</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<EmployeeDepartment> GetDepartmentName();

        /// <summary>
        /// Lấy tên phòng ban nhân viên dựa trên id 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>tên phòng ban nhân viên dựa trên id</returns>
        /// CreatedBy: NABANG (18/05/21)
        public EmployeeDepartment GetDepartmentById(Guid departmentId);


        /// <summary>
        /// Check trùng mã nhân viên trong database
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="employeeId"></param>
        /// <param name="http"></param>
        /// <returns>True: Nếu trùng</returns>
        /// <returns>False: Nếu không trùng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public bool CheckDuplicateEmployeeCode(string employeeCode, Guid employeeId, HttpType http);


        /// <summary>
        /// Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns>Dữ liệu tìm kiếm tương ứng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetSearchResult(string searchResult);


        /// <summary>
        /// Trả về mã khách hàng lớn nhất trong database
        /// </summary>
        /// <returns>Mã khách hàng lớn nhất</returns>
        /// CreatedBy: NABANG (18/05/21)
        public string GetMaxCode();

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetEmployees(int pageSize, int pageIndex, string filter);

        /// <summary>
        /// Lấy số lượng nhân viên sau khi filter
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>số lượng nhân viên</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int GetTotalEmployees(string filter);
    }
}
