<script setup lang="ts">
import type {Person} from '~/types'

const id = useRouteParam<string>('id')
const {data} = await getPerson(id.value)

const person = data.value as Person

useHead({
  title: person.name,
  meta: [
    {name: 'description', content: person.biography || person.name}
  ]
})
</script>

<template>
  <PersonInfo :item="person"/>
</template>