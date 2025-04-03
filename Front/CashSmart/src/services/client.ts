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


// Interceptor para tratar erros
HTTPClient.interceptors.response.use(
  (response) => response, // Se a resposta for bem-sucedida, apenas retorne
  (error) => {
      // Tratamento centralizado de erros
      if (error.response) {
          // Erros 4xx/5xx (como 400, 401, 500)
          const errorMessage = error.response.data?.message || "Erro desconhecido na requisição";
          return Promise.reject(new Error(errorMessage));
      } else if (error.request) {
          // A requisição foi feita, mas não houve resposta (ex: timeout)
          return Promise.reject(new Error("Sem resposta do servidor"));
      } else {
          // Erro genérico (ex: erro de configuração do Axios)
          return Promise.reject(error);
      }
  }
);

export default HTTPClient;
