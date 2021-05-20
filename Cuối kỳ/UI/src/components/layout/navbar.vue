<template>
    <div id="navBar">
        
        <div id="Navbar_logo_box">
            <div id="sidebar"></div>
            <div id="logo"></div>
        </div>
        <div id="Navbar_content_box">
            <div class="navbar-item">
                <div class="navbar-icon icon-1"></div>
                <div class="navbar-text">Tổng quan</div>
            </div>
            <router-link class="navbar-item" tag="li" to="/employee">
                <div class="navbar-icon icon-2"></div>
                <div class="navbar-text">Tiền mặt</div>
            </router-link>
            <div class="navbar-item">
                <div class="navbar-icon icon-3"></div>
                <div class="navbar-text">Tiền gửi</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-4"></div>
                <div class="navbar-text">Mua hàng</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-5"></div>
                <div class="navbar-text">Bán hàng</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-6"></div>
                <div class="navbar-text">Quản lý hóa đơn</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-7"></div>
                <div class="navbar-text">Kho</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-8"></div>
                <div class="navbar-text">Công cụ dụng cụ</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-9"></div>
                <div class="navbar-text">Tài sản cố định</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-10"></div>
                <div class="navbar-text">Thuế</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-11"></div>
                <div class="navbar-text">Giá thành</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-12"></div>
                <div class="navbar-text">Tổng hợp</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-13"></div>
                <div class="navbar-text">Ngân sách</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-14"></div>
                <div class="navbar-text">Báo cáo</div>
            </div>
            <div class="navbar-item">
                <div class="navbar-icon icon-15"></div>
                <div class="navbar-text">Phân tích tài chính</div>
            </div>
        </div>
        <!-- <div id="Navbar_content_box">
            <div class="navbar-item">
                <div class="navbar-icon icon-dashboard"></div>
                <div class="navbar-text">Tổng quan</div>
            </div>
            <router-link class="navbar-item" tag="li" to="/report">
                <div class="navbar-icon icon-report"></div>
                <div class="navbar-text">Báo cáo</div>
            </router-link>
            <router-link class="navbar-item" tag="li" to="/customer">
                <div class="navbar-icon icon-dic-employee"></div>
                <div class="navbar-text">Danh mục khách hàng</div>
            </router-link>
            <router-link class="navbar-item" tag="li" to="/employee">
                <div class="navbar-icon icon-dic-employee"></div>
                <div class="navbar-text">Danh mục nhân viên</div>
            </router-link>
            <router-link class="navbar-item" tag="li" to="/setting">
                <div class="navbar-icon icon-setting"></div>
                <div class="navbar-text">Thiết lập hệ thống</div>
            </router-link>
        </div> -->
    </div>
</template>

<style scoped>
    #navBar{
        position: absolute;
        top: 0px;
        left: 0px;
        
        height: 100%;
        width: 178px;
        background-color: #38393d;
        
    }
    #Navbar_logo_box{
        /* width: 100%; */
        height: 48px;
        display: flex;
        align-items: center;
        /* background-color: #38393d; */
        padding-left: 24px;
        margin-bottom: 16px;
    }
    #sidebar{
        width: 24px;
        height: 24px;
        margin-right: 10px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -424px -86px;
    }
    #logo{
        height: 32px;
        width: 96px;
        display: flex;
        align-items: center;
        background: url(../../assets/img/Logo_Module_TiengViet_White.66947422.svg) no-repeat center;
        background-size: contain;
    }
    .navbar-item{
        width: 100%;
        height: 42px;
        display: flex;
        align-items: center;
        padding-left: 8px;
        color: #fff;
        border-left: 6px solid transparent;
        line-height: 42px;
        font-size: 15px;
        
    }
    .navbar-item{
        font-size: 13px;
    }
    .navbar-icon{
        width: 24px;
        height: 100%;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat;
        padding-right: 8px;
    }
    .icon-1{
        background-position: -30px -1620px;           
    }
    .icon-2{
        background-position: -73px -1620px;           
    }
    .icon-3{
        background-position: -117px -1620px;           
    }
    .icon-4{
        background-position: -161px -1620px;           
    }
    .icon-5{
        background-position: -204px -1620px;           
    }
    .icon-6{
        background-position: -249px -1620px;           
    }
    .icon-7{
        background-position: -291px -1620px;           
    }
    .icon-8{
        background-position: -336px -1620px;           
    }
    .icon-9{
        background-position: -378px -1620px;           
    }
    .icon-10{
        background-position: -423px -1620px;           
    }
    .icon-11{
        background-position: -467px -1620px;          
    }
    .icon-12{
        background-position: -510px -1620px;          
    }
    .icon-13{
        background-position: -378px -1650px;           
    }
    .icon-14{
        background-position: -547px -1620px;        
    }
    .icon-15{
        background-position: -204px -1654px;         
    }



</style>