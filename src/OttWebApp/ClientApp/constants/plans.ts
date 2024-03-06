import type {PricingPlan} from '~/types'

export const plans = [
    {
        name: 'Basic',
        price: 5,
        devices: 1
    },
    {
        name: 'Standard',
        price: 10,
        devices: 3
    },
    {
        name: 'Premium',
        price: 15,
        devices: 5
    }
] as PricingPlan[]