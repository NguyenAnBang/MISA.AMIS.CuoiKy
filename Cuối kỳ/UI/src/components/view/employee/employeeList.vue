<template>
  <div id="router">
    <!-- <router-view></router-view> -->
    <div id="router-content">
      <div id="title-bar">
        <div class="title">Nhân viên</div>
        <div class="btn-wrapper">
          <button id="btn-add" @click="showEmployeeDialog()">
            Thêm mới nhân viên
          </button>
        </div>
      </div>
      <div id="tablewrapper">
        <div id="table">
          <div id="search">
            <div id="search-bar-wrapper">
              <div class="search-bar-and-icon">
                <input
                  id="search-bar"
                  type="text"
                  placeholder="Tìm theo mã, tên nhân viên"
                  v-model="message"
                  @keyup="searchData()"
                />
                <button id="search-icon"></button>
              </div>

              <div id="btn-refresh-wrapper">
                <button id="btn-refresh" @click="refreshData()"></button>
                <button id="btn-export" @click="exportData()"></button>
              </div>
            </div>
          </div>
          

          <div class="data">
            <div class="scroll">
              <table class="table">
                <thead>
                  <tr>
                    <th>
                      <input type="checkbox" v-model="checkAll" />
                    </th>
                    <th style="min-width: 100px">MÃ NHÂN VIÊN</th>
                    <th style="min-width: 200px">TÊN NHÂN VIÊN</th>
                    <th style="min-width: 50px">GIỚI TÍNH</th>
                    <th style="min-width: 110px; text-align: center;">NGÀY SINH</th>
                    <th style="min-width: 100px">SỐ CMND</th>
                    <th style="min-width: 100px">CHỨC DANH</th>
                    <th style="min-width: 150px">TÊN ĐƠN VỊ</th>
                    <th style="min-width: 100pxpx">SỐ TÀI KHOẢN</th>
                    <th style="min-width: 100pxpx">TÊN NGÂN HÀNG</th>
                    <th style="min-width: 100px">CHI NHÁNH TK NGÂN HÀNG</th>
                    <!-- <th style="min-width: 83.33%">CHỨC NĂNG</th> -->
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="employee in employees" :key="employee.employeeId">
                    <td>
                      <input
                        type="checkbox"
                        v-model="checked"
                        :value="employee.employeeId"
                        />
                    </td>
                    <td>{{ employee.employeeCode }}</td>
                    <td>{{ employee.fullName }}</td>
                    <td>{{ employee.genderName }}</td>
                    <td style="text-align: center;">{{ dateFormat(employee.dateOfBirth) }}</td>
                    <td>{{ employee.identityNumber }}</td>
                    <td>{{ employee.jobTitle }}</td>
                    <td>{{ employee.departmentName }}</td>
                    <td>{{ employee.bankAccount }}</td>
                    <td>{{ employee.bankName }}</td>
                    <td>{{ employee.bankBranch }}</td>
                    
                  </tr>
                </tbody>
              </table >
              <table class="table1" style="position: sticky; z-index=3; top:0; right: 0; border:0;">
                <thead>
                  <tr style="border-top: 0px solid #ccc;">
                  <th style="min-width: 100px; postion: sticky; top:0;background-color: #f4f5f6; text-align: center;">CHỨC NĂNG</th>
                  </tr>
                </thead>
                <tbody style="background-color: white ">
                    <tr v-for="employee in employees" :key="employee.employeeId"  style="background: white border: 1px solid #ccc;">
                      <td>
                      
                      <Option
                        @showDeleteDialog="showDeleteDialog(employee.employeeId)"
                        @showEmployeeDetail="
                          showEmployeeDetail(employee.employeeId)
                        "
                        @showEmployeeDuplicate="
                          showEmployeeDuplicate(employee.employeeId)
                        "
                      />
                    </td>
                    </tr>
                </tbody>
              </table>
            </div>
          </div>

          <div id="footer">
            <div id="total-data">
              Tổng số <b>{{ this.employeeNumber }}</b> bản ghi
            </div>
            <div id="footer-right">
              <div id="footer-2">
                <ComboBox @setPerPage="handlePerPage" ref="comboBox" />
              </div>
              <div id="footer-3">
                
                <Paging
                  :totalPages="totalPage"
                  :perPage="perPage"
                  :page="currentPage"
                  @onChangePage="onPageChange"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    
    <EmployeeDetail
      v-if="isShowDialogEmployee"
      @hideDialog="hideDialog"
      :employee="selectedEmployee"
      :formmode="formmode"
      @showEmployeeDialog="showEmployeeDialog"
    />
    <EmployeeDelete
      :isShow="isShowDialogDelete"
      @hideDialog="hideDialog"
      :employee="selectedEmployee"
    />
    <div class="fa-3x" v-if="isBusy">
      <i class="fas fa-spinner fa-spin" style="color: green"></i>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import Option from "../../common/option.vue";
import ComboBox from "../../common/comboBox.vue";
// import Pagination from '../../common/pagination.vue'
import Paging from "../../common/paging.vue";
import EmployeeDetail from "./employeeDetail.vue";
import EmployeeDelete from "./employeeDelete.vue";

const getAll = "https://localhost:44308/api/v1/Employees";
const getMaxCode = "https://localhost:44308/api/v1/Employees/MaxCode";
const getExport = "https://localhost:44308/api/v1/Employees/Export";
const getDepartments = "https://localhost:44308/api/v1/Employees/Department";
const getFilter = "https://localhost:44308/api/v1/Employees/Filter?";
const gender = {
  male: "Nam",
  female: "Nữ",
  rest: "Khác",
};
const form = {
  add: "add",
  edit: "edit",
};
export default {
  components: {
    Option,
    ComboBox,
    //Pagination,
    EmployeeDetail,
    EmployeeDelete,
    Paging,
  },
  created() {
    // load dữ liệu cho trang
    //Mặc định sau khi lấy xong dữ liệu thì số bản ghi 1 trang là 10 bản ghi
    //CurrentPage là 1
    this.currentPage = 1;
    this.perPage = 20;
    this.message = "";
    axios
      .get(
        getFilter +
          "pageSize=" +
          this.perPage +
          "&" +
          "pageIndex=" +
          this.currentPage +
          "&" +
          "filter=" +
          this.message
      )
      .then((res) => {
        console.log(res);
        this.employees = res.data.data;

        //Mặc định sau khi lấy xong dữ liệu thì tự động format giới tính (genderName) theo giới tính (gender) trả về từ api
        this.genderFormat(this.employees);

        //Mặc định sau khi lấy xong dữ liệu thì tự động format tên phòng ban (departmentName) theo id phòng ban (departmentId) trả về từ api
        this.departmentFormat(this.employees);
        /**Số lượng bản ghi hợp lệ */
        this.employeeNumber = res.data.totalRecord;
        if (this.employeeNumber % this.perPage == 0) {
          this.totalPage = this.employeeNumber / this.perPage;
        } else {
          this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
        }

        this.isBusy = false;
      })
      .catch((res) => {
        console.log(res);
      });

    axios.get(getAll)
      .then((res) => {
        this.initialEmployees = res.data;
      })
      .catch((res) => {
        console.log(res);
      })
  },
  mounted: function () {},
  data() {
    return {
      initialEmployees: [], //Dữ liệu đầu vào cho api getAll
      employees: [], //dữ liệu đầu vào (axios.get) -> để gán dữ liệu //Danh sách nhân viên

      employeeNumber: 0, //Tổng số bản ghi

      departments: [], //Dữ liệu phòng ban (đơn vị) từ database

      isShowDialogEmployee: false, //Kiểm tra đóng/mở dialog thêm nhân viên

      isShowDialogDelete: false, //Kiểm tra đóng/mở dialog xóa nhân viên

      message: null, //Biến nhận giá trị tìm kiếm

      selectedEmployee: {}, //Dữ liệu của nhân viên được chọn

      checked: [], //Danh sách được check vào checkbox

      currentPage: 0, //Dấu trang

      perPage: 0, //Số lượng bản ghi được hiển thị trong 1 trang

      totalPage: 0, //Số lượng trang

      isBusy: true, //Kiểm tra xem data đã load xong chưa

      formmode: "", //Phân biệt giữa sửa và thêm
    };
  },
  methods: {
    /**
     * Lấy tất cả dữ liệu từ API
     * CreatedBy: NABANG (20/05/21)
     */
    getAllData(){
      axios.get(getAll)
      .then((res) => {
        this.initialEmployees = res.data;
      })
      .catch((res) => {
        console.log(res);
      })
    },
    /**
     * refresh lại dữ liệu của trang
     * CreatedBy: NABANG (20/05/21)
    */
    refreshData() {
      this.currentPage = 1;
      this.perPage = 20;
      //Load lại data
      this.loadData();
      //Mặc định option được chọn trong comboBox luôn là 10 bản ghi 1 trang khi load lại data
      this.$refs.comboBox.resetPerPage();
      
    },

    //Load lại dữ liệu cho trang (không refresh)

    loadData() {
      this.isBusy = true;
      this.message = "";
      axios
        .get(
          getFilter +
            "pageSize=" +
            this.perPage +
            "&" +
            "pageIndex=" +
            this.currentPage +
            "&" +
            "filter=" +
            this.message
        )
        .then((res) => {
          console.log(res);
          this.employees = res.data.data;

          //Mặc định sau khi lấy xong dữ liệu thì tự động format giới tính (genderName) theo giới tính (gender) trả về từ api
          this.genderFormat(this.employees);

          //Mặc định sau khi lấy xong dữ liệu thì tự động format tên phòng ban (departmentName) theo id phòng ban (departmentId) trả về từ api
          this.departmentFormat(this.employees);
          /**Số lượng bản ghi hợp lệ */
          this.employeeNumber = res.data.totalRecord;
          if (this.employeeNumber % this.perPage == 0) {
            this.totalPage = this.employeeNumber / this.perPage;
          } else {
            this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
          }

          this.isBusy = false;
        })
        .catch((res) => {
          console.log(res);
        });

      //Load lại tất cả data
      this.getAllData();
    },

    //hiện dialog thêm nhân viên
    showEmployeeDialog() {
      axios
        .get(getMaxCode)
        .then((res) => {
          this.isShowDialogEmployee = true;
          this.formmode = form.add;
          this.selectedEmployee = {};
          this.selectedEmployee.employeeCode = res.data;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    //Hiện dialog sửa thông tin nhân viên
    showEmployeeDetail(employeeId) {
      //Get dữ liệu từ APi về phù hợp với bản ghi
      return axios
        .get(getAll + "/" + employeeId)
        .then((res) => {
          this.isShowDialogEmployee = true;
          this.formmode = form.edit;
          this.selectedEmployee = res.data;
          //Vì lấy theo id chưa format giới tính và tên đơn vị
          //Nên phải format riêng
          //Trong employeeList đã làm format tên phòng ban nên không cần format trong employeeDetail
          this.departments.forEach((department) => {
            if (this.selectedEmployee.departmentId == department.departmentId)
              this.selectedEmployee.departmentName = department.departmentName;
          });

          return Promise.resolve();
        })
        .catch((res) => {
          console.log(res);
          return Promise.reject();
        });
    },
    //Hiện dialog nhân bản thông tin nhân viên
    showEmployeeDuplicate(employeeId) {
      this.showEmployeeDetail(employeeId).then(() =>
        axios
          .get(getMaxCode)
          .then((res) => {
            this.selectedEmployee.employeeCode = res.data;
            this.formmode = form.add;
          })
          .catch((res) => {
            console.log(res);
          })
      );
    },
    //ẩn dialog thêm nhân viên và dialog xóa nhân viên
    hideDialog() {
      this.isShowDialogEmployee = false;
      this.isShowDialogDelete = false;
      this.loadData();
    },

    //hiện dialog xác nhận xóa nhân viên
    showDeleteDialog(employeeId) {
      this.isShowDialogDelete = true;
      //Get dữ liệu từ APi về phù hợp với bản ghi
      axios
        .get(getAll +"/" + employeeId)
        .then((res) => {
          this.selectedEmployee = res.data;
        })
        .catch((res) => {
          console.log(res);
        });
    },

    //Hiện kết quả tìm kiếm dựa trên mã nhân viên hoặc tên nhân viên
    searchData() {
      this.currentPage = 1;
      axios
        .get(
          getFilter +
            "pageSize=" +
            this.perPage +
            "&" +
            "pageIndex=" +
            this.currentPage +
            "&" +
            "filter=" +
            this.message
        )
        .then((res) => {
          console.log(res);
          this.employees = res.data.data;

          //Mặc định sau khi lấy xong dữ liệu thì tự động format giới tính (genderName) theo giới tính (gender) trả về từ api
          this.genderFormat(this.employees);

          //Mặc định sau khi lấy xong dữ liệu thì tự động format tên phòng ban (departmentName) theo id phòng ban (departmentId) trả về từ api
          this.departmentFormat(this.employees);
          /**Số lượng bản ghi hợp lệ */
          this.employeeNumber = res.data.totalRecord;
          if (this.employeeNumber % this.perPage == 0) {
            this.totalPage = this.employeeNumber / this.perPage;
          } else {
            this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },
    //format date of birth
    dateFormat(dateOfBirth) {
      var newDate = new Date(dateOfBirth);
      var stringDate = newDate.getDate();
      if (stringDate < 10) stringDate = "0" + stringDate;
      var stringMonth = newDate.getMonth() + 1;
      if (stringMonth < 10) stringMonth = "0" + stringMonth;
      var stringYear = newDate.getFullYear();
      return stringDate + "/" + stringMonth + "/" + stringYear;
    },
    //format giới tính
    genderFormat(array) {
      array.forEach((element) => {
        if (element.gender == 0) element.genderName = gender.female;
        else if (element.gender == 1) element.genderName = gender.male;
        else element.genderName = gender.rest;
      });
    },
    //format tên phòng ban
    departmentFormat(array) {
      //Lấy tất cả dữ liệu phòng ban từ database
      axios
        .get(getDepartments)
        .then((res) => {
          this.departments = res.data;
          //Gán giá trị departmentName của từng nhân viên với id tương ứng trong dữ liệu phòng ban trả về từ api
          array.forEach((element) => {
            this.departments.forEach((department) => {
              if (element.departmentId == department.departmentId)
                element.departmentName = department.departmentName;
            });
          });
        })
        .catch((res) => {
          console.log(res);
        });
    },
    //Xuất dữ liệu ra file excel
    exportData() {
      window.open(getExport);
    },

    //Thay đổi số trang, thay đổi currentPage
    onPageChange(page) {
      if (this.employeeNumber % this.perPage == 0) {
        this.totalPage = this.employeeNumber / this.perPage;
      } else {
        this.totalPage = Math.floor(this.employeeNumber / this.perPage) + 1;
      }
      if (page > 0 && page <= this.totalPage) {
        this.currentPage = page;

        axios
          .get(
            getFilter +
              "pageSize=" +
              this.perPage +
              "&" +
              "pageIndex=" +
              this.currentPage +
              "&" +
              "filter=" +
              this.message
          )
          .then((res) => {
            console.log(res);
            this.employees = res.data.data;

            //Mặc định sau khi lấy xong dữ liệu thì tự động format giới tính (genderName) theo giới tính (gender) trả về từ api
            this.genderFormat(this.employees);

            //Mặc định sau khi lấy xong dữ liệu thì tự động format tên phòng ban (departmentName) theo id phòng ban (departmentId) trả về từ api
            this.departmentFormat(this.employees);
            /**Số lượng bản ghi hợp lệ */
            this.employeeNumber = res.data.totalRecord;
          })
          .catch((res) => {
            console.log(res);
          });
      }
    },
    //Xử lý khi set số bản ghi 1 trang //info là số lượng bản ghi 1 trang truyền lên từ ComboBox (component con)
    handlePerPage(info) {
      this.currentPage = 1;
      this.perPage = info;
      this.loadData();
    },
  },
  computed: {
    //check tất cả checkbox //Có 1 mảng để kiểm tra xem tất cả các bản ghi đã được checked chưa
    checkAll: {
      get: function () {
        return this.initialEmployees
          ? this.checked.length == this.employeeNumber
          : false;
      },
      set: function (value) {
        var checked = [];

        if (value) {
          this.initialEmployees.forEach(function (employee) {
            checked.push(employee.employeeId);
          });
        }

        this.checked = checked;
      },
    },
  },
  
};
</script>

<style scoped>
#router-content {
  position: absolute;
  top: 48px;
  left: 178px;
  box-sizing: border-box;
  display: inline-block;
  background: rgb(244, 245, 246);
  width: calc(100% - 178px);
  height: calc(100% - 48px);
  /* background-color: aqua; */
  padding-top: 24px;
  padding-left: 24px;
  padding-bottom: 24px;
  padding-right: 24px;
}
#title-bar {
  display: flex;
  align-items: center;
  width: 100%;
  height: 40px;
  /* background-color: aqua; */
}
.title {
  /* font-weight: bold; */
  font-size: 24px;
  font-weight: 700;
  color: #111;
}
.btn-wrapper {
  position: absolute;
  right: 24px;
  height: 40px;
}
#btn-add {
  height: 100%;
  width: 200px;
  border-radius: 4px;
  border: 1px solid transparent;
  color: #fff;
  background-color: #2ca01c;
}
#tablewrapper {
  background: #fff;
  color: #111;
  height: calc(100% - 36px);
  width: 100%;
  overflow: hidden;
  /* margin-right: 24px; */
  /* padding: 0px 24px 24px 24px; */
  padding-bottom: 24px;
  box-sizing: border-box;
  border-collapse: collapse;
  /* background-color: #2ca01c; */
  /* background-color: gray; */
}
#table{
  height: 100%;
  overflow: auto;
  
}

#search {
  height: 80px;
  width: 100%;
}
#search-bar-wrapper {
  height: 80px;
  /* background-color: aqua; */
  display: flex;
  align-items: center;
  position: absolute;
  right: 36px;
}
.search-bar-and-icon {
  border-radius: 2px;
  box-sizing: border-box;
  width: 232px;
  display: flex;
  align-items: center;
  border: 1px solid #babec5;
  border-collapse: collapse;
}
.search-bar-and-icon:focus-within {
  border: 1px solid #2ca01c;
}
#search-bar {
  width: 199px;
  height: 31px;
  font-size: 13px;
  /* border-radius: 2px;
        border: 1px solid #babec5; */
  outline: none;
  border: none;
  /* border: 1px solid #babec5; */
  /* border-right: none; */
  box-sizing: border-box;
  padding: 6px 10px;
  font-style: italic;
  border-collapse: collapse;
}
#search-icon {
  width: 31px;
  height: 31px;
  background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
  background-position: -984px -353px;
  border: none;
  outline: none;
  border-collapse: collapse;
}

#btn-refresh-wrapper {
  height: 80px;
  width: 80px;
  padding-left: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  /* background-color: #2ca01c; */
}

tbody tr:hover {
  background-color: #f6f7f8;
}
#btn-refresh {
  height: 32px;
  width: 32px;
  background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
  background-position: -423px -197px;
  border: none;
  outline: none;
  cursor: pointer;
}
#btn-export {
  width: 32px;
  height: 32px;
  background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
  background-position: -700px -197px;
  cursor: pointer;
  border: none;
  outline: none;
}

#footer {
  /* margin-top: 24px; */
  position: absolute;
  bottom: 24px;
  /* background-color: red; */
  width: calc(100% - 48px);
  box-sizing: border-box;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
#footer-right {
  display: flex;
  align-items: center;
  position: absolute;
  right: 48px;
}
/* #footer-2{
        background-color: aqua;
    }
    #footer-3{
        background-color: red;
        position: absolute;
        right: 48px;
        float: right;
        
    } */
.fa-3x {
  position: absolute;
  left: 54%;
  top: 50%;
  transform: translate(-50%, -50%);
}
</style>