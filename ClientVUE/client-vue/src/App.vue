<template>
    <div id="app">
        <h1>Elke's fantastic blog</h1>
        <BlogPost v-for="blogPost in blogPosts" :post="blogPost" :key="blogPost.title" />
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import BlogPost from './components/BlogPost.vue';
    import Post from './components/BlogPost.vue';
    import { AxiosResponse } from 'axios';

    @Component({
        components: {
            BlogPost,
        },
    })
    export default class App extends Vue {
        private blogPosts: Post[] = [];
        private created() {
            this.$http.get('http://localhost:3000/blogposts').then((response: AxiosResponse) => {
                this.blogPosts = response.data.map((val: any) => ({
                    title: val.title,
                    body: val.body,
                    author: val.author,
                    datePosted: new Date(val.datePosted),
                }));
            });
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
