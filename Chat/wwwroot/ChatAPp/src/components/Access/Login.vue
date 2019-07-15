<template>
  <div id="login" class="login">
    <b-form @submit="onSubmit" @reset="onReset">
      <b-form-group id="input-group-email" label="Login" label-for="input-email">
        <b-form-input
          id="input-email"
          v-model="login.login"
          type="text"
          required
          placeholder="Enter login or email"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-password" label="Password" label-for="input-password">
        <b-form-input
          id="input-password"
          v-model="login.password"
          type="password"
          required
          placeholder="Enter password"
        ></b-form-input>
      </b-form-group>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>
    <a class="index_registration" href="#/registration">Registration</a>
    <b-button v-on:click="token" type="submit" variant="primary">Submasdasdit</b-button>
  </div>
</template>
<script>
export default {
  name: "Login",
  data() {
    return {
      login: {
        login: "",
        password: ""
      }
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      this.$http
        .post("access/login", {
          Login: this.login.login,
          Password: this.login.password
        })
        .then(function(response) {
          console.log(response);
        });
    },
    onReset(evt) {
      evt.preventDefault();
      this.login.login = "";
    },
    token(evt) {
      var token, authStr;
      var that = this;
      this.$http
        .post("access/token", {
          Login: "Admin@Admin",
          Password: "admin"
        })
        .then(function(response) {     
          token = response.data.accessToken;    
        })
        .then(function(response){ 
          that.$http//TODO: change that to something
            .get("values/getlogin", { headers: { Authorization: authStr } })
            .then(response => {
              console.log(response.data);
            });
        })
        
    }
  }
};
</script>
<style>
.login {
  max-width: 350px;
  margin: auto;
  margin-top: 12px;
  border: 1px solid;
  padding: 10px;
}
</style>
