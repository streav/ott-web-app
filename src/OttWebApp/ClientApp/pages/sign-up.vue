<template>
  <h1 flex="~" p8 gap2 text-xl lg:text-2xl justify-center text-center>
    Choose the plan that’s right for you
  </h1>

  <PricingGrid v-model:plan="plan" p8/>

  <div ref="divider" class="flex justify-center py8 px10">
    <div class="w-full bg-gray:5 h-2px"/>
  </div>

  <h1 flex="~" p8 gap2 text-xl lg:text-2xl justify-center text-center>
    Create an account to start your membership
  </h1>

  <SignupForm p8 :plan="plan.name"/>

  <TheFooter/>

</template>
<script setup lang="ts">
import {plans} from '~/constants/plans'

useHead({
  title: 'Sign up'
})

const plan = ref(plans[1])

const divider = ref()

watch(plan, () => {
  if (divider.value) {
    divider.value.scrollIntoView({behavior: 'smooth'})
  }
})
</script>