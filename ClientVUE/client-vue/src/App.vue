<template>
    <div id="app">
        <h1>Products</h1>
       
        <p class="mt-3">Current Page: {{ currentPage }}</p>

        <b-table id="my-table"
                 :items="productList"
                 :fields="fields" 
                 :filter="filter"
                 :per-page="perPage"
                 :current-page="currentPage"
                 small></b-table>

        <b-pagination v-model="currentPage"
                      :total-rows="rows"
                      :per-page="perPage"
                      align="center"
                      aria-controls="my-table"></b-pagination>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import ProductList from './components/ProductList.vue';
    import Product from './components/ProductList.vue';
    import { AxiosResponse } from 'axios';

    @Component({
        components: {
            ProductList,
        },
    })
    export default class App extends Vue {
        private productList: Product[] = [];
        private filter = null;
        private perPage = 10;
        private currentPage = 1;
        private fields = [
            {
                key: 'productId',
                sortable: true
            },
            {
                key: 'name',
                sortable: true
            },
            {
                key: 'productNumber',
                sortable: true
            },
            {
                key: 'listPrice',
                sortable: true
            },
            {
                key: 'online',
                sortable: true
            },
            {
                key: 'locked',
                sortable: true
            },

        ];
        private async created() {
            try {
                const response = await this.axios.get('/home/index/')
                this.productList = response.data.map((val: any) => ({
                    productId: val.productId,
                    name: val.name,
                    productNumber: val.productNumber,
                    listPrice: val.listPrice,
                    online: val.online,
                    locked: val.locked,
                }));
            } catch (err) {
                console.log(err)
            }
        }

        get rows() {
            return this.productList.length
        }
    }
</script>

<style lang="scss">
    #app {
        font-family: 'Avenir', Helvetica, Arial, sans-serif;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        color: #2c3e50;
        margin-top: 60px;
    }
</style>
