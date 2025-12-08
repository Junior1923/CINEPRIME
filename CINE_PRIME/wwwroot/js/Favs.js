document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.movie-card').forEach((card, i) => {
        setTimeout(() => card.classList.add('loaded'), 120 * i);
    });
});

document.querySelectorAll('.remove-form').forEach(form => {
    form.addEventListener('submit', e => {
        e.preventDefault();
        const card = form.closest('.fav-wrapper');

        card.classList.add('remove-anim');

        setTimeout(() => {
            form.submit();
        }, 450);
    });
});
