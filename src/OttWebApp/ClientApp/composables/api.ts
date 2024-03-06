import type {Movie, Person, Show, ShowEpisode, ShowSeason} from '~/types'

export function getPopularMovies() {
    return useFetch<Movie[]>('/api/movies/popular')
}

export function getPopularShows() {
    return useFetch<Show[]>('/api/shows/popular')
}

export function getLatestMovie() {
    return useFetch<Movie>('/api/movies/latest-movie')
}

export function getLatestShow() {
    return useFetch<Show>('/api/shows/latest-show')
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

export function getLatestMovies() {
    return useFetch<Movie[]>(`/api/movies/latest`)
}

export function getLatestShows() {
    return useFetch<Show[]>(`/api/shows/latest`)
}

export function getMovies(page: number, sortBy: string, name: string | null) {
    return useFetch<Movie[]>(`/api/movies?page=${page}&sortBy=${sortBy}&name=${name}`)
}

export function getShows(page: number, sortBy: string, name: string | null) {
    return useFetch<Show[]>(`/api/shows?page=${page}&sortBy=${sortBy}&name=${name}`)
}

export function getPerson(id: number) {
    return useFetch<Person>(`/api/person/${id}`)
}

export function signUp(email: string, password: string, plan: string) {
    return useFetch(`api/auth/register`, {
        method: 'post',
        body: {
            email,
            password,
            plan
        }
    })
}