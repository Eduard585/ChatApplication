<template>
  <div id="registration" class="registration">
    <b-form @submit="onSubmit" @reset="onReset">
      <b-form-group id="input-group-email" label="Email" label-for="input-email">
        <b-form-input
          id="input-email"
          v-model="registration.email"
          type="email"
          required
          placeholder="Enter email"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-login" label="Login" label-for="input-login">
        <b-form-input
          id="input-login"
          v-model="registration.login"
          type="text"
          required
          placeholder="Enter login"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-password" label="Password" label-for="input-password">
        <b-form-input
          id="input-password"
          v-model="registration.password"
          type="password"
          required
          placeholder="Enter password"
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-rpassword" label="Repeat password" label-for="input-rpassword">
        <b-form-input
          id="input-rpassword"
          v-model="registration.rpassword"
          v-on:keyup="this.checkPassword"
          type="password"
          required
          placeholder="Repeat password"
        ></b-form-input>
        <h6 class="password-not-same-warning" v-if="validation.showWarning">Passwords are not same!</h6>
      </b-form-group>
      <b-button type="submit" variant="primary">Submit</b-button>
    </b-form>
  </div>
</template>
<script>
export default {
  name: "Registration",
  data() {
    return {
      registration: {
        email: "",
        login: "",
        password: "",
        rpassword: ""
      },
      validation: {
        showWarning: false
      }
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      if (this.checkPassword()) {
        this.$http
          .post("/user/registration", {
            Login: this.registration.login,
            Password: this.registration.password,
            Email: this.registration.email
          })
          .then(function(response) {
            console.log(response);
          });
      }
    },
    onReset(evt) {
      evt.preventDefault();
      this.registration.email = "";
    },
    checkPassword(evt) {
      if (this.registration.password != this.registration.rpassword) {
        this.validation.showWarning = true;
        return false;
      } else {
        this.validation.showWarning = false;
        return true;
      }
    }
  }
};
</script>

<style>
.registration {
  max-width: 350px;
  margin: auto;
  margin-top: 12px;
  border: 1px solid;
  padding: 10px;
}
.password-not-same-warning {
  color: red;
}
</style>

