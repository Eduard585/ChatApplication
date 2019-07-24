<template>
  <a v-bind:href="url" class="user-list-item-wrap">
    <div class="user-list-item">
        <div class="user-list-item_photo"></div>
        <div class="user-list-item_name">{{name}}</div>
        <div class="user-list-item_checkbox">
         <input type="checkbox">
        </div>
    </div>
  </a>
</template>
<script>
export default {
    name:"UserListItemCheck",
    props:['url','name']
}
</script>

