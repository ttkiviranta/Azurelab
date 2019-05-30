<template>
    <div class="productlist">
        <h2>Products</h2>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    export interface Product {
        productid: string;
        name: string;
        productNumber: string;
        listPrice: string;
        online: boolean;
        locked: boolean;
    }

    @Component
    export default class ProductList extends Vue {
        @Prop() private product!: Product;
    }
</script>

<style lang="scss">
    div.productlist {
        h2

    {
        text-decoration: underline;
    }

    p.meta {
        font-style: italic;
    }
    }
</style>