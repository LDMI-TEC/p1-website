// html elements
const grid = document.getElementsByClassName("zodiac-grid")[0];
const result = document.getElementById("result");
const horoscopeImage = document.getElementById("horoskop-billede");
const title = document.getElementById("overskrift");

// advice sections
const loveAdvice = document.getElementById("love-advice");
const financialAdvice = document.getElementById("financial-advice");
const generalAdvice = document.getElementById("general-advice");

const hideGrid = () => {
  grid.style.display = "none";
};

const showGrid = () => {
  grid.style.display = "grid";
};

const showResult = () => {
  result.style.display = "flex"
}

const hideResult = () => {
  result.style.display = "none";
}

// function to fetch the data
const fetchHoroscope = async (zodiacId) => {
  const BASE_API_URL = "https://10.0.1.211/api/horoscope/";

  try {
    const response = await fetch(`${BASE_API_URL}${zodiacId}`);

    if (!response.ok) {
      throw new Error (`Error fetching data status: ${response.status}`)
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error fetching horoscope:", error);
    throw error;
  }
};

// handle click on zodiac card
const handleZodiacClick = async (event) => {
  const card = event.currentTarget;
  const zodiacId = card.id;
  const img = card.querySelector('img');
  const zodiacName = card.querySelector('.zodiac-name').textContent;

  // update the result immediately
  title.textContent = zodiacName;
  horoscopeImage.src = img.src;
  horoscopeImage.alt = img.alt;

  // hide grid show result
  hideGrid();
  showResult();

  try {
    const horoscopeData = await fetchHoroscope(zodiacId);
    console.log('Processing horoscope data:', horoscopeData); // Debug

    // update with fetched data
    if (horoscopeData && Array.isArray(horoscopeData) && horoscopeData.length === 3) {
      const [love, finance, general] = horoscopeData;

      loveAdvice.textContent = love;
      financialAdvice.textContent = finance;
      generalAdvice.textContent = general;
    } else {
      loveAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
      financialAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
      generalAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
    }
  } catch (error) {
    console.error('Error in handleZodiacClick:', error);
    loveAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
    financialAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
    generalAdvice.textContent = "Kunne ikke indlæse horoskop. Prøv igen senere.";
  }
};

// event listeners

// first add to all zodiac cards
document.addEventListener('DOMContentLoaded', () => {
  const zodiacCards = document.querySelectorAll('.zodiac-card');
  const backButton = document.getElementById("back-button");

  zodiacCards.forEach(card => {
    card.addEventListener('click', handleZodiacClick);
    // add a pointy cursor
    card.style.cursor = 'pointer';
  });

  if (backButton) {
    backButton.addEventListener('click', () => {
      hideResult();
      showGrid();
    });
  }

  // start with hiding the result section
  hideResult();
});
