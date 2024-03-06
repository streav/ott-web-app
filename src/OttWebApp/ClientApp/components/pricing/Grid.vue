<template>
  <div class="mx-auto flex flex-col items-center justify-center gap-6 lg:flex-row">
    <PricingCard v-for="plan in plans" :plan="plan.name" :price="plan.price" :devices="plan.devices"
                 :active="plan.name === props.plan.name" @select="emits('update:plan', plan)"/>
  </div>
</template>

<script setup lang="ts">
import type {PricingPlan} from '~/types'
import {plans} from '~/constants/plans'

const props = withDefaults(defineProps<{
  plan: PricingPlan
}>(), {
  plan: () => plans[1]
})

const emits = defineEmits(['update:plan'])
</script>