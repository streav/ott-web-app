<template>
  <div flex gap2 justify-end items-center p5>
    Season:
    <select rounded-md text-sm p1 px3 @change="onSeasonSelected">
      <option v-for="season in seasons" :key="season.number" :value="season.number" p-1>
        {{ season.name }}
      </option>
    </select>
  </div>

  <div flex justify-center p5 style="height: 500px" v-if="loading">
    <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
  </div>

  <MediaGrid v-else>
    <MediaEpisodeCard
        v-for="i of episodes || []"
        :key="i.number"
        :item="i"
        :show-id="item.id"
        :season-number="selectedSeason?.number"
        type="show"
        flex-1 w-40 md:w-60
    />
  </MediaGrid>
</template>

<script setup lang="ts">
import type {Show, ShowEpisode, ShowSeason} from '~/types'

const props = withDefaults(defineProps<{
  item: Show
}>(), {
  item: () => ({} as Show)
})

const loading = ref(false)

const {data} = await getShowSeasons(props.item.id)

const seasons = ref(data.value)

const episodes = ref([] as ShowEpisode[] | null)

const selectedSeason = ref({} as ShowSeason | null)

if (seasons.value && seasons.value.length > 0) {
  await loadSeason(seasons.value[0].number)
}

function onSeasonSelected(event) {
  const number = event.target.value
  return loadSeason(number)
}

async function loadSeason(number: number) {
  loading.value = true

  selectedSeason.value = seasons.value?.find(s => s.number == number)

  const {data, error} = await getShowEpisodes(props.item.id, number)

  episodes.value = error.value ? [] : data.value

  loading.value = false
}
</script>