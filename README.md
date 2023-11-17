# Perguntech API

Perguntech is a tech question-and-answer repository where users can manage and search for questions within various categories. The API is built using .NET Core 7, MongoDB and Client side with Next.Js.

## Architecture

### Entities

- **Question**
  - ID
  - Question Text
  - Answer Text
  - List of Categories

- **Category**
  - ID
  - Category Name
  - List of Questions

### Database

- **MongoDB**: Used as the primary database for storing questions and categories.
- **Redis**: Utilized for caching to enhance the performance of autocomplete searches.

### Endpoints

- **/questions**: CRUD operations for questions.
- **/categories**: CRUD operations for categories.
- **/questions/search**: Autocomplete functionality for searching questions.

### Projects Structure

- **Perguntech.API**: Contains application logic and endpoints.
- **Perguntech.Core**: Contains entities and business logic.
- **Perguntech.Data**: Contains data access logic, interacting with MongoDB and Redis.

### Workflow of Autocomplete Feature

- User inputs text in the search bar.
- API gets called with the current text as a parameter.
- Redis is checked for cached results.
- If cache misses, MongoDB is queried, and results are cached in Redis.
- Related questions are returned to the user.

## Considerations

- MongoDB allows for flexibility and scalability, handling CRUD operations and full-text search.
- Redis acts as a high-speed cache to store frequently accessed questions for fast retrieval during searches.

## Setup & Running

Instructions for setting up and running the project will be added here.

