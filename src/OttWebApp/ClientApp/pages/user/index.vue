<template>

  <div flex flex-col gap12 p8>
    <h1 flex="~" gap2 text-3xl>
      Account Information
    </h1>

    <div flex flex-col gap3 v-if="user">
      <div class="flex items-center">
        <div class="font-bold w-50">Email:</div>
        <div>{{ user.email }}</div>
      </div>
      <div class="flex items-center">
        <div class="font-bold w-50">Valid until:</div>
        <div>{{ new Date(user?.expiresAt).toLocaleString() }}</div>
      </div>
      <div class="flex items-center">
        <div class="font-bold w-50">Simultaneous Devices:</div>
        <div>{{ user?.maxConnections }}</div>
      </div>
    </div>

    <button @click="logout"
            class="flex items-center gap3 px-4 py-2 rounded border text-red-500 border-red-500 hover:text-red-700 hover:border-red-700 w-30">
      <i class="i-ph-sign-out"/>
      Logout
    </button>


  </div>

</template>

<script setup>
definePageMeta({middleware: 'auth'})

useHead({title: 'Account Information'})

const auth = useAuth()

const user = ref(null)

try {
  user.value = await auth.getSession()
} catch (e) {
  throw createError({
    statusCode: 404
  })
}

async function logout() {
  await auth.signOut({external: true})
}
</script>