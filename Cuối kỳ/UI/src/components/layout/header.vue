<template>
    <div id="Header">        
        <button id="menu-icon"></button>
        <div id="company-name">CÔNG TY CỔ PHẦN MISA</div>
        <button id="company-name-icon"></button>

        <button id="bell-icon"></button>
        <div id="profile">
            <div id="avatar"></div>
            <div id="profile-name">Nguyễn Văn Mạnh</div>
            <button id="profile-name-icon"></button>
            
        </div>
    </div>
</template>

<style scoped>
    #Header{
        position: absolute;
        left: 178px;
        top: 0px;
        height: 48px;
        width: calc(100% - 178px);
        /* width: 100%; */
        /* box-sizing: border-box; */
        display: flex;
        align-items: center;
        /* background-color:aqua; */
    }
    #menu-icon{
        width: 54px;
        height: 36px;
        
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -835px -26px;
        border: none;
        outline: none;
    }
    #company-name-icon{
        width: 36px;
        height: 36px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -1120px -353px;
        border: none;
        outline: none;
    }
    #profile{
        position: absolute;
        right: 24px;
        top: 0px;
        display: flex;
        align-items: center;
        justify-content: flex-end;
        width: 220px;
        height: 48px;
        /* background-color: aqua; */
    }
    #bell-icon{
        width: 36px;
        height: 36px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -780px -26px;
        border: none;
        outline: none;
        position: absolute;
        right: 260px;
    }
    #profile-name-icon{
        width: 36px;
        height: 36px;
        background: url(../../assets/img/Sprites.64af8f61.svg) no-repeat -932px -351px;
        border: none;
        outline: none;
    }
</style>