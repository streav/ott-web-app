<template>
  <div @click="emits('select')" class="flex flex-col gap-8 rounded-2 px-6 py-12"
       :class="props.active ? 'border-3 border-primary' : 'border-1 border-gray:50'">
    <div class="flex flex-col gap-2"><h6 class="body-semibold text-slate-12">{{ props.plan }}</h6>
      <div class="flex items-center gap-3"><h5 class="text-[32px] font-bold leading-9">${{ props.price }}</h5>
        <div class="flex flex-col items-start">
          <span class="caption">USD</span>
          <span class="caption-s text-slate-11">Billed monthly</span>
        </div>
      </div>
    </div>
    <button
        @click="emits('select')"
        :class="props.active ? 'bg-primary' : 'ring-1 ring-inset hover:bg-gray:50 hover:text-slate-12'"
        class="inline-flex gap-0.5 justify-center overflow-hidden text-sm font-medium transition rounded-[4px] py-1 px-3 w-[256px]">
      Select
    </button>
    <div class="flex flex-col gap-4">
      <div class="flex items-center gap-3">
        <div :class="props.active ? 'text-primary' : ''" text-xl i-ph-check/>
        <p>TV, Computer, Phone, Tablet</p>
      </div>

      <div class="flex items-center gap-3">
        <div :class="props.active ? 'text-primary' : ''" text-xl i-ph-check/>
        <p>Movies, Shows, Live</p>
      </div>

      <div class="flex items-center gap-3">
        <div :class="props.active ? 'text-primary' : ''" text-xl i-ph-check/>
        <p>{{ props.devices }} Simultaneous {{ props.devices === 1 ? 'Device' : 'Devices' }}</p>
      </div>

    </div>
  </div>
</template>

<script setup lang="ts">
const props = withDefaults(defineProps<{
  plan: string,
  price: number,
  devices: number,
  active: boolean
}>(), {
  active: false
})

const emits = defineEmits(['select'])
</script>