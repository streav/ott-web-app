import type {Movie, PaginatedList, Show} from '~/types'

export function getPopularMovies() {
    return useFetch<PaginatedList<Movie>>('/api/movies/popular')
}

export function getPopularShows() {
    return useFetch<PaginatedList<Show>>('/api/shows/popular')
}

export function getLatestMovie() {
    return useFetch<Movie>('/api/movies/latest')
}

export function getMovie(id: string) {
    return useFetch<Movie>(`/api/movies/${id}`)
}

export function getShow(id: string) {
    return useFetch<Show>(`/api/shows/${id}`)
}