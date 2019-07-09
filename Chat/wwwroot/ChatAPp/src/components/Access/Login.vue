<template>
  <div id="login" class=login>   
    <b-form @submit="onSubmit" @reset="onReset">
      <b-form-group id="input-group-email" label="Email" label-for="input-email">
        <b-form-input
          id="input-email"
          v-model="login.email"
          type="email"
          required
          placeholder="Enter email"
        ></b-form-input>
      </b-form-group>      
    </b-form>
    <a class="index_registration" href="#/registration">Registration</a>
  </div>
</template>
<script>
export default {
  name: "Login",
  data() {
    return {
      login: {
        email: ""      
      }
    };
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      alert(JSON.stringify(this.form));
    },
    onReset(evt) {
      evt.preventDefault();
      this.form.email = "";
    }
  }
};
</script>
<style>
.login {
  max-width: 350px;
  margin:auto;
  margin-top:12px;
  border: 1px solid;
  padding: 10px;
}
</style>
