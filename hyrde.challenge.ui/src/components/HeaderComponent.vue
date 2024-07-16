<template>
  <q-header elevated>
    <q-toolbar>
      <q-btn flat @click="drawer = !drawer" round dense icon="menu" />
      <q-toolbar-title><a class="header-title" href="#">Alex's Weather Forecast API</a></q-toolbar-title>
      <div class="toolbar-welcome" v-if="username">Welcome {{ username }}</div>
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
      <router-link class="drawer-link" to="/login">
        <q-item class="drawer-item" clickable v-ripple @click="onLogout">
          <q-icon class="info-icon" color="primary" name="logout" />
          <q-item-section>
            Logout
          </q-item-section>
        </q-item>
      </router-link>
    </q-list>
  </q-drawer>
</template>

<script>

import { useQuasar } from 'quasar'
import { computed, ref } from 'vue'
import { store } from 'src/store/store'

export default {
  name: 'HeaderComponent',
  setup () {
    const $q = useQuasar()
    const drawer = ref(false)
    const username = computed(() => store.username)

    const onLogout = () => {
      localStorage.removeItem('authToken')
      localStorage.removeItem('username')
      store.username = ''

      $q.notify({
        color: 'positive',
        textColor: 'white',
        icon: 'cloud_off',
        message: 'Logout successful'
      })
    }

    return {
      drawer,
      username,
      onLogout
    }
  }
}
</script>
