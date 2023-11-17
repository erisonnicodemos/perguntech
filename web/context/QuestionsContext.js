import {
  createQuestion,
  searchQuestionsByTitle,
  updateQuestion,
  deleteQuestion,
  getPaginatedQuestions,
} from "@/services/api";
import React, { createContext, useState, useEffect } from "react";

export const QuestionsContext = createContext();

export const QuestionsProvider = ({ children }) => {
  const [questions, setQuestions] = useState([]);
  const [error, setError] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const pageSize = 5;

  const searchQuestions = async (title) => {
    if (title.length > 2) {
      try {
        const results = await searchQuestionsByTitle(title);
        setQuestions(results);
        setError("");
      } catch (error) {
        setError("Error when find the questions.");
        setQuestions([]);
      }
    } else {
      setQuestions([]);
    }
  };

  const loadQuestions = async (search = "") => {
    try {
      const response = await getPaginatedQuestions(
        currentPage,
        pageSize,
        search
      );
      setQuestions((prevQuestions) => [...prevQuestions, ...response]);
      setError("");
      
      setHasMore(response.length === pageSize);
    } catch (error) {
      setError("Error when fetching questions.");
      setQuestions([]);
    }
  };

  useEffect(() => {
    loadQuestions();
  }, [currentPage]);

  const getAllQuestions = async (search = "") => {
    try {
      const response = await getPaginatedQuestions(
        currentPage,
        pageSize,
        search
      );
      setQuestions(response);
      setError("");
    } catch (error) {
      setError("Error when find the questions.");
      setQuestions([]);
    }
  };

  const addQuestion = async (questionData) => {
    const newQuestion = await createQuestion(questionData);
    setQuestions([...questions, newQuestion]);
  };

  const editQuestion = async (id, questionData) => {
    const updatedQuestion = await updateQuestion(id, questionData);
    setQuestions(questions.map((q) => (q.id === id ? updatedQuestion : q)));
  };

  const removeQuestion = async (id) => {
    await deleteQuestion(id);
    setQuestions(questions.filter((q) => q.id !== id));
  };

  return (
    <QuestionsContext.Provider
      value={{
        questions,
        searchQuestions,
        addQuestion,
        editQuestion,
        removeQuestion,
        error,
        getAllQuestions,
        loadQuestions,
        setCurrentPage,
        hasMore
      }}
    >
      {children}
    </QuestionsContext.Provider>
  );
};
