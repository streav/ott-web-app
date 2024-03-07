<template>
  
</template>

<script setup lang="ts">
definePageMeta({middleware: 'auth', layout: 'no-nav'})

const videoPlayerEl = ref()
const player = ref()
const options = ref({})

onMounted(() => {
  
})

onUnmounted(() => {
  if (player.value) {
    
  }
})
</script>