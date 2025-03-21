// base url to fetch scores from
const baseUrl = "/api/scores/";

const getTokenOnGameStart = async () => {
  let gameToken;
  try {
    const response = await fetch(baseUrl + "generate-token");
    gameToken = await response.text();
  } catch (error) {
    console.error("Problem fetching token");
    return null;
  }
  return gameToken;
};

const submitScore = async (playerName, score, gameToken) => {
  if (!gameToken) {
    throw new Error("Can't submit a score without playing :)");
  }
  const data = {
    playerName: playerName,
    score: score,
    timeOfScore: new Date().toISOString(),
  };

  try {
    const response = await fetch(baseUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        token: gameToken,
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      alert("can't submit score try again later");
    } else {
      alert(
        "Score submitted successfully! see your score at the leaderboardpage"
      );
    }
  } catch (error) {
    console.error("Error submitting your score try again later.");
  }
};

const getScores = async () => {
  try {
    const response = await fetch(baseUrl);

    if (!response.ok) {
      alert("Problem fetching scores... try again later");
    }
    const scores = await response.json();

    return scores;
  } catch (error) {
    console.error(`error while fetching scoress ${error}`);
  }
};

export { getTokenOnGameStart, submitScore, getScores };
