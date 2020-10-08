//https://gist.github.com/franklinjavier

import axios from 'axios'
import { shim } from 'promise.prototype.finally'

shim()

const axiosInstance = axios.create({
    baseURL: process.env.REACT_APP_PROXY || '',
    withCredentials: true
})

const parseError = data => {
    data = data.responseJSON || data

    try {
        data = JSON.parse(data.responseText)
    } catch (o_O) {}

    return data
}

axiosInstance.interceptors.response.use(
    response => response,
    error => {
        const _response = error.response || {} || error
        if (_response.status >= 400) {
            throw new Error(parseError(_response.data))
        }
        return _response
    }
)

const api = {
    get(url, params) {
        return axiosInstance.get(url, { params })
    },

    post(url, data, headers = { 'content-type': 'application/json' }) {
        return axiosInstance.post(url, data, { headers })
    },

    put(url, data, headers = { 'content-type': 'application/json' }) {
        return axiosInstance.put(url, data, { headers })
    },

    delete(url) {
        return axiosInstance.delete(url)
    }
}

export default api