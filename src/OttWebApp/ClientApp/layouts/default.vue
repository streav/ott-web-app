<template>
  <div
      h-full w-full font-sans
      grid="~ lt-lg:rows-[1fr_max-content] lg:cols-[max-content_1fr]"
      of-hidden view-transition-app transition duration-0
  >
    <div id="app-scroller" of-x-hidden of-y-auto relative>
      <NuxtPage/>
    </div>
    <NavBar lg:order-first/>
  </div>
</template>