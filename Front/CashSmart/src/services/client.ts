import axios from "axios";

export const HTTPClient = axios.create({
  baseURL: "http://localhost:5108",
  headers: {
    "Content-Type": "application/json;charset=UTF-8",
  }
});

// Interceptor para adicionar o token dinamicamente
HTTPClient.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  const parsedToken = token ? JSON.parse(token) : null;

  if (parsedToken) {
    if (config.headers) {
      config.headers.Authorization = `Bearer ${parsedToken}`;
    }
  }

  return config;
});
