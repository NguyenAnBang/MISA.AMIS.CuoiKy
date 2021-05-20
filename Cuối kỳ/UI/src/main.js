import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import '../src/assets/font/fontawesome-5.15.1/css/all.min.css'
import moment from 'moment'

Vue.prototype.moment = moment
import './styles/checkbox.css'
import './styles/radio_button.css'
import './styles/table.css'
// import CustomerList from './components/views/customer/customerList.vue'
// import EmployeeList from './components/views/employee/employeeList.vue'
// import Report from './components/views/report/report.vue'
// import Setting from './components/views/setting/setting.vue'
import EmployeeList from './components/view/employee/employeeList.vue'
Vue.config.productionTip = false
//1. Định nghĩa các path
const routes = [
//   {path: '/customer', component: CustomerList},
  {path: '/employee', component: EmployeeList},
//   {path: '/report', component: Report},
//   {path: '/setting', component: Setting},
]
//2. Khởi tạo router
const router = new VueRouter({
  routes // short for routes:routes
})
//3.khai báo sử dụng router
Vue.use(VueRouter)
new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
