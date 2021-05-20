using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        IEmployeeRepository _employeeRepository;
        IEmployeeServices _employeeServices;
        public EmployeeController(IEmployeeRepository employeeRepository, IEmployeeServices employeeServices) : base(employeeRepository, employeeServices)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        }

        /// <summary>
        /// Lấy dữ liệu phù hợp với kết quả tìm kiếm của người dùng
        /// </summary>
        /// <param name="searchResult"></param>
        /// <returns>Dữ liệu bản ghi phù hợp với kết quả tìm kiếm</returns>
        /// <response code = "200">: có dữ liệu trả về</response>
        /// <response code = "204">: không có dữ liệu trả về</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("Search/{searchResult}")]
        public IActionResult GetSearchResult(string searchResult)
        {
            var response = _employeeServices.GetSearchResult(searchResult);
            if (response.Count() > 0) return Ok(response);
            else return NoContent();
        }
        /// <summary>
        /// Xuất dữ liệu ra file excel
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>file excel</returns>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("Export")]
        public IActionResult Export()
        {
            var stream = _employeeServices.ExportExcel();
            string excelName = $"{Properties.Resources.ExcelFileName}.xlsx";

            //return File(stream, "application/octet-stream", excelName);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        }
        
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong database
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// <response code = "200">: có dữ liệu trả về</response>
        /// <response code = "204">: không có dữ liệu trả về</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("MaxCode")]

        public IActionResult GetMaxCode()
        {
            var response = _employeeServices.GetMaxCode();
            if (response != null) return Ok(response);
            else return NoContent();
        }
        /// <summary>
        /// Lấy tất cả phòng ban từ database
        /// </summary>
        /// <returns>Tên phòng ban tương ứng</returns>
        /// <response code = "200">: có dữ liệu trả về</response>
        /// <response code = "204">: không có dữ liệu trả về</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("Department")]
        public IActionResult GetDepartmentName()
        {
            var departments = _employeeServices.GetDepartmentName();
            if (departments.Count() > 0) return Ok(departments);
            else return NoContent();
        }

        /// <summary>
        /// Lấy danh sách nhân viên có lọc
        /// </summary>
        /// <param name="pageSize">số lượng nhân viên / trang</param>
        /// <param name="pageIndex">trang số bao nhiêu</param>
        /// <param name="filter">chuỗi để lọc</param>
        /// <returns>Danh sách nhân viên</returns>
        /// <response code = "200">: có dữ liệu trả về</response>
        /// <response code = "204">: không có dữ liệu trả về</response>
        /// CreatedBy: NABANG (18/05/21)

        [HttpGet("Filter")]
        public IActionResult GetEmployees(int pageSize, int pageIndex, string filter)
        {
            var totalRecord = _employeeRepository.GetTotalEmployees(filter);
            var employees = _employeeServices.GetEmployees(pageSize, pageIndex, filter);
            var response = new
            {
                totalRecord = totalRecord,
                data = employees
            };
            if (response.data.Any() && response.totalRecord != 0)
            {
                return Ok(response);
            }
            return NoContent();
        }
    }
}
