using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase where MISAEntity : class
    {
        IBaseRepository<MISAEntity> _dataAccessBaseRepository;
        IBaseServices<MISAEntity> _dataAccessBaseServices;

        public BaseController(IBaseRepository<MISAEntity> dataAccessBaseRepository, IBaseServices<MISAEntity> dataAccessBaseServices)
        {
            _dataAccessBaseRepository = dataAccessBaseRepository;
            _dataAccessBaseServices = dataAccessBaseServices;
        }
        /// <summary>
        /// Lấy tất cả bản ghi từ database
        /// </summary>
        /// <returns>Tất cả bản ghi trong database</returns>
        /// <response code="200">có dữ liệu được trả về, lấy về thành công</response>
        /// <response code="204">không có dữ liệu được trả về, lấy về thất bại</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _dataAccessBaseServices.GetAll();
            if (entities.Count() > 0) return Ok(entities);
            else return NoContent();
        }

        /// <summary>
        /// Lấy bản ghi từ database theo id
        /// </summary>
        /// <param name="entityid"></param>
        /// <returns>1 bản ghi có id tương ứng</returns>
        /// <response code="200">có dữ liệu được trả về, lấy về thành công</response>
        /// <response code="204">không có dữ liệu được trả về, lấy về thất bại</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("{entityid}")]
        public IActionResult GetGetCustomerById(Guid entityid)
        {
            var entity = _dataAccessBaseServices.GetCustomerById(entityid);
            if (entity != null)
            {
                return Ok(entity);
            }
            else
            {
                return NoContent();
            }
        }
        /// <summary>
        /// Lấy bản ghi theo phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Tất cả bản ghi tương ứng với phân trang</returns>
        /// <response code="200">có dữ liệu được trả về, lấy về thành công</response>
        /// <response code="204">không có dữ liệu được trả về, lấy về thất bại</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpGet("Paging")]

        public IActionResult GetPaging(int pageIndex, int pageSize)
        {

            var entities = _dataAccessBaseServices.GetPaging(pageIndex, pageSize);
            if (entities.Count() > 0)
            {
                return Ok(entities);
            }
            else
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Insert 1 bản ghi vào database 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// <response code="200">có số lượng bản ghi bị ảnh hưởng, thêm thành công</response>
        /// <response code="204">không có số lượng bản ghi bị ảnh hưởng, thêm thất bại</response>
        /// CreatedBy: NABANG (18/05/21)

        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            var rowsAffect = _dataAccessBaseServices.Post(entity);

            if (rowsAffect > 0) return Ok(rowsAffect);
            else return NoContent();
        }

        /// <summary>
        /// Update 1 bản ghi vào database (sau khi validate dữ liệu trong service)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// <response code="200">có số lượng bản ghi bị ảnh hưởng, sửa thành công</response>
        /// <response code="204">không có số lượng bản ghi bị ảnh hưởng, sửa thất bại</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] MISAEntity entity)
        {
            //customer.CustomerId = id;
            var properties = typeof(MISAEntity).GetProperties();
            var tableName = typeof(MISAEntity).Name;
            foreach (var item in properties)
            {
                if (item.Name == $"{tableName}id")
                {
                    item.SetValue(entity, id);
                }
            }
            var rowsAffect = _dataAccessBaseServices.Put(entity);
            if (rowsAffect > 0) return Ok(rowsAffect);
            else return NoContent();
        }

        /// <summary>
        /// Xóa 1 bản ghi trong database 
        /// </summary>
        /// <param name="entityid"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// <response code="200">có số lượng bản ghi bị ảnh hưởng, xóa thành công</response>
        /// <response code="204">không có số lượng bản ghi bị ảnh hưởng, xóa thất bại</response>
        /// CreatedBy: NABANG (18/05/21)
        [HttpDelete("{entityid}")]
        public IActionResult Delete(Guid entityid)
        {
            var rowsAffect = _dataAccessBaseServices.Delete(entityid);
            if (rowsAffect > 0) return Ok(rowsAffect);
            else return NoContent();
        }
    }
}
