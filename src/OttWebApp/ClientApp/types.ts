export type MediaType = 'movie' | 'show' | 'tv'

export interface Movie {
    id: number;
    title?: string;
    overview?: string;
    releaseDate?: string;
    runtimeMinutes?: number;
    language?: string;
    countryCode?: string;
    posterUrl?: string;
    backdropUrl?: string;
    rating?: number;
    ratingSource?: string;
    director?: string;
    streamId?: number;
    genres?: Genre[];
    casts?: Cast[];
}

export interface Show {
    id: number;
    title?: string;
    overview?: string;
    releaseDate?: string;
    lastReleaseDate?: string;
    runtimeMinutes?: number;
    language?: string;
    countryCode?: string;
    posterUrl?: string;
    backdropUrl?: string;
    rating?: number;
    ratingSource?: string;
    director?: string;
    genres?: Genre[];
    casts?: Cast[];
}

export interface Genre {
    id: number;
    name?: string;
}

export interface Cast {
    id: number;
    name?: string;
    profilePictureUrl?: string;
    character?: string;
}

export enum PersonGender {
    Unspecified = 0,
    Female = 1,
    Male = 2,
    NonBinary = 3,
}

export interface Person {
    id: number;
    name?: string;
    biography?: string;
    knownFor?: string;
    birthDate?: string;
    deathDay?: string;
    birthPlace?: string;
    profilePictureUrl?: string;
    gender: PersonGender;
    imdbId?: string;
    popularity: number;
}

export interface ShowSeason {
    number: number;
    name?: string;
    overview?: string;
    releaseDate?: string;
    posterUrl?: string;
    backdropUrl?: string;
}

export interface ShowEpisode {
    number: number;
    name?: string;
    overview?: string;
    releaseDate?: Date;
    runtimeMinutes?: number;
    backdropUrl?: string;
    streamId?: number;
}

export interface PricingPlan {
    name: string,
    price: number,
    devices: number
}

export interface StreamUrl {
    format: string,
    fileFormat?: string,
    url: string
}