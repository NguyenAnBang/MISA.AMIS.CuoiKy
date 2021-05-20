<template>

    <div class="dialog" :class="{'dialog_hide': !isShow}">
        <div id="model"></div>
        <div class="dialog-content">
                <div class="header">
                    <button class="btn-warning"></button>
                    <div style="">Bạn có thực sự muốn xóa Nhân viên &#60;<span>{{employee.employeeCode}}</span>&#62; không?</div>
                </div>
                <div class="footer">
                    <button class="btn-cancel" @click="hideDialog()">Không</button>
                    <button class="btn-confirm" @click="btnDelete()">Có</button>
                </div>
        </div>
    </div>
    
</template>
<script>
import axios from 'axios'
const getAll = "https://localhost:44308/api/v1/Employees/"
export default ({
    setup() {
        
    },
    created() {
        
    },
    props: {
        //Biến quyết định ẩn/hiện component
        isShow: {
            type: Boolean,
            default: false
        },
        //Nhân viên được chọn
        employee: {
            type: Object,
            default: null
        }
    },
    methods: {
        //Ẩn dialog xóa nhân viên đi
        hideDialog(){
            this.$emit('hideDialog');
        },
        //Xóa nhân viên
        btnDelete(){
            axios.delete(getAll + this.employee.employeeId)
                .then(res => {
                    console.log(res);
                    //Đóng Dialog
                    this.hideDialog();
                })
                .catch(res => {
                    console.log(res);
                })
        }
    },
    data() {
        return {
        }
    },
})
</script>
<style scoped>
.dialog_hide {
    display:none;
}
#model {
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
    width: 444px;
    height: 200px;
    top: calc(50% - 100px);
    left: calc(50% - 222px);
    background-color: white;
    border-radius: 3px;
    box-sizing: border-box;
    padding-left: 28px;
    padding-right: 28px;
    z-index: 3;
}
.header{
    width: 100%;
    height: 60%;
    display: flex;
    align-items: center;
    border-bottom: 1px solid #babec5;
}
.footer{
    width: 100%;
    height: 40%;
    display: flex;
    align-items: center;
}
.btn-warning{
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -594px -457px;
    border: none;
    outline: none;
    margin-right: 12px;
}
.btn-cancel{
    border: 1px solid #8d9096;
    color: #111;
    background-color: transparent;
    border-radius: 3px;
    height: 36px;
    padding: 8px 20px;
    cursor: pointer;

}
.btn-confirm{
    position: absolute;
    right: 24px;
    height: 36px;
    padding: 8px 20px;
    border: 1px solid transparent;
    color: #fff;
    background-color: #2ca01c;
    border-radius: 3px;
    cursor: pointer;
}

</style>