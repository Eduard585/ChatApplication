// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import BootstrapVue from 'bootstrap-vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import Auth from '@/components/Controllers/Auth'

Vue.config.productionTip = false

axios.defaults.baseURL = 'https://localhost:44301/api/';
Vue.prototype.$http = axios;

Vue.use(BootstrapVue)
//If route require Auth, redirect to /login
router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth);
  const isAuthentificated = Auth.data().isAuthentificated;
  console.log(isAuthentificated);
  if (requiresAuth && !isAuthentificated) {
    next('/login');
    return;
  } else {
    if (requiresAuth && !isAuthentificated) {
      next();
      return;
    }
  }
  next();
});

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
