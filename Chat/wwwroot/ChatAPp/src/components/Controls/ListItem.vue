<template>
    <a v-bind:href="url" class="list-item">
        <span class="list-item_icon"></span>
        <span class="list-item_label">{{title}}</span>
    </a>
</template>
<script>
export default {
    name:"ListItem",
    props:['title','url']
}
</script>
