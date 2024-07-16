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
      <q-item class="drawer-item" clickable v-ripple @click="onLogin">
        <q-icon class="info-icon" color="primary" name="login" />
        <q-item-section>
          Login
        </q-item-section>
      </q-item>
      <q-item class="drawer-item" clickable v-ripple @click="onLogout">
        <q-icon class="info-icon" color="primary" name="logout" />
        <q-item-section>
          Logout
        </q-item-section>
      </q-item>
    </q-list>
  </q-drawer>
</template>

<script>

import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import { store } from 'src/global/store'

export default {
  name: 'HeaderComponent',
  setup () {
    const $q = useQuasar()
    const router = useRouter()
    const drawer = ref(false)
    const username = computed(() => store.username)
    const isLoggedIn = computed(() => !!store.username)

    const onLogin = () => {
      if (isLoggedIn.value) {
        $q.notify({
          color: 'negative',
          textColor: 'white',
          icon: 'warning',
          message: 'You are already logged in'
        })
      } else {
        router.push({ name: 'login' })
      }
    }

    const onLogout = () => {
      if (isLoggedIn.value) {
        localStorage.removeItem('authToken')
        localStorage.removeItem('username')
        store.username = ''

        $q.notify({
          color: 'positive',
          textColor: 'white',
          icon: 'cloud_off',
          message: 'Logout successful'
        })
      } else {
        $q.notify({
          color: 'negative',
          textColor: 'white',
          icon: 'warning',
          message: 'You are already logged out'
        })
      }
      router.push({ name: 'login' })
    }

    return {
      drawer,
      username,
      isLoggedIn,
      onLogin,
      onLogout
    }
  }
}
</script>
