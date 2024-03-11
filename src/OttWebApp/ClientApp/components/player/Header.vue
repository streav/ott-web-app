<template>
  <div :style="{opacity: show ? 1 : 0}" flex flex-col gap-2 absolute z-1
       bg-gradient-to-b from-black:80 via-black:40 to-transparent p8 w-full
       @mouseenter="resetTimeout" @mouseleave="setHideTimeout" @click="show = !show" transition duration-500>
    <div text-3xl flex gap4 items-center>
      {{ props.item.title }}
      <span p="x2 y1" rounded text-xs bg="gray/10" v-if="props.item.releaseDate">{{
          props.item.releaseDate.slice(0, 4)
        }}</span>
    </div>
    <div flex="~ row wrap gap1">
      <div
          v-for="genre of props.item.genres" :key="genre.id"
          bg="gray/10" p="x2 y1"
          rounded text-xs
      >
        {{ genre.name }}
      </div>
    </div>
    <div v-if="props.episode" text-xl>
      S{{ props.seasonNumber }}:E{{ props.episode.number }} "{{ props.episode.name }}"
    </div>
    <div max-w-200>{{ props.episode?.overview ?? props.item.overview }}</div>
  </div>
</template>

<script setup lang="ts">
import type {Movie, Show, ShowEpisode} from '~/types'

const props = defineProps<{
  item: Movie | Show;
  episode: ShowEpisode | undefined,
  seasonNumber: number | undefined
}>()

const show = ref(true)
const timeout = ref()

function resetTimeout() {
  show.value = true
  if (timeout.value) {
    clearTimeout(timeout.value)
  }
}

function setHideTimeout() {
  show.value = true
  setTimeout(() => show.value = false, 5000)
}

onMounted(() => setHideTimeout())
</script>