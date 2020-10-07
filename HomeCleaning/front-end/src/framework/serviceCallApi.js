//https://gist.github.com/franklinjavier

import axios from 'axios'
import { shim } from 'promise.prototype.finally'
import locale from 'constants/locale'
import { is } from 'utils'
import { slugify } from 'utils/string'
import { translateNotification } from 'utils/response'

shim()

const axiosInstance = axios.create({
    baseURL: process.env.REACT_APP_PROXY || '',
    withCredentials: true
})

const parseError = data => {
    data = data.responseJSON || data

    const dontTranslate = /COMMITMENT_LOCATION_HAS_CHANGED|PROCESSING_PAYMENT|OUT_OF_STOCK|CART_WITHOUT_ITEMS/i
    const needTranslationNotification = /PRICE_CHANGE|OUT_OF_STOCK|GIFT_REMOVED/i

    try {
        data = JSON.parse(data.responseText)
    } catch (o_O) {}

    if (is.string(data)) {
        return locale[data] || data
    }

    const arr = is.array(data) ? data : [data]

    if (/^COUPON/.test(arr[0].logref) && arr[0].message) {
        return (
            locale[arr[0].message] ||
            locale[slugify(arr[0].message, '_').toUpperCase()] ||
            arr[0].message ||
            locale[arr[0].logref] ||
            locale['try_again']
        )
    }

    if (needTranslationNotification.test(arr[0].logref)) {
        return translateNotification(arr) || locale['try_again']
    }

    if (dontTranslate.test(arr[0].logref)) {
        return arr[0].logref
    }

    return locale[arr[0].message] || locale[arr[0].logref] || locale['try_again']
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