<script setup>
import '@unocss/reset/tailwind.css'

useHead({
  htmlAttrs: {
    lang: 'en',
  },
  charset: 'utf-8',
  title: 'Streav OTT Web App',
  titleTemplate: title => title !== 'Streav OTT Web App' ? `${title} · Streav OTT Web App` : title,
  meta: [
    { name: 'description', content: 'An OTT web app built with Streav to show the potential of it.' }
  ]
})
</script>

<template>
  <NuxtLoadingIndicator />
  <NuxtLayout>
    <NuxtPage />
  </NuxtLayout>
</template>

<style>
html, body , #__nuxt{
  height: 100vh;
  margin: 0;
  padding: 0;
  background: #111;
  color: white;
  color-scheme: dark;
}
</style>
