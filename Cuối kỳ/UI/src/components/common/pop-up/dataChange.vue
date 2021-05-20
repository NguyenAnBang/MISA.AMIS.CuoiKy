<template>

    <div class="dialog" :class="{'dialog_hide': !isShow}">
        <div id="model"></div>
        <div class="dialog-content">
                <div class="header">
                    <button class="btn-warning"></button>
                    <div style="padding-top: 10px;">{{errorMsg}}</div>
                </div>
                <div class="footer">
                    <button class="btn-cancel" @click="hidePopUp()">Hủy</button>
                    <div class="footer-2">
                        <button class="btn-cancel" @click="hideDialog()">Không</button>
                        <button class="btn-confirm" @click="hideDialogAndSaveData()">Có</button>
                    </div>
                </div>
        </div>
    </div>
    
</template>
<script>
export default ({
    setup() {
        
    },
    created() {
        
    },
    props: {
        //Thuộc tính quyết định ẩn/hiện pop-up
        isShow: {
            type: Boolean,
            default: false
        },
        //Thông báo lỗi
        errorMsg: {
            type: String,
            default: null
        }
    },
    methods: {
        /**
         * Ẩn pop-up thông báo và giữ lại form chi tiết
         * CreatedBy: NABANG (20/05/21)
        */
        hidePopUp(){
            this.$emit('hidePopUp');
        },
        
        /**
         * Đóng form chi tiết và không lưu dữ liệu
         * CreatedBy: NABANG (20/05/21)
        */
        hideDialog(){
            this.$emit('hideDialog');
        },
        /**
         * Đóng form chi tiết và lưu dữ liệu
         * CreatedBy: NABANG (20/05/21)
        */
        hideDialogAndSaveData(){
            this.$emit('hidePopUp');
            this.$emit('btnSave');
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
    z-index: 4;
}
.dialog-content{
    position: fixed;
    width: 444px;
    height: 180px;
    top: calc(50% - 100px);
    left: calc(50% - 222px);
    background-color: white;
    border-radius: 3px;
    box-sizing: border-box;
    padding-left: 32px;
    padding-right: 32px;
    z-index: 5;
}
.header{
    width: 100%;
    height: 50%;
    display: flex;
    align-items: center;
    border-bottom: 1px solid #babec5;
}
.footer{
    width: 100%;
    height: 50%;
    display: flex;
    position: relative;
    justify-content: space-between;
    align-items: center;
}
.footer-2{
    width: 150px;
    display: flex;
    justify-content: space-between;
}
.btn-warning{
    width: 48px;
    height: 48px;
    min-width: 48px;
    min-height: 48px;
    background: url(../../../assets/img/Sprites.64af8f61.svg) no-repeat;
    cursor: pointer;
    background-position: -826px -450px;
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
}
.btn-confirm{
    /* position: absolute; */
    /* top: 24px; */
    height: 36px;
    padding: 8px 20px;
    border: 1px solid transparent;
    color: #fff;
    background-color: #2ca01c;
    border-radius: 3px;
    cursor: pointer;
}

</style>