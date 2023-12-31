import React, { useContext, useEffect, useState } from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import { QuestionsContext } from "@/context/QuestionsContext";

const questionSchema = Yup.object().shape({
  question: Yup.string()
    .min(5, "The question must be 5 words")
    .max(250, "The question dont must be 250 caracteres.")
    .required("This field is obrigatório."),
  answer: Yup.string()
    .min(5, "The question must be 5 words")
    .max(250, "The question dont must be 250 caracteres.")
    .required("This field is obrigatório."),
});

const CreateQuestion = () => {
  const {
    questions,
    addQuestion,
    editQuestion,
    removeQuestion,
    getAllQuestions,
    loadQuestions,
    setCurrentPage,
    hasMore
  } = useContext(QuestionsContext);

  const [selectedQuestion, setSelectedQuestion] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    const delayDebounce = setTimeout(() => {
      getAllQuestions(searchTerm);
    }, 500);

    return () => clearTimeout(delayDebounce);
  }, [searchTerm]);

  const handleLoadMore = () => {
    setCurrentPage((prevPage) => prevPage + 1);
    loadQuestions();
  };

  return (
    <div className="container mx-auto mt-10">
      <Formik
        initialValues={selectedQuestion || { question: "", answer: "" }}
        enableReinitialize
        validationSchema={questionSchema}
        onSubmit={(values, { setSubmitting, resetForm }) => {
          if (selectedQuestion) {
            editQuestion(selectedQuestion.id, values);
          } else {
            addQuestion(values);
          }
          setSubmitting(false);
          resetForm();
          setSelectedQuestion(null);
        }}
      >
        {({ isSubmitting }) => (
          <Form className="space-y-6">
            <div>
              <label
                htmlFor="question"
                className="block text-sm font-medium text-gray-700"
              >
                Question
              </label>
              <Field
                as="textarea"
                name="question"
                className="mt-1 block w-full p-2.5 border border-gray-300 rounded-md shadow-sm"
              />
              <ErrorMessage
                name="question"
                component="div"
                className="text-red-600 text-sm"
              />
            </div>

            <div>
              <label
                htmlFor="answer"
                className="block text-sm font-medium text-gray-700"
              >
                Answer
              </label>
              <Field
                as="textarea"
                name="answer"
                className="mt-1 block w-full p-2.5 border border-gray-300 rounded-md shadow-sm"
              />
              <ErrorMessage
                name="answer"
                component="div"
                className="text-red-600 text-sm"
              />
            </div>

            <button
              type="submit"
              disabled={isSubmitting}
              className="w-full flex justify-center justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
            >
              {isSubmitting ? "Saving..." : "Save"}
            </button>
          </Form>
        )}
      </Formik>
      <div className="container flex-auto mx-auto mt-10">
        <input
          type="text"
          placeholder="Search questions..."
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          className="w-full flex justify-center mb-4 p-2 border border-gray-300 rounded-md"
        />
      </div>
      <div className="mt-8 overflow-hidden shadow ring-1 ring-black ring-opacity-5 md:rounded-lg">
        <table className="min-w-full divide-y divide-gray-300">
          <thead className="bg-gray-50">
            <tr>
              <th
                scope="col"
                className="py-3 px-6 text-left text-sm font-semibold text-gray-900"
              >
                Pergunta
              </th>
              <th
                scope="col"
                className="py-3 px-6 text-right text-sm font-semibold text-gray-900"
              >
                Ações
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-gray-200 bg-white">
            {questions.map((question) => (
              <tr key={question.id}>
                <td className="py-4 px-6 text-sm font-medium text-gray-900">
                  {question.question}
                </td>
                <td className="py-4 px-6 text-sm font-medium text-right">
                  <button
                    onClick={() => setSelectedQuestion(question)}
                    className="text-indigo-600 hover:text-indigo-900"
                  >
                    Editar
                  </button>
                  <button
                    onClick={() => {
                      removeQuestion(question.id);
                      if (selectedQuestion?.id === question.id) {
                        setSelectedQuestion(null);
                      }
                    }}
                    className="text-red-600 hover:text-red-900 ml-4"
                  >
                    Deletar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="flex justify-center mt-4 mb-4">
          {hasMore && (
            <button
              onClick={handleLoadMore}
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded shadow-lg transition duration-300 ease-in-out transform hover:scale-105"
            >
              Load More
            </button>
          )}
        </div>
    </div>
  );
};

export default CreateQuestion;
