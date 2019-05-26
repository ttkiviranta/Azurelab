import axios from 'axios';
import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import VueAxios from 'vue-axios';
import App from './App.vue';
import BPagination from 'bootstrap-vue/es/components/pagination/pagination';
import Pagination from 'bootstrap-vue/es/components/pagination';

Vue.config.productionTip = false;
Vue.use(BootstrapVue);
Vue.use(VueAxios, axios);
Vue.component('b-pagination', BPagination);
Vue.use(Pagination);

new Vue({
  render: (h) => h(App),
}).$mount('#app');
