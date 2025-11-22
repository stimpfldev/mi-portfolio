// ===============================
// Navbar móvil
// ===============================
// Navbar móvil
document.addEventListener("DOMContentLoaded", function () {
    const menuToggle = document.getElementById("mobile-menu");
    const navLinks = document.querySelector(".nav-links");

    if (menuToggle && navLinks) {
        // Abrir/Cerrar al hacer clic en el ícono ☰
        menuToggle.addEventListener("click", () => {
            navLinks.classList.toggle("active");
        });

        // Cerrar el menú al hacer clic en cualquier opción del nav
        const links = navLinks.querySelectorAll("a");
        links.forEach(link => {
            link.addEventListener("click", () => {
                if (navLinks.classList.contains("active")) {
                    navLinks.classList.remove("active");
                }
            });
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
document.addEventListener('DOMContentLoaded', () => {
    const modales = document.querySelectorAll('.modal');

    modales.forEach(modal => {
        const id = modal.id;

        // Mostrar: actualiza hash
        modal.addEventListener('show.bs.modal', () => {
            if (location.hash !== `#${id}`) {
                history.pushState(null, '', `#${id}`);
            }
        });

        // Ocultar: si el hash coincide, vuelve atrás
        modal.addEventListener('hide.bs.modal', () => {
            if (location.hash === `#${id}`) {
                history.back();
            }
        });
    });

    // Cierra el modal si el usuario va atrás
    window.addEventListener('popstate', () => {
        document.querySelectorAll('.modal.show').forEach(modal => {
            const modalInstance = bootstrap.Modal.getInstance(modal);
            if (modalInstance) {
                modalInstance.hide();
            }
        });
    });
});
