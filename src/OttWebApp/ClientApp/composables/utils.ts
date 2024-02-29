import LANGUAGES from '~/constants/languages'

export function formatDate(string: string) {
    return new Date(string).toLocaleDateString()
}

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

    return `${hours ? `${hours}h` : ''} ${mins}min`
}

export function formatLang(iso: string) {
    const fullLang = LANGUAGES.find(lang => lang.iso_639_1 === iso)

    if (fullLang)
        return fullLang.english_name

    return iso
}

export const {format: formatVote} = Intl.NumberFormat('en-GB', {notation: 'compact', maximumFractionDigits: 1})

export function getTmdbImageUrl(url: string | null) {
    return url?.replace('{TMDB_BASE_PATH}', 'https://image.tmdb.org/t/p/w500')
}