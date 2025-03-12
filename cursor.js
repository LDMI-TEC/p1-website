// Site wide cursor
const site_wide_cursor = document.querySelector('.custom-cursor.site-wide');

// Visible on site only
document.addEventListener('mouseenter', () => {
    site_wide_cursor.style.display = 'block';
});

document.addEventListener('mouseleave', () => {
    site_wide_cursor.style.display = 'none';
});

//Tracks the mouse
document.addEventListener('mousemove', TrackCursor)

function TrackCursor(evt) {
    const w = site_wide_cursor.clientWidth;
    const h = site_wide_cursor.clientHeight;
    site_wide_cursor.style.transform = `translate(${evt.clientX - w/2}px, ${evt.clientY -h/2}px)`;
}

//Adds and removes the active class on click
document.addEventListener('mousedown', () => site_wide_cursor.classList.add('active'));
document.addEventListener('mouseup', () => site_wide_cursor.classList.remove('active'));