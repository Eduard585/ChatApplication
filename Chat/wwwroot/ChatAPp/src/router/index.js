import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Access/Login'
import Registration from '@/components/Access/Registration'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login
    },
    {
      path:'/registration',
      name:'Registration',
      component:Registration
    }
  ]
})
