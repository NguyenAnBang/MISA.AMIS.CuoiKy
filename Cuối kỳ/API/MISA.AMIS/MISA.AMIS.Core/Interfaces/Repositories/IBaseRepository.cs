using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Repositories
{
    public interface IBaseRepository<MISAEntity> where MISAEntity : class
    {
        
        /// <summary>
        /// Lấy tất cả bản ghi từ database
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<MISAEntity> GetAll();
        
        /// <summary>
        /// Lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Bản ghi tương úng với id</returns>
        /// CreatedBy: NABANG (18/05/21)
        public MISAEntity GetCustomerById(Guid entityId);
        
        /// <summary>
        /// Insert 1 bản ghi vào database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Post(MISAEntity entity);

        /// <summary>
        /// Update 1 bản ghi theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Put(MISAEntity entity);

        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Delete(Guid entityId);
        
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Dữ liệu bản ghi phân trang</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<MISAEntity> GetPaging(int pageIndex, int pageSize);
    }
}
