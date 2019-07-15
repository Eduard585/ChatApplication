import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Access/Login'
import Registration from '@/components/Access/Registration'
import Home from '@/components/HomePage/Home'
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path:'/registration',
      name:'Registration',
      component:Registration
      
    },
    {
      path:'/home',
      name:'Home',
      component:Home,
      meta:{requiresAuth:true}
    }
  ]
})
