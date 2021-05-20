<template>
  <div id="app">
    <Header/>
    <Navbar/>
    <Content/>
  </div>
</template>

<script>
import Header from './components/layout/header.vue'
import Navbar from './components/layout/navbar.vue'
import Content from './components/layout/content.vue'
export default {
  name: 'App',
  components: {
    Header,
    Navbar,
    Content
  }
}
</script>

<style>
@font-face {
  font-family: NotoSans;
 src: url("./assets/fonts/notosans-regular.2cb88a13.woff2") format("woff2");
}
body{
  font-size: 13px;
  font-family: NotoSans;
}
button{
  cursor: pointer;
}

.input,
#date-input {
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

  /* &:focus {
    border-color: #2ca01c;
  }

  &.has-icon {
    padding-right: 28px;
  }

  &.has-error {
    border-color: #f20;

    & + .text-error {
      display: none;
    }

    &:hover + .text-error {
      display: inline-block;
      position: absolute;
      top: 100%;
      left: 0;
      background-color: #38393d;
      color: @color-white;
      padding: 8px;
      z-index: 10;
    }
  } */
}
#date-input:focus{
  border-color: #2ca01c;
}

/* width */
::-webkit-scrollbar {
  width: 10px;
  height: 10px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1; 
}
 
/* Handle */
::-webkit-scrollbar-thumb {
  background: #888; 
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555; 
}

</style>
