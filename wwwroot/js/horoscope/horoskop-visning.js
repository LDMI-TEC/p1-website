// Hent ?tegn=vædder fra URL'en
const params = new URLSearchParams(window.location.search);
const tegn = params.get('tegn'); // F.eks. "løven", "vædder", "tyr"

// Opdater overskrift og billede
document.getElementById("overskrift").innerText = tegn.charAt(0).toUpperCase() + tegn.slice(1);
document.getElementById("horoskop-billede").src = `Resources/Horosop-picture/${tegn}.png`;

// Hent horoskoptekst fra API
async function hentHoroskop() {
    try {
        // !!! VIGTIGT: RET DENNE LINJE TIL DIN KORREKTE API-URL !!!
        // Erstat 'DIN_BACKEND_PORT' med den faktiske port, din ASP.NET Core API kører på (f.eks. 7005).
        // Brug 'https://' eller 'http://' som relevant (https anbefales, da du har konfigureret port 7005 for https).
        const response = await fetch(`https://localhost:7005/api/horoscope`);

        // Tjek om svaret er OK
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`HTTP fejl! Status: ${response.status}, Besked: ${errorText}`);
        }

        const horoscopes = await response.json(); // Forventer et ARRAY af Horoscope-objekter

        // Find det specifikke horoskop-objekt, der matcher 'tegn'
        // Bruger 'Zodiac' property'en fra din C# model (husk Zodiac skal matche 'tegn' fx "løven" i din database)
        const specificHoroscope = horoscopes.find(h => h.zodiac && h.zodiac.toLowerCase() === tegn.toLowerCase());

        if (specificHoroscope) {
            // Vælg, hvilken type horoskop du vil vise.
            // Du har LoveHoroscope, EconomyHoroscope, AdviceHoroscope i din Horoscope model.
            // Eksempel: Viser 'LoveHoroscope'
            if (specificHoroscope.loveHoroscope) {
                document.getElementById("horoskop-tekst").innerText = specificHoroscope.loveHoroscope;
            } else {
                document.getElementById("horoskop-tekst").innerText = "Kærlighedshoroskop ikke fundet for dette stjernetegn.";
            }

            // Hvis du vil vise alle typer, kan du bygge en streng (fjern kommentarerne for at bruge denne):
            /*
            let fullHoroscopeText = `Kærlighed: ${specificHoroscope.loveHoroscope || 'Ikke tilgængelig'}\n`;
            fullHoroscopeText += `Økonomi: ${specificHoroscope.economyHoroscope || 'Ikke tilgængelig'}\n`;
            fullHoroscopeText += `Råd: ${specificHoroscope.adviceHoroscope || 'Ikke tilgængelig'}`;
            document.getElementById("horoskop-tekst").innerText = fullHoroscopeText;
            */

        } else {
            document.getElementById("horoskop-tekst").innerText = `Kunne ikke finde horoskop for stjernetegnet '${tegn}' i dataen.`;
            console.warn(`Ingen horoskop fundet for '${tegn}'. Modtagne data:`, horoscopes);
        }

    } catch (error) {
        console.error("Fejl ved hentning af horoskop:", error);
        document.getElementById("horoskop-tekst").innerText = "Kunne ikke hente horoskop. Tjek konsollen for detaljer.";
    }
}

hentHoroskop();