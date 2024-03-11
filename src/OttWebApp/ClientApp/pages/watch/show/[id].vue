<template>
  <div flex justify-center items-center w-full h-full v-if="loading">
    <div class="i-svg-spinners:180-ring w-3em h-3em"></div>
  </div>

  <div v-else w-full h-full>
    <PlayerHeader :item="show" :episode="episode" :season-number="seasonNumber"/>

    <PlayerEpisodeSelector :show-id="showId" :seasons="seasons"/>

    <video w-full h-full ref="videoPlayerEl" class="video-js"
           :poster="episode?.backdropUrl?.replace('{TMDB_BASE_PATH}', 'https://image.tmdb.org/t/p/original')"></video>
  </div>

</template>

<script setup lang="ts">
import videojs from 'video.js'
import type {Show, ShowEpisode, ShowSeason, StreamUrl} from '~/types'

definePageMeta({middleware: 'auth', layout: 'no-nav'})

const loading = ref(false)

const route = useRoute()

const showId = ref()
const seasonNumber = ref()

const show = ref(null as Show | null)
const seasons = ref([] as ShowSeason[])
const episodes = ref([] as ShowEpisode[])
const episode = ref(null as ShowEpisode | null | undefined)

const videoPlayerEl = ref()
const player = ref()
const options = ref()

await fetchEpisode()

async function fetchEpisode() {
  loading.value = true

  showId.value = parseInt(route.params.id as string)

  const showRes = await getShow(showId.value as number)

  show.value = showRes.data.value

  if (!show.value) {
    loading.value = false

    throw createError({
      statusCode: 404,
      message: 'Show not found'
    })
  }

  seasonNumber.value = parseInt(route.query.season as string)
  let episodeNumber = parseInt(route.query.episode as string)

  const seasonsRes = await getShowSeasons(showId.value as number)

  seasons.value = seasonsRes.data.value

  if (!seasons || seasons.value.length === 0) {
    loading.value = false

    throw createError({
      statusCode: 404,
      message: 'Show not found'
    })
  }

  if (isNaN(seasonNumber.value) || !seasons.value.find(s => s.number === seasonNumber.value)) {
    seasonNumber.value = seasons.value[0].number
  }

  const episodesRes = await getShowEpisodes(showId.value as number, seasonNumber.value as number)

  if (episodesRes.data?.value && episodesRes.data.value.length > 0) {
    episodes.value = episodesRes.data.value
  } else {
    loading.value = false

    throw createError({
      statusCode: 404,
      message: 'Show not found'
    })
  }

  if (isNaN(episodeNumber) || !episodes.value.find(e => e.number === episodeNumber)) {
    episodeNumber = episodes.value[0].number
  }

  episode.value = episodes.value.find(e => e.number === episodeNumber)

  useHead({
    title: `S${seasonNumber.value}:E${episodeNumber} ${show.value.title}`,
    meta: [
      {name: 'description', content: show.value.overview}
    ]
  })

  let urlsRes = await getOnDemandStreamUrls(episode.value.streamId)

  const urls = urlsRes.data.value as StreamUrl[] | null
  if (!urls || urls.length === 0) {
    loading.value = false

    throw createError({
      statusCode: 404,
      message: 'Show not found'
    })
  }

  const hlsUrl = urls.find(u => u.format === 'hls')
  const mp4Url = urls.find(u => u.fileFormat === 'mp4')

  if (!hlsUrl && !mp4Url) {
    loading.value = false

    throw createError({
      statusCode: 404,
      message: 'Show not found'
    })
  }

  const type = hlsUrl ? 'application/x-mpegurl' : 'video/mp4'
  const src = hlsUrl ? hlsUrl.url : mp4Url?.url

  options.value = {
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
  }

  loading.value = false
}

async function queryChanged() {
  if (player.value) {
    player.value.dispose()
  }

  await fetchEpisode()

  player.value = videojs(videoPlayerEl.value, options.value)
}

onMounted(() => {
  player.value = videojs(videoPlayerEl.value, options.value)
})

onBeforeUnmount(() => {
  if (player.value) {
    player.value.dispose()
  }
})

watch(() => route.query.season, () => {
  return queryChanged()
})

watch(() => route.query.episode, () => {
  return queryChanged()
})
</script>