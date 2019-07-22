module.exports = {
  data() {
    return {
      isAuthentificated: false
    };
  },
  request: function(req, token) {
    this.options.http._setHeaders.call(this, req, {
      Authorization: "Token " + token
    });
  },

  response: function(res) {
    let token = res.data.token;
    if (token) {
      return token;
    }
  }
};
