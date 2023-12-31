import axios from 'axios';

const API_BASE_URL = "http://localhost:5000/questions";

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const getPaginatedQuestions = (page, pageSize, search = "") => {
  let query = `/all?page=${page}&pageSize=${pageSize}`;
  if (search) {
    query += `&search=${encodeURIComponent(search)}`;
  }
  return apiClient.get(query).then(response => response.data.items);
};

export const getQuestionById = (id) => apiClient.get(`/${id}`).then(response => response.data);

export const createQuestion = (questionData) => apiClient.post('/create', questionData).then(response => response.data);

export const updateQuestion = (id, questionData) => apiClient.put(`/update/${id}`, questionData).then(response => response.data);

export const deleteQuestion = (id) => apiClient.delete(`/${id}`).then(response => response.data);

export const searchQuestionsByTitle = (title) => apiClient.get(`/search/${encodeURIComponent(title)}`).then(response => response.data);
