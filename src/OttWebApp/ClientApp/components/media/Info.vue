<script setup lang="ts">
import type {Movie, Show, MediaType} from '~/types'
import {formatDate, formatLang, formatTime} from '~/composables/utils'

const props = withDefaults(defineProps<{
  item: Movie | Show
  type: MediaType
}>(), {
  item: () => ({} as Movie | Show),
  type: 'movie',
})
</script>

<template>
  <div p4 grid="~ cols-[max-content_1fr]" gap-8 items-center ma max-w-300>
    <img
        width="400"
        height="600"
        :src="getTmdbImageUrl(item.posterUrl)"
        :alt="props.item.title || props.item.name"
        block border="4 gray4/10" w-79 lt-md:hidden
        transition duration-400 object-cover aspect="10/16"
        :style="{ 'view-transition-name': `item-${props.item.id}` }"
    />
    <div lt-md:w="[calc(100vw-2rem)]" flex="~ col" md:p4 gap6>
      <div v-if="props.item.overview">
        <h2 text-3xl mb4>
          Storyline
        </h2>
        <div op80 v-text="props.item.overview"/>
      </div>

      <div text-sm op80>
        <ul grid="~ cols-[max-content_1fr] lg:cols-[max-content_1fr_max-content_1fr] gap3" items-center>
          <template v-if="props.item.release_date">
            <div>
              Release Date
            </div>
            <div>
              {{ formatDate(props.item.releaseDate) }}
            </div>
          </template>
          <template v-if="props.item.runtimeMinutes">
            <div>
              Runtime
            </div>

            <div>
              {{ formatTime(props.item.runtimeMinutes) }}
            </div>
          </template>
          <template v-if="props.item.director">
            <div>
              Director
            </div>

            <div>
              {{ props.item.director }}
            </div>
          </template>
          <template v-if="props.item?.genres?.length">
            <div>
              Genre
            </div>

            <div flex="~ row wrap gap1">
              <div
                  v-for="genre of props.item.genres" :key="genre.id"
                  bg="gray/10 hover:gray/20" p="x2 y1"
                  rounded text-xs
              >
                {{ genre.name }}
              </div>
            </div>
          </template>
          <template v-if="props.item.language">
            <div>
              Language
            </div>

            <div>
              {{ formatLang(props.item.language.toLowerCase()) }}
            </div>
          </template>
        </ul>
      </div>
    </div>
  </div>
</template>
