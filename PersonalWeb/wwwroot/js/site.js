// ===============================
// Navbar móvil
// ===============================
document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.getElementById("mobile-menu");
    const navLinks = document.querySelector(".nav-links");

    if (menuToggle && navLinks) {
        menuToggle.addEventListener("click", () => {
            navLinks.classList.toggle("active");
        });
    }
});

// ===============================
// Función para detectar si estás realmente en Home
// ===============================
function isHome() {
    const p = location.pathname.toLowerCase();
    return (p === "/" || p === "" || p === "/home" || p === "/home/index");
}

// ===============================
// Scroll suave SIN HASHES
// ===============================
document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll("[data-scroll]").forEach(link => {
        link.addEventListener("click", function (e) {
            e.preventDefault();

            const section = this.getAttribute("data-scroll");

            // Si NO estoy en Home → ir al inicio y recordar destino
            if (!isHome()) {
                sessionStorage.setItem("scrollTo", section);
                location.href = "/";
                return;
            }

            // Si estoy en Home → scroll directo
            const target = document.getElementById(section);
            if (target) {
                target.scrollIntoView({ behavior: "smooth" });
            }
        });
    });

    // Si vengo desde otra página → scroll automático
    const pending = sessionStorage.getItem("scrollTo");
    if (pending) {
        const t = document.getElementById(pending);
        if (t) {
            setTimeout(() => {
                t.scrollIntoView({ behavior: "smooth" });
            }, 60);
        }
        sessionStorage.removeItem("scrollTo");
    }
});

// ===============================
// Animación reveal
// ===============================
document.addEventListener("DOMContentLoaded", () => {
    const elements = document.querySelectorAll(".reveal");

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("visible");
                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.2 });

    elements.forEach(el => observer.observe(el));
});
//back cel
// HISTORIAL DE MODALES – Manejo del botón “atrás” en móviles
let modalAbiertoId = null;

// Seleccionamos todos los modales de Bootstrap
const modales = document.querySelectorAll('.modal');

modales.forEach(modal => {
    modal.addEventListener('show.bs.modal', () => {
        modalAbiertoId = modal.id;
        // Añadimos el estado al historial sin cambiar el hash
        history.pushState({ modalId: modalAbiertoId }, '', window.location.pathname);
    });

    modal.addEventListener('hide.bs.modal', () => {
        modalAbiertoId = null;
    });
});

// Detectamos cuando el usuario usa el botón “atrás”
window.addEventListener('popstate', event => {
    if (event.state && event.state.modalId) {
        const modalElement = document.getElementById(event.state.modalId);
        const modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    }
});
