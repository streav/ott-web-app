/**
 * Format minutes into hours and mins
 */
export function formatTime(minutes: number) {
    // seconds
    let secondsLeft = minutes * 60

    // hours
    const hours = Math.floor(secondsLeft / 3600)
    secondsLeft = secondsLeft % 3600

    // mins
    const mins = Math.floor(secondsLeft / 60)
    secondsLeft = secondsLeft % 60

    return `${hours ? `${hours}h` : ''} ${mins}min`
}

export const {format: formatVote} = Intl.NumberFormat('en-GB', {notation: 'compact', maximumFractionDigits: 1})