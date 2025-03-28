import axios from "axios";

const token = localStorage.getItem("token"); // Pode ser null
const parsedToken = token ? JSON.parse(token) : null; // Evita erro de JSON.parse()

export const HTTPClient = axios.create({
  baseURL: "http://localhost:5108",
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Headers": "Authorization",
    "Access-Control-Allow-Methods": "GET, POST, OPTIONS, PUT, PATCH, DELETE",
    "Content-Type": "application/json;charset=UTF-8",
    ...(parsedToken && { Authorization: `Bearer ${parsedToken}` }) // Só adiciona se existir
  }
});
