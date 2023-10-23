import axios from "axios";
import ServerURL from "./ServerURL";

export const axiosInstance = axios.create({baseURL: ServerURL});
