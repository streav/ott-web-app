<template>
  <video w-full h-full ref="videoPlayerEl" class="video-js"></video>
</template>

<script setup lang="ts">
import videojs from 'video.js'

definePageMeta({middleware: 'auth'})

const videoPlayerEl = ref()
const player = ref()
const options = ref({
  autoplay: true,
  controls: true,
  sources: [
    {
      src: ''
    }
  ]
})

onMounted(() => {
  player.value = videojs(videoPlayerEl.value, options.value)
})

onUnmounted(() => {
  if (player.value) {
    player.value.dispose()
  }
})
</script>