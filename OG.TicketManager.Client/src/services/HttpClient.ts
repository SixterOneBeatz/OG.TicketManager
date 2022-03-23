import axios, { AxiosRequestConfig } from "axios";


axios.defaults.baseURL = process.env.API_URL;
axios.interceptors.request.use(
  (config) => {
    const TOKEN: string | null = '';
    if (TOKEN !== null) {
      config.headers = {
        Authorization: `bearer ${TOKEN}`,
      };
    }
    return config;
  },
  (error) => Promise.reject(error)
);

const client = {
  get<T = any>(url: string, config?: AxiosRequestConfig) {
    return axios.get<T>(url, config);
  },
  post<T = any>(url: string, body?: any, config?: AxiosRequestConfig) {
    return axios.post<T>(url, body, config);
  },
  put<T = any>(url: string, body?: any, config?: AxiosRequestConfig) {
    return axios.put<T>(url, body, config);
  },
  delete<T = any>(url: string, config?: AxiosRequestConfig) {
    return axios.delete<T>(url, config);
  },
};

export default client;