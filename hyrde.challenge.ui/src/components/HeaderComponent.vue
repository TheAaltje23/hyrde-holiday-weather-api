<template>
  <q-header elevated>
    <q-toolbar>
      <q-btn flat @click="drawer = !drawer" round dense icon="menu" />
      <q-toolbar-title><a class="header-title" href="#">Alex's Weather Forecast</a></q-toolbar-title>
      <div class="toolbar-welcome">Welcome {{ username }}</div>
    </q-toolbar>
  </q-header>
  <q-drawer v-model="drawer" class="drawer" behavior="desktop" overlay>
    <q-list padding class="menu-list drawer-list">
      <router-link class="drawer-link" to="/login">
        <q-item class="drawer-item" clickable v-ripple>
          <q-icon class="info-icon" color="primary" name="login" />
          <q-item-section>
            Login
          </q-item-section>
        </q-item>
      </router-link>
    </q-list>
  </q-drawer>
</template>

<script>
import { computed, ref } from 'vue'
import { store } from 'src/store/store'

export default {
  name: 'HeaderComponent',
  setup () {
    const drawer = ref(false)
    const username = computed(() => store.username)

    return {
      drawer,
      username
    }
  }
}
</script>
