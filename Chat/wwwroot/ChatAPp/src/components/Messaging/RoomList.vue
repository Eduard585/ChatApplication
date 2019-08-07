<template>
  <div>
    <a href="#/rooms/userList">asdasdasd</a>
    <div class="room-list" v-if="!showUsers">
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
      <h1>ROOMLIST</h1>
    </div>
  </div>
</template>
<script>
import HTTP from "@/utils/axios";

export default {
  name: "RoomList",
  data(){
    return{
      showUsers:false
    }
  },
  methods:{
    toggleUsers(){
      HTTP.get('/')
      this.showUsers = !this.showUsers;
    }
  }
};
</script>
