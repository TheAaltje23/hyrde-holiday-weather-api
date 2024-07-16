import { reactive } from 'vue'

export const store = reactive({
  username: localStorage.getItem('username') || ''
})
