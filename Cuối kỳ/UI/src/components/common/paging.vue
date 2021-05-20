<template>
    <div class="pagination">
        
        <div style="width: 10px"></div>
        <div class="pager">
        <div
          class="page"
          :class="{ disable: page == 1 }"
          @click.prevent="$emit('onChangePage', page - 1)"
        >
          Trước
        </div>

        <div
          class="page"
          :class="{ active: page == 1 }"
          @click.prevent="$emit('onChangePage', 1)"
        >
          1
        </div>

        <div v-if="page > 3" class="page disable">...</div>

        <div
          v-for="p in pages"
          :key="p"
          class="page"
          :class="{ active: page == p }"
          @click.prevent="$emit('onChangePage', p)"
        >
          {{ p }}
        </div>

        <div v-if="page < totalPages - 3" class="page disable">...</div>

        <div
          v-if="totalPages > 1"
          class="page"
          :class="{ active: page == totalPages }"
          @click.prevent="$emit('onChangePage', totalPages)"
        >
          {{ totalPages }}
        </div>

        <div
          class="page"
          :class="{ disable: page == totalPages }"
          @click.prevent="$emit('onChangePage', page + 1)"
        >
          Sau
        </div>
        <div style="width: 10px"></div>
      </div>
    </div>
</template>

<script>
export default {
    name: "Pagination",
 props:{
     perPage: {
         type: Number,
        default: 10,
     },
     page: {
      type: Number,
      default: 1,
    },
     totalPages: {
      type: Number,
      default: 0,
    },

 } ,
 computed: {
    pages: function () {
      let ps = [];
      let start = this.page > 3 ? this.page - 1 : 2;
      let end =
        this.page < this.totalPages - 3 ? this.page + 1 : this.totalPages - 1;
      for (let i = start; i <= end; i++) ps.push(i);
      console.log(ps);
      return ps;
    },
    // isInFirstPage() {
    //   return this.currentPage === 1;
    // },
    // isInLastPage() {
    //   return this.page === this.totalPages;
    // },
  },  
}
</script>

<style scoped>
.pagination{
    /* position: absolute; */
    /* bottom: 0;
    right: 48px; */
    width: 100%;
    height: 46px;
    display: flex;
    align-items: center;
    /* background-color: aqua; */
    justify-content: space-between;
}
.pager{
    display: flex;
    /* background-color: green; */
}
.page{
    padding: 4px;
    cursor: pointer;
    line-height: 20px;
    margin: 2px;
    outline: none;
    border: red;
}
.disable{
    color: #aaaaaa;
}
.active{

    margin-bottom: 1px;
    border: 1px solid #e0e0e0;    
    font-weight: bold;
    color: black;
    cursor: pointer;
}
</style>