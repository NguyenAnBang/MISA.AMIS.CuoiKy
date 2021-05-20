using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public interface IEmployeeServices : IBaseServices<Employee>
    {
        /// <summary>
        /// Trả về tên phòng ban 
        /// </summary>
        /// <returns>Tên phòng ban</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<EmployeeDepartment> GetDepartmentName();

        /// <summary>
        /// Trả về dữ liệu dựa theo kết quả tìm kiếm theo mã hoặc tên nhân viên
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns>Dữ liệu tìm kiếm tương ứng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetSearchResult(string searchResult);

        /// <summary>
        /// Trả về mã nhân viên lớn nhất, validate để trả về dạng "NV-0000"
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// CreatedBy: NABANG (18/05/21)
        public string GetMaxCode();
        /// <summary>
        /// Validate file excel để xuất dữ liệu
        /// </summary>
        /// <returns>file Excel định dạng tương ứng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public Stream ExportExcel();

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<Employee> GetEmployees(int pageSize, int pageIndex, string filter);

    }
}
