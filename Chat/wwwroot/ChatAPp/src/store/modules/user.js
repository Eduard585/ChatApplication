import { USER_REQUEST, USER_ERROR, USER_SUCCESS } from '../actions/user';
import Vue from 'vue';
import { AUTH_LOGOUT } from '../actions/auth';
import HTTP from '@/utils/axios';

const state = { status: '', profile: {} };

const getters = {
  getProfile: state => state.profile,
  isProfileLoaded: state => !!state.profile.name
};

const actions = {
  [USER_REQUEST]: ({ commit, dispatch }) => {
    commit(USER_REQUEST);
    HTTP.get('im/')
      .then(response => {
        commit(USER_SUCCESS, response);
      })
      .catch(response => {
        commit(USER_ERROR);
        dispatch(AUTH_LOGOUT);
      });
  }
};

const mutations = {
  [USER_REQUEST]: state => {
    state.status = 'loading';
  },
  [USER_SUCCESS]: (state, resp) => {
    state.status = 'success';
    Vue.set(state, 'profile', resp);
  },
  [USER_ERROR]: state => {
    state.status = 'error';
  },
  [AUTH_LOGOUT]: state => {
    state.profile = {};
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
