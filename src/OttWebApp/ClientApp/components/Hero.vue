<template>
  <MediaHero v-if="item" :item="item" :type="props.type"/>
</template>

<script setup lang="ts">
import type {Movie, Show, MediaType} from '~/types'

const props = withDefaults(
    defineProps<{
      type?: MediaType
    }>(),
    {
      type: 'movie',
    }
)

const item = ref(null as Movie | Show | null)

onMounted(async () => {
  const {data, error} = props.type === 'movie' ? await getLatestMovie() : await getLatestShow()

  if (!error.value) {
    item.value = data.value
  }
})
</script>