<script setup lang="ts">
import type { Show } from '~/types'

const route = useRoute()

const {data} = await getShow(route.params.id as number)

const item = ref(data.value as Show)

useHead({
  title: item.value.title,
  meta: [
    { name: 'description', content: item.value.overview }
  ],
})
</script>

<template>
  <div>
    <MediaHero :item="item" type="show" />
    <MediaDetails :item="item" type="show" />
  </div>
</template>