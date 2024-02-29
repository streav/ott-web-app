import type {Movie, PaginatedList, Show, ShowEpisode, ShowSeason} from '~/types'

export function getPopularMovies() {
    return useFetch<PaginatedList<Movie>>('/api/movies/popular')
}

export function getPopularShows() {
    return useFetch<PaginatedList<Show>>('/api/shows/popular')
}

export function getLatestMovie() {
    return useFetch<Movie>('/api/movies/latest')
}

export function getMovie(id: number) {
    return useFetch<Movie>(`/api/movies/${id}`)
}

export function getShow(id: number) {
    return useFetch<Show>(`/api/shows/${id}`)
}

export function getShowSeasons(id: number) {
    return useFetch<ShowSeason[]>(`/api/shows/${id}/seasons`)
}

export function getShowEpisodes(id: number, number: number) {
    return useFetch<ShowEpisode[]>(`/api/shows/${id}/seasons/${number}`)
}