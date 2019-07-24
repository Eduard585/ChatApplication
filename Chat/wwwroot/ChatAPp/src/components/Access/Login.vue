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
  </div>
</template>
<script>
/* eslint-disable */
import { AUTH_REQUEST } from "@/store/actions/auth";


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
      console.log(this);
      this.$store
        .dispatch(AUTH_REQUEST, {
          Login: this.login.login,
          Password: this.login.password
        })
        .then(() => {
          console.log("davay");
          this.$router.push("/home");
        });
    },
    onReset(evt) {
      evt.preventDefault();
      this.login.login = "";
    } 
  }
};
</script>
<style>
.login {
  text-align:center;
  max-width: 350px;
  margin: auto;
  margin-top: 12px;
  border: 1px solid;
  padding: 10px;
}
</style>
