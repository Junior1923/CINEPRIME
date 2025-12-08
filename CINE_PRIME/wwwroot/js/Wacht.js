/* ================================
   Animaciones de la Watchlist
   ================================ */

// Animación de aparición suave en cada tarjeta
document.addEventListener("DOMContentLoaded", () => {
    const cards = document.querySelectorAll(".watch-card");

    cards.forEach((card, index) => {
        setTimeout(() => {
            card.classList.add("loaded");
        }, index * 120); // pequeña diferencia entre tarjetas
    });

    // Animación para cuando se elimina un elemento
    const forms = document.querySelectorAll(".remove-form");

    forms.forEach(form => {
        form.addEventListener("submit", e => {
            // Evita efecto instantáneo visual
            const wrapper = form.closest(".watch-wrapper");
            if (wrapper) {
                e.preventDefault();
                wrapper.classList.add("remove-anim");

                setTimeout(() => {
                    form.submit();
                }, 350); // espera a animación
            }
        });
    });
});
