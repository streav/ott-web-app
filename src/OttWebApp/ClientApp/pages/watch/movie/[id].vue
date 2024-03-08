<template>
  <PlayerHeader :item="movie"/>

  <div w-full h-full>
    <video w-full h-full ref="videoPlayerEl" class="video-js"
           :poster="movie.backdropUrl ? getTmdbImageUrl(movie.backdropUrl) : undefined"></video>
  </div>
</template>

<script setup lang="ts">
import videojs from 'video.js'
import type {Movie, StreamUrl} from '~/types'

definePageMeta({middleware: 'auth', layout: 'no-nav'})

const route = useRoute()

const movieRes = await getMovie(route.params.id as number)

const movie = ref(movieRes.data.value as Movie)

if (!movie.value) {
  throw createError({
    statusCode: 404,
    message: 'Movie not found'
  })
}

useHead({
  title: movie.value.title,
  meta: [
    {name: 'description', content: movie.value.overview}
  ]
})

let urlsRes = await getOnDemandStreamUrls(movie.value.streamId)

const urls = urlsRes.data.value as StreamUrl[] | null
if (!urls || urls.length === 0) {
  throw createError({
    statusCode: 404,
    message: 'Movie not found'
  })
}

const hlsUrl = urls.find(u => u.format === 'hls')
const mp4Url = urls.find(u => u.fileFormat === 'mp4')

if (!hlsUrl && !mp4Url) {
  throw createError({
    statusCode: 404,
    message: 'Movie not found'
  })
}

const type = hlsUrl ? 'application/x-mpegurl' : 'video/mp4'
const src = hlsUrl ? hlsUrl.url : mp4Url?.url

const videoPlayerEl = ref()
const player = ref()
const options = ref({
  autoplay: true,
  controls: true,
  controlBar: {
    pictureInPictureToggle: false
  },
  sources: [
    {
      type,
      src: src?.replace('host.docker.internal', 'localhost')
    }
  ]
})

onMounted(() => {
  player.value = videojs(videoPlayerEl.value, options.value)
})

onBeforeUnmount(() => {
  if (player.value) {
    player.value.dispose()
  }
})
</script>