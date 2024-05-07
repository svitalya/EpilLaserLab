import { createStore } from 'vuex';

interface State {
  username: string;
}

const store = createStore<State>({
  state: {
    username: '',
  },
  mutations: {
    setUsername(state, username: string) {
      state.username = username;
    },
  },
});

export default store;