<template>
  <CarouselBase>
    <template #title>
      {{ _.upperFirst(props.query) }} {{ props.type === 'movie' ? 'Movies' : 'Shows' }}
    </template>
    <template #more>
      <NuxtLink :to="`/${props.type}/explore?from=latest`" n-link>
        Explore more
      </NuxtLink>
    </template>

    <div style="height: 500px" v-if="loading">
      <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
    </div>

    <MediaCard
        v-else-if="result && result.length > 0"
        v-for="i of result || []"
        :key="i.id"
        :item="i"
        :type="props.type"
        flex-1 w-40 md:w-60
    />

    <div class="py8" v-else>
      <p>Nothing to show.</p>
    </div>
  </CarouselBase>
</template>

<script setup lang="ts">
import _ from 'lodash'
import type {Movie, Show, MediaType} from '~/types'

const props = withDefaults(
    defineProps<{
      type?: MediaType,
      query: 'latest' | 'popular'
    }>(),
    {
      type: 'movie',
      query: 'latest'
    }
)

const result = ref(null as Movie[] | Show[] | null)

const loading = ref(false)

function fetch() {
  if(props.type == 'show') {
    if(props.query == 'popular') {
      return getPopularShows()
    }
    
    return getLatestShows()
  }

  if(props.query == 'popular') {
    return getPopularMovies()
  }

  return getLatestMovies()
}

onMounted(async () => {
  loading.value = true

  const {data, error} = await fetch()

  if (!error.value) {
    result.value = data.value?.data as Movie[] | Show[]
  }

  loading.value = false
})

</script>