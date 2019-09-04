<template>
  <div class="room-user-list_container">
    <a href="#/rooms" class="room-list_link">К комнатам</a>
    <div>
      <a v-bind:class="{checked: unchecked}" v-on:click="checkUser()" href="" class="room-user-list_wrap" v-for="user in userList">
        <div class="room-user-list_item">
          <div class="room-user-list_ava">
            <p class="image">Krat</p>
          </div>
          <div class="room-user-list_name">{{user.login}}</div>
          <div class="room-user-list_checkbox"></div>
        </div>
      </a>
    </div>
  </div>
</template>
<script>
import HTTP from "@/utils/axios";

export default {
  data() {
    return {
      userList: []
    };
  },
  methods:{
      checkUser: function(evt){
          this.checked
          console.log(evt)
      }
  },
  beforeCreate() {
    HTTP.get("im/friends").then(response => {
      response.data.forEach(element => {
        var item={
            login:element.login,
            enabled:false
        }
        this.userList.push(item);      
      });
    });
  },
  created() {
    console.log("Created!");
  },
  beforeMount() {
    console.log("BeforeMount!");
  },
  mounted() {
    console.log("Mounted!");
  }
};
</script>
<style>
.image {
  width: 50px;
  height: 50px;
}
.room-user-list_ava {
  float:left;
}

.room-user-list_name {
  float:left;
}

.room-user-list_checkbox {
  width: 30px;
  height: 30px;
  float: right;
  margin: 19px 10px;
}

.room-user-list_item {
  display: block;
  overflow: auto;
}

.room-user-list_wrap{
    display:block;
}

.room-user-list_checkbox{
  background: url(../../assets/checkbox-unchecked.svg); 
}

.room-user-list_wrap.checked
.room-user-list_checkbox{
    background: url(../../assets/checkbox-checked.svg); 
}
</style>
