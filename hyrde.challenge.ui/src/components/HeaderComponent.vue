<template>
    <q-header elevated>
        <q-toolbar>
            <q-btn flat @click="drawer = !drawer" round dense icon="menu" />
            <q-toolbar-title><a class="header-title" href="#">Alex's Weather Forecast</a></q-toolbar-title>
        </q-toolbar>
    </q-header>
    <q-drawer v-model="drawer" class="drawer">
        <q-list padding class="menu-list drawer-list" >
            <q-item clickable v-ripple>
                <q-item-section>
                    Login
                </q-item-section>
            </q-item>
        </q-list>
    </q-drawer>
</template>

<script>
import { ref } from 'vue'
export default {
  name: 'HeaderComponent',
  setup () {
    return {
      drawer: ref(false)
    }
  }
}
</script>
