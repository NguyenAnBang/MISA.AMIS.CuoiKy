using MISA.AMIS.Core.AttributeCustom;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repositories;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseServices<MISAEntity> : IBaseServices<MISAEntity> where MISAEntity : class
    {
        IBaseRepository<MISAEntity> _dataAccessBaseRepository;

        public BaseServices(IBaseRepository<MISAEntity> dataAccessBaseRepository)
        {
            _dataAccessBaseRepository = dataAccessBaseRepository;
        }
        /// <summary>
        /// Xóa 1 bản ghi theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Delete(Guid entityId)
        {
            var rowsAffect = _dataAccessBaseRepository.Delete(entityId);
            return rowsAffect;
        }
        /// <summary>
        /// Lấy tất cả bản ghi từ database, nhận giá trị từ dataAccessBaseRepository
        /// </summary>
        /// <returns>Tất cả dữ liệu từ database</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<MISAEntity> GetAll()
        {
            var response = _dataAccessBaseRepository.GetAll();

            return response;
        }
        /// <summary>
        /// Lấy 1 bản ghi từ database dựa theo id
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Dữ liệu bản ghi tương ứng với id</returns>
        /// CreatedBy: NABANG (18/05/21)
        public MISAEntity GetCustomerById(Guid entityId)
        {
            var response = _dataAccessBaseRepository.GetCustomerById(entityId);

            return response;
        }
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns>Dữ liệu phân theo trang</returns>
        /// CreatedBy: NABANG (18/05/21)
        public IEnumerable<MISAEntity> GetPaging(int pageIndex, int pageSize)
        {
            if (pageIndex < 0 || pageSize < 0)
            {
                var response = new
                {
                    devMsg = Properties.Resources.Invalid_Paging_number,
                    MISACode = Properties.Resources.MISACode
                };
                throw new EmployeeException(response.devMsg);
            }


            var result = _dataAccessBaseRepository.GetPaging(pageIndex, pageSize);
            return result;
        }
        /// <summary>
        /// Insert 1 bản ghi vào database, nhận dữ liệu từ dataAccessBaseRepository, validate dữ liệu, rồi đẩy dữ liệu về controller
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Post(MISAEntity entity)
        {
            //Validate dữ liệu
            Validate(entity, HttpType.POST);
            var rowsAffect = _dataAccessBaseRepository.Post(entity);
            return rowsAffect;
        }

        /// <summary>
        /// Update 1 bản ghi vào database dựa theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số lượng bản ghi bị ảnh hưởng</returns>
        /// CreatedBy: NABANG (18/05/21)
        public int Put(MISAEntity entity)
        {
            Validate(entity, HttpType.PUT);
            var rowsAffect = _dataAccessBaseRepository.Put(entity);
            return rowsAffect;
        }
        /// <summary>
        /// Kiểm tra dữ liệu, trả về exception nếu gặp lỗi
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="http"></param>
        /// <returns>Hàm void không return gì cả</returns>
        /// CreatedBy: NABANG (18/05/21)
        private void Validate(MISAEntity entity, HttpType http)
        {
            //Viết những đoạn mã validate chung

            //Lấy ra tất cả property của class
            var properties = typeof(MISAEntity).GetProperties();
            foreach (var property in properties)
            {
                //Lấy giá trị
                var propertyValue = property.GetValue(entity);
                //Kiểm tra required field có null không, nếu có thì throw exception
                //Lấy ra property RequiredField
                var requiredProperties = property.GetCustomAttributes(typeof(RequiredField), true);

                if (requiredProperties.Length > 0)
                {
                    //Kiểm tra nếu giá trị là null
                    if (propertyValue == null)
                    {
                        propertyValue = "";
                    }
                    //Kiểm tra nếu giá trị bị bỏ trống
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        var msgError = (requiredProperties[0] as RequiredField).MsgError;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = $"{property.Name} {Properties.Resources.Null_msg}";
                        }
                        throw new EmployeeException(msgError);
                    }


                }
                //Kiểm tra email field có đúng định dạng hay không, nếu có thì throw exception
                //Lấy ra property emailField
                var emailProperties = property.GetCustomAttributes(typeof(EmailField), true);
                if (emailProperties.Length > 0)
                {
                    //Kiểm tra giá trị
                    if (!Regex.IsMatch(propertyValue.ToString(), Properties.Resources.Regex_email))
                    {
                        var response = new
                        {
                            devMsg = Properties.Resources.Email_Invalid_msg,
                            MISACode = Properties.Resources.MISACode
                        };
                        throw new EmployeeException(response.devMsg);
                    }
                }
                //Kiểm tra phoneNumberField có đúng định dạng hay không, nếu có thì throw exception
                //Lấy ra property phoneNumberField
                var phoneNumberProperties = property.GetCustomAttributes(typeof(PhoneNumberField), true);
                if (phoneNumberProperties.Length > 0)
                {
                    //Kiểm tra giá trị
                    if (!Regex.IsMatch(propertyValue.ToString(), Properties.Resources.Regex_phoneNumber))
                    {
                        var response = new
                        {
                            devMsg = Properties.Resources.PhoneNumber_Invalid_msg,
                            MISACode = Properties.Resources.MISACode
                        };
                        throw new EmployeeException(response.devMsg);
                    }
                }
                //Kiểm tra MaxLengthField không quá 20 kí tự, nếu quá thì throw exception
                //Lấy ra property MaxLengthField
                var maxLengthProperties = property.GetCustomAttributes(typeof(MaxLengthField), true);
                if (maxLengthProperties.Length > 0)
                {
                    var maxLength = (maxLengthProperties[0] as MaxLengthField).MaxLength;
                    if (propertyValue.ToString().Length > maxLength)
                    {
                        var msgError = (maxLengthProperties[0] as MaxLengthField).MsgError;
                        if (string.IsNullOrEmpty(msgError))
                        {
                            msgError = $"{property.Name} {Properties.Resources.Max_length_msg} {maxLength}";
                        }
                        throw new EmployeeException(msgError);
                    }
                }

            }
            CustomValidate(entity, http);
        }

        protected virtual void CustomValidate(MISAEntity entity, HttpType http)
        {
        }
    }
}
