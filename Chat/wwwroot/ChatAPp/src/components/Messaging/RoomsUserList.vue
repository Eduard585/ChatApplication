<template>
  <div class="room-user-list-wrap">
    <ul class="room-user-list">
        <li v-for="user in userList">
            {{user.Name}}
        </li>
    </ul>
    
  </div>
</template>
<script>
export default {
    data(){
        return{
            userList:[{
                Name:null
            }]
        }
    },
    beforeCreate(){
        console.log('BeforeCreate!')
    },
    created(){
        console.log('Created!')
    },
    beforeMount(){
        console.log('BeforeMount!')
    },
    mounted(){
        console.log('Mounted!')
    }
}
</script>
