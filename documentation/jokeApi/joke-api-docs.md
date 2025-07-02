# Joke API Documentation

## Overview

This documentation outlines how to use the Joke API, which provides endpoints for creating, retrieving, updating, and deleting jokes, as well as interacting with authors and categories.

Base URL: `/api/Joke`

## API Endpoints

### Get All Jokes

Retrieves all approved jokes.

- **URL:** `/api/Joke`
- **Method:** `GET`
- **Response:** Array of joke objects with author and category information
- **Example Response:**
```json
[
  {
    "id": 1,
    "jokeText": "Why did the developer go broke? Because he used up all his cache!",
    "createdAt": "2025-03-15T10:30:00",
    "isApproved": true,
    "likes": 42,
    "dislikes": 5,
    "author": {
      "id": 1,
      "name": "John Doe",
      "age": 32
    },
    "category": {
      "id": 1,
      "name": "Programming"
    }
  }
]
```

### Get Joke by ID

Retrieves a specific joke by its ID.

- **URL:** `/api/Joke/{id}`
- **Method:** `GET`
- **URL Parameters:** `id` - The ID of the joke
- **Response:** Joke object with author and category information
- **Example Response:**
```json
{
  "id": 1,
  "jokeText": "Why did the developer go broke? Because he used up all his cache!",
  "createdAt": "2025-03-15T10:30:00",
  "isApproved": true,
  "likes": 42,
  "dislikes": 5,
  "author": {
    "id": 1,
    "name": "John Doe",
    "age": 32
  },
  "category": {
    "id": 1,
    "name": "Programming"
  }
}
```

### Get Jokes by Category

Retrieves all approved jokes in a specific category.

- **URL:** `/api/Joke/category/{catId}`
- **Method:** `GET`
- **URL Parameters:** `catId` - The ID of the category
- **Response:** Array of joke objects with author and category information
- **Example Response:** Similar to Get All Jokes, but filtered by category

### Get Jokes by Author

Retrieves all approved jokes by a specific author.

- **URL:** `/api/Joke/author/{aId}`
- **Method:** `GET`
- **URL Parameters:** `aId` - The ID of the author
- **Response:** Array of joke objects with author and category information
- **Example Response:** Similar to Get All Jokes, but filtered by author

### Create a New Joke

Creates a new joke. The joke will be set to `isApproved: false` by default and will need approval before appearing in general listings.

- **URL:** `/api/Joke`
- **Method:** `POST`
- **Request Body:**
```json
{
  "authorName": "John Doe",
  "authorAge": 32,          // Optional, defaults to 40 if not provided
  "categoryId": 1,
  "jokeText": "Why did the developer go broke? Because he used up all his cache!"
}
```
- **Notes:** 
  - If the author name doesn't exist in the database, a new author will be created
  - If the author already exists, the existing author will be used
- **Response:** The created joke object with a 201 Created status
- **Example Response:** Same as Get Joke by ID format

### Update a Joke

Updates the text of an existing joke.

- **URL:** `/api/Joke/{id}`
- **Method:** `PUT`
- **URL Parameters:** `id` - The ID of the joke to update
- **Request Body:** Joke DTO with updated text
```json
{
  "id": 1,
  "jokeText": "Updated joke text goes here",
  "createdAt": "2025-03-15T10:30:00",
  "isApproved": true,
  "likes": 42,
  "dislikes": 5,
  "author": {
    "id": 1,
    "name": "John Doe",
    "age": 32
  },
  "category": {
    "id": 1,
    "name": "Programming"
  }
}
```
- **Notes:** Currently, only the `jokeText` field can be updated
- **Response:** 204 No Content if successful

### Like a Joke

Increments the like count for a specific joke.

- **URL:** `/api/Joke/{id}/like`
- **Method:** `PUT`
- **URL Parameters:** `id` - The ID of the joke
- **Response:** Current likes and dislikes count
- **Example Response:**
```json
{
  "likes": 43,
  "dislikes": 5
}
```

### Dislike a Joke

Increments the dislike count for a specific joke.

- **URL:** `/api/Joke/{id}/dislike`
- **Method:** `PUT`
- **URL Parameters:** `id` - The ID of the joke
- **Response:** Current likes and dislikes count
- **Example Response:**
```json
{
  "likes": 42,
  "dislikes": 6
}
```

### Delete a Joke

Deletes a specific joke.

- **URL:** `/api/Joke/{id}`
- **Method:** `DELETE`
- **URL Parameters:** `id` - The ID of the joke to delete
- **Response:** 204 No Content if successful

## Data Models

### Joke

| Field | Type | Description |
|-------|------|-------------|
| id | integer | Unique joke identifier |
| jokeText | string | The text content of the joke |
| createdAt | datetime | When the joke was created |
| isApproved | boolean | Whether the joke is approved for public display |
| likes | integer | Number of likes on the joke |
| dislikes | integer | Number of dislikes on the joke |
| author | object | Author details (id, name, age) |
| category | object | Category details (id, name) |

### JokeCreateDto (for creating jokes)

| Field | Type | Description |
|-------|------|-------------|
| authorName | string | Name of the joke author |
| authorAge | integer | Age of the author (optional, defaults to 40) |
| categoryId | integer | ID of the joke category |
| jokeText | string | The text content of the joke |

## Error Handling

- **404 Not Found:** Returned when the requested joke, author, or category doesn't exist
- **400 Bad Request:** Returned when the request data is invalid (e.g., missing required fields)

## Notes

- Only approved jokes (`isApproved: true`) are returned in the general listings
- New jokes are set to `isApproved: false` by default and require approval (this would need to be done via database or a separate admin API)
- When creating a joke with a new author, the author is automatically created in the database
