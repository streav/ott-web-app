<template>
  <ShowHero/>
  <CarouselPopularShows :explore="false"/>
  <CarouselLatestShows :explore="false"/>
  <TheFooter/>
</template>

<script setup lang="ts">
useHead({
  title: 'Shows'
})
</script>