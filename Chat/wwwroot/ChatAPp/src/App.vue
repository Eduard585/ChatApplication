<template>
  <div id="app">
    <article class="page-body">
      <router-view />
    </article>
  </div>
</template>

<script>
export default {
  name: "App"
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
  max-width: 935px;
  margin: auto;
}
</style>
