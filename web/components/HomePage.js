import React, { useContext, useState } from "react";
import { QuestionsContext } from "@/context/QuestionsContext";

const HomePage = () => {
  const { questions, searchQuestions, error } = useContext(QuestionsContext);
  const [searchTerm, setSearchTerm] = useState("");

  const handleSearchChange = (event) => {
    const title = event.target.value;
    setSearchTerm(title);
    searchQuestions(title);
  };

  return (
    <div>
      <div className="p-8">
        <input
          type="text"
          className="w-full p-4 text-lg border rounded-md"
          placeholder="Search questions..."
          value={searchTerm}
          onChange={handleSearchChange}
        />
      </div>
      {error && <div className="mt-2 text-sm text-red-600">{error}</div>}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-8">
        {questions.map((question) => (
          <div key={question.id} className="bg-white shadow-lg rounded-lg p-6">
            <h3 className="text-xl font-semibold mb-2">{question.question}</h3>
            <p className="text-gray-700">{question.answer}</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default HomePage;
