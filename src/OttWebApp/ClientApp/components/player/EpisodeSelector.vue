<template>
  <div
      v-if="!visible"
      absolute bottom-20 right-0 z-10
      flex gap3 items-center p3 w-30
      bg="gray/15 hover:gray/20"
      title="Episodes"
      cursor-pointer
      @click="visible = true"
  >
    <div i-ph-list/>
    Episodes
  </div>

  <div v-show="visible" flex flex-col absolute bottom-10 bg-black border-1 rounded w-full p2 z-1 transition duration-300>
    
    <div flex justify-between w-full p4>
      <div flex gap2 justify-end p5>
        Season:
        <select rounded-md text-sm p1 px3 @change="onSeasonSelected">
          <option v-for="season in props.seasons" :key="season.number" :value="season.number" p-1>
            {{ season.name }}
          </option>
        </select>
      </div>

      <button @click="visible = false" i-ph-x-bold/>
    </div>

    <div min-h-20>
      <div flex justify-center p5 v-if="loading">
        <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
      </div>

      <CarouselBase v-else>
        <MediaEpisodeCard
            v-for="i of episodes"
            :key="i.number"
            :item="i"
            :show-id="showId"
            :season-number="selectedSeason?.number ?? 1"
            direct
            flex-1 w-40 md:w-60
        />
      </CarouselBase>

    </div>


  </div>
</template>

<script setup lang="ts">
import type {ShowEpisode, ShowSeason} from '~/types'

const props = defineProps<{
  showId: number,
  seasons: ShowSeason[]
}>()

const visible = ref(false)

const episodes = ref([] as ShowEpisode[] | null)
const loading = ref(false)
const selectedSeason = ref(null as ShowSeason | null | undefined)

function onSeasonSelected(event) {
  const number = event.target.value
  return loadSeason(number)
}

async function loadSeason(number: number) {
  loading.value = true

  selectedSeason.value = props.seasons?.find(s => s.number == number)

  const {data, error} = await getShowEpisodes(props.showId, number)

  episodes.value = error.value ? [] : data.value

  loading.value = false
}

onMounted(() => {
  if (props.seasons.length > 0) {
    return loadSeason(props.seasons[0].number)
  }
})
</script>