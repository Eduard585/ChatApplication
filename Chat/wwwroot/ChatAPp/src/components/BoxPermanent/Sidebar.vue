<template>
    <div class="sidebar fl_l">
        <div class="sidebar_inner">
            <nav>
                <ol>
                    <li id="l_messages">
                        <v-list-item title="Сообщения" url="#/rooms"></v-list-item>
                    </li>
                    <li id="l_messages">
                        <v-list-item title="Что то еще"></v-list-item>
                    </li>
                    <li id="l_messages">
                        <v-list-item title="Еще"></v-list-item>
                    </li>
                    <li id="l_messages">
                        <v-list-item title="еще..."></v-list-item>
                    </li>
                </ol>
            </nav>
        </div>
    </div>
</template>
<script>
import ListItem from '../Controls/ListItem'
export default {
    name:"SideBar",
    components:{
        'v-list-item': ListItem
    }
}
</script>
<style>

</style>
