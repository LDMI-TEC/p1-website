# Joke API - JavaScript Fetch Examples

This guide provides practical examples of how to interact with the Joke API using JavaScript's fetch API.

## Base Setup

All examples assume the API is running on the same domain as your frontend. If you're using a different domain, you'll need to adjust the URLs accordingly.

```javascript
// Base URL for all API calls
const API_BASE_URL = '/api/Joke';

// Helper function to handle JSON responses
const handleResponse = async (response) => {
  if (!response.ok) {
    const errorData = await response.json().catch(() => null);
    throw new Error(errorData || `API error: ${response.status}`);
  }
  return response.status !== 204 ? response.json() : null;
};
```

## Get All Jokes

Retrieves all approved jokes.

```javascript
const getAllJokes = async () => {
  try {
    const response = await fetch(`${API_BASE_URL}`);
    const jokes = await handleResponse(response);
    console.log('All jokes:', jokes);
    return jokes;
  } catch (error) {
    console.error('Error fetching jokes:', error);
    throw error;
  }
};

// Usage
getAllJokes()
  .then(jokes => {
    // Update UI with jokes
    jokes.forEach(joke => {
      console.log(`${joke.jokeText} - by ${joke.author.name}`);
    });
  })
  .catch(error => {
    // Handle error in UI
    displayErrorMessage(error.message);
  });
```

## Get Joke by ID

Retrieves a specific joke by its ID.

```javascript
const getJokeById = async (jokeId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/${jokeId}`);
    const joke = await handleResponse(response);
    console.log('Retrieved joke:', joke);
    return joke;
  } catch (error) {
    console.error(`Error fetching joke ${jokeId}:`, error);
    throw error;
  }
};

// Usage
const jokeId = 1;
getJokeById(jokeId)
  .then(joke => {
    // Update UI with joke
    displayJoke(joke);
  })
  .catch(error => {
    // Handle error in UI
    displayErrorMessage(`Could not fetch joke #${jokeId}: ${error.message}`);
  });
```

## Get Jokes by Category

Retrieves all approved jokes in a specific category.

```javascript
const getJokesByCategory = async (categoryId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/category/${categoryId}`);
    const jokes = await handleResponse(response);
    console.log(`Jokes in category ${categoryId}:`, jokes);
    return jokes;
  } catch (error) {
    console.error(`Error fetching jokes for category ${categoryId}:`, error);
    throw error;
  }
};

// Usage
const categoryId = 2; // Example: "Dad Jokes" category
getJokesByCategory(categoryId)
  .then(jokes => {
    // Update UI with jokes
    displayJokesInCategory(jokes, categoryId);
  })
  .catch(error => {
    // Handle error in UI
    displayErrorMessage(`Could not fetch jokes for this category: ${error.message}`);
  });
```

## Get Jokes by Author

Retrieves all approved jokes by a specific author.

```javascript
const getJokesByAuthor = async (authorId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/author/${authorId}`);
    const jokes = await handleResponse(response);
    console.log(`Jokes by author ${authorId}:`, jokes);
    return jokes;
  } catch (error) {
    console.error(`Error fetching jokes for author ${authorId}:`, error);
    throw error;
  }
};

// Usage
const authorId = 3; // Example author ID
getJokesByAuthor(authorId)
  .then(jokes => {
    // Update UI with jokes
    jokes.forEach(joke => {
      console.log(`${joke.jokeText} (${joke.category.name})`);
    });
  })
  .catch(error => {
    // Handle error in UI
    displayErrorMessage(`Could not fetch jokes for this author: ${error.message}`);
  });
```

## Create a New Joke

Creates a new joke. Note that new jokes are not approved by default.

```javascript
const createJoke = async (jokeData) => {
  try {
    const response = await fetch(`${API_BASE_URL}`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(jokeData)
    });
    const createdJoke = await handleResponse(response);
    console.log('Created joke:', createdJoke);
    return createdJoke;
  } catch (error) {
    console.error('Error creating joke:', error);
    throw error;
  }
};

// Usage with form data
document.getElementById('joke-form').addEventListener('submit', async (event) => {
  event.preventDefault();
  
  const jokeData = {
    authorName: document.getElementById('author-name').value,
    authorAge: parseInt(document.getElementById('author-age').value) || null, // Optional
    categoryId: parseInt(document.getElementById('category').value),
    jokeText: document.getElementById('joke-text').value
  };
  
  try {
    const createdJoke = await createJoke(jokeData);
    showSuccessMessage('Your joke has been submitted for approval!');
    document.getElementById('joke-form').reset();
  } catch (error) {
    showErrorMessage(`Failed to submit joke: ${error.message}`);
  }
});
```

## Update a Joke

Updates the text of an existing joke.

```javascript
const updateJoke = async (jokeId, jokeData) => {
  try {
    const response = await fetch(`${API_BASE_URL}/${jokeId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(jokeData)
    });
    await handleResponse(response);
    console.log(`Joke ${jokeId} updated successfully`);
    return true;
  } catch (error) {
    console.error(`Error updating joke ${jokeId}:`, error);
    throw error;
  }
};

// Usage
const updateJokeExample = async () => {
  const jokeId = 1;
  
  // First, get the current joke
  const currentJoke = await getJokeById(jokeId);
  
  // Update the text
  currentJoke.jokeText = "Updated joke text: Why don't scientists trust atoms? Because they make up everything!";
  
  try {
    await updateJoke(jokeId, currentJoke);
    showSuccessMessage('Joke updated successfully!');
    // Refresh the joke display
    const updatedJoke = await getJokeById(jokeId);
    displayJoke(updatedJoke);
  } catch (error) {
    showErrorMessage(`Failed to update joke: ${error.message}`);
  }
};
```

## Like a Joke

Increments the like count for a specific joke.

```javascript
const likeJoke = async (jokeId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/${jokeId}/like`, {
      method: 'PUT'
    });
    const likeData = await handleResponse(response);
    console.log(`Joke ${jokeId} liked:`, likeData);
    return likeData;
  } catch (error) {
    console.error(`Error liking joke ${jokeId}:`, error);
    throw error;
  }
};

// Usage with a like button
document.getElementById('like-button').addEventListener('click', async () => {
  const jokeId = document.getElementById('current-joke').dataset.jokeId;
  
  try {
    const likeData = await likeJoke(jokeId);
    // Update the like/dislike counters in the UI
    document.getElementById('likes-count').textContent = likeData.likes;
    document.getElementById('dislikes-count').textContent = likeData.dislikes;
  } catch (error) {
    showErrorMessage(`Could not like joke: ${error.message}`);
  }
});
```

## Dislike a Joke

Increments the dislike count for a specific joke.

```javascript
const dislikeJoke = async (jokeId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/${jokeId}/dislike`, {
      method: 'PUT'
    });
    const likeData = await handleResponse(response);
    console.log(`Joke ${jokeId} disliked:`, likeData);
    return likeData;
  } catch (error) {
    console.error(`Error disliking joke ${jokeId}:`, error);
    throw error;
  }
};

// Usage with a dislike button
document.getElementById('dislike-button').addEventListener('click', async () => {
  const jokeId = document.getElementById('current-joke').dataset.jokeId;
  
  try {
    const likeData = await dislikeJoke(jokeId);
    // Update the like/dislike counters in the UI
    document.getElementById('likes-count').textContent = likeData.likes;
    document.getElementById('dislikes-count').textContent = likeData.dislikes;
  } catch (error) {
    showErrorMessage(`Could not dislike joke: ${error.message}`);
  }
});
```

## Delete a Joke

Deletes a specific joke.

```javascript
const deleteJoke = async (jokeId) => {
  try {
    const response = await fetch(`${API_BASE_URL}/${jokeId}`, {
      method: 'DELETE'
    });
    await handleResponse(response);
    console.log(`Joke ${jokeId} deleted successfully`);
    return true;
  } catch (error) {
    console.error(`Error deleting joke ${jokeId}:`, error);
    throw error;
  }
};

// Usage with confirmation
const confirmAndDeleteJoke = (jokeId) => {
  if (confirm('Are you sure you want to delete this joke?')) {
    deleteJoke(jokeId)
      .then(() => {
        showSuccessMessage('Joke deleted successfully!');
        // Remove the joke from the UI or refresh the joke list
        document.getElementById(`joke-${jokeId}`).remove();
      })
      .catch(error => {
        showErrorMessage(`Failed to delete joke: ${error.message}`);
      });
  }
};

// Example with a delete button
document.querySelectorAll('.delete-joke-button').forEach(button => {
  button.addEventListener('click', () => {
    const jokeId = button.dataset.jokeId;
    confirmAndDeleteJoke(jokeId);
  });
});
```

## Complete Example: Joke UI Integration

Here's a more complete example that shows how to integrate these API calls with a simple UI:

```javascript
// When the page loads
document.addEventListener('DOMContentLoaded', () => {
  // Load all jokes
  getAllJokes()
    .then(jokes => {
      const jokesList = document.getElementById('jokes-list');
      jokesList.innerHTML = ''; // Clear existing content
      
      jokes.forEach(joke => {
        const jokeElement = document.createElement('div');
        jokeElement.id = `joke-${joke.id}`;
        jokeElement.className = 'joke-card';
        
        jokeElement.innerHTML = `
          <h3>${joke.category.name}</h3>
          <p>${joke.jokeText}</p>
          <p class="author">By: ${joke.author.name}</p>
          <div class="joke-stats">
            <span class="likes">👍 <span class="count">${joke.likes}</span></span>
            <span class="dislikes">👎 <span class="count">${joke.dislikes}</span></span>
          </div>
          <div class="joke-actions">
            <button class="like-button" data-joke-id="${joke.id}">Like</button>
            <button class="dislike-button" data-joke-id="${joke.id}">Dislike</button>
            <button class="delete-button" data-joke-id="${joke.id}">Delete</button>
          </div>
        `;
        
        jokesList.appendChild(jokeElement);
      });
      
      // Add event listeners to buttons
      setupJokeButtons();
    })
    .catch(error => {
      showErrorMessage(`Failed to load jokes: ${error.message}`);
    });
  
  // Load categories for the dropdown
  loadCategories();
});

// Setup buttons for like, dislike, delete
function setupJokeButtons() {
  // Like buttons
  document.querySelectorAll('.like-button').forEach(button => {
    button.addEventListener('click', async () => {
      const jokeId = button.dataset.jokeId;
      try {
        const likeData = await likeJoke(jokeId);
        const jokeCard = document.getElementById(`joke-${jokeId}`);
        jokeCard.querySelector('.likes .count').textContent = likeData.likes;
        jokeCard.querySelector('.dislikes .count').textContent = likeData.dislikes;
      } catch (error) {
        showErrorMessage(`Failed to like joke: ${error.message}`);
      }
    });
  });
  
  // Dislike buttons
  document.querySelectorAll('.dislike-button').forEach(button => {
    button.addEventListener('click', async () => {
      const jokeId = button.dataset.jokeId;
      try {
        const likeData = await dislikeJoke(jokeId);
        const jokeCard = document.getElementById(`joke-${jokeId}`);
        jokeCard.querySelector('.likes .count').textContent = likeData.likes;
        jokeCard.querySelector('.dislikes .count').textContent = likeData.dislikes;
      } catch (error) {
        showErrorMessage(`Failed to dislike joke: ${error.message}`);
      }
    });
  });
  
  // Delete buttons
  document.querySelectorAll('.delete-button').forEach(button => {
    button.addEventListener('click', () => {
      const jokeId = button.dataset.jokeId;
      confirmAndDeleteJoke(jokeId);
    });
  });
}

// Add a joke form handler
document.getElementById('add-joke-form').addEventListener('submit', async (event) => {
  event.preventDefault();
  
  const jokeData = {
    authorName: document.getElementById('author-name').value,
    authorAge: parseInt(document.getElementById('author-age').value) || null,
    categoryId: parseInt(document.getElementById('category-id').value),
    jokeText: document.getElementById('joke-text').value
  };
  
  try {
    const newJoke = await createJoke(jokeData);
    showSuccessMessage('Joke submitted for approval!');
    document.getElementById('add-joke-form').reset();
  } catch (error) {
    showErrorMessage(`Failed to submit joke: ${error.message}`);
  }
});

// Helper functions for UI messages
function showSuccessMessage(message) {
  const messageElement = document.getElementById('status-message');
  messageElement.textContent = message;
  messageElement.className = 'success';
  messageElement.style.display = 'block';
  setTimeout(() => {
    messageElement.style.display = 'none';
  }, 3000);
}

function showErrorMessage(message) {
  const messageElement = document.getElementById('status-message');
  messageElement.textContent = message;
  messageElement.className = 'error';
  messageElement.style.display = 'block';
  setTimeout(() => {
    messageElement.style.display = 'none';
  }, 5000);
}
```

## Error Handling Best Practices

Here's an enhanced error handling approach:

```javascript
// Improved error handling helper
const handleApiResponse = async (response) => {
  if (response.ok) {
    // For 204 No Content responses, return null
    return response.status !== 204 ? response.json() : null;
  }
  
  // Handle different error status codes
  switch (response.status) {
    case 400:
      const badRequestData = await response.json().catch(() => ({ message: 'Invalid request data' }));
      throw new Error(`Bad Request: ${badRequestData.message || 'Please check your input'}`);
    
    case 404:
      throw new Error('The requested resource was not found');
    
    case 500:
      throw new Error('Server error. Please try again later or contact support');
      
    default:
      throw new Error(`API error (${response.status}): Please try again later`);
  }
};

// Usage example with better error handling
const safeApiCall = async (apiFunction, ...args) => {
  try {
    return await apiFunction(...args);
  } catch (error) {
    // Log the error for developers
    console.error('API call failed:', error);
    
    // Show user-friendly error message
    showErrorMessage(error.message || 'An unexpected error occurred');
    
    // You could also report the error to your monitoring service here
    // reportErrorToMonitoring(error);
    
    // Rethrow to allow further handling
    throw error;
  }
};

// Example usage with the safe wrapper
const handleJokeLike = async (jokeId) => {
  try {
    const likeData = await safeApiCall(likeJoke, jokeId);
    updateLikeCounters(jokeId, likeData);
  } catch (error) {
    // Error is already logged and displayed to the user
    // Additional error-specific handling if needed
  }
};
```

This documentation provides all the JavaScript fetch examples you need to work with your Joke API. Each endpoint has a corresponding function and example of how to use it in a real application.
