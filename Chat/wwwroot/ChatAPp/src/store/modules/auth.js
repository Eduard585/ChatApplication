import {
  AUTH_REQUEST,
  AUTH_ERROR,
  AUTH_SUCCESS,
  AUTH_LOGOUT
} from '../actions/auth';
import {USER_REQUEST} from '../actions/user'
import HTTP from '@/utils/axios';

const state = {
  token: localStorage.getItem('user-token') || '',
  status: '',
  hasLoadedOnce: false
};

const getters = {
  isAuthentificated: state => !!state.token,
  authStatus: state => state.status
};

const actions = {
  [AUTH_REQUEST]: ({ commit, dispatch }, userInfo) => {
    return new Promise((resolve, reject) => {
      commit(AUTH_REQUEST);

      HTTP.post('access/login', userInfo) // TODO: change localStorage to HttpOnly Cookies
        .then(response => {
          localStorage.setItem('user-token', response.data.accessToken);
          console.log(localStorage.getItem('user-token'))
          HTTP.setAuthorizationHeader(response.data.accessToken)          
          commit(AUTH_SUCCESS, response); // TODO: reposnse.??        
          resolve(response);
        })
        .catch(err => {
          commit(AUTH_ERROR, err);
          localStorage.removeItem('user-token');
          reject(err);
        });
    });
  },
  [AUTH_LOGOUT]: ({ commit, dispatch }) => {
    return new Promise((resolve, reject) => {
      commit(AUTH_LOGOUT);
      localStorage.removeItem('user-token');
      resolve();
    });
  }
};

const mutations = {
  [AUTH_REQUEST]: state => {
    state.status = 'loading';
  },
  [AUTH_SUCCESS]: (state, response) => {
      console.log(response)
    state.status = 'success';
    state.token = response.data.accessToken;
    state.hasLoadedOnce = true;
  },
  [AUTH_LOGOUT]: state => {
    state.token = '';
  },
  [AUTH_ERROR]: state => {
    state.status = 'error';
    state.hasLoadedOnce = true;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
