import axios from "axios"
import { CommentGet, CommentPost } from "../Models/Comment"
import { handleError } from "../Helpers/ErrorHandler"

const api = "http://localhost:5242/api/comment/"



export const commentPostAPI = async (title: string, content: string, symbol: string) => {
    try {
        const data = axios.post<CommentPost>(api + `${symbol}`, {
            title: title,
            content: content
        })
        return data
    } catch (error) {
        handleError(error)
    }
}

export const commentGetAPI = async (symbol: string) => {
    try {
        const data = axios.get<CommentGet[]>(api + `?Symbol=${symbol}`)
        return data
    } catch (error) {
        handleError(error)
    }
}