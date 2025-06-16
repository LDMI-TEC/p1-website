// horoskop.js

// Funktion: hent og vis horoskop
async function hentHoroskop(tegn) {
  try {
    const response = await fetch(`/api/horoskop/${tegn}`);
    const data = await response.json();
    document.getElementById("horoskop-resultat").innerText = data.tekst;
  } catch (error) {
    console.error("Fejl ved hentning af horoskop:", error);
    document.getElementById("horoskop-resultat").innerText = "Kunne ikke hente horoskop.";
  }
}
