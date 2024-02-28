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
    releaseDate?: Date | null;
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
    casts?: BasicPerson[] | null;
}

export interface Show {
    id: number;
    title?: string | null;
    overview?: string | null;
    releaseDate?: Date | null;
    lastReleaseDate?: Date | null;
    runtimeMinutes?: number | null;
    language?: string | null;
    countryCode?: string | null;
    posterUrl?: string | null;
    backdropUrl?: string | null;
    rating?: number | null;
    ratingSource?: string | null;
    director?: string | null;
    genres?: Genre[] | null;
    casts?: BasicPerson[] | null;
}

export interface Genre {
    id: number;
    name?: string | null;
}

export interface BasicPerson {
    id: number;
    name?: string | null;
    profilePictureUrl?: string | null;
}
