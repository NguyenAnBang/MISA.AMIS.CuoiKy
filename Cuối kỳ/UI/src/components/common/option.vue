<template>
    <div style="display: flex; justify-content: center; align-items:center; cursor: pointer;" >
        <div style="color: blue;" @click="showEmployeeDetail()">Sửa</div> 
        <button id="dropdown-icon" @click="showDropContent()" @blur="hideDropContent()"></button>
        <div id="dropdown" >     
            <div class="dropdown-content" :class="{'hide': !isShowOption}" >
                <div class="dropdown-content-a" @click="showEmployeeDuplicate()" @mouseenter="enterClick()" @mouseleave="leaveClick()">Nhân bản</div>
                <div class="dropdown-content-a" @click="showDeleteDialog()" @mouseenter="enterClick()" @mouseleave="leaveClick()">Xóa</div>
                <div class="dropdown-content-a">Ngưng sử dụng</div>
            </div>
        </div>
    </div>
</template>
<script>


export default ({
    created() {
        
    },
    
    methods: {
        
        //Hiện ra các thông tin tùy chọn để select (dropdown-option)
        showDropContent() {
            // if(this.isShowOption == true) 
            // else this.isShowOption = true;
            this.isShowOption = true;
            
        },
        //Ẩn dropdown-option
        hideDropContent(){
            if(this.overClick == false) this.isShowOption = false;
        },

        //Hiện dialog để xác nhận xóa nhân viên
        showDeleteDialog(){
            this.$emit('showDeleteDialog');
            this.isShowOption = false;
        },
        //Hiện dialog chi tiết thông tin nhân viên
        showEmployeeDetail(){
            this.$emit('showEmployeeDetail');
            this.isShowOption = false;

        },
        //Hiện dialog nhân bản thông tin nhân viên
        showEmployeeDuplicate(){
            this.$emit('showEmployeeDuplicate');
            this.isShowOption = false;
        },

        //Phân biệt khi nào di chuyển chuột vào dropbox-content
        enterClick(){
            this.overClick = true;
        },
        //Phân biệt khi nào di chuyển chuột ra ngoài dropbox-content
        leaveClick(){
            this.overClick = false;
        }
    },
    data() {
        return {
            
            //Hiện/Ẩn option sửa/xóa
            isShowOption: false,
            //Biến để phân biệt click và blur, vì khi click vào lựa chọn trong dropdown-content thì sự kiện blur sẽ chạy trước
            overClick: false,
        }
    },
})
</script>

<style scoped>
    #dropdown{
        position: relative;
        display: inline-block;
        /* z-index: 1000; */
    }
    #dropdown-icon{
        width: 20px;
        height: 16px;
        min-width: 16px;
        min-height: 16px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat;
        cursor: pointer;
        background-position: -890px -359px;
        border: none;
        /* outline: aqua; */
    }
    #dropdown-icon:hover{
        border-color:aqua;
    }
    .dropdown-content{
        
        position: absolute;
        right: 0px;
        top: 10px;
        background-color: #fff;
        /* min-width: 160px; */
        overflow: auto;
        width: 100px;
        height: 75px;
        border: 1px solid #babec5;
        box-sizing: border-box;
        border-radius: 4px;
        
        
    }
    .reserve{
        top: auto;
        bottom: 100%;
    }
    .dropdown-content-a{
        font-size: 12px;
        height: 24px;
        width: 100%;
        display: flex;
        align-items: center;
        padding-left: 4px;
        box-sizing: border-box;
        border: none;
        outline: none;
    }
    .dropdown-content-a:hover{
        background-color: #f1f1f1;
        cursor: pointer;
        color: #2ca01c;
    }
    .hide{
        display: none;
    }
</style>