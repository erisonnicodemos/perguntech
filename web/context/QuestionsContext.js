import {
  createQuestion,
  searchQuestionsByTitle,
  updateQuestion,
  deleteQuestion,
  getQuestions
} from "@/services/api";
import React, { createContext, useState } from "react";

export const QuestionsContext = createContext();

export const QuestionsProvider = ({ children }) => {
  const [questions, setQuestions] = useState([]);
  const [error, setError] = useState("");

  const searchQuestions = async (title) => {
    if (title.length > 3) {
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

  const getAllQuestions = async () => {
    try {
      const results = await getQuestions();
      setQuestions(results);
      setError("");
    } catch (error) {
      setError("Error when find the questions.");
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
        getAllQuestions
      }}
    >
      {children}
    </QuestionsContext.Provider>
  );
};
