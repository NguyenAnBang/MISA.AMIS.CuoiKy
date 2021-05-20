<template>

    <div class="dialog" >
     <div class="model"></div>
     <div class="dialog-content">
        <div class="header">
            <div id="title">Thông tin nhân viên</div>
            <button class="btn-check"></button>
            <div class="div">Là khách hàng</div>
            <button class="btn-check"></button>
            <div>Là nhà cung cấp</div>
            <button class="btn-help"></button>
            <button class="btn-X" @click="hideDialogDataCondition()"></button>
        </div>
        <div class="middle">
            <div class="middle-1">
                <div class="column clm1">
                    <div style="display: flex; align-items: center">
                        <div style="width: 35%; margin-right: 2px;">
                            <div style="position: relative"><b>Mã</b> <span style="color: red;">*</span>
                                <div class="error-message" v-show="isValid.employeeCodeMessage == false">Mã nhân viên chưa hợp lệ</div>
                            </div>
                            <input type="text" class="code-blank-box" 
                            :class="{'blank-box-invalid': isValid.employeeCode == false}"
                            ref="employeeCode"  
                            v-model="employee.employeeCode"
                            @mouseenter="mouseEnterError('employeeCode')"
                            @mouseleave="mouseLeaveError()"
                            />
                            
                        </div>
                        <div style="width: 65%; margin-left: 2px;">
                            <div style="position: relative"><b>Tên</b> <span style="color: red;">*</span>
                                <div class="error-message" v-show="isValid.fullNameMessage == false">Tên nhân viên không được để trống</div>
                            </div>
                            <input type="text" class="fullname-blank-box" 
                            :class="{'blank-box-invalid': isValid.fullName == false }" 
                            v-model="employee.fullName"
                            @mouseenter="mouseEnterError('fullName')"
                            @mouseleave="mouseLeaveError()"
                            />
                            
                        </div>
                    </div>
                    <div style="position: relative"><b>Đơn vị</b> <span style="color: red;">*</span>
                        <div class="error-message" v-show="isValid.departmentNameMessage == false">Tên đơn vị chưa hợp lệ</div>
                    </div>
                    <!-- combobox -->
                    <div class="dropdown-text-and-icon" :class="{'blank-box-invalid': isValid.departmentName == false}">
                        <input type="text" class="department-blank-box" 
                        @focus="showDropDownContent()" 
                        @blur="hideDropDownContent()" 
                        id="departmentName"
                        
                        v-model="employee.departmentName"
                        @mouseenter="mouseEnterError('departmentName')"
                        @mouseleave="mouseLeaveError()"
                        @keyup="searchOption()"
                        />
                        <button id="dropdown-icon" @click="showDropDownContent(options)" @blur="hideDropDownContent()"></button>
                    </div>
                    <div id="dropdown">     
                        <div class="dropdown-content" :class="{'dialog_hide': !isShowOption}" >
                            
                            <div class="dropdown-content-a" 
                            :class="{'drop-down-content-selected' : option.id == employee.departmentId}"
                            v-for="option in options" 
                            :key="option.id" 
                            @click="chooseOption(option)" 
                            @mouseenter="enterClick()" 
                            @mouseleave="leaveClick()"
                            >{{option.name}}</div>
                        </div>
                    </div>
                    <div><b>Chức danh</b></div>
                    <input type="text" class="medium-blank-box" v-model="employee.jobTitle"/>
                </div>
                <div class="column clm2">
                    <div style="display: flex; align-items: center">
                        <div style="width: 35%; margin-right: 2px;">
                            <div><b>Ngày sinh</b></div>
                            <datepicker v-model="employee.dateOfBirth" format="dd/MM/yyyy" id="date-input" placeholder="dd/mm/yyyy"/>
                            
                        </div>
                        <div style="width: 65%; margin-left: 2px; padding-left: 6px;">
                            <div><b>Giới tính</b></div>
                            <div class="radio-box" >
                                <input type="radio" 
                                class="btn-radio" 
                                name="gender" 
                                v-model="employee.gender"
                                :value="1"
                                />
                                <div class="gender" >Nam</div>
                                <input type="radio" 
                                class="btn-radio"
                                name="gender" 
                                v-model="employee.gender" 
                                :value="0"
                                />
                                <div class="gender">Nữ</div>
                                <input type="radio" 
                                class="btn-radio" 
                                name="gender" 
                                v-model="employee.gender" 
                                :value="2"
                                />
                                <div class="gender">Khác</div>
                            </div>
                        </div>
                    </div>
                    <div style="display: flex; align-items: center">
                        <div style="width: 65%; margin-right: 2px;">
                            <div><b>Số CMND</b></div>
                            <input type="text" class="fullname-blank-box" v-model="employee.identityNumber"/>
                        </div>
                        <div style="width: 35%; margin-left: 2px;">
                            <div><b>Ngày cấp</b></div>
                            <datepicker v-model="employee.identityDate" format="dd/MM/yyyy" id="date-input" placeholder="dd/mm/yyyy"/>
                        </div>
                    </div>
                    <div><b>Nơi cấp</b></div>
                    <input type="text" class="medium-blank-box" v-model="employee.identityPlace"/>
                </div>
            </div>
            <div class="middle-2">
                <div><b>Địa chỉ</b></div>
                <input type="text" class="large-blank-box" v-model="employee.address"/>
                <div style="display:flex;">
                    <div class="column-1">
                        <div><b>ĐT di động</b>
                            
                        </div>
                        <input type="text" class="small-blank-box" v-model="employee.phone"/>
                        <div><b>Tài khoản ngân hàng</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankAccount"/>
                    </div>
                    <div class="column-1">
                        <div style="position: relative"><b>ĐT cố định</b>
                            
                        </div>
                        <input type="text" class="small-blank-box" 
                        v-model="employee.telephone"
                        />
                        <div><b>Tên ngân hàng</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankName"/>
                    </div>
                    <div class="column-1">
                        <div style="position: relative"><b>Email</b>
                        </div>
                        <input type="text" class="small-blank-box"     
                        v-model="employee.email"
                        />
                        <div><b>Chi nhánh</b></div>
                        <input type="text" class="small-blank-box" v-model="employee.bankBranch"/>
                    </div>
                </div>
            </div>
        </div>
        <div class="footer">
            <button class="btn-small cancel" @click="hideDialogDataCondition()">Hủy</button>
            
            <button class="btn-small post" @click="btnSave()">Cất</button>
            <button class="btn-post-and-put" @click="btnSaveAndAdd()">Cất và thêm</button>

        </div>
     </div>
     <ErrorDialog
     :isShow="isErrorDialogShow"
     :errorMsg="errorMsg"
     @hidePopUp="hidePopUp"
     />


     <ErrorPopUp
     :isShow="isErrorPopUpShow"
     :errorMsg="errorMsg"
     @hidePopUp="hidePopUp"
     />

     <DataChange
     :isShow="isDataChange"
     :errorMsg="errorMsg"
     @hidePopUp="hidePopUp"
     @hideDialog="hideDialog"
     @btnSave="btnSave"
     />
     
    </div>
</template>
<script>
import axios from 'axios'
// import moment from 'moment'
import ErrorDialog from '../../common/pop-up/errorDialog.vue'
import ErrorPopUp from '../../common/pop-up/errorPopUp.vue'
import DataChange from '../../common/pop-up/dataChange.vue'

import datepicker from 'vuejs-datepicker';

const getAll = "https://localhost:44308/api/v1/Employees"
const form = {
    add: "add",
    edit: "edit"
}
const errorMessage = {
    invalidDepartment: "Tên đơn vị không tồn tại trong hệ thống, vui lòng kiểm tra lại.",
    nullFullName: "Tên không được để trống.",
    nullEmployeeCode: "Mã nhân viên không được để trống.",
    nullDepartment: "Tên đơn vị không được để trống.",
    dataChange: "Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
}
const property = {
    fullName: "fullName",
    employeeCode: "employeeCode",
    departmentName: "departmentName"
}
const genderName = {
    male: "Nam",
    female: "Nữ",
    rest: "Khác"
}
export default ({
    components: {
        ErrorDialog,
        ErrorPopUp,
        DataChange,
        datepicker
    },
    setup() {
        
    },
    created() {
    },
    props: {
        
        //Thông tin nhân viên được nhập vào dialog
        employee: {
            type: Object,
            default: null
        },    
        
        //Phân biệt giữa sửa và thêm khi nhấn cất
        formmode: {
            type: String,
            default: null
        },
    },
    updated() {
        //Format giới tính sau khi dialog được hiện ra, để binding dữ liệu
        this.genderFormat(this.employee.gender);
        
    },
    mounted() {
        //auto focus vào ô đầu tiên
        this.focusInput();
    
        this.genderFormat(this.employee.gender);
        //copy giá trị sang một object khác để so sánh có sự thay đổi hay không
        this.initialEmployee = {...this.employee};
    },
    methods: {
        /**
         * Phương thức so sánh 2 object để kiểm tra xem dữ liệu có bị thay đổi chưa
         * CreatedBy: NABANG (19/05/21)
         */
        compareDataObjects(ob1, ob2){
            for (let index in ob2) {
                if(ob2[index] !== ob1[index]){
                    return true;
                }
            }
            return false;
        },
        /**
         * Phương thức ẩn dialog
         * Nếu có dữ liệu bị thay đổi thì hiện pop-up dataChange
         * Nếu không có thì ẩn employeeDetail
         * CreatedBy: NABANG (19/05/21)
         */
        hideDialogDataCondition(){
            //Kiểm tra xem dữ liệu có thay đổi chưa
            if (this.compareDataObjects(this.initialEmployee, this.employee)) {
                this.isDataChange = true;
                this.errorMsg = errorMessage.dataChange;
            }
            else this.hideDialog();

            
        },
        /**
         * Phương thức gọi ẩn form chi tiết employeeDetail
         * CreatedBy: NABANG (20/05/21)
         */
        hideDialog(){
            //Reset lại các biến validate form
            this.isValid.fullName = true;
            this.isValid.employeeCode = true;
            this.isValid.departmentName = true;
            //Gọi đến phương thức ẩn dialog của cha
            this.$emit('hideDialog');
        },
        //Hiện ra dữ liệu cho combobox
        showDropDownContent(){
            this.options = this.initialOptions;
            this.isShowOption = !this.isShowOption;      
            
        },
        //Ẩn đi dữ liệu cho combobox
        hideDropDownContent(){
            if(this.overClick == false) this.isShowOption = false;
        },
        //Gán dữ liệu được chọn từ combobox
        chooseOption(option){
            //Gán giá trị được chọn cho id và tên phòng ban của employee
            this.employee.departmentId = option.id;
            // this.employee.departmentName = option.name;
            this.departmentFormat(this.employee.departmentId);
            this.overClick = false;
            this.hideDropDownContent();
        },
        //Tìm kiếm và hiện giá trị của các option tương ứng với tìm kiếm
        searchOption(){
            this.options = this.initialOptions.filter(option => {
                return (
                    option.name.toLowerCase().includes(this.employee.departmentName.toLowerCase())
                )
            })
        },
        //Khi di chuyển chuột vào trong các option
        enterClick(){
            //Gán overClick = true để tránh lỗi click và focusout overlapping (khi click thì focusout sẽ chạy trước mà không chạy click)
            this.overClick = true;
        },
        //Khi di chuyển chuột ra khỏi các option
        leaveClick(){
            this.overClick = false;
        },

        //auto focus vào ô input đầu tiên sau khi hiện dialog
        focusInput(){
            this.$refs.employeeCode.focus();
        },
        //format giới tính để binding vào form
        genderFormat(gender){
            if(gender == 0) {
                this.employee.genderName = genderName.female;
            }
            else if(gender == 1) {
                this.employee.genderName = genderName.male;
            }
            else if(gender == 2){
                this.employee.genderName = genderName.rest;
            }
        },
        //Format tên phòng ban để binding vào form
        departmentFormat(departmentId){
            this.options.forEach(option => {
                    if(departmentId == option.id) {
                        this.employee.departmentName = option.name;
                    }
                });
        },
        
        /**
         * Kiểm tra xem giá trị có null không
         * CreatedBy: NABANG (19/05/21)
         */
        nullValidation(propertyValue, propertyName){
            propertyValue = propertyValue || '';
            if (propertyValue.trim() == "") {
                if(propertyName == property.employeeCode) {
                    this.isValid.employeeCode = false;
                    this.isErrorPopUpShow = true;
                    this.errorMsg = errorMessage.nullEmployeeCode;
                }
                else if(propertyName == property.fullName) {
                    this.isValid.fullName = false;
                    this.isErrorPopUpShow = true;
                    this.errorMsg = errorMessage.nullFullName;
                }
            } 
            else{
                if(propertyName == property.employeeCode) {
                    this.isValid.employeeCode = true;
                }
                else if(propertyName == property.fullName) {
                    this.isValid.fullName = true;
                }
            }    
        },
        
        
        //Validate tên đơn vị
        departmentNameValidation(value){
            //Validate tên đơn vị không được trống
            value = value || '';
            if (value.trim() == "") {
                this.isValid.departmentName = false;
                this.isErrorPopUpShow = true;
                this.errorMsg = errorMessage.nullDepartment;
            }
            else {
                this.isValid.departmentName = false;
                //Validate tên đơn vị giống với tên đơn vị trong option
                //Kiểm tra xem thông tin nhập vào có đúng với các option tên đơn vị không
                this.options.forEach(option => {
                    if(option.name == value) {
                        this.isValid.departmentName = true;
                        this.employee.departmentId = option.id;
                    }
                });
                if(this.isValid.departmentName == false) 
                {
                    this.isErrorPopUpShow = false;
                    this.isErrorDialogShow = true;
                    this.errorMsg = errorMessage.invalidDepartment;
                }
            }
        },
        
        
        //Ẩn pop-up thông báo trùng mã nhân viên và pop-up thông báo tên đơn vị không hợp lệ
        //Ẩn pop-up phân biệt post hoặc put dữ liệu
        hidePopUp(){
            //đóng thông báo trùng mã, tên đơn vị không trong hệ thống
            this.isErrorDialogShow = false;
            //Đóng thông báo mã null, tên đơn vị null, mã nhân viên null
            this.isErrorPopUpShow = false;
            //Đóng thông báo đã có dữ liệu bị thay đổi
            this.isDataChange = false;
            //
            this.errorMsg = "";
        },
        
        //Validate form trước khi gửi lên database 
        formValidation(){
            //Validate tên nhân viên
            this.nullValidation(this.employee.fullName, property.fullName);
            //Validate mã nhân viên
            this.nullValidation(this.employee.employeeCode, property.employeeCode);
            //Validate tên đơn vị
            this.departmentNameValidation(this.employee.departmentName);
            if(this.isValid.fullName == true
                && this.isValid.employeeCode == true
                && this.isValid.departmentName == true
            ) this.isAppropriate = true;
            else this.isAppropriate = false;
        
            
        },
        //Di chuyển chuột vào trong hiện lỗi
        mouseEnterError(propertyName){
            if( this.isValid.employeeCode == false && propertyName == property.employeeCode) 
                this.isValid.employeeCodeMessage = false;
            else if( this.isValid.fullName == false && propertyName == property.fullName) 
                this.isValid.fullNameMessage = false;
            if( this.isValid.departmentName == false && propertyName == property.departmentName) 
                this.isValid.departmentNameMessage = false;
        },
      
        
        
        //Di chuyển chuột ra ngoài ẩn lỗi
        mouseLeaveError(){
            this.isValid.employeeCodeMessage = true;
            this.isValid.fullNameMessage = true;
            this.isValid.departmentNameMessage = true;
        },
        
        
        //Phân biệt put và post
        formmodeValidation(){
            if (this.formmode == form.add) {
                return axios.post(getAll, this.employee)
                .then(res => {
                    console.log(res);
                    return Promise.resolve();  
                })
                .catch(res => { 
                    this.errorMsg = res.response.data.devMsg;
                    this.isErrorDialogShow = true;
                    return Promise.reject();
                })
            }
            else if (this.formmode == form.edit) {
                return axios.put(getAll + "/" + this.employee.employeeId, this.employee)
                .then(res => {
                    console.log(res); 
                    return Promise.resolve();            
                })
                .catch(res => {
                    this.errorMsg = res.response.data.devMsg;
                    this.isErrorDialogShow = true;
                    return Promise.reject();
                })
            }
        },
        //Reset thông tin nhân viên lớn nhất
        resetEmployee(){
            this.$emit('showEmployeeDialog');   
            this.focusInput();
        },
        //Lưu dữ liệu lên database (để cập nhật dữ liệu)
        btnSave(){
            this.formValidation();
            if(this.isAppropriate == true){
                this.formmodeValidation().then(() => this.hideDialog());
            } 
        },
        //Lưu dữ liệu lên database (để thêm dữ liệu)
        btnSaveAndAdd(){
            this.formValidation();
            if(this.isAppropriate == true){             
                this.formmodeValidation().then(() => this.resetEmployee()); 
            } 
        }
    },
    data() {
        return {
            //Biến kiểm tra xem dữ liệu combobox có hiện ra hay không
            isShowOption: false,
            
            //Option được chọn trong dropbox
            selectedOption: {
                id: null,
                name: null
            },
            //Danh sách các lựa chọn
            options: [
                {
                    id: "142cb08f-7c31-21fa-8e90-67245e8b283e",
                    name: "Phòng Đào tạo"
                },
                {
                    id: "17120d02-6ab5-3e43-18cb-66948daf6128",
                    name: "Phòng Kế toán"
                },
                {
                    id: "469b3ece-744a-45d5-957d-e8c757976496",
                    name: "Phòng Marketing"
                },
                {
                    id: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
                    name: "Phòng Nhân sự"
                },
            ],
            //Danh sách các lựa chọn ban đầu (không thay đổi)
            initialOptions: [
                {
                    id: "142cb08f-7c31-21fa-8e90-67245e8b283e",
                    name: "Phòng Đào tạo"
                },
                {
                    id: "17120d02-6ab5-3e43-18cb-66948daf6128",
                    name: "Phòng Kế toán"
                },
                {
                    id: "469b3ece-744a-45d5-957d-e8c757976496",
                    name: "Phòng Marketing"
                },
                {
                    id: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
                    name: "Phòng Nhân sự"
                },
            ],
            //Biến kiểm tra xem chuột có di chuyển vào các option hay không, để phân biệt click với blur
            overClick: false,
            //Biến kiểm tra xem ô đầu tiên có được focus không
            focusFirstInput: true,
            //Biến kiểm tra xem tên đơn vị (departmentName) đã được format chưa
            formatDepartmentName: true,
            
            
            isValid: {
                employeeCode: true,
                fullName: true,
                departmentName: true,
                employeeCodeMessage: true,
                fullNameMessage: true,
                departmentNameMessage: true,
            },
            
            //Biển để hiện dialog thông báo trùng mã nhân viên
            isErrorDialogShow: false,
            //Biến để hiện pop-up thông báo trống mã hoặc tên
            isErrorPopUpShow: false,
            //Biến để nhận giá trị của employee truyền vào ban đầu, để so sánh sau khi thay đổi
            initialEmployee: {},
            //Biến để hiện thông báo đã có thay đổi dữ liệu
            isDataChange: false,
            //Biến để nhận thông báo lỗi trả về từ API
            errorMsg: "",
            
            //Biến để xác nhận thông tin nhân viên đã hợp lệ để put/post lên database
            isAppropriate: null,
        }
    },
})
</script>

<style scoped>
b{
    font-size: 13px;
}

.dialog_hide {
    display:none;
}
.model {
    position: fixed;
    top: 0px;
    left: 0px;
    right: 0px;
    bottom: 0px;
    background-color: #000000;
    opacity: .4;
    z-index: 2;
}
.dialog-content{
    position: fixed;
    width: 900px;
    height: 625px;
    top: calc(50% - 312px);
    left: calc(50% - 450px);
    background-color: white;
    border-radius: 4px;
    box-sizing: border-box;
    z-index: 3;
}
.header{
    display: flex;
    align-items: center;
    width: 100%;
    height: 60px;
    width: 100%;
    padding: 20px 32px;
    padding-right: 12px;
    position: relative;
    overflow: hidden;
    border-radius: 5px 5px 0 0;
    box-sizing: border-box;
}
#title{
    font-size: 24px;
    color: #111;
    font-weight: 700;
}
.btn-check{
    width: 18px;
    height: 18px;
    cursor: pointer;
    margin-left: 24px;
    margin-right: 8px;
}
.div{
    margin-right: 24px;
}
.btn-help{
    height: 48px;
    width: 48px;
    position: absolute;
    right: 45px;
    top: 0px;
    min-width: 24px;
    min-height: 24px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -70px -132px;
    border: none;
    outline: none;
    border-radius: 0px 4px 0px 4px;
}
.btn-X{
    height: 48px;
    width: 48px;
    position: absolute;
    right: 0px;
    top: 0px;
    min-width: 24px;
    min-height: 24px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -134px -132px;
    border: none;
    outline: none;
    border-radius: 0px 4px 0px 4px;
}
.middle{
    padding: 24px;
}
.middle-1{
    display: flex;
}
.column{
    width: 420px;
    height: 220px;
}
.clm1{
    margin-right: 12px;
}
.clm2{
    margin-left: 12px;
}
.code-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.code-blank-box:focus{
    border-color: #2ca01c;
}
/* #date-input{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.date-blank-box:focus{
    border-color: #2ca01c;
} */

.radio-box{
    width: 100%;
    font-size: 13px;
    height: 32px;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    display: flex;
    align-items: center;
}
.radio-box .gender{
    margin-right: 16px;
}
.btn-radio{
    
    /* margin-left: 16px; */
    margin-right: 10px;
    cursor: pointer;
}

.fullname-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.fullname-blank-box:focus{
    border-color: #2ca01c;
}
.medium-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    display: flex;
    align-items: center;
    outline: none;
}
.medium-blank-box:focus{
    border-color: #2ca01c;
}
.dropdown-text-and-icon{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: -2px;
    border-radius: 3px;
    /* box-sizing: border-box; */
    display: flex;
    align-items: center;
}
.dropdown-text-and-icon:focus-within{
    border-color: #2ca01c;
}
.department-blank-box{
    height: 30px;
    width: calc(100% - 32px);
    /* padding: 6px 0 6px 12px; */
    box-sizing: border-box;
    border: none;
    outline: none;
    border-collapse: collapse;
    border-radius: 4px;
}
#dropdown-icon{
    width: 30px;
    height: 30px;
    border: none;
    outline: none;
    border-collapse: collapse;
    border-radius: 4px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    background-position: -545px -352px;
    transform: rotate(0deg);
    transition: transform .15s linear;
    /* background-color: aqua; */
}

.large-blank-box{
    width: 100%;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    border-radius: 3px;
    outline: none;
}
.large-blank-box:focus{
    border-color: #2ca01c;
}
.column-1{
    width: 220px;
    height: 100px;
}
.small-blank-box{
    width: 214px;
    padding: 6px 10px;
    font-size: 13px;
    height: 32px;
    border: 1px solid #babec5;
    box-sizing: border-box;
    margin-top: 8px;
    margin-bottom: 16px;
    outline: none;
    border-radius: 3px;
}
.small-blank-box:focus{
    border-color: #2ca01c;

}
.footer{
    position: absolute;
    bottom: 0px;
    width: calc(100% - 48px);
    height: 68px;
    display: flex;
    align-items: center;
    /* background-color: aqua; */
    margin-left: 24px;
    margin-right: 24px;
    box-sizing: border-box;
    border-top: 1px solid #ccc;
}
.btn-small{
    height: 36px;
    padding: 8px 20px;
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
    border-radius: 3px;
    cursor: pointer;
}
.btn-post-and-put{
    position: absolute;
    right: 0px;
    height: 36px;
    padding: 8px 20px;
    border: 1px solid transparent;
    color: #fff;
    background-color: #2ca01c;
    border-radius: 3px;
    cursor: pointer;
}
.post{
    position: absolute;
    right: 124px;
}

#dropdown{
        position: relative;
        display: inline-block;
        width: 100%;
        /* background-color: #2ca01c; */
    }
    .dropdown-content{
        /* height: 120px; */
        width: 410px;
        
        top: -6px;
        position: absolute;
        /* right: 0px; */
        border: 1px solid;
        background-color: #fff;
        border-radius: 3px;
        border: 1px solid #babec5;
    }
    .dropdown-content-a{
        /* position: absolute; */
        height: 24px;
        width: 100%;
        display: flex;
        align-items: center;
        padding-left: 12px;
        box-sizing: border-box;
        border: none;
        outline: none;
        font-size: 13px;
        margin-bottom: 2px;
    }
    .dropdown-content-a:hover{
        color: #2ca01c;
        background-color: rgb(219, 219, 219);
    }
    .drop-down-content-selected{
        background-color: #2ca01c;
        color: #fff;
    }
    .drop-down-content-selected:hover{
        background-color: #2ca01c;
        color: #fff;
    }

.blank-box-invalid {
    border-color: #F65454;
    outline: none;
}
.error-message{
    position: absolute;
    top: 50px;
    left: calc(50% - 80px);
    background-color: black;
    color: #babec5;
    font-size: 11px;
    padding-left: 8px;
    padding-top: 2px;
    padding-bottom: 2px;
    padding-right: 8px;
    width: 160px;
    border-radius: 4px;
}

</style>