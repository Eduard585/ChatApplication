<template>
  <div class="page-wrap-fix" v-bind:style="{width: windowWidth}">
    <div class="page-header-parent" v-bind:style="{width: windowWidth}">
      <v-header></v-header>
    </div>
    <div class="page-layout">
      <v-sidebar></v-sidebar>
      <article class="page-body">
        <router-view></router-view>
      </article>
    </div>
  </div>
</template>

<script>
import SideBar from "./components/BoxPermanent/Sidebar";
import Header from "./components/BoxPermanent/Header";
export default {
  name: "App",
  data() {
    return {
      windowWidth: window.outerWidth
    };
  },
  components: {
    "v-sidebar": SideBar,
    "v-header": Header
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.page-body {
  max-width: 800px;
  margin: auto;
}
.fl_l {
  float: left;
}
.page-layout {
  width: 1100px;
  margin: 0 auto;
  margin-top:58px;
}
.page-header-parent {
  height: 50px;
  background-color:pink;
  position:fixed;
  top:0;
  width:100%;
}
div{
border: 1px solid #000000;
}

ul{
  list-style-type: none;
}

.checked{
  background: url(assets/checkbox-checked.svg); 
}

.unchecked{
  background: url(assets/checkbox-unchecked.svg); 
}
</style>
