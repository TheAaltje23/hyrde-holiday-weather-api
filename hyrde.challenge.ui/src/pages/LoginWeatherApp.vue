<template>
  <div class="page-container">

    <q-card class="login-card">
      <q-card-section class="text-center login-text">
        <div class="text-h5 text-weight-bold">Log in</div>
        <div>Log in below to access your account</div>
      </q-card-section>
      <q-card-section>
        <div class="q-pa-sm">
          <q-form @submit="onSubmit" @reset="onReset" class="q-gutter-md">

            <q-input bg-color="white" filled v-model="username" label="Your username" lazy-rules
              :rules="[val => val && val.length > 0 || 'Please type your username']" />

            <q-input bg-color="white" filled type="password" v-model="password" label="Your password" lazy-rules
              :rules="[val => val && val.length > 0 || 'Please type your password']" />

            <div>
              <q-btn label="Login" type="submit" color="primary" />
              <q-btn label="Reset" type="reset" color="white" flat class="q-ml-sm" />
            </div>

          </q-form>
        </div>
      </q-card-section>
    </q-card>
  </div>
</template>

<script>
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import axios from 'axios'
import { store } from 'src/global/store'

export default {
  setup () {
    const $q = useQuasar()
    const username = ref(null)
    const password = ref(null)
    const router = useRouter()

    const onSubmit = async () => {
      try {
        const responseLogin = await axios.post('http://localhost:5114/user/login', {
          username: username.value,
          password: password.value
        })

        if (responseLogin.data.success === true) {
          // Store token and username
          localStorage.setItem('authToken', responseLogin.data.data.token)
          store.username = username.value
          localStorage.setItem('username', store.username)

          $q.notify({
            color: 'positive',
            textColor: 'white',
            icon: 'cloud_done',
            message: responseLogin.data.validationMessage
          })
          router.push({ name: 'homepage' })
        } else {
          $q.notify({
            color: 'negative',
            textColor: 'white',
            icon: 'warning',
            message: responseLogin.data.errors.join(', ')
          })
        }
      } catch (error) {
        console.error(error)
        $q.notify({
          color: 'negative',
          textColor: 'white',
          icon: 'warning',
          message: 'An error occurred while trying to log in'
        })
      }
    }

    const onReset = () => {
      username.value = null
      password.value = null
    }

    return {
      username,
      password,
      onSubmit,
      onReset
    }
  }
}
</script>
