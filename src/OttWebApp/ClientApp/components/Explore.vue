<script setup lang="ts">
import _ from 'lodash'
import type {Movie, Show, MediaType} from '~/types'

const props = withDefaults(
    defineProps<{
      type?: MediaType
    }>(),
    {
      type: 'movie',
    }
)

const route = useRoute()
const from = computed(() => route.query.from as string)

const sortBy = ref(from.value === 'popular' ? 'rating_desc' : 'releaseDate_desc')
const search = ref('')

const page = ref(0 as number)
const end = ref(false)

const items = ref([] as Movie[] | Show[])

async function fetch(page: number) {
  const result = props.type === 'movie' ? await getMovies(page, sortBy.value, search.value) : await getShows(page, sortBy.value, search.value)

  if (!result.data.value || result.data.value.length === 0) {
    end.value = true
    return
  }

  items.value.push(...result.data.value)
}

function resetPagination() {
  end.value = false
  page.value = 1
  items.value = []
  return fetch(1)
}

watch(search, _.debounce(function () {
  resetPagination()
}, 500))

watch(sortBy, (newValue, oldValue) => {
  if (newValue != oldValue) {
    resetPagination()
  }
})
</script>

<template>
  <MediaAutoLoadGrid
      :fetch="fetch"
      :type="props.type"
      :items="items"
      v-model:page="page"
      :end="end"
  >
    <h1 flex="~" px8 pt8 gap2 text-3xl>
      Explore {{ props.type === 'movie' ? 'Movies' : 'Shows' }}
    </h1>

    <div flex flex-col gap3 p8>
      <div flex items-center>
        <span w-20>Search:</span>
        <input v-model="search" rounded-md text-sm placeholder="Search by name" p1 px3 w-60/>
      </div>
      <div flex items-center>
        <span w-20>Sort By:</span>
        <select v-model="sortBy" rounded-md text-sm p1 px3 w-60>
          <option value="releaseDate">Release Date</option>
          <option value="releaseDate_desc">Release Date Desc.</option>
          <option value="rating">Rating</option>
          <option value="rating_desc">Rating Desc.</option>
        </select>
      </div>
    </div>
  </MediaAutoLoadGrid>
</template>