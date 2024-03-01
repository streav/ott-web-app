<script setup lang="ts">
import type {Show, Movie, MediaType} from '~/types'

const props = withDefaults(
    defineProps<{
      items: Movie[] | Show[]
      type: MediaType
      fetch: (page: number) => Promise<void>
      end: boolean,
      page: number
    }>(),
    {},
)

const emits = defineEmits(['update:page'])

const tailEl = ref<HTMLDivElement>()

const isLoading = ref(false)

async function loadingNext() {
  if (isLoading.value)
    return
  isLoading.value = true
  try {
    const nextPage = props.page + 1
    emits('update:page', nextPage)
    await props.fetch(nextPage)
  } finally {
    isLoading.value = false
  }
}

onMounted(() => {
  window.addEventListener('wheel', scrollListener)
})

onUnmounted(() => {
  window.removeEventListener('wheel', scrollListener)
})

loadingNext()

function scrollListener() {
  if (!tailEl.value || isLoading.value || props.end) return

  const {top} = tailEl.value.getBoundingClientRect()
  const delta = top - window.innerHeight

  if (delta < 400) {
    loadingNext()
  }
}
</script>

<template>
  <div>
    <slot/>
    <MediaGrid>
      <MediaCard
          v-for="item of items"
          :key="item.id"
          :type="type"
          :item="item"
      />
    </MediaGrid>
    <div ref="tailEl"/>
    <div v-if="isLoading" flex justify-center p2>
      <div class="i-svg-spinners:180-ring w-2em h-2em"></div>
    </div>
  </div>
</template>
