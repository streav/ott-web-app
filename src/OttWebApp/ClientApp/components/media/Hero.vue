<script setup lang="ts">
import type {MediaType, Movie, Show} from '~/types'
import {formatTime} from '~/composables/utils'

const props = withDefaults(defineProps<{
  item: Movie | Show,
  type: MediaType
}>(), {
  type: 'movie',
  item: () => ({} as Movie)
})

const mounted = useMounted()
</script>

<template>
  <div :key="item.id" relative class="aspect-ratio-3/2 lg:aspect-ratio-25/9" bg-black>
    <div
        absolute top-0 right-0
        lt-lg="left-0"
        lg="bottom-0 left-1/3"
    >
      <img
          width="1220"
          height="659"
          :src="props.item.backdropUrl?.replace('{TMDB_BASE_PATH}', 'https://image.tmdb.org/t/p/original')"
          :alt="props.item.title"
          h-full w-full object-cover
      />
    </div>
    <div
        absolute bottom-0 left-0 top-0 px-10
        flex="~ col" justify-center
        lt-lg="bg-gradient-to-t right-0 p10"
        lg="px25 w-2/3 bg-gradient-to-r"
        from-black via-black to-transparent
    >
      <Transition appear name="hero">
        <div v-show="mounted">
          <h1 py-2 text-4xl lg:text-5xl line-clamp-2>
            {{ props.item.title }}
          </h1>
          <div flex="~ row wrap" gap2 items-center mt4>
            <StarsRate w-25 :value="props.item.rating"/>
            <div class="op50 hidden md:block">
              {{ formatVote(props.item.rating) }}
            </div>
            <span op50>·</span>
            <div v-if="props.item.releaseDate" op50>
              {{ props.item.releaseDate.slice(0, 4) }}
            </div>
            <span op50>·</span>
            <div v-if="props.item.runtimeMinutes" op50>
              {{ formatTime(props.item.runtimeMinutes) }}
            </div>
          </div>
          <p class="mt-2 op80 leading-relaxed of-hidden line-clamp-3 md:line-clamp-5 text-xs md:text-base">
            {{ props.item.overview }}
          </p>
          <div class="py5 display-none lg:block">
            <NuxtLink
                flex="~ gap2" items-center p="x6 y3" w-30
                bg="gray/15 hover:gray/20" transition
                title="Watch"
                :to="`/watch/${props.type}/${props.item.id}`"
            >
              <div i-ph-play/>
              Watch
            </NuxtLink>
          </div>
        </div>
      </Transition>
    </div>
    <div lg:hidden absolute left-0 top-0 right-0 h="2/3" items-center justify-center>
      <NuxtLink
          items-center p10 text-5xl op20 hover:op80 transition
          title="Watch"
          :to="`/watch/${props.type}/${props.item.id}`"
      >
        <div i-ph-play-circle-light/>
      </NuxtLink>
    </div>
  </div>
</template>

<style>
.hero-enter-active,
.hero-leave-active {
  transition: transform .75s cubic-bezier(.4, .25, .3, 1), opacity .3s cubic-bezier(.4, .25, .3, 1);
}

.hero-enter-from,
.hero-leave-to {
  opacity: 0;
  transform: translate3d(0, 2rem, 0);
}

.hero-enter-to,
.hero-leave-from {
  opacity: 1;
  transform: translateZ(0);
}
</style>
