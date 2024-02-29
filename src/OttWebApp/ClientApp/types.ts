export type MediaType = 'movie' | 'show' | 'tv'

export interface PaginatedList<T> {
    page: number;
    totalPages: number;
    total: number;
    data: T[];
}

export interface Movie {
    id: number;
    title?: string | null;
    overview?: string | null;
    releaseDate?: string | null;
    runtimeMinutes?: number | null;
    language?: string | null;
    countryCode?: string | null;
    posterUrl?: string | null;
    backdropUrl?: string | null;
    rating?: number | null;
    ratingSource?: string | null;
    director?: string | null;
    streamId?: number | null;
    genres?: Genre[] | null;
    casts?: Cast[] | null;
}

export interface Show {
    id: number;
    title?: string | null;
    overview?: string | null;
    releaseDate?: string | null;
    lastReleaseDate?: string | null;
    runtimeMinutes?: number | null;
    language?: string | null;
    countryCode?: string | null;
    posterUrl?: string | null;
    backdropUrl?: string | null;
    rating?: number | null;
    ratingSource?: string | null;
    director?: string | null;
    genres?: Genre[] | null;
    casts?: Cast[] | null;
}

export interface Genre {
    id: number;
    name?: string | null;
}

export interface Cast {
    id: number;
    name?: string | null;
    profilePictureUrl?: string | null;
    character?: string | null;
}

export enum PersonGender {
    Unspecified = 0,
    Female = 1,
    Male = 2,
    NonBinary = 3,
}

export interface Person {
    id: number;
    name?: string | null;
    biography?: string | null;
    knownFor?: string | null;
    birthDate?: string | null;
    deathDay?: string | null;
    birthPlace?: string | null;
    profilePictureUrl?: string | null;
    gender: PersonGender;
    imdbId?: string | null;
    popularity: number;
}