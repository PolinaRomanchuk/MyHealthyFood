import axios from 'axios'

const baseUrl = 'https://localhost:7103/api/';

const GetReactGames = (page, perPage) =>
    axios
        .get(`${baseUrl}game/GetGames?page=${page}&perPage=${perPage}`);

export const gamesApi = {
    GetReactGames
};

