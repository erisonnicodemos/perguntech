db.createUser({
    user: 'root',
    pwd: 'admin123',
    roles: [
        {
            role: 'readWrite',
            db: 'perguntechdb'
        }
    ]
});

// Create Categories
var csharpCategory = {
    "_id": UUID(),
    "name": "C#",
    "questionIds": []
};

var dotnetCategory = {
    "_id": UUID(),
    "name": ".NET",
    "questionIds": []
};

db.categories.insertMany([csharpCategory, dotnetCategory]);

// .NET Questions
var dotnetQuestions = [
    {
        "_id": UUID(),
        "question": "What is .NET?",
        "answer": ".NET is a free, cross-platform, open-source developer platform for building many different types of applications.",
        "categoryIds": [dotnetCategory._id]
    },
];

// C# Questions
var csharpQuestions = [
    {
        "_id": UUID(),
        "question": "What is C#?",
        "answer": "C# (pronounced as 'C sharp') is a programming language developed by Microsoft.",
        "categoryIds": [csharpCategory._id]
    },
];

// Insert questions
db.questions.insertMany(dotnetQuestions.concat(csharpQuestions));

// Update categories with question IDs
dotnetCategory.questionIds = dotnetQuestions.map(q => q._id);
csharpCategory.questionIds = csharpQuestions.map(q => q._id);

db.categories.updateOne({ "_id": dotnetCategory._id }, { "$set": { "questionIds": dotnetCategory.questionIds } });
db.categories.updateOne({ "_id": csharpCategory._id }, { "$set": { "questionIds": csharpCategory.questionIds } });
